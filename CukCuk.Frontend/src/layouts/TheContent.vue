<template>
  <div class="content">
    <div class="content_header">
      <div class="content_header_left">
        <div class="content_header_title">{{ title }}</div>
      </div>
      <div class="content_header_right">
        <slot name="header"></slot>
      </div>
    </div>
    <div class="content_body">
      <div class="content_body_left">
        <slot></slot>
      </div>
      <slot name="sidebar"></slot>
    </div>
  </div>
</template>

<script setup>
defineProps({
  title: {
    type: String,
    default: '',
  },
})

defineEmits(['back'])
</script>

<style scoped>
.content {
  height: 100%;
  display: flex;
  flex-direction: column;
}

.content_header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 12px 20px;
  height: 56px;
  box-sizing: border-box;
}

.content_header_left {
  display: flex;
  align-items: center;
  gap: 8px;
}

.content_header_title {
  font-weight: 600;
  font-size: 20px;
}

.content_header_right {
  display: flex;
  align-items: center;
}

.content_body {
  flex: 1;
  display: flex;
  overflow: hidden;
  position: relative;
  gap: 16px;
  padding: 0 16px 16px;
}

.content_body_left {
  flex: 1;
  display: flex;
  flex-direction: column;
  overflow: hidden;
}
</style>
