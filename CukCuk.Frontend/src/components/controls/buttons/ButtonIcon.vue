<template>
  <button
    class="m-btn-icon"
    :class="[`m-btn-${variant}`, { 'is-active': active }]"
    :style="{
      ...buttonStyle,
    }"
    :disabled="disabled"
    @click="$emit('click', $event)"
  >
    <!-- Slot mặc định chứa icon (svg, i, component) -->
    <div class="m-btn-icon-inner">
      <slot name="icon-active" v-if="active && $slots['icon-active']">
        <!-- Hiển thị icon active nếu có -->
      </slot>
      <slot v-else>
        <!-- Icon mặc định -->
      </slot>
    </div>
  </button>
</template>

<script setup>
import { computed } from 'vue'

/**
 * Button Icon Component
 * Chỉ dùng cho các nút chỉ có icon, không có text.
 */
const props = defineProps({
  /**
   * Loại button (màu sắc)
   * Values: 'primary', 'secondary', 'outline', 'danger'
   */
  variant: {
    type: String,
    default: 'outline',
    validator: (value) =>
      ['primary', 'secondary', 'outline', 'danger', 'text', 'danger-outline'].includes(value),
  },
  /**
   * Component icon (nếu không dùng slot)
   */
  icon: {
    type: [Object, String, Function],
    default: null,
  },
  width: {
    type: [String, Number],
    default: 32,
  },
  height: {
    type: [String, Number],
    default: 32,
  },
  minWidth: {
    type: [String, Number],
    default: 32,
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
  /**
   * Màu tùy chỉnh (Hex, RGB, Name)
   */
  color: {
    type: String,
    default: null,
  },
})

defineEmits(['click'])

const buttonStyle = computed(() => ({
  height: typeof props.height === 'number' ? `${props.height}px` : props.height,
  width: typeof props.width === 'number' ? `${props.width}px` : props.width,
  minWidth: typeof props.minWidth === 'number' ? `${props.minWidth}px` : props.minWidth,
  color:
    (props.variant === 'outline' || props.variant === 'text') && props.color
      ? props.color
      : undefined,
  borderColor: props.color ? props.color : undefined,
  backgroundColor:
    props.variant !== 'outline' && props.variant !== 'text' && props.color
      ? props.color
      : undefined,
}))
</script>

<style scoped>
.m-btn-icon {
  display: flex;
  align-items: center;
  justify-content: center;
  border-radius: 8px;
  border: none;
  cursor: pointer;
  transition: all 0.2s ease;
  padding: 0;
  padding: 8px;
}

.m-btn-icon-inner {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 16px;
  height: 16px;
}

.m-btn-icon-inner :deep(svg) {
  width: 100%;
  height: 100%;
}

/* Variants Colors - Đồng bộ với Button.vue */
.m-btn-primary {
  background-color: var(--color-primary);
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

/* Variant Text - Chỉ icon, không background/border */
.m-btn-text {
  background-color: transparent;
  color: #344054;
}
.m-btn-text:hover {
  background-color: #f9fafb;
  color: #1570ef;
}

/* Variant Danger Outline */
.m-btn-danger-outline {
  background-color: #ffffff;
  border: 1px solid #d0d5dd;
  color: #d92d20;
}
.m-btn-danger-outline:hover {
  background-color: #fef3f2;
  border-color: #d92d20;
}

/* Style khi nút active */
.m-btn-outline.is-active {
  background-color: #e6f7ff; /* Màu nền xanh nhạt */
  border-color: #245fdf;
  color: #245fdf;
}

.m-btn-icon:disabled {
  opacity: 0.5;
  cursor: not-allowed;
  pointer-events: none;
}
</style>
