<script setup>
import BaseIcon from '@/components/base/BaseIcon.vue'

defineProps({
  size: [Number, String],
  color: String,
})
</script>

<template>
  <BaseIcon :size="size" :color="color" :stroke-width="strokeWidth" :viewBox="'0 0 10 10'">
    <path
      d="M8.75 0.75L0.75 8.75M0.75 0.75L8.75 8.75"
      stroke-linecap="round"
      stroke-linejoin="round"
    />
  </BaseIcon>
</template>
