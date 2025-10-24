<template>
  <div class="flex column errorBody">
    <div class="errorContainer">
      <div class="errorIcon">
        <i :class="iconClass"></i>
      </div>
      
      <h1 v-if="shouldShowCode" class="errorCode">{{ errorCode }}</h1>
      <h2 class="errorTitle">{{ errorTitle }}</h2>
      <p class="errorDescription">{{ errorDescription }}</p>
      
      <div class="buttonRow">
        <router-link to="/" class="buttons"><i class="pi pi-home"></i>Go Back Home</router-link>
        <router-link to="#" class="buttons" @click="$router.go(-1)"><i class="pi pi-arrow-left"></i>Go Back</router-link>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import 'primeicons/primeicons.css'
import { useRoute } from 'vue-router';
import { useRouter } from 'vue-router';

const route = useRoute();
const router = useRouter();
const previousPage = () => {
      router.back();
};

const errorCode = computed<string>(() => {
  const code =  route.params.code ?? 'unknown'
  if (Array.isArray(code)) {
    return code[0] ?? 'unknown'
  }
  return code
})

const shouldShowCode = computed(() => {
  const code = errorCode.value
  return code !== 'unknown' && !isNaN(Number(code))
})

const getError = computed(() => {
  const errors: Record<string, { title: string; description: string; icon: string }> = {
    '400': {
      title: 'Bad Request',
      description: 'The request could not be understood by the server.',
      icon: 'pi-exclamation-triangle'
    },
    '401': {
      title: 'Unauthorized',
      description: 'You need to be logged in to access this page.',
      icon: 'pi-lock'
    },
    '403': {
      title: 'Forbidden',
      description: 'You don\'t have permission to access this resource.',
      icon: 'pi-ban'
    },
    '404': {
      title: 'Page Not Found',
      description: 'Sorry, the page you are looking for does not exist or has been moved.',
      icon: 'pi-exclamation-circle'
    },
    '500': {
      title: 'Internal Server Error',
      description: 'Something went wrong on our end. Please try again later.',
      icon: 'pi-times-circle'
    },
    '503': {
      title: 'Service Unavailable',
      description: 'The service is temporarily unavailable. Please try again later.',
      icon: 'pi-exclamation-triangle'
    },
    'unknown': { 
      title: 'Something Went Wrong',
      description: 'An unexpected error occurred. Please try again later.',
      icon: 'pi-exclamation-triangle'
    }
  }
  
  return errors[errorCode.value] || errors['unknown']
})

const errorTitle = computed(() => getError.value.title)
const errorDescription = computed(() => getError.value.description)
const iconClass = computed(() => `pi ${getError.value.icon}`)
</script>

<style scoped>
.flex {
  display: flex;
}

.row {
  flex-direction: row;
}

.column {
  flex-direction: column;
}

.errorBody {
  width: auto;
  height: 97vh;
  top: 0;
  left: 0;
  justify-content: center;
  align-items: center;
  background: white;
}

.errorContainer {
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
  max-width: 500px;
  padding: 3rem 2rem;
}

.errorIcon {
  color: #d4d4d4;
  margin-bottom: 1.5rem;
}

.errorIcon i {
  font-size: 7.5rem;
}

.errorCode {
  font-size: 4rem;
  font-weight: 700;
  color: #000;
  margin: 0 0 1rem 0;
  letter-spacing: -0.02em;
}

.errorTitle {
  font-size: 1.5rem;
  font-weight: 600;
  color: #000;
  margin: 0 0 0.75rem 0;
}

.errorDescription {
  font-size: 0.9375rem;
  color: #737373;
  margin: 0 0 2rem 0;
  line-height: 1.6;
}

.buttons {
  display: inline-flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.75rem 1.5rem;
  background-color: #000;
  color: white;
  border: none;
  border-radius: 6px;
  font-size: 0.875rem;
  font-weight: 500;
  text-decoration: none;
  cursor: pointer;
  transition: background-color 0.15s;
}

.buttonRow {
  display: flex;
  gap: 1rem;
  justify-content: center;
}

.buttons:hover {
  background-color: #262626;
}

/* Dark mode support */
@media (prefers-color-scheme: dark) {
  .errorBody {
    background: #1a1a1a;
  }

  .errorIcon {
    color: #404040;
  }

  .errorCode {
    color: #ffffff;
  }

  .errorTitle {
    color: #ffffff;
  }

  .errorDescription {
    color: #a3a3a3;
  }

  .buttons {
    background-color: #ffffff;
    color: #000;
  }

  .homeButton {
    background-color: #ffffff;
    color: #000;
  }

  .buttons:hover {
    background-color: #e5e5e5;
  }
}
</style>