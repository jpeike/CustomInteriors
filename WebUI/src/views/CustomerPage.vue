<template>
  <div class="flex column customerBody">
    <div class="flex row pageHeader">
        <div class = "flex column leftPanel">
            <h2 style="margin: 0;">Customers</h2>
            <p style="margin: 0;">Manage your customer information and contact details.</p>
        </div>
        <div class = "rightPanel">
            <button class = "addButton" @click="createCustomerDisplay()">
                <p style="margin: 0; text-align: center;"> + Add Customers</p>
            </button>
        </div>
    </div>
    
    <div class = "flex row searchBarWrapper">
        <i class="pi pi-search"> </i>
      <InputText v-model="searchValue"  type="text" class="searchBar" placeholder="Search"/>
    </div>
 
    <div v-if="!isLoading" class="flex row customerDisplay">
        <CustomerCard
          v-for="customer in filteredCustomers" 
          :key="customer.customerId"
          :customer="customer"
          :email="formatEmail(customer.emails?.[0])"
          :address="formatAddress(customer.addresses?.[0])"
          @edit="editCustomerUI"
          @delete="openDeleteModal(); getCustomerIndex(customer.customerId ?? 0);"
        />
      
    </div>
    <div v-else-if="isError">{{ isError }}</div>
    <div v-else>
      <p>Loading...</p>
    </div>
  </div>

  <div v-if="displayCustomerDetails" class="flex row customerWindowBlur">
    <CustomerInformation 
      :currentCustomerInformation="currentCustomer" 
      :currentEmails="currentEmailAddresses" 
      :currentAddresses = "currentCustomerAddresses" 
      :title="customerTitle" 
      :description="customerDescription" 
      :buttonDesctipnion="customerButtonDesc" 
      @closePage="closePage" 
      @updateCustomerInformation="updateCustomerInformation">
    </CustomerInformation>
  </div>

  <div v-if="deleteConfirmation" class="flex row customerWindowBlur">
    <deleteConfirmation 
      :currentCustomerInformation="customers[currentCustomerIndex]" 
      :title="(customers[currentCustomerIndex].firstName + ' ' + customers[currentCustomerIndex].lastName)" 
      @closePage="closeDeleteModal" 
      @deleteCustomer="handleDelete">
    </deleteConfirmation>
  </div>
</template>

<script setup lang="ts">

import CustomerInformation from '../components/customers/CustomerInformation.vue';
import DeleteConfirmation from '../components/DeleteConfirmation.vue';
import CustomerCard from '../components/customers/CustomerCard.vue'
import { CustomerModel, AddressModel, EmailModel} from '../client/client'
import { computed } from 'vue'
import { onMounted } from 'vue'
import InputText from 'primevue/inputtext';
import 'primeicons/primeicons.css'
import { useAddresses } from '@/composables/useAddresses.ts'
import { useCustomers } from '@/composables/useCustomers.ts'
import { useEmails } from '@/composables/useEmails.ts'
import { useCustomerSearch } from '@/composables/useCustomerSearch'
import { useCustomerEditFlow } from '@/composables/useCustomerEditFlow'
import { useCustomerModals } from '@/composables/useCustomerModals'

const customersStore = useCustomers()
const { 
  customers, 
  customersLoading, 
  customersError, 
  fetchCustomersWithDetails, 
} = customersStore


const addressesStore = useAddresses()
const { 
  addressesLoading, 
  addressesError,
  formatAddress 
} = addressesStore

const emailsStore = useEmails()
const { 
  emailsLoading, 
  emailsError, 
  formatEmail 
} = emailsStore

const customerModalsStore = useCustomerModals({
  customersStore,
  addressesStore,
  emailsStore,
})

const {
    currentCustomer,
    currentCustomerAddresses,
    currentEmailAddresses,
    currentCustomerIndex,
    displayCustomerDetails,
    customerModalLoading,
    customerTitle,
    customerDescription,
    customerButtonDesc,
    deleteConfirmation,

    closePage,
    createCustomerDisplay,
    editCustomerUI,
    getCustomerIndex,
    openDeleteModal,
    closeDeleteModal
   } = customerModalsStore

const customerEditFlow = useCustomerEditFlow({
  customersStore,
  addressesStore,
  emailsStore,
  customerModalsStore,
})

const {updateCustomerInformation, handleDelete}  = customerEditFlow
const { searchValue, filteredCustomers } = useCustomerSearch(customers)

onMounted(() => {
  console.log('AboutView mounted')
  fetchCustomersWithDetails()
})

const isLoading = computed(() =>
  customersLoading.value ||
  addressesLoading.value ||
  emailsLoading.value    ||
  customerModalLoading.value
)

const isError = computed(() =>
  customersError.value ||
  addressesError.value ||
  emailsError.value
)

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