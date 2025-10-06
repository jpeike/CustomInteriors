<template>
  <button @click="login">Login</button>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import { useAuthStore } from '@/stores/auth'

export default defineComponent({
  setup() {
    const authStore = useAuthStore()

    const login = () => {
      authStore.login() // redirects to Cognito Hosted UI
    }

    return { login }
  },
})
</script>
