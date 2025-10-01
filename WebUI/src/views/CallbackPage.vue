<!-- src/views/CallbackPage.vue -->
<template>
  <div>Logging in...</div>
</template>

<script lang="ts">
import { defineComponent, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth'

export default defineComponent({
  setup() {
    const authStore = useAuthStore()
    const router = useRouter()

    onMounted(async () => {
      try {
        // Completes PKCE flow and stores tokens in authStore.user
        await authStore.handleCallback()
        // Redirect to home or intended route
        router.replace('/')
      } catch (err) {
        console.error('Error handling login callback:', err)
      }
    })
  },
})
</script>
