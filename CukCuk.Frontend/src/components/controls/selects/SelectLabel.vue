<script setup>
import { onMounted, computed, watch } from 'vue'
import BaseSelect from './BaseSelect.vue'

const props = defineProps({
  modelValue: [String, Number],
  label: {
    type: String,
    default: '',
  },
  error: Boolean,
  options: {
    type: Array,
    default: () => [],
  },
  placeholder: {
    type: String,
    default: '',
  },
  disabled: {
    type: Boolean,
    default: false,
  },
  optionLabel: {
    type: String,
    default: '',
  },
  optionValue: {
    type: String,
    default: '',
  },
})

const emit = defineEmits(['update:modelValue', 'change'])

const getLabel = (option) => {
  if (props.optionLabel && typeof option === 'object') {
    return option[props.optionLabel]
  }
  return option
}

const getValue = (option) => {
  if (props.optionValue && typeof option === 'object') {
    return option[props.optionValue]
  }
  return option
}

const selectedOptionLabel = computed(() => {
  const selected = props.options.find((opt) => getValue(opt) === props.modelValue)
  return selected ? getLabel(selected) : ''
})

const isSelected = (option) => {
  return props.modelValue === getValue(option)
}

const selectOption = (option, close) => {
  const val = getValue(option)
  emit('update:modelValue', val)
  emit('change', val)
  close()
}

const autoSelect = () => {
  if (
    props.options.length > 0 &&
    (props.modelValue === null || props.modelValue === undefined || props.modelValue === '')
  ) {
    // Tự động chọn phần tử đầu tiên nếu chưa có giá trị
    emit('update:modelValue', getValue(props.options[0]))
  }
}

watch(
  () => props.options,
  (newOptions) => {
    if (newOptions && newOptions.length > 0) autoSelect()
  },
)

onMounted(() => {
  autoSelect()
})
</script>

<template>
  <BaseSelect :error="error" :disabled="disabled">
    <template #trigger>
      <div class="select-label-trigger">
        <span v-if="label" class="label-prefix">{{ label }}: </span>
        <span v-if="selectedOptionLabel">{{ selectedOptionLabel }}</span>
        <span v-else class="placeholder">{{ placeholder }}</span>
      </div>
    </template>

    <template #options="{ close }">
      <li
        v-for="(option, index) in options"
        :key="index"
        @click="selectOption(option, close)"
        :class="{ selected: isSelected(option) }"
      >
        {{ getLabel(option) }}
      </li>
    </template>
  </BaseSelect>
</template>

<style scoped>
.select-label-trigger {
  display: flex;
  align-items: center;
  overflow: hidden;
  white-space: nowrap;
  width: 100%;
  height: 32px;
  padding: 0 12px;
  cursor: pointer;
  font-size: 13px;
}

.label-prefix {
  color: #717680;
  margin-right: 4px;
}

.placeholder {
  color: #757575;
}

.select-options li {
  padding: 8px 12px;
  cursor: pointer;
  display: flex;
  justify-content: space-between;
  align-items: center;
  transition: background 0.2s;
}

.select-options li:hover {
  background-color: #f2f9ff;
  color: #2680eb;
}

.select-options li.selected {
  background-color: #e6f7ff;
  color: #2680eb;
  font-weight: 500;
}
</style>
