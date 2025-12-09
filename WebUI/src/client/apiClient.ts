import { Client as GeneratedApiClient } from './client'
import { useAuthStore } from '@/stores/auth'


// Custom fetch wrapper
const authFetch: typeof fetch = (input, init) => {
  const authStore = useAuthStore()
  const headers = {
    ...(init?.headers || {}),
    Authorization: authStore.accessToken ? `Bearer ${authStore.accessToken}` : '',
  }
  return fetch(input, { ...init, headers })
}

// Instantiate client with custom fetch
export const Client = new GeneratedApiClient(import.meta.env.VITE_API_BASE_URL, { fetch: authFetch })
