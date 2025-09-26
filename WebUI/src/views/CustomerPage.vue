<template>
  <div>
    <h1>Customer Page</h1>
    <p>This is the customer page.</p>
  </div>
  <div v-if="!state.loading">
    <Card v-for="customer in state.customer" :key="customer.customerId" class="mb-3">
      <template #title>{{customer.customerId}} : {{ customer.firstName }} {{ customer.lastName }}</template>
      <template #content>
        <p>Customer Notes: {{ customer.customerNotes }}</p>
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
import { Client, CustomerModel } from '../client/client'
import { onMounted, reactive } from 'vue'

const client = new Client(import.meta.env.VITE_API_BASE_URL)

const state = reactive({
  customer: [] as CustomerModel[],
  loading: false,
  error: null as string | null,
})

onMounted(() => {
  console.log('AboutView mounted')
  fetchCustomers()
})

function fetchCustomers() {
  state.loading = true
  state.error = null
  client
    .customersAll()
    .then((response) => {
      state.customer = response
    })
    .catch((error) => {
      state.error = error.message || 'An error occurred'
    })
    .finally(() => {
      state.loading = false
    })
}
</script>

<style scoped>
  
</style>
