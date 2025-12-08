<template>
  <div class="landing-wrapper">
    <div class="content-box">
      <h1 class="welcome-title">Welcome to Our Portal</h1>

      <p class="welcome-subtitle">
        Access powerful tools, manage your account, and explore everything our platform has to
        offer.
      </p>

      <div class="action-area">
        <button v-if="!auth.isLoggedIn" class="btn-primary navButton" @click="auth.login()">
          <i class="pi pi-sign-in"></i>
          <span>Login</span>
        </button>

        <button v-else class="btn-primary navButton" @click="auth.logout()">
          <i class="pi pi-sign-out"></i>
          <span>Logout</span>
        </button>

        <RouterLink to="/about" class="about-link"> Learn more about our platform </RouterLink>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useAuthStore } from '@/stores/auth'
const auth = useAuthStore()
</script>

<style scoped>
.landing-wrapper {
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 2rem;
  background: var(--surface-ground);
}

.content-box {
  max-width: 450px;
  padding: 2rem;
  background: var(--surface-card);
  border-radius: 12px;
  box-shadow: var(--shadow-3);
  text-align: center;
}

.welcome-title {
  font-size: 2rem;
  margin-bottom: 0.75rem;
  color: var(--text-color);
}

.welcome-subtitle {
  font-size: 1.1rem;
  color: var(--text-color-secondary);
  margin-bottom: 2rem;
  line-height: 1.4;
}

.action-area {
  display: flex;
  flex-direction: column;
  gap: 1rem;
  align-items: center;
}

.navButton {
  width: 100%;
  border: none;
  border-radius: 7px;
  padding: 0.75rem;
  cursor: pointer;
  background-color: var(--primary);
  color: var(--primary-foreground);
  font-weight: 600;
  display: flex;
  align-items: center;
  gap: 0.5rem;
  justify-content: center;
  transition:
    background-color 0.2s ease,
    transform 0.2s ease;
}

.navButton:hover {
  background-color: var(--primary-hover);
  transform: translateY(-2px);
}

.about-link {
  font-size: 0.95rem;
  color: var(--primary);
  text-decoration: none;
  transition: color 0.2s ease;
}

.about-link:hover {
  color: var(--primary-600);
  text-decoration: underline;
}
</style>
