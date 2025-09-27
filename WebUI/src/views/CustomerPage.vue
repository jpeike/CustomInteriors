
<template>
  <div class="customerBody">
    <PageHeader title="Customers" description="Manage your customer information and contact details." add="Add Customers"></PageHeader>
    <div class = "searchBarWrapper">
      <!--
      <InputIcon>
        <i class="pi pi-search"/>
      </InputIcon>-->
      <InputText type="text" class="searchBar" placeholder="Search"/>
    </div>
 
    <div v-if="!state.loading" class="customerDisplay">
      <Card v-for="customer in state.customer" :key="customer.customerId" class="mb-3" @click="displayCustomerDetails = !displayCustomerDetails; currentCustomerIndex = customer.customerId ?? 0">
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
  </div>

  <div v-if="displayCustomerDetails" class="customerWindowBlur">
    <CustomerInformation :currentCustomerInformation="state.customer[currentCustomerIndex - 1]" @closePage="displayCustomerDetails = !displayCustomerDetails" @updateCustomerInformation="updateCustomerInformationTest"></CustomerInformation>
  </div>
  <div v-else>

  </div>
</template>

<script setup lang="ts">

import PageHeader from '../components/PageHeader.vue';
import CustomerInformation from '../components/CustomerInformation.vue';
import Card from 'primevue/card'
import { ref } from 'vue'
import { Client, CustomerModel } from '../client/client'
import { onMounted, reactive } from 'vue'
import InputText from 'primevue/inputtext';

const client = new Client(import.meta.env.VITE_API_BASE_URL)
let displayCustomerDetails = ref(false)
const currentCustomerIndex = ref(0)

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

function updateCustomerInformationTest(currentID: number, updatedCustomer: CustomerModel){
  const tempType = state.customer[currentCustomerIndex.value - 1].customerType;//remove later since customerType cannot be NULL
  state.customer[currentCustomerIndex.value - 1] = updatedCustomer;
  state.customer[currentCustomerIndex.value - 1].customerType = tempType;//remove later
  state.customer[currentCustomerIndex.value - 1].customerId = currentID;
  
  state.loading = true
  state.error = null
  client
  .update(state.customer[currentCustomerIndex.value - 1])
  .catch((error) => {
    state.error = error.message || 'An error occurred'
  })
  .finally(() => {
    state.loading = false
  })
  displayCustomerDetails = ref(false);
}

</script>

<style scoped>
  .customerBody{
    width: 80%;
    margin-left: 10%;
    background-color: rgb(255, 255, 255); 
    padding: 3%;
    display: flex;
    flex-grow: 2;
    flex-direction: column;
    gap: 10px;
    margin-top: 1vh;
    border-radius: 10px;
  }
  .searchBarWrapper{
    display: flex;
    align-items: center;
    align-content: center;
    flex-direction: row;
    padding: 1%;
    border: solid;
    border-width: 1px;
    border-color: rgb(222, 222, 222);
    border-radius: 10px;
  }
  .searchBar{
    width: 100%;
    box-shadow: none;
    border: none;
  }
  .customerDisplay{
    width: 100%;
    display: flex;
    flex-wrap: wrap;
    justify-content: flex-start;
    column-gap: 2%;
  }
  .mb-3{
    border: solid;
    border-width: 1px;
    border-color: rgb(222, 222, 222);
    border-radius: 10px;
    width: 23.5%;
    height: 50vh;
    margin-bottom: 2%;
  }
  .customerWindowBlur{
        position: fixed;
        backdrop-filter: brightness(60%) blur(2px);
        height: 100vh;
        width: 100vw;
        top: 0px;
        left: 0px;
    }
</style>
