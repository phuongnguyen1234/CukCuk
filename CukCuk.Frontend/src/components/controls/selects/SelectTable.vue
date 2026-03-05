<script setup>
import { computed, ref } from 'vue'
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
})

const emit = defineEmits(['update:modelValue', 'change', 'add'])

const baseSelectRef = ref(null)

// Tìm item đang được chọn dựa trên modelValue
const selectedItem = computed(() => {
  if (!props.modelValue) return null
  return props.items.find((i) => i[props.valueField] === props.modelValue)
})

// Text hiển thị trên ô Input
const displayValue = computed(() => {
  return selectedItem.value ? selectedItem.value[props.displayField] : ''
})

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
          @click.stop="!disabled && toggle()"
        />
      </div>
    </template>

    <!-- Slot Options: Chứa Table -->
    <template #options>
      <div class="table-select-content">
        <Table
          :headers="columns"
          :data="items"
          :show-vertical-borders="false"
          @row-click="handleRowClick"
        />
      </div>
    </template>
  </BaseSelect>
</template>

<style scoped>
.table-select-content {
  background: #fff;
  /* Đảm bảo table không bị vỡ layout */
  width: 100%;
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
