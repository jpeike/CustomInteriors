// src/composables/useErrorHandler.ts
import { useRouter } from 'vue-router'
import { RouteNames } from '@/enums/RouteNames'

export function useErrorHandler() {
  const router = useRouter()
  
  function redirectToError(error: any) {
    const code = getErrorCode(error)

    router.push({ 
        name: RouteNames.ERROR_PAGE, 
        params: {code: code}, 
        query: { routeName: router.currentRoute.value.fullPath } 
    })
  }
  
  function getErrorCode(error: any): string {
    if (error.status) return error.status.toString()
    if (error.message?.includes('ERR_CONNECTION_REFUSED')) return '503'
    if (error.message?.includes('timeout')) return '408'
    if (error.message?.includes('Network Error')) return '503'
    return 'unknown'
  }
  
  return { redirectToError }
}