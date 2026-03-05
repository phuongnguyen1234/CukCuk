<template>
  <!-- Toast Container -->
  <div class="toast-container">
    <TransitionGroup name="toast">
      <div v-for="toast in toasts" :key="toast.id" class="toast" :class="`toast--${toast.type}`">
        <!-- Icon -->
        <div class="toast_icon">
          <i :class="getIconClass(toast.type)"></i>
        </div>

        <!-- Message -->
        <div class="toast_msg">{{ toast.message }}</div>

        <!-- Close Button -->
        <div class="toast_close" @click="removeToast(toast.id)">
          <i class="fi fi-rr-cross-small"></i>
        </div>
      </div>
    </TransitionGroup>
  </div>
</template>

<script setup>
import { useToast } from '@/utils/use-toast'

const { toasts, removeToast } = useToast()

const iconMap = {
  success: 'fi fi-rr-check-circle',
  error: 'fi fi-rr-cross-circle',
  warning: 'fi fi-rr-triangle-warning',
}

function getIconClass(type) {
  return iconMap[type] || iconMap.success
}
</script>

<style>
/* Toast message styles */
.toast-container {
  position: fixed;
  top: 24px;
  right: 24px;
  z-index: 10000;
}
.toast {
  display: flex;
  align-items: center;
  border-radius: 4px;
  padding: 16px;
  margin-bottom: 12px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
  font-family: 'Inter', sans-serif;
  font-size: 14px;
  min-width: 300px;
  color: #fff;
}
.toast_icon {
  font-size: 24px;
  margin-right: 12px;
  display: flex;
  align-items: center;
  color: #fff;
}
.toast_msg {
  flex-grow: 1;
  line-height: 1.4;
}
.toast_close {
  cursor: pointer;
  color: rgba(255, 255, 255, 0.7);
  margin-left: 12px;
  display: flex;
  align-items: center;
  font-size: 20px;
}
.toast_close:hover {
  color: #fff;
}

.toast--success {
  background-color: #2ca01c;
}
.toast--error {
  background-color: #e61d1d;
}
.toast--warning {
  background-color: #f4a100;
}

/* Vue Transition Styles */
.toast-enter-active {
  transition: all 0.3s ease-out;
}

.toast-leave-active {
  transition: all 0.3s ease-in;
}

.toast-enter-from,
.toast-leave-to {
  opacity: 0;
  transform: translateX(100%);
}
</style>
