import { createApp } from 'vue'
import { createPinia } from 'pinia'

import PrimeVue from 'primevue/config'
import Skeleton from 'primevue/skeleton'
import Aura from '@primeuix/themes/aura'
import { definePreset } from '@primeuix/themes'
import App from './App.vue'
import router from './router'
import Toast from 'primevue/toast'
import ToastService from 'primevue/toastservice'
import googleAutoComplete from './types/googleAutoComplete'

import { useAuthStore } from './stores/auth' // <-- make sure to import

const app = createApp(App)
// Register Pinia first
const pinia = createPinia()
app.use(pinia)

const MyPreset = definePreset(Aura, {
  semantic: {
    primary: {
      50: '{indigo.50}',
      100: '{indigo.100}',
      200: '{indigo.200}',
      300: '{indigo.300}',
      400: '{indigo.400}',
      500: '{indigo.500}',
      600: '{indigo.600}',
      700: '{indigo.700}',
      800: '{indigo.800}',
      900: '{indigo.900}',
      950: '{indigo.950}',
    },
  },
})

app.use(PrimeVue, {
  theme: {
    preset: MyPreset,
  },
})



app.use(router)

app.use(ToastService)
app.component('Toast', Toast)
app.component('Skeleton', Skeleton)

const authStore = useAuthStore()
authStore.init()

app.directive('google-autocomplete', googleAutoComplete)

app.mount('#app')
