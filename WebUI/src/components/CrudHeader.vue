<script setup lang="ts">
import Fluid from 'primevue/fluid';
import Button from 'primevue/button';
import 'primeicons/primeicons.css'

defineProps(['title']);
</script>

<template>
  <Fluid>
    <div class="parent">
      <div class="header">
        <h2>{{ title }}</h2>
      </div>
<!--      <div class="crud-buttons">-->
<!--        <Button label="Add" icon="pi pi-plus" severity="secondary"/>-->
<!--        <Button label="Edit" icon="pi pi-pencil" severity="secondary"/>-->
<!--        <Button label="Delete" icon="pi pi-trash" severity="secondary"/>-->
<!--      </div>-->
    </div>
  </Fluid>
</template>

<style scoped>
.parent {
  column-width: 20rem;
}

.p-button {
  margin: 0.5rem auto;
  width: 10rem;
}
</style>
