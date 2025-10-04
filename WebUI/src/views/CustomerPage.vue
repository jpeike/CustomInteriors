<template>
  <div>
    <AddressListModal
      :isOpen="showAddressList"
      :customerId="selectedCustomerId"
      :addresses="state.addresses"
      @close="showAddressList = false"
      @create="openCreateAddressModal"
      @edit="openUpdateAddressModal"
      @delete="deleteAddress"
    />

    <CreateAddressModal
      :isOpen="showCreateAddressModal"
      :customerId="selectedCustomerId"
      :errorMessage="createError"
      @close="closeCreateModal"
      @created="createAddress"
    />

    <UpdateAddressModal
      :isOpen="showUpdateAddressModal"
      :customerId="selectedCustomerId"
      :address="selectedAddress"
      :errorMessage="updateError"
      @close="closeUpdateModal"
      @updated="updateaddress"
    />
  </div>
  <div>
    <h1>Customer Page</h1>
    <p>This is the customer page.</p>
  </div>
  <div v-if="!state.loading">
    <Card v-for="customer in state.customer" :key="customer.customerId" class="mb-3">
      <template #title>{{customer.customerId}} : {{ customer.firstName }} {{ customer.lastName }}</template>
      <template #content>
        <p>Customer Notes: {{ customer.customerNotes }}</p>
        <button @click="openAddressListModal(customer.customerId!)">View Addresses</button>
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
import { ref } from 'vue'
import { Client, CustomerModel, AddressModel } from '../client/client'
import { onMounted, reactive } from 'vue'
import AddressListModal from '../components/modals/AddressListModal.vue'
import CreateAddressModal from '../components/modals/CreateAddressModal.vue'
import UpdateAddressModal from '../components/modals/UpdateAddressModal.vue'

const client = new Client(import.meta.env.VITE_API_BASE_URL)
const showAddressList = ref(false)
const showCreateAddressModal = ref(false)
const showUpdateAddressModal = ref(false)
const createError = ref<string | null>(null)
const updateError = ref<string | null>(null)
const selectedCustomerId = ref<number | null>(null)
const selectedAddress = ref<AddressModel | null>(null)

const state = reactive({
  customer: [] as CustomerModel[],
  addresses: [] as AddressModel[],
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

function fetchAddressesByCustomerId(customerId: number) {
  state.loading = true
  state.error = null
  client
    .address2(customerId)
    .then((response) => {
      state.addresses = response
    })
    .catch((error) => {
      state.error = error.message || 'An error occurred'
    })
    .finally(() => {
      state.loading = false
    })
}

function openAddressListModal(customerId: number) {
  fetchAddressesByCustomerId(customerId)
  selectedCustomerId.value = customerId
  showAddressList.value = true
}

function openCreateAddressModal() {
  showAddressList.value = false 
  showCreateAddressModal.value = true 
}

function closeCreateModal() {
  showCreateAddressModal.value = false
  showAddressList.value = true 
}

function openUpdateAddressModal(address: AddressModel) {
  selectedAddress.value = address
  showAddressList.value = false 
  showUpdateAddressModal.value = true 
}

function closeUpdateModal() {
  showUpdateAddressModal.value = false
  showAddressList.value = true 
}

async function deleteAddress(addressId: number) {
  try {
    await client.deleteAddress(addressId)
    fetchAddressesByCustomerId(selectedCustomerId.value!)
  } catch (error) {
    console.error('Delete failed:', error)
  }
}

async function createAddress(address: AddressModel) {
  try {
    createError.value = null
    await client.createAddress(address)
    fetchAddressesByCustomerId(selectedCustomerId.value!)
  } catch (error) {
    console.error('Create failed:', error)
    createError.value = 'Failed To Create Address'
  }
}

async function updateaddress(address: AddressModel) {
  try {
    updateError.value = null
    await client.updateAddress(address)
    fetchAddressesByCustomerId(selectedCustomerId.value!)
  } catch (error) {
    updateError.value = 'Failed To Create Address'
    console.error('Update failed:', error)
  }
}
</script>

<style scoped>
  
</style>
