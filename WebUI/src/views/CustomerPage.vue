
<template>
  <div class="flex column customerBody">
    
    <div class="flex row pageHeader">
        <div class = "flex column leftPanel">
            <h2 style="margin: 0;">Customers</h2>
            <p style="margin: 0;">Manage your customer information and contact details.</p>
        </div>
        <div class = "rightPanel">
            <button class = "addButton" @click="displayCustomerDetails = !displayCustomerDetails; currentCustomerIndex = -1; customerTitle = 'Create Customer'; customerDescription = 'Create customer information and contact details'; customerButtonDesc = 'Create'">
                <p style="margin: 0; text-align: center; color: white;"> + Add Customers</p>
            </button>
        </div>
    </div>
    
    <div class = "flex row searchBarWrapper">
        <i class="pi pi-search"> </i>
      <InputText type="text" class="searchBar" placeholder="Search"/>
    </div>
 
    <div v-if="!state.loading" class="flex row customerDisplay">
      <Card v-for="customer in state.customer" :key="customer.customerId" class="mb-3">
        <template #header>
          <div class = "flex row customCardHeader">
            <i class="pi pi-pen-to-square editButton" style="font-size: 1.25rem" @click="displayCustomerDetails = !displayCustomerDetails; currentCustomerIndex = customer.customerId ?? 0; customerTitle = 'Update Customer'; customerDescription = 'Update customer information and contact details'; customerButtonDesc = 'Update'"></i>
            <i class="pi pi-trash editButton" style="font-size: 1.25rem"></i>
          </div>
        </template>
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
    <CustomerInformation :currentCustomerInformation="state.customer[currentCustomerIndex - 1]" :title="customerTitle" :description="customerDescription" :buttonDesctipnion="customerButtonDesc" @closePage="displayCustomerDetails = !displayCustomerDetails" @updateCustomerInformation="updateCustomerInformationTest"></CustomerInformation>
  </div>
  <div v-else>

  </div>
</template>

<script setup lang="ts">

import CustomerInformation from '../components/CustomerInformation.vue';
import Card from 'primevue/card'
import { ref } from 'vue'
import { Client, CustomerModel } from '../client/client'
import { onMounted, reactive } from 'vue'
import InputText from 'primevue/inputtext';
import 'primeicons/primeicons.css'

const client = new Client(import.meta.env.VITE_API_BASE_URL)

let displayCustomerDetails = ref(false)
let customerTitle = ref('');
let customerDescription = ref('');
let customerButtonDesc = ref('');
let currentCustomerIndex = ref(0);

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
  if(currentID == null){
    updatedCustomer.customerId = 0;
    state.loading = true;
    state.error = null;
    client
    .create(updatedCustomer)
    .catch((error) => {
      state.error = error.message || 'An error occurred'
    })
    .finally(() => {
      fetchCustomers();
      state.loading = false
    })
    //updatedCustomer.customerId = state.customer.length + 1;
    //state.customer.push(updatedCustomer);
  }
  else{
    state.customer[currentCustomerIndex.value - 1] = updatedCustomer;
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
  }
  displayCustomerDetails = ref(false); 
}

</script>

<style scoped>
  .flex{
    display: flex;
  }

  .row{
    flex-direction: row;
  }

  .column{
    flex-direction: column;
  }

  .pageHeader{
      height: 15vh;
      width: 100%;
      justify-content: space-between;
  }
  .leftPanel{
      flex-grow: 5;
  }
  .rightPanel{
    flex-grow: 1;
    align-content: center;
    text-align: right;
  }
  .addButton{
    background-color: rgb(0, 0, 0);
    border: none;
    height: 50%;
    width: 75%;
    align-content: center;
    border-radius: 7px;
  }
  .customerBody{
    width: 80%;
    margin-left: 10%;
    background-color: rgb(255, 255, 255); 
    padding: 3%;
    flex-grow: 2;
    gap: 10px;
    margin-top: 1vh;
    border-radius: 10px;
  }
  .searchBarWrapper{
    align-items: center;
    align-content: center;
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
  .customCardHeader{
    padding: 10%;
    padding-bottom: 0;
    gap: 7%;
    justify-content: right;
  }
  .editButton:hover{
    scale: 1.25;
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
