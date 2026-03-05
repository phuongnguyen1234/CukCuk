<template>
  <button
    class="m-btn-icon"
    :class="[`m-btn-${variant}`]"
    :style="{
      height: typeof height === 'number' ? `${height}px` : height,
      width: typeof width === 'number' ? `${width}px` : width,
      minWidth: typeof minWidth === 'number' ? `${minWidth}px` : minWidth,
    }"
    @click="$emit('click', $event)"
  >
    <!-- Slot mặc định chứa icon (svg, i, component) -->
    <div class="m-btn-icon-inner">
      <slot>
        <component :is="icon" v-if="icon" />
      </slot>
    </div>
  </button>
</template>

<script setup>
/**
 * Button Icon Component
 * Chỉ dùng cho các nút chỉ có icon, không có text.
 */
defineProps({
  /**
   * Loại button (màu sắc)
   * Values: 'primary', 'secondary', 'outline', 'danger'
   */
  variant: {
    type: String,
    default: 'outline',
    validator: (value) => ['primary', 'secondary', 'outline', 'danger', 'text'].includes(value),
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
})

defineEmits(['click'])
</script>

<style scoped>
@import url('../../../assets/styles/variables.css');

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
</style>
