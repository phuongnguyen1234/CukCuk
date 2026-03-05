<template>
  <button
    class="m-btn"
    :class="[`m-btn-${variant}`]"
    :style="{
      width: typeof width === 'number' ? `${width}px` : width,
      height: typeof height === 'number' ? `${height}px` : height,
      minWidth: typeof minWidth === 'number' ? `${minWidth}px` : minWidth,
      fontSize: typeof textSize === 'number' ? `${textSize}px` : textSize,
      color: variant === 'outline' && color ? color : undefined,
      borderColor: color ? color : undefined,
      backgroundColor: variant !== 'outline' && color ? color : undefined,
      padding: padding,
    }"
    @click="$emit('click', $event)"
  >
    <!-- Icon bên trái (nếu có) -->
    <div v-if="icon || $slots.icon" class="m-btn-icon">
      <slot name="icon">
        <component :is="icon" />
      </slot>
    </div>

    <!-- Text (nội dung button) -->
    <div class="m-btn-text">
      <slot></slot>
    </div>
  </button>
</template>

<script setup>
/**
 * Base Button Component
 * Created by: Gemini Code Assist
 */
defineProps({
  /**
   * Loại button (màu sắc)
   * Values: 'primary' (xanh), 'secondary' (xám nhạt), 'outline' (trắng viền), 'danger' (đỏ)
   */
  variant: {
    type: String,
    default: 'primary',
    validator: (value) => ['primary', 'secondary', 'outline', 'danger'].includes(value),
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
    default: '0 12px',
  },
  /**
   * Màu tùy chỉnh (Hex, RGB, Name)
   * - Với variant outline: Áp dụng cho text và border
   * - Với variant khác: Áp dụng cho background
   */
  color: {
    type: String,
    default: null,
  },
})

defineEmits(['click'])
</script>

<style scoped>
@import url('../../../assets/styles/variables.css');

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
</style>
