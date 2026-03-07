<script setup>
import { computed } from 'vue'
import { formatCurrency } from '@/utils/format-util.js'

defineOptions({
  inheritAttrs: false,
})

const props = defineProps({
  modelValue: [String, Number],
  error: Boolean,
  icon: {
    type: [Object, String, Function],
    default: null,
  },
  rules: {
    type: Array,
    default: () => [],
  },
})

const emit = defineEmits(['update:modelValue'])

const displayValue = computed(() => {
  if (props.rules.includes('currency')) {
    return formatCurrency(props.modelValue)
  }
  return props.modelValue
})

function handleInput(event) {
  let value = event.target.value

  if (props.rules.includes('currency')) {
    const numericValue = value.replace(/\D/g, '')
    const valueToEmit = numericValue === '' ? null : Number(numericValue)
    emit('update:modelValue', valueToEmit)
  } else {
    if (props.rules.includes('number')) {
      // Chỉ cho phép nhập số (loại bỏ các ký tự không phải là số)
      value = value.replace(/\D/g, '')
    }

    // Cập nhật lại giá trị trong input nếu nó đã bị thay đổi bởi rule
    // Điều này giúp ngăn con trỏ nhảy lung tung khi đang gõ
    if (value !== event.target.value) {
      event.target.value = value
    }

    emit('update:modelValue', value)
  }
}

function handleKeydown(event) {
  // Nếu không phải là input số hoặc tiền tệ, bỏ qua
  if (!props.rules.includes('number') && !props.rules.includes('currency')) {
    return
  }

  // Cho phép các phím điều khiển, điều hướng, và các phím tắt thông dụng
  // (Backspace, Tab, Enter, Delete, Pfeile, Home, End, Ctrl+A/C/V/X)
  if (
    [
      'Backspace',
      'Tab',
      'Enter',
      'Escape',
      'Delete',
      'ArrowLeft',
      'ArrowRight',
      'Home',
      'End',
    ].includes(event.key) ||
    // Allow: Ctrl+A, Ctrl+C, Ctrl+V, Ctrl+X
    (event.ctrlKey === true && ['a', 'c', 'v', 'x'].includes(event.key.toLowerCase())) ||
    // Allow: Cmd+A, Cmd+C, Cmd+V, Cmd+X (for Mac)
    (event.metaKey === true && ['a', 'c', 'v', 'x'].includes(event.key.toLowerCase()))
  ) {
    return // Cho phép thực hiện
  }

  // Chặn tất cả các phím không phải là số
  if (!/^\d$/.test(event.key)) {
    event.preventDefault()
  }
}
</script>

<template>
  <div class="m-input-wrapper">
    <div v-if="icon || $slots.icon" class="m-input-icon">
      <slot name="icon">
        <component :is="icon" />
      </slot>
    </div>
    <input
      v-bind="$attrs"
      :value="displayValue"
      @input="handleInput"
      @keydown="handleKeydown"
      class="input_form"
      :class="{ 'input--error': error, 'input--has-icon': icon || $slots.icon }"
    />
  </div>
</template>

<style scoped>
.m-input-wrapper {
  position: relative;
  width: 100%;
  min-width: 100px;
}

.m-input-icon {
  position: absolute;
  left: 12px;
  top: 50%;
  transform: translateY(-50%);
  display: flex;
  align-items: center;
  justify-content: center;
  width: 16px;
  height: 16px;
  color: var(--color-icon);
  pointer-events: none;
}

.m-input-icon :deep(svg) {
  width: 100%;
  height: 100%;
}

.input--has-icon {
  padding-left: 32px !important;
}
</style>
