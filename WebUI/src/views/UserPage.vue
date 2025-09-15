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
import { Client, UserModel } from '../client/client'
import { onMounted, reactive } from 'vue'

const client = new Client(import.meta.env.VITE_API_BASE_URL)

const state = reactive({
  users: [] as UserModel[],
  loading: false,
  error: null as string | null,
})

onMounted(() => {
  console.log('AboutView mounted')
  fetchUsers()
})

function fetchUsers() {
  state.loading = true
  state.error = null
  client
    .usersAll()
    .then((response) => {
      state.users = response
    })
    .catch((error) => {
      state.error = error.message || 'An error occurred'
    })
    .finally(() => {
      state.loading = false
    })
}
</script>
