<template>
<link rel="preconnect" href="https://fonts.googleapis.com">
  <!-- Hamburger button - only visible when menu is closed -->
  <button
    v-if="!isOpen"
    class="hamburger"
    @click="isOpen = true"
  >
    <i class="pi pi-bars"></i>
  </button>

  <!-- Navigation drawer -->
  <div class="sideNav" :class="{ open: isOpen }">
    <h3 class="logo">CUSTOM INTERIORS</h3>
    <button
      v-if="isOpen"
      class="closeButton"
      @click="isOpen = false">
      <i class="pi pi-times"></i>
    </button>
    <button class="btn-primary navButton" @click="router.push(RoutePaths.DASHBOARD)">
      <i class="pi pi-home"></i><label> Dashboard</label>
    </button>

    <!-- <button class="btn-primary navButton" @click="router.push(RoutePaths.ABOUT)">
      <i class="pi pi-info-circle"></i><label> About</label>
    </button> -->

    <button class="btn-primary navButton" @click="router.push(RoutePaths.USERS)">
      <i class="pi pi-users"></i><label> Users</label>
    </button>

    <button class="btn-primary navButton" @click="router.push(RoutePaths.JOBS)">
      <i class="pi pi-briefcase"></i><label> Jobs</label>
    </button>

    <button class="btn-primary navButton" @click="router.push('/billing')">
      <i class="pi pi-wallet"></i><label> Billing</label>
    </button>

    <button class="btn-primary navButton" @click="router.push(RoutePaths.CUSTOMERS)">
      <i class="pi pi-address-book"></i><label> Customers</label>
    </button>

    <!-- <button class="btn-primary navButton" @click="router.push(RoutePaths.EMPLOYEES)">
      <i class="pi pi-sitemap"></i><label> Employees</label>
    </button> -->

    <button v-if="!auth.isLoggedIn" class="btn-primary navButton" @click="auth.login()">
      <i class="pi pi-sign-in"></i><label> Login</label>
    </button>

    <button v-else class="btn-primary navButton" @click="auth.logout()">
      <i class="pi pi-sign-out"></i><label> Logout</label>
    </button>
  </div>
</template>



<script setup lang="ts">
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'
import 'primeicons/primeicons.css'
import { RouteNames } from '@/enums/RouteNames'
import { RoutePaths } from '@/enums/RoutePaths'
import '../style/Theme.css';
import { useAuthStore } from '@/stores/auth'
import { label } from '@primeuix/themes/aura/metergroup'

const router = useRouter()

// Define your navigation items
// Define your navigation items using enums
const items = ref([
  { label: 'Dashboard', icon: 'pi pi-home', command: () => router.push(RoutePaths.DASHBOARD) },
  //{ label: 'About', icon: 'pi pi-info-circle', command: () => router.push(RoutePaths.ABOUT) },
  { label: 'Users', icon: 'pi pi-users', command: () => router.push(RoutePaths.USERS) },
  { label: 'Jobs', icon: 'pi pi-briefcase', command: () => router.push(RoutePaths.JOBS) }, 
  { label: 'Billing', icon: 'pi pi-wallet', command: () => router.push('/billing') }, // same
  {
    label: 'Customers',
    icon: 'pi pi-address-book',
    command: () => router.push(RoutePaths.CUSTOMERS),
  },
  //{ label: 'Employees', icon: 'pi pi-sitemap', command: () => router.push(RoutePaths.EMPLOYEES) },
  {
    label: 'Logout',
    icon: 'pi pi-sitemap',
    command: () => auth.logout(),
  },
])

const auth = useAuthStore()
const isOpen = ref(false);
watch(() => router.currentRoute.value.fullPath, () => {
  isOpen.value = false;
});
</script>

<style scoped>

.logo {
  margin-bottom: 5%;
  font-size: 1.75rem;
  font-family: "Instrument Serif", serif;
}

.sideNav {
  position: fixed;
  left: 0;
  top: 0;
  height: 100vh;
  width: 15vw;
  padding: 0.5rem 1rem;
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
  background: var(--sidebar);
  border: 1px solid var(--border);
  border-radius: 0 10px 10px 0;
  box-shadow: var(--p-card-shadow);
  transition: transform 0.25s ease, width 0.25s ease;
}

.navButton {
  width: 100%;
  border: none;
  border-radius: 7px;
  padding: 0.5rem;
  cursor: pointer;
  background-color: var(--primary);
  color: var(--primary-foreground);
  transition:
    background-color 0.2s ease,
    transform 0.2s ease;
}

.navButton:hover {
  background-color: var(--secondary);
  color: var(--foreground);
  transform: scale(1.05);
}
.hamburger {
  display: none;
  position: fixed;
  top: 1rem;
  left: 1rem;
  background: var(--primary);
  color: var(--card);
  border: none;
  border-radius: 6px;
  padding: 0.6rem 0.8rem;
  font-size: 1.2rem;
  cursor: pointer;
  z-index: 999;
}
.closeButton {
  display: none;            /* Hidden on desktop */
  position: absolute;
  top: 1rem;
  right: 1rem;
  color: var(--foreground);
  background: var(--sidebar);
  border: none;
  padding: 0.4rem 0.6rem;
  font-size: 1.2rem;
  cursor: pointer;
  z-index: 1001;
}
/* Tablet shrinking effect */
@media (max-width: 1350px) {
  .sideNav {
    width: 18vw;
  }
  .navButton { font-size: 0.95rem; }
}

@media (max-width: 1150px) {
  .sideNav {
    width: 20vw;
  }
  .navButton { font-size: 0.9rem; }
}

/* Approaching mobile */
@media (max-width: 800px) {
  .sideNav {
    width: 20vw;
  }
  .navButton { font-size: 0.8rem; }
}

/* MOBILE – use hamburger instead */
@media (max-width: 700px) {
  .sideNav {
    transform: translateX(-100%);
    width: 70vw;
    border-radius: 0;
    min-width: unset;
    position: fixed;
    top: 0;
    left: 0;
    height: 100vh;
    z-index: 1000;
  }
  .hamburger {
    display: block;
  }
  .closeButton {
    display: block;
  }
  /* Drawer open */
  .sideNav.open {
    transform: translateX(0);
  }
}

/* VERY SMALL PHONES */
@media (max-width: 480px) {
  .sideNav {
    width: 100vw;
    position: fixed;
    top: 0;
    left: 0;
    height: 100vh;
    z-index: 1000;
  }
  .hamburger {
    display: block;
  }
  .closeButton {
    display: block;
  }
}

</style>
