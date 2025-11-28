<script setup lang="ts">
import AppHeader from './AppHeader.vue'
</script>

<template>
  <div style="display: flex; flex-direction: row;">
    <AppHeader></AppHeader>
    <main>
      <slot />
    </main>
  </div>
</template>
