<template>
  <div class="m-drawer">
    <!-- Header -->
    <div class="m-drawer-header">
      <div class="m-drawer-title">{{ title }}</div>
      <div class="m-drawer-close" @click="$emit('close')">
        <CloseIcon size="16" />
      </div>
    </div>

    <!-- Content -->
    <div class="m-drawer-content">
      <slot></slot>
    </div>

    <!-- Footer -->
    <div class="m-drawer-footer">
      <slot name="footer"></slot>
    </div>
  </div>
</template>

<script setup>
import CloseIcon from '@/components/icons/CloseIcon.vue'

defineProps({
  title: {
    type: String,
    default: '',
  },
})

defineEmits(['close'])
</script>

<style scoped>
.m-drawer {
  background-color: #ffffff;
  width: 240px;
  border-radius: 8px;
  display: flex;
  flex-direction: column;
  padding: 12px 12px 0 12px;
  gap: 8px;
  box-shadow: 0 4px 16px rgba(0, 0, 0, 0.1);
  box-sizing: border-box;
}

.m-drawer-header {
  height: 32px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  flex-shrink: 0;
  padding: 0 8px;
}

.m-drawer-title {
  font-weight: 600;
  font-size: 16px;
  line-height: 22px;
}

.m-drawer-close {
  cursor: pointer;
  display: flex;
  align-items: center;
}

.m-drawer-content {
  flex: 1;
  overflow: auto;
}

.m-drawer-footer {
  background-color: #fafafa;
  border-top: 1px solid #d5d7da;
  padding: 12px 20px;
  border-radius: 0 0 8px 8px;
  margin: 0 -12px; /* Kéo footer ra sát lề để bù lại padding của container */
  flex-shrink: 0;
}
</style>
