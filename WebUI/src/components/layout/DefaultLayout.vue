<script setup lang="ts">
import AppHeader from './AppHeader.vue'
</script>

<template>
  <div class="layoutWrapper">
    <AppHeader />
    <main class="pageContent">
      <slot />
    </main>
  </div>
</template>

<style scoped>
.layoutWrapper {
  display: flex;
  min-height: 100vh;
}

.pageContent {
  flex-grow: 1; /* take remaining space */
  padding: 1rem;
  margin-left: 0; /* for mobile we can adjust */
  transition: margin-left 0.25s ease;
}

/* Optional: make sidebar overlay on mobile */
@media (max-width: 700px) {
  .pageContent {
    margin-left: 0; /* content takes full width */
  }
}
</style>
