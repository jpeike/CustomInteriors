<template>
  <div>
    <h1>Employee Page</h1>
    <p>This is the user page.</p>
  </div>
  <div v-if="!state.loading">
    <Card v-for="employee in state.employees" :key="employee.id" class="mb-3">
      <template #title>{{ employee.id }}: {{ employee.username }}</template>
      <template #subtitle>{{ employee.email }}</template>
      <template #content>
        <p>Created on: {{ employee.createdOn }}</p>
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
  employees: [] as UserModel[],
  loading: false,
  error: null as string | null,
})

onMounted(() => {
  console.log('AboutView mounted')
  fetchEmployees()
})

function fetchEmployees() {
  state.loading = true
  state.error = null
  client
    .usersAll()
    .then((response) => {
      state.employees = response
    })
    .catch((error) => {
      state.error = error.message || 'An error occurred'
    })
    .finally(() => {
      state.loading = false
    })
}
</script>
