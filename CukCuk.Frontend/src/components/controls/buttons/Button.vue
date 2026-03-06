<template>
  <button
    class="m-btn"
    :class="[`m-btn-${variant}`]"
    :style="buttonStyle"
    :disabled="disabled"
    @click="$emit('click', $event)"
  >
    <!-- Icon bên trái (nếu có) -->
    <div v-if="icon || $slots.icon || $slots['icon-active']" class="m-btn-icon">
      <slot v-if="active && $slots['icon-active']" name="icon-active"></slot>
      <slot v-else name="icon">
        <component :is="icon" v-if="icon" />
      </slot>
    </div>

    <!-- Text (nội dung button) -->
    <div v-if="hasDefaultSlotContent" class="m-btn__text">
      <slot></slot>
    </div>
  </button>
</template>

<script setup>
import { computed, useSlots, Comment, Text } from 'vue'

/**
 * Base Button Component
 * Created by: Gemini Code Assist
 */
const props = defineProps({
  /**
   * Loại button (màu sắc)
   * Values: 'primary' (xanh), 'secondary' (xám nhạt), 'outline' (trắng viền), 'danger' (đỏ)
   */
  variant: {
    type: String,
    default: 'primary',
    validator: (value) =>
      ['primary', 'secondary', 'outline', 'danger', 'text', 'danger-outline'].includes(value),
  },
  /**
   * Component icon (import từ file .vue khác)
   */
  icon: {
    type: [Object, String, Function],
    default: null,
  },
  width: {
    type: [String, Number],
    default: null,
  },
  height: {
    type: [String, Number],
    default: 32,
  },
  minWidth: {
    type: [String, Number],
    default: 80,
  },
  textSize: {
    type: [String, Number],
    default: 13,
  },
  padding: {
    type: String,
    default: null,
  },
  /**
   * Màu tùy chỉnh (Hex, RGB, Name)
   * - Với variant outline: Áp dụng cho text và border
   * - Với variant text: Áp dụng cho text
   * - Với variant khác: Áp dụng cho background
   */
  color: {
    type: String,
    default: null,
  },
  /**
   * Trạng thái active/toggled của nút
   */
  active: {
    type: Boolean,
    default: false,
  },
  /**
   * Trạng thái disabled của nút
   */
  disabled: {
    type: Boolean,
    default: false,
  },
})

defineEmits(['click'])

const slots = useSlots()

const hasDefaultSlotContent = computed(() => {
  if (!slots.default) return false
  // Lọc ra các node không phải là comment hoặc text rỗng
  return slots.default().some((node) => {
    if (node.type === Comment) return false // Bỏ qua comment nodes
    if (node.type === Text && !node.children.trim()) return false // Bỏ qua text nodes chỉ có khoảng trắng
    return true // Giữ lại các element nodes hoặc text nodes có nội dung
  })
})

const buttonStyle = computed(() => ({
  width: typeof props.width === 'number' ? `${props.width}px` : props.width,
  height: typeof props.height === 'number' ? `${props.height}px` : props.height,
  minWidth: typeof props.minWidth === 'number' ? `${props.minWidth}px` : props.minWidth,
  fontSize: typeof props.textSize === 'number' ? `${props.textSize}px` : props.textSize,
  color:
    (props.variant === 'outline' || props.variant === 'text') && props.color
      ? props.color
      : undefined,
  borderColor: props.color ? props.color : undefined,
  backgroundColor:
    props.variant !== 'outline' && props.variant !== 'text' && props.color
      ? props.color
      : undefined,
  // Nếu padding không được truyền vào, tự động quyết định dựa trên việc có text hay không
  // - Có text: '0 12px'
  // - Chỉ có icon: '8px' (giống ButtonIcon)
  padding: props.padding ?? (hasDefaultSlotContent.value ? '0 12px' : '8px'),
}))
</script>

<style scoped>
.m-btn {
  display: flex;
  align-items: center;
  justify-content: center;
  box-sizing: border-box;
  border-radius: 8px;
  border: none;
  cursor: pointer;
  font-weight: 500;
  transition: all 0.2s ease;
  gap: 8px; /* Khoảng cách giữa icon và text */
  white-space: nowrap;
}

/* Icon styling */
.m-btn-icon {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 16px;
  height: 16px;
}

.m-btn-icon :deep(svg) {
  width: 100%;
  height: 100%;
}

/* Inner text wrapper */
.m-btn__text {
  color: inherit;
}

/* Variants Colors */
.m-btn-primary {
  background-color: var(--color-primary); /* Màu xanh chủ đạo MISA */
  color: #ffffff;
}
.m-btn-primary:hover {
  background-color: #1056b8;
}

.m-btn-secondary {
  background-color: #e9eaeb;
  color: #1f2937;
}
.m-btn-secondary:hover {
  background-color: #d1d5db;
}

.m-btn-outline {
  background-color: #ffffff;
  border: 1px solid #d0d5dd;
  color: #344054;
}
.m-btn-outline:hover {
  background-color: #f9fafb;
  border-color: #1570ef;
  color: #1570ef;
}

.m-btn-danger {
  background-color: #d92d20;
  color: #ffffff;
}
.m-btn-danger:hover {
  background-color: #b42318;
}

/* Variant Text */
.m-btn-text {
  background-color: transparent;
  color: #344054;
}
.m-btn-text:hover {
  background-color: #f9fafb;
  color: #1570ef;
}

.m-btn-danger-outline {
  background-color: #ffffff;
  border: 1px solid #d0d5dd;
  color: #d92d20;
}
.m-btn-danger-outline:hover {
  background-color: #fef3f2;
  border-color: #d92d20;
  color: #d92d20;
}

.m-btn:disabled {
  opacity: 0.5;
  cursor: not-allowed;
  pointer-events: none;
}
</style>
