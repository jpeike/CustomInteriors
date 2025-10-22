// src/client/apiClient.ts
import { Client as GeneratedApiClient, UserModel } from './client'
import { useAuthStore } from '@/stores/auth'

// Grab Pinia store for authentication
const authStore = useAuthStore()

// Custom fetch wrapper that injects Authorization header into every request
const customHttp = {
  fetch(url: RequestInfo, init?: RequestInit) {
    // Ensure headers exist
    const headers = {
      ...(init?.headers || {}),
      Authorization: authStore.accessToken ? `Bearer ${authStore.accessToken}` : '',
    }

    // Call the native fetch with modified headers
    return fetch(url, { ...init, headers })
  },
}

// Instantiate the NSwag-generated client with our custom http wrapper
export const proxiedApi = new GeneratedApiClient('https://localhost:44351', customHttp)

// Example usage (optional helper functions)
export async function getAllUsers(): Promise<UserModel[]> {
  return proxiedApi.getAllUsers()
}
