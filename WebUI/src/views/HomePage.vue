<template>
  <h1>Home Page</h1>
  <button @click="logout">Log me out please</button>
</template>

<script setup lang="ts">
import { useAuthStore } from '@/stores/auth'

const auth = useAuthStore()

function logout() {
  auth.logout()
}
</script>
