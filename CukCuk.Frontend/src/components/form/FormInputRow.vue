<template>
  <div class="form-input-row">
    <div class="form-label" :title="label">
      {{ label }} <span v-if="required" class="required">*</span>
      <div v-if="tooltip" class="info-icon" :title="tooltip">
        <svg
          width="14"
          height="14"
          viewBox="0 0 14 14"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            d="M6.75 4.75H6.75667M6.08333 6.75H6.75V9.41667H7.41667M0.75 6.75C0.75 7.53793 0.905195 8.31815 1.20672 9.0461C1.50825 9.77405 1.95021 10.4355 2.50736 10.9926C3.06451 11.5498 3.72595 11.9917 4.4539 12.2933C5.18185 12.5948 5.96207 12.75 6.75 12.75C7.53793 12.75 8.31815 12.5948 9.0461 12.2933C9.77405 11.9917 10.4355 11.5498 10.9926 10.9926C11.5498 10.4355 11.9917 9.77405 12.2933 9.0461C12.5948 8.31815 12.75 7.53793 12.75 6.75C12.75 5.1587 12.1179 3.63258 10.9926 2.50736C9.86742 1.38214 8.3413 0.75 6.75 0.75C5.1587 0.75 3.63258 1.38214 2.50736 2.50736C1.38214 3.63258 0.75 5.1587 0.75 6.75Z"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
          />
        </svg>
      </div>
    </div>
    <div class="form-input-wrapper">
      <template v-if="$slots.default">
        <component
          v-for="(vnode, i) in $slots.default()"
          :key="i"
          :is="isVNode(vnode) ? cloneVNode(vnode, { required: required, error: !!error }) : vnode"
        />
      </template>
      <div v-if="error" class="form-error-msg">
        {{ error }}
      </div>
    </div>
  </div>
</template>

<script setup>
import { cloneVNode, isVNode } from 'vue'

defineProps({
  label: {
    type: String,
    default: '',
  },
  required: {
    type: Boolean,
    default: false,
  },
  labelWidth: {
    type: String,
    default: '200px',
  },
  alignItems: {
    type: String,
    default: 'center',
  },
  tooltip: {
    type: String,
    default: '',
  },
  error: {
    type: String,
    default: '',
  },
})
</script>

<style scoped>
.form-input-row {
  width: 100%;
  min-height: 32px;
  display: flex;
  align-items: v-bind(alignItems);
  gap: 8px;
}
.form-label {
  width: v-bind(labelWidth);
  font-weight: 400;
  font-size: 13px;
  color: #111;
  display: flex;
  align-items: center;
  min-width: 200px;
}

.required {
  color: #e61d1d;
}

.info-icon {
  margin-left: 6px;
  cursor: pointer;
  display: flex;
}

.form-input-wrapper {
  flex: 1;
  display: flex;
  align-items: center;
  gap: 8px;
  height: 100%;
  position: relative;
}

.form-error-msg {
  position: absolute;
  top: 100%;
  left: 50%;
  transform: translateX(-50%);
  margin-top: 10px;
  background-color: #e61d1d;
  color: #fff;
  padding: 6px 12px;
  border-radius: 8px;
  font-size: 12px;
  z-index: 9;
  white-space: nowrap;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.2);
}

.form-error-msg::before {
  content: '';
  position: absolute;
  top: -6px;
  left: 50%;
  transform: translateX(-50%);
  border-width: 0 6px 6px 6px;
  border-style: solid;
  border-color: transparent transparent #e61d1d transparent;
}
</style>
