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
import { RouteNames } from '@/enums/RouteNames'
import { RoutePaths } from '@/enums/RoutePaths'

const router = useRouter()

// Define your navigation items
// Define your navigation items using enums
const items = ref([
  { label: 'Home', icon: 'pi pi-home', command: () => router.push(RoutePaths.HOME) },
  { label: 'About', icon: 'pi pi-info-circle', command: () => router.push(RoutePaths.ABOUT) },
  { label: 'Users', icon: 'pi pi-users', command: () => router.push(RoutePaths.USERS) },
  { label: 'Jobs', icon: 'pi pi-briefcase', command: () => router.push('/jobs') }, // if Jobs not yet in enum
  { label: 'Billing', icon: 'pi pi-wallet', command: () => router.push('/billing') }, // same
  {
    label: 'Customers',
    icon: 'pi pi-address-book',
    command: () => router.push(RoutePaths.CUSTOMERS),
  },
  { label: 'Emails', icon: 'pi pi-envelope', command: () => router.push(RoutePaths.EMAILS) },
  { label: 'Employees', icon: 'pi pi-sitemap', command: () => router.push(RoutePaths.EMPLOYEES) },
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
