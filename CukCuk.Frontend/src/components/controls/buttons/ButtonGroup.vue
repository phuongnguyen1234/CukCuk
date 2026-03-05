<template>
  <div class="m-button-group">
    <slot></slot>
  </div>
</template>

<script setup>
/**
 * ButtonGroup Component
 * Container dùng để nhóm các Button lại với nhau, tự động căn chỉnh và tạo khoảng cách.
 */
</script>

<style scoped>
.m-button-group {
  display: flex;
  align-items: center; /* Căn giữa các button theo chiều dọc */
  gap: 8px; /* Khoảng cách giữa các button là 8px */

  /* Width và Height tự động co giãn (fit) theo nội dung bên trong */
  width: fit-content;
  height: fit-content;
  flex-wrap: wrap; /* Cho phép xuống dòng nếu không gian quá hẹp */
}
</style>
