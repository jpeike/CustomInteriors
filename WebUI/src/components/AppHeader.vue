<template>
  <Menubar :model="items" class="app-header">
    <template #start>
      <h2 class="logo">Custom Interiors</h2>
    </template>
  </Menubar>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import Menubar from 'primevue/menubar'
import { useRouter } from 'vue-router'
import 'primeicons/primeicons.css'

const router = useRouter()

// Define your navigation items
const items = ref([
  {
    label: 'Home',
    icon: 'pi pi-home',
    command: () => router.push('/'),
  },
  {
    label: 'About',
    icon: 'pi pi-info-circle',
    command: () => router.push('/about'),
  },
  {
    label: 'Users',
    icon: 'pi pi-info-circle',
    command: () => router.push('/users'),
  },
  {
    label: 'Jobs',
    icon: 'pi pi-briefcase',
    command: () => router.push('/jobs'),
  },
  {
    label: 'Billing',
    icon: 'pi pi-wallet',
    command: () => router.push('/billing'),
  },
  {
    label: 'Customers',
    icon: 'pi pi-address-book',
    command: () => router.push('/customers'),
  },
  {
    label: 'Emails',
    icon: 'pi pi-wallet',
    command: () => router.push('/emails'),
  },
  {
    label: 'Employees',
    icon: 'pi pi-sitemap',
    command: () => router.push('/employees'),
  },
])
</script>

<style scoped>
.app-header {
  padding: 0.5rem 1rem;
  border-radius: 0;
}

.logo {
  margin: 0;
  font-size: 1.25rem;
  font-weight: bold;
}
</style>
