<script setup>
import { computed, ref, watch } from 'vue'
import BaseSelect from './BaseSelect.vue'
import Table from '@/components/controls/tables/Table.vue'
import Input from '@/components/controls/inputs/Input.vue'

const props = defineProps({
  modelValue: [String, Number, Object], // Giá trị được chọn (thường là ID)
  items: { type: Array, default: () => [] }, // Dữ liệu cho bảng
  columns: { type: Array, required: true }, // Cấu hình cột cho bảng
  displayField: { type: String, default: 'name' }, // Trường hiển thị lên ô Input
  valueField: { type: String, default: 'id' }, // Trường giá trị thực (ID)
  placeholder: String,
  dropdownWidth: { type: String, default: '500px' }, // Độ rộng của bảng khi xổ xuống
  error: Boolean,
  showAddButton: { type: Boolean, default: false }, // Prop để hiển thị nút thêm
  disabled: { type: Boolean, default: false }, // Prop để vô hiệu hóa
  searchable: { type: Boolean, default: false }, // Prop để bật/tắt ô tìm kiếm
})

const emit = defineEmits(['update:modelValue', 'change', 'add'])

const baseSelectRef = ref(null)
const searchQuery = ref('')

// Tìm item đang được chọn dựa trên modelValue
const selectedItem = computed(() => {
  if (!props.modelValue) return null
  return props.items.find((i) => i[props.valueField] === props.modelValue)
})

// Text hiển thị trên ô Input
const displayValue = computed(() => {
  return selectedItem.value ? selectedItem.value[props.displayField] : ''
})

// Lọc danh sách item dựa trên searchQuery
const filteredItems = computed(() => {
  if (!props.searchable || !searchQuery.value) {
    return props.items
  }
  const query = searchQuery.value.toLowerCase().trim()
  return props.items.filter((item) => {
    return String(item[props.displayField] || '')
      .toLowerCase()
      .includes(query)
  })
})

// Reset ô tìm kiếm mỗi khi dropdown đóng/mở
watch(
  () => baseSelectRef.value?.isOpen,
  () => (searchQuery.value = ''),
)

const handleRowClick = (item) => {
  // Cập nhật giá trị
  emit('update:modelValue', item[props.valueField])
  emit('change', item)

  // Đóng dropdown
  if (baseSelectRef.value) {
    baseSelectRef.value.close()
  }
}
</script>

<template>
  <BaseSelect
    ref="baseSelectRef"
    :width="dropdownWidth"
    :error="error"
    :disabled="disabled"
    :show-add-button="showAddButton"
    @add="$emit('add')"
    class="table-select"
  >
    <!-- Slot Trigger: Ô Input hiển thị giá trị -->
    <template #trigger="{ toggle, isOpen }">
      <div class="trigger-container" :class="{ 'is-active': isOpen, 'is-disabled': disabled }">
        <Input
          :model-value="displayValue"
          :placeholder="placeholder"
          readonly
          :disabled="disabled"
        />
      </div>
    </template>

    <!-- Slot Options: Chứa Table -->
    <template #options="{ close }">
      <div class="table-select-content" @click.stop>
        <div v-if="searchable" class="search-wrapper">
          <Input v-model="searchQuery" placeholder="Tìm kiếm..." @keydown.esc="close" />
        </div>
        <Table
          :headers="columns"
          :data="filteredItems"
          :show-vertical-borders="false"
          @row-click="handleRowClick"
        />
      </div>
    </template>
  </BaseSelect>
</template>

<style scoped>
.search-wrapper {
  padding: 8px 12px;
  border-bottom: 1px solid #e9eaeb;
  background-color: #fff;
  position: sticky;
  top: 0;
  z-index: 2;
}

.table-select-content {
  background: #fff;
  /* Đảm bảo table không bị vỡ layout */
  width: 100%;
}

/* Điều chỉnh header của table con để nằm dưới ô tìm kiếm */
.table-select-content :deep(.content_body_table th) {
  /* 49px = 32px (input) + 16px (padding) + 1px (border) */
  top: v-bind("props.searchable ? '49px' : '0px'");
  z-index: 1;
}

.trigger-container {
  position: relative;
  width: 100%;
  /* Thừa kế cursor từ BaseSelect */
}

/* Ghi đè để input bên trong không có border, vì BaseSelect đã có */
.trigger-container :deep(.input_form) {
  border: none !important;
}
</style>
