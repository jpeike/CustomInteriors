<script setup lang="ts">
import AppHeader from './AppHeader.vue'
</script>

<template>
  <div>
    <AppHeader></AppHeader>
    <main>
      <slot />
    </main>
  </div>
</template>
