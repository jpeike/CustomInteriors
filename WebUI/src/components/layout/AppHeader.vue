<template>

  <div class="sideNav">
    <h3 class="logo">Custom Interiors</h3>
    <button class="btn-primary navButton" v-on:click="router.push(RoutePaths.HOME)">
      <i class="pi pi-home"></i> <label>Home</label>
    </button>

    <button class="btn-primary navButton" v-on:click="router.push(RoutePaths.ABOUT)">
      <i class="pi pi-info-circle"></i> <label>About</label>
    </button>

    <button class="btn-primary navButton" v-on:click="router.push(RoutePaths.USERS)">
      <i class="pi pi-users"></i> <label>Users</label>
    </button>

    <button class="btn-primary navButton" v-on:click="router.push(RoutePaths.JOBS)">
      <i class="pi pi-briefcase"></i> <label>Jobs</label>
    </button>

    <button class="btn-primary navButton" v-on:click="router.push('/billing')">
      <i class="pi pi-wallet"></i> <label>Billing</label>
    </button>

    <button class="btn-primary navButton" v-on:click="router.push(RoutePaths.CUSTOMERS)">
      <i class="pi pi-address-book"></i> <label>Customers</label>
    </button>

    <button class="btn-primary navButton" v-on:click="router.push(RoutePaths.EMPLOYEES)">
      <i class="pi pi-sitemap"></i> <label>Employees</label>
    </button>

    <button v-if="!auth.isLoggedIn" class="btn-primary navButton" @click="auth.login()">
      <i class="pi pi-sign-in"></i>
      <label>Login</label>
    </button>

    <button v-else class="btn-primary navButton" @click="auth.logout()">
      <i class="pi pi-sign-out"></i>
      <label>Logout</label>
    </button>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import Menubar from 'primevue/menubar'
import Menu from 'primevue/menu'

import { useRouter } from 'vue-router'
import 'primeicons/primeicons.css'
import { RouteNames } from '@/enums/RouteNames'
import { RoutePaths } from '@/enums/RoutePaths'
import '../style/Theme.css';

const router = useRouter()

// Define your navigation items
// Define your navigation items using enums
const items = ref([
  { label: 'Home', icon: 'pi pi-home', command: () => router.push(RoutePaths.HOME) },
  { label: 'About', icon: 'pi pi-info-circle', command: () => router.push(RoutePaths.ABOUT) },
  { label: 'Users', icon: 'pi pi-users', command: () => router.push(RoutePaths.USERS) },
  { label: 'Jobs', icon: 'pi pi-briefcase', command: () => router.push(RoutePaths.JOBS) }, 
  { label: 'Billing', icon: 'pi pi-wallet', command: () => router.push('/billing') }, // same
  {
    label: 'Customers',
    icon: 'pi pi-address-book',
    command: () => router.push(RoutePaths.CUSTOMERS),
  },
  { label: 'Employees', icon: 'pi pi-sitemap', command: () => router.push(RoutePaths.EMPLOYEES) },
  {
    label: 'Logout', icon: 'pi pi-sitemap', command: () => auth.logout()
  }
])

import { useAuthStore } from '@/stores/auth'
import { label } from '@primeuix/themes/aura/metergroup'

const auth = useAuthStore()

</script>

<style scoped>
.logo {
  margin: 0%;
  margin-bottom: 5%;
  font-size: 1.25rem;
  font-weight: bold;
}

.sideNav {
  position: fixed;

  width: 15vw;
  height: 97%;

  padding: 0.5rem 1rem;
  display: flex;
  flex-direction: column;

  box-shadow: var(--p-card-shadow);

  border: solid;
  border-width: 1px;
  border-color: rgb(222, 222, 222);
  border-radius: 10px;
}

.navButton {
  width: 75%;
  border: none;
  border-radius: 7px;
  padding: 0.5rem;
  cursor: pointer;

  /* Theme colors */
  background-color: var(--primary);
  color: var(--primary-foreground);
  transition: background-color 0.2s ease, transform 0.2s ease;
}

.navButton:hover {
  background-color: var(--secondary);
  color: var(--foreground);
  transform: scale(1.05);
}
</style>
