import { useToast as usePrimeToast } from 'primevue/usetoast'

export function useToast() {
  const toast = usePrimeToast()
  
  function showSuccess(message: string, summary = 'Success') {
    toast.add({
      severity: 'success',
      summary,
      detail: message,
      life: 3000
    })
  }
  
  function showError(error: any, summary = 'Error') {
    let message = 'An unexpected error occurred'
    
    if (typeof error === 'string') {
      message = error
    } else if (error?.message) {
      message = error.message
    } else if (error?.response?.data?.message) {
      message = error.response.data.message
    }
    
    if (error?.status) {
      const statusMessages: Record<number, string> = {
        400: 'Invalid request. Please check your input.',
        401: 'You need to be logged in to perform this action.',
        403: 'You don\'t have permission to perform this action.',
        404: 'The requested resource was not found.',
        409: 'This action conflicts with existing data.',
        422: 'Validation failed. Please check your input.',
        500: 'Server error. Please try again later.',
        503: 'Service temporarily unavailable.'
      }
      message = statusMessages[error.status] || message
    }
    
    toast.add({
      severity: 'error',
      summary,
      detail: message,
      life: 5000
    })
  }
  
  function showWarning(message: string, summary = 'Warning') {
    toast.add({
      severity: 'warn',
      summary,
      detail: message,
      life: 4000
    })
  }
  
  function showInfo(message: string, summary = 'Info') {
    toast.add({
      severity: 'info',
      summary,
      detail: message,
      life: 3000
    })
  }
  
  return { 
    showSuccess, 
    showError, 
    showWarning, 
    showInfo 
  }
}