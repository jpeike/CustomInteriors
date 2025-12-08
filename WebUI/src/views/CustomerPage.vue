<template>
  <div v-if="isLoading" class="skeletonPage">
  <Skeleton width="12rem" height="2rem" class="mb-3" />

  <div class="flex row pageHeader">
    <Skeleton height="2.8rem" class="flex-grow-1" style="border-radius: 10px;" />
    <Skeleton width="150px" height="2.8rem" style="margin-left: 1rem; border-radius: 10px;" />
  </div>

  <div class="flex row customerDisplay">
    <div v-for="n in 8" :key="n" class="customerSkeleton">
      <Skeleton height="100%" width="100%" borderRadius="10px" />
    </div>
  </div>

</div>
  <div v-else>
    <div class="flex column customerBody">
      <div class="flex row pageHeader">

      <div class="flex column leftPanel">
          <h2 style="margin: 0; padding:0;">Customers</h2>
      </div>

      <div class="searchBarWrapper">
          <i class="pi pi-search"></i>
          <InputText v-model="searchValue" type="text" class="searchBar" placeholder="Search" data-testid="customerSearchInput"/>
      </div>

      <div class="rightPanel">
          <button class="addButton" @click="createCustomerDisplay()" data-testid="addCustomerButton">
              <p style="margin: 0; text-align: center;">+New Customer</p>
          </button>
          <ToggleButton
            v-model="showActiveOnly"
            onLabel="Active Only"
            offLabel="All Customers"
            onIcon="pi pi-check"
            offIcon="pi pi-filter"
            class="ml-3"/>
      </div>
  </div>
      <div v-if="!isLoading" class="flex row customerDisplay">
          <CustomerCard
            v-for="customer in filteredAndStatusCustomers"
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
        description="Create a customer"
        :buttonDesctipnion="customerButtonDesc"
        @closePage="closePage"
        @updateCustomerInformation="updateCustomerInformation">
      </CustomerInformation>
    </div>

    <div v-if="deleteConfirmation" class="flex row customerWindowBlur">
      <deleteConfirmation
        :currentInfo="customers[currentCustomerIndex]"
        :title="(customers[currentCustomerIndex].firstName + ' ' + customers[currentCustomerIndex].lastName)"
        @closePage="closeDeleteModal"
        @deleteCustomer="handleDelete">
      </deleteConfirmation>
    </div>
  </div>
</template>

<script setup lang="ts">

import CustomerInformation from '../components/customers/CustomerInformation.vue';
import DeleteConfirmation from '../components/DeleteConfirmation.vue';
import CustomerCard from '../components/customers/CustomerCard.vue'
import { CustomerModel, AddressModel, EmailModel} from '../client/client'
import { ref, computed } from 'vue'
import { onMounted } from 'vue'
import ToggleButton from 'primevue/togglebutton';
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

const showActiveOnly = ref(false)
const filteredAndStatusCustomers = computed(() => {
  return filteredCustomers.value.filter(c =>
    !showActiveOnly.value || c.status === 'Active'
  )
})

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
.skeletonPage {
  width: 85vw;
  margin-left: 15vw;
  padding: 3%;
}

.customerSkeleton {
  width: 23.5%;
  height: 50vh;
  margin-bottom: 2%;
  border-radius: 10px;
}

 /* Flex helpers */
.flex {
  display: flex;
}

.row {
  flex-direction: row;
}

.column {
  flex-direction: column;
}

/* Page layout */
.customerBody {
  width: 85vw;
  margin-left: 15vw;
  height: 90%;
  position: absolute;
  padding: 3%;
  flex-grow: 2;
  gap: 10px;
  margin-top: 1vh;
  border-radius: 10px;
}

.pageHeader {
  width: 100%;
  display: flex;
  align-items: center;
  gap: 1rem;
  flex-wrap: wrap;
}

.leftPanel {
  flex-basis: 100%;
  margin-bottom: 1rem;}

.rightPanel {
  flex-grow: 0;
  display: flex;
  justify-content: flex-end;
  align-items: center;
}

/* Add Customers Button */
.addButton {
  border: none;
  height: 50%;
  width: 12vw;
  border-radius: 7px;
  padding: 0.5rem 1rem;
  margin-left: 1rem;
  margin-right: 1rem;
  cursor: pointer;
  background-color: var(--primary);
  color: var(--primary-foreground);
  transition: background-color 0.2s ease, transform 0.2s ease;
}

.addButton:hover {
  background-color: var(--secondary);
  color: var(--foreground);
  transform: scale(1.05);
}

/* Search bar */
.searchBarWrapper {
  flex-grow: 2;
  display: flex;
  align-items: center;
  padding: 0.5rem;
  border: solid 1px rgb(222, 222, 222);
  border-radius: 10px;
  margin-bottom: 1rem;
}

.searchBar {
  width: 40vw;
  border: none;
  box-shadow: none;
  padding: 0.5rem;
  border-radius: 5px;
}

/* Customer display cards */
.customerDisplay {
  width: 100%;
  padding-right: 2%;
  flex-wrap: wrap;
  justify-content: flex-start;
  column-gap: 2%;
  overflow-y: auto;
  height: calc(100% - 20vh);
}

/* Individual cards */
.mb-3 {
  border: solid 1px rgb(222, 222, 222);
  border-radius: 10px;
  width: 23.5%;
  height: 50vh;
  margin-bottom: 2%;
  overflow: hidden;
  display: flex;
  flex-direction: column;
}

/* Card header */
.customCardHeader {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 1rem;
  margin-top: 1rem;
  gap: 0.5rem;
}

.customCardHeader p {
  flex-grow: 1;
  margin: 0;
  font-size: 1.2rem;
  font-weight: bold;
  overflow-wrap: anywhere;
}

/* Edit/Delete icons */
.editButton {
  font-size: 1.1rem;
  cursor: pointer;
  padding: 0.25rem;
  transition: transform 0.15s ease, color 0.15s ease;
  flex-shrink: 0;
}

.editButton:hover {
  transform: scale(1.25);
  color: var(--primary);
}

/* Blur modal overlay */
.customerWindowBlur {
  position: fixed;
  backdrop-filter: brightness(60%) blur(2px);
  height: 100vh;
  width: 100vw;
  top: 0;
  left: 0;
  justify-content: center;
  align-items: center;
  display: flex;
}

/* Responsive adjustments */
@media (max-width: 1200px) {
  .mb-3 {
    width: 48%;
  }
     .customerSkeleton {
    width: 48%;
  }
}

@media (max-width: 768px) {
  .mb-3 {
    width: 100%;
  }

  .customerSkeleton {
    width: 100%;
  }

  .rightPanel {
    justify-content: center;
    margin-top: 1rem;
  }

  .addButton {
    width: 100%;
  }
}

</style>
