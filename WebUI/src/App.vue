<!-- src/App.vue -->
<template>
    <Toast />
    <component :is="layoutComponent">
      <router-view />
    </component>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useRoute } from 'vue-router'

// Import layouts
import DefaultLayout from '@/components/layout/DefaultLayout.vue'
import NoLayout from '@/components/layout/NoLayout.vue'

const route = useRoute()

// Choose layout dynamically based on meta.layout
const layoutComponent = computed(() => {
  switch (route.meta.layout) {
    case 'none':
      return NoLayout
    case 'default':
    default:
      return DefaultLayout
  }
})
</script>

<style scoped>
:global(body) {
  font-family: 'Roboto', sans-serif;
}
</style>
