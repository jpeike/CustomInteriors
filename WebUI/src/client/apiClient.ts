import { Client as GeneratedApiClient } from './client.ts'
import { useAuthStore } from '@/stores/auth'

const authStore = useAuthStore()
const api = new GeneratedApiClient('/api')

export const proxiedApi = new Proxy(api, {
  get(target, prop) {
    const orig = (target as any)[prop]
    if (typeof orig === 'function') {
      return (...args: any[]) => {
        const lastArg = args[args.length - 1] || {}
        const headers = {
          ...(lastArg.headers || {}),
          Authorization: authStore.accessToken ? `Bearer ${authStore.accessToken}` : '',
        }
        args[args.length - 1] = { ...lastArg, headers }
        return orig.apply(target, args)
      }
    }
    return orig
  },
})
