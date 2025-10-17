<template>
  <div>
    <h1>User Page</h1>
    <p>This is the user page.</p>
  </div>
  <div v-if="!state.loading">
    <Card v-for="user in state.users" :key="user.id" class="mb-3">
      <template #title>{{ user.id }}: {{ user.username }}</template>
      <template #subtitle>{{ user.email }}</template>
      <template #content>
        <p>Created on: {{ user.createdOn }}</p>
      </template>
    </Card>
  </div>
  <div v-else-if="state.error">{{ state.error }}</div>
  <div v-else>
    <p>Loading...</p>
  </div>
</template>

<script setup lang="ts">
import Card from 'primevue/card'
import { UserModel } from '../client/client'
import { proxiedApi as Client } from '@/client/apiClient'
import { onMounted, reactive } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()

const state = reactive({
  users: [] as UserModel[],
  loading: false,
  error: null as string | null,
})

onMounted(() => {
  console.log('AboutView mounted')
  fetchUsers()
})

function redirectToErrorPage(errorCode: number) {
  switch (errorCode) {
    case 401:
      router.push({ name: 'unauthorized' })
      break
    case 403:
      router.push({ name: 'forbidden' })
      break
    case 500:
      router.push({ name: 'InternalServerError' })
      break
    case 502:
      router.push({ name: 'badGateway' })
      break
    case 503:
      router.push({ name: 'serviceUnavailable' })
      break
    case 408:
      router.push({ name: 'requestTimeout' })
      break
    case 404:
      router.push({ name: 'notFound' })
      break
    default:
      router.push({ name: 'generalError' })
      break
  }
}

function fetchUsers() {
  state.loading = true
  state.error = null
  Client.usersAll()
    .then((response) => {
      state.users = response
    })
    .catch((error) => {
      state.error = error.message || 'An error occurred'
      if (error.status) {
        redirectToErrorPage(error.status)
      } else {
        redirectToErrorPage(0) // Unknown error
      }
    })
    .finally(() => {
      state.loading = false
    })
}
</script>
