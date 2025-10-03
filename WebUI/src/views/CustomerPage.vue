
<template>
  <div class="flex column customerBody">
    <div class="flex row pageHeader">
        <div class = "flex column leftPanel">
            <h2 style="margin: 0;">Customers</h2>
            <p style="margin: 0;">Manage your customer information and contact details.</p>
        </div>
        <div class = "rightPanel">
            <button class = "addButton" @click="displayCustomerDetails = !displayCustomerDetails; currentCustomerIndex = -1; customerTitle = 'Create Customer'; customerDescription = 'Create customer information and contact details'; customerButtonDesc = 'Create'">
                <p style="margin: 0; text-align: center;"> + Add Customers</p>
            </button>
        </div>
    </div>
    
    <div class = "flex row searchBarWrapper">
        <i class="pi pi-search"> </i>
      <InputText v-model="searchValue"  type="text" class="searchBar" placeholder="Search"/>
    </div>
 
    <div v-if="!state.loading" class="flex row customerDisplay">
      <Card v-for="customer in filterCustomer()" :key="customer.customerId" class="mb-3">
        <template #header>  
          <div class = "flex row customCardHeader">
            <p style="margin: 0; flex-grow: 2; font-size: 1.2rem; font-weight: bold;">{{ customer.firstName }} {{ customer.lastName }}</p>
            <div class="flex row" style="justify-content: left; flex-grow: 0; gap: 15%">
              <i class="pi pi-pen-to-square editButton" style="font-size: 1.1rem" @click="displayCustomerDetails = !displayCustomerDetails; getCustomerIndex(customer.customerId ?? 0); customerTitle = 'Update Customer'; customerDescription = 'Update customer information and contact details'; customerButtonDesc = 'Update'"></i>
              <i class="pi pi-trash editButton" style="font-size: 1.1rem" @click="deleteConfirmation = !deleteConfirmation; getCustomerIndex(customer.customerId ?? 0);"></i>
            </div>
          </div>
        </template>
        <template #content>
          <div class="flex row" style="gap:5%;">
            <div>
              <p style="margin: 0;">Contact: </p>
            </div>
            <div v-if="customer.prefferedContactMethod == 'Email'">
              Temp Email
            </div>
            <div v-else-if="customer.prefferedContactMethod == 'Phone'">
              Temp Phone
            </div>
            <div v-else>
              Not Listed
            </div>
          </div>
          
          <br/><p style="margin: 0;">Address: (temp)</p>
          <br/>
          <p style="margin: 0;">Type: {{ customer.customerType }}</p>
          <br/>
          <p style="margin: 0;">Company: {{ customer.companyName }}</p>
          <br/>
          <p style="margin: 0;">Status: {{ customer.companyName }}</p>
          <br/>
          <p style="margin: 0;">Notes: {{ customer.customerNotes }}</p>
        </template>
      </Card>
    </div>
    <div v-else-if="state.error">{{ state.error }}</div>
    <div v-else>
      <p>Loading...</p>
    </div>
  </div>

  <div v-if="displayCustomerDetails" class="flex row customerWindowBlur">
    <CustomerInformation :currentCustomerInformation="state.customer[currentCustomerIndex]" :title="customerTitle" :description="customerDescription" :buttonDesctipnion="customerButtonDesc" @closePage="displayCustomerDetails = !displayCustomerDetails" @updateCustomerInformation="updateCustomerInformation"></CustomerInformation>
  </div>

  <div v-if="deleteConfirmation" class="flex row customerWindowBlur">
    <deleteConfirmation :currentCustomerInformation="state.customer[currentCustomerIndex]" :title="(state.customer[currentCustomerIndex].firstName + ' ' + state.customer[currentCustomerIndex].lastName)" @closePage="deleteConfirmation = !deleteConfirmation" @deleteCustomer="deleteCustomer"></deleteConfirmation>
  </div>
  <div v-else>

  </div>
</template>

<script setup lang="ts">

import CustomerInformation from '../components/CustomerInformation.vue';
import DeleteConfirmation from '../components/DeleteConfirmation.vue';
import Card from 'primevue/card'
import { ref } from 'vue'
import { Client, CustomerModel } from '../client/client'
import { onMounted, reactive } from 'vue'
import InputText from 'primevue/inputtext';
import 'primeicons/primeicons.css'

const client = new Client(import.meta.env.VITE_API_BASE_URL)

let displayCustomerDetails = ref(false);
let deleteConfirmation = ref(false);

let customerTitle = ref('');
let customerDescription = ref('');
let customerButtonDesc = ref('');
let currentCustomerIndex = ref(0);
let searchValue = ref('');

const state = reactive({
  customer: [] as CustomerModel[],
  loading: false,
  error: null as string | null,
})

onMounted(() => {
  console.log('AboutView mounted')
  fetchCustomers()
})

function getCustomerIndex(customerID: number){
  for (let i = 0; i < state.customer.length; i++){
    if (state.customer[i].customerId == customerID){
      currentCustomerIndex = ref(i);
    }
  }
}

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

function updateCustomerInformation(currentID: number, updatedCustomer: CustomerModel){
  if(currentID == null){
    updatedCustomer.customerId = 0;
    state.loading = true;
    state.error = null;
    client
    .createCustomer(updatedCustomer)
    .catch((error) => {
      state.error = error.message || 'An error occurred'
    })
    .finally(() => {
      fetchCustomers();
      state.loading = false
    })
  }
  else{
    state.customer[currentCustomerIndex.value - 1] = updatedCustomer;
    state.customer[currentCustomerIndex.value - 1].customerId = currentID;
    
    state.loading = true
    state.error = null
    client
    .updateCustomer(state.customer[currentCustomerIndex.value - 1])
    .catch((error) => {
      state.error = error.message || 'An error occurred'
    })
    .finally(() => {
      fetchCustomers();
      state.loading = false
    })
  }
  displayCustomerDetails = ref(false); 
}

function deleteCustomer(currentID: number){
    console.log(currentID);
    state.loading = true
    state.error = null
    client
    .deleteCustomer(currentID)
    .catch((error) => {
      state.error = error.message || 'An error occurred'
    })
    .finally(() => {
      fetchCustomers();
      state.loading = false
    })
  deleteConfirmation = ref(false); 
}

function filterCustomer(){
  return state.customer.filter((customers) =>
    customers.firstName?.toLowerCase().includes(searchValue.value.toLowerCase())
    ||
    customers.lastName?.toLowerCase().includes(searchValue.value.toLowerCase())
    ||
    customers.prefferedContactMethod?.toLowerCase().includes(searchValue.value.toLowerCase())
  );
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
    border: none;
    height: 50%;
    width: 75%;
    align-content: center;
    border-radius: 7px;
  }
  .customerBody{
    width: 90vw;
    height: 90%;
    position: absolute;
    padding: 3%;
    flex-grow: 2;
    gap: 10px;
    margin-top: 1vh;
    margin-left: 10%;
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
    padding-right: 2%;
    flex-wrap: wrap;
    justify-content: flex-start;
    column-gap: 2%;
    overflow: scroll;
    height: 100%;
  }
  .mb-3{
    border: solid;
    border-width: 1px;
    border-color: rgb(222, 222, 222);
    border-radius: 10px;
    width: 23.5%;
    height: 50vh;
    margin-bottom: 2%;
    overflow: scroll;
  }
  .customCardHeader{
    padding: 10%;
    padding-bottom: 0;
    gap: 7%;
    justify-content: space-between;
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
    justify-content: center;
    align-items: center;
  }
</style>
