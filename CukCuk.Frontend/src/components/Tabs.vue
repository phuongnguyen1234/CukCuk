<script setup>
defineProps({
  modelValue: {
    type: [String, Number],
    required: true,
  },
  tabs: {
    type: Array,
    default: () => [], // Expects array of objects: { label: 'Tab Name', value: 'tab_key' }
  },
})

const emit = defineEmits(['update:modelValue', 'change'])

function selectTab(tab) {
  emit('update:modelValue', tab.value)
  emit('change', tab.value)
}
</script>

<template>
  <div class="m-tabs-container">
    <div class="m-tabs">
      <div
        v-for="(tab, index) in tabs"
        :key="index"
        class="m-tab-item"
        :class="{ active: modelValue === tab.value }"
        @click="selectTab(tab)"
      >
        {{ tab.label }}
      </div>
    </div>
    <div class="m-tabs-content">
      <slot :name="modelValue"></slot>
    </div>
    <slot name="footer"></slot>
  </div>
</template>

<style scoped>
.m-tabs-container {
  display: flex;
  flex-direction: column;
}

.m-tabs {
  height: 44px;
  border-bottom: 0.5px solid #e0e0e0;
  padding: 8px 20px 0 20px;
  gap: 10px;
  background-color: white;
  display: flex;
  align-items: flex-end;
  box-sizing: border-box;
  flex-shrink: 0;
  position: sticky;
  top: 0;
  z-index: 10;
}

.m-tab-item {
  padding: 8px 16px;
  gap: 8px;
  font-size: 13px;
  font-weight: 600;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  color: #111;
  border-bottom: 2px solid transparent;
  transition: all 0.2s ease;
}

.m-tab-item:not(.active):hover {
  color: var(--color-primary);
  border-bottom-color: #e0e0e0;
}

.m-tab-item.active {
  color: var(--color-primary);
  border-bottom: 2px solid var(--color-primary);
}

.m-tabs-content {
  flex: 1;
}
</style>
