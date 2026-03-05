<script setup>
import { onMounted, computed, ref, watch, nextTick } from 'vue'
import BaseSelect from './BaseSelect.vue'

const props = defineProps({
  modelValue: [String, Number],
  required: Boolean,
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
  showAddButton: {
    type: Boolean,
    default: false,
  },
  searchable: {
    type: Boolean,
    default: false,
  },
})

const emit = defineEmits(['update:modelValue', 'change', 'add'])

const baseSelectRef = ref(null)
const inputRef = ref(null)
const searchQuery = ref('')

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

const displayValue = computed(() => {
  const selected = props.options.find((opt) => getValue(opt) === props.modelValue)
  return selected ? getLabel(selected) : ''
})

// Cập nhật searchQuery khi displayValue thay đổi (ví dụ: khi modelValue được set từ bên ngoài)
watch(
  displayValue,
  (newVal) => {
    searchQuery.value = newVal
  },
  { immediate: true },
)

const filteredOptions = computed(() => {
  if (!props.searchable || !searchQuery.value) {
    return props.options
  }

  // Nếu text trong input giống hệt với display value của item đã chọn,
  // tức là người dùng chưa gõ gì mới, thì hiển thị tất cả.
  if (searchQuery.value === displayValue.value) {
    return props.options
  }

  const query = searchQuery.value.toLowerCase()
  return props.options.filter((option) => {
    return String(getLabel(option)).toLowerCase().includes(query)
  })
})

const isSelected = (option) => {
  return props.modelValue === getValue(option)
}

const selectOption = (option, close) => {
  const val = getValue(option)
  emit('update:modelValue', val)
  emit('change', val)
  // Cập nhật lại searchQuery để input hiển thị đúng giá trị vừa chọn
  if (props.searchable) {
    searchQuery.value = getLabel(option)
  }
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

onMounted(() => {
  autoSelect()
})

const handleOpen = () => {
  if (props.searchable) {
    // Khi mở, xóa trắng query để người dùng tìm kiếm, nhưng chỉ khi nó đang hiển thị giá trị đã chọn
    if (searchQuery.value === displayValue.value) {
      searchQuery.value = ''
    }
    nextTick(() => {
      inputRef.value?.focus()
    })
  }
}

const handleClose = () => {
  // Khi đóng, reset lại query về giá trị đang được chọn
  searchQuery.value = displayValue.value
}
</script>

<template>
  <BaseSelect
    ref="baseSelectRef"
    :error="error"
    :disabled="disabled"
    :required="required"
    :show-add-button="showAddButton"
    @add="$emit('add')"
    @open="handleOpen"
    @close="handleClose"
  >
    <template #trigger>
      <input
        v-if="!searchable"
        type="text"
        class="select-input-field"
        :value="displayValue"
        :placeholder="placeholder"
        readonly
        :disabled="disabled"
      />
      <input
        v-else
        ref="inputRef"
        type="text"
        class="select-input-field"
        v-model="searchQuery"
        :placeholder="placeholder"
        :disabled="disabled"
        autocomplete="off"
        @click.stop="baseSelectRef?.open()"
      />
    </template>

    <template #options="{ close }">
      <li
        v-for="(option, index) in filteredOptions"
        :key="index"
        @click="selectOption(option, close)"
        :class="{ selected: isSelected(option) }"
      >
        {{ getLabel(option) }}
      </li>
      <li v-if="filteredOptions.length === 0" class="no-result">Không tìm thấy dữ liệu</li>
    </template>
  </BaseSelect>
</template>

<style scoped>
.select-input-field {
  width: 100%;
  height: 34px; /* Match parent height minus border */
  border: none;
  outline: none;
  padding: 0 12px;
  font-size: 14px;
  color: #111;
  background: transparent;
  cursor: pointer;
  text-overflow: ellipsis;
  box-sizing: border-box;
}
.select-input-field:disabled {
  cursor: not-allowed;
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

.no-result {
  padding: 8px 12px;
  color: #999;
  text-align: center;
  cursor: default;
}
</style>
