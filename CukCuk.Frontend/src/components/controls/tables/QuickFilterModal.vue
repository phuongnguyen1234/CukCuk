<template>
  <BaseModal
    :visible="visible"
    :has-overlay="false"
    close-position="title"
    width="350px"
    @update:visible="$emit('update:visible', $event)"
  >
    <template #title> Lọc {{ column.label.toLowerCase() }} </template>

    <div class="quick-filter-content">
      <div class="filter-row">
        <Select
          v-model="localFilter.operator"
          :options="stringOperations"
          option-label="label"
          option-value="value"
          placeholder="Chọn điều kiện"
        />
      </div>

      <div class="filter-row">
        <InputTag v-model="localFilter.value" placeholder="Nhập giá trị và nhấn Enter" />
      </div>
    </div>

    <template #footer>
      <div class="quick-filter-footer">
        <Button variant="outline" @click="handleReset">Bỏ lọc</Button>
        <ButtonGroup>
          <Button variant="outline" @click="close">Hủy</Button>
          <Button variant="primary" @click="handleApply">Áp dụng</Button>
        </ButtonGroup>
      </div>
    </template>
  </BaseModal>
</template>

<script setup>
import { ref, watch } from 'vue'
import BaseModal from '@/components/base/BaseModal.vue'
import Select from '@/components/controls/selects/Select.vue'
import InputTag from '@/components/controls/inputs/InputTag.vue'
import Button from '@/components/controls/buttons/Button.vue'
import ButtonGroup from '@/components/controls/buttons/ButtonGroup.vue'
import { FilterOperation } from '@/models/enums/filter-operation.js'

const props = defineProps({
  visible: Boolean,
  column: {
    type: Object,
    default: () => ({ label: '', key: '' }),
  },
  // Nhận giá trị filter hiện tại từ cha (đồng bộ với FilterDrawer)
  filter: {
    type: Object,
    default: () => ({
      operator: FilterOperation.Contains,
      value: [],
    }),
  },
})

const emit = defineEmits(['update:visible', 'apply', 'reset'])

const localFilter = ref({
  operator: FilterOperation.Contains,
  value: [],
})

const stringOperations = [
  { value: FilterOperation.Contains, label: 'Chứa' },
  { value: FilterOperation.NotContains, label: 'Không chứa' },
  { value: FilterOperation.Equals, label: 'Bằng' },
  { value: FilterOperation.StartsWith, label: 'Bắt đầu bằng' },
  { value: FilterOperation.EndsWith, label: 'Kết thúc bằng' },
]

watch(
  () => props.filter,
  (newVal) => {
    if (newVal) {
      localFilter.value = { ...newVal }
    }
  },
  { immediate: true, deep: true },
)

function close() {
  emit('update:visible', false)
}

function handleReset() {
  emit('reset', props.column.key)
  close()
}

function handleApply() {
  emit('apply', { ...localFilter.value, key: props.column.key })
  close()
}
</script>

<style scoped>
.quick-filter-content {
  display: flex;
  flex-direction: column;
  gap: 16px;
  padding: 8px 0;
}

.filter-row {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.quick-filter-footer {
  display: flex;
  justify-content: space-between;
  width: 100%;
}
</style>
