<template>
  <TheHeader />
  <TheSidebar :isExpanded="isSidebarExpanded" @toggle="toggleSidebar" />
  <div class="main-content" :class="{ content_expanded: isSidebarExpanded }">
    <router-view></router-view>
  </div>
  <Toast />
</template>

<script setup>
import { ref } from 'vue'
import TheHeader from './TheHeader.vue'
import TheSidebar from './TheSidebar.vue'
import Toast from '@/components/Toast.vue'

const isSidebarExpanded = ref(true)

const toggleSidebar = () => {
  isSidebarExpanded.value = !isSidebarExpanded.value
}
</script>
