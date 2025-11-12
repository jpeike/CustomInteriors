<template>
  <div>
    <AddressListModal
      :isOpen="showAddressList"
      :customerId="selectedCustomerId"
      :addresses="state.modalAddresses"
      @close="showAddressList = false"
      @create="openCreateAddressModal"
      @edit="openUpdateAddressModal"
      @delete="deleteAddress"
    />

    <CreateAddressModal
      :isOpen="showCreateAddressModal"
      :customerId="selectedCustomerId"
      @close="closeCreateModal"
      @created="createAddress"
    />

    <UpdateAddressModal
      :isOpen="showUpdateAddressModal"
      :customerId="selectedCustomerId"
      :address="selectedAddress"
      @close="closeUpdateModal"
      @updated="updateAddress"
    />
  </div>

  <div class="flex column customerBody">
    <div class="flex row pageHeader">
        <div class = "flex column leftPanel">
            <h2 style="margin: 0;">Customers</h2>
            <p style="margin: 0;">Manage your customer information and contact details.</p>
        </div>
        <div class = "rightPanel">
            <button class = "addButton" @click="currentEmailAddresses = undefined; currentCustomerAddresses = undefined; displayCustomerDetails = !displayCustomerDetails; currentCustomerIndex = -1; customerTitle = 'Create Customer'; customerDescription = 'Create customer information and contact details'; customerButtonDesc = 'Create'; console.log(state.customer)">
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
              <i class="pi pi-pen-to-square editButton" style="font-size: 1.1rem" @click="editCustomerUI(customer)"></i>
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
              {{getEmailString(customer.customerId!)}}
            </div>
            <div v-else-if="customer.prefferedContactMethod == 'Phone'">
              Temp Phone
            </div>
            <div v-else>
              Not Listed
            </div>
          </div>

          <br/>
          <p style="margin: 0;"> Address: {{getAddressString(customer.customerId!)}}</p>
          <br/>
          <p style="margin: 0;">Type: {{ customer.customerType }}</p>
          <br/>
          <p style="margin: 0;">Company: {{ customer.companyName }}</p>
          <br/>
          <p style="margin: 0;">Status: {{ customer.status }}</p>
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
    <CustomerInformation :currentCustomerInformation="state.customer[currentCustomerIndex]" :currentEmails="currentEmailAddresses" :currentAddresses = "currentCustomerAddresses" :title="customerTitle" :description="customerDescription" :buttonDesctipnion="customerButtonDesc" @closePage="displayCustomerDetails = !displayCustomerDetails" @updateCustomerInformation="updateCustomerInformation" @openAddressListModal="openAddressListModal"></CustomerInformation>
  </div>

  <div v-if="deleteConfirmation" class="flex row customerWindowBlur">
    <deleteConfirmation :currentCustomerInformation="state.customer[currentCustomerIndex]" :title="(state.customer[currentCustomerIndex].firstName + ' ' + state.customer[currentCustomerIndex].lastName)" @closePage="deleteConfirmation = !deleteConfirmation" @deleteCustomer="deleteCustomer"></deleteConfirmation>
  </div>
</template>

<script setup lang="ts">

import CustomerInformation from '../components/CustomerInformation.vue';
import DeleteConfirmation from '../components/DeleteConfirmation.vue';
import Card from 'primevue/card'
import { Client, CustomerModel, AddressModel, EmailModel} from '../client/client'
import { ref } from 'vue'
import { onMounted, reactive } from 'vue'
import AddressListModal from '../components/modals/AddressListModal.vue'
import CreateAddressModal from '../components/modals/CreateAddressModal.vue'
import UpdateAddressModal from '../components/modals/UpdateAddressModal.vue'
import InputText from 'primevue/inputtext';
import 'primeicons/primeicons.css'
import { useToast } from '@/composables/useToast.ts'

const { showSuccess, showError } = useToast()

const client = new Client(import.meta.env.VITE_API_BASE_URL)
const showAddressList = ref(false)
const showCreateAddressModal = ref(false)
const showUpdateAddressModal = ref(false)
let selectedCustomerId = ref<number | null>(null)
const selectedAddress = ref<AddressModel | null>(null)

let displayCustomerDetails = ref(false);
let deleteConfirmation = ref(false);

let customerTitle = ref('');
let customerDescription = ref('');
let customerButtonDesc = ref('');
let currentCustomerIndex = ref(0);
let searchValue = ref('');

let currentEmailAddresses = ref<EmailModel[] | undefined>(undefined)
let currentCustomerAddresses = ref<AddressModel[] | undefined>(undefined)

const state = reactive({
  customer: [] as CustomerModel[],
  addresses: [] as AddressModel[],
  modalAddresses: [] as AddressModel[],
  emails: [] as EmailModel[],
  loading: false,
  error: null as string | null,
})

onMounted(() => {
  console.log('AboutView mounted')
  fetchCustomers()
  fetchAddresses()
  fetchEmails()
})

function getCustomerIndex(customerID: number){
  for (let i = 0; i < state.customer.length; i++){
    if (state.customer[i].customerId == customerID){
      currentCustomerIndex = ref(i);
    }
  }
}

function getAddressString(customerID: number){
  for (let i = 0; i < state.addresses.length; i++){
    if (state.addresses[i].customerId == customerID){
      return state.addresses[i].street + " " + state.addresses[i].city + ", " + state.addresses[i].state;
    }
  }
}

function getEmailString(customerID: number){
  for (let i = 0; i < state.emails.length; i++){
    if (state.emails[i].customerId == customerID){
      return state.emails[i].emailAddress;
    }
  }
}

async function fetchCustomers() {
  try {
    await client.getAllCustomers()
    .then((response) => {
      state.customer = response
    })
  } catch (error) {
    console.error('Create failed:', error)
  }
}

function fetchAddresses() {
  state.loading = true
  state.error = null
  client
    .getAllAddresses()
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

function fetchEmails() {
  state.loading = true
  state.error = null
  client
    .getAllEmails()
    .then((response) => {
      console.log(response);
      state.emails = response
    })
    .catch((error) => {
      state.error = error.message || 'An error occurred'
      return null;
    })
    .finally(() => {
      state.loading = false
    })
}

async function fetchAddressesByCustomerId(customerId: number) {
  state.loading = true
  state.error = null
  await client
    .getAddressesByCustomerId(customerId)
    .then((response) => {
      currentCustomerAddresses = ref(response);
    })
    .catch((error) => {
      state.error = error.message || 'An error occurred'
      return null;
    })
    .finally(() => {
      state.loading = false
    })
}

function fetchEmailsByCustomerId(customerId: number){
  state.loading = true
  state.error = null

  let userEmails = new Array();
  for (let i = 0; i < state.emails.length; i++){
    console.log(state.emails[i])

    if(state.emails[i].customerId == customerId){
      userEmails.push(state.emails[i]);
    }
  }
  currentEmailAddresses = ref(userEmails);
  state.loading = false
}

async function editCustomerUI(customer: CustomerModel){
    selectedCustomerId.value = customer.customerId!;

    getCustomerIndex(selectedCustomerId.value);
    await fetchAddressesByCustomerId(customer.customerId!)
    fetchEmailsByCustomerId(customer.customerId!);

    state.loading = true;
    displayCustomerDetails = ref(true);
    state.loading = false;

    customerTitle = ref('Update Customer');
    customerDescription = ref('Update customer information and contact details');
    customerButtonDesc = ref('Update');
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

function deleteAddress(addressId: number){
    state.loading = true
    state.error = null
    client
    .deleteAddress(addressId)
    .then(() => {
      showSuccess('Address Deleted Successfully');
    })
    .catch((error) => {
      showError(error);
      state.error = error.message || 'An error occurred'
    })
    .finally(() => {
      fetchAddressesByCustomerId(selectedCustomerId.value!)
      state.loading = false
    })
}

function createAddress(address: AddressModel){
    state.loading = true
    state.error = null
    client
    .createAddress(address)
    .then(() => {
      showSuccess('Address Created Successfully');
    })
    .catch((error) => {
      showError(error);
      state.error = error.message || 'An error occurred'
    })
    .finally(() => {
      fetchAddressesByCustomerId(selectedCustomerId.value!)
      state.loading = false
    })
}

function updateAddress(address: AddressModel){
    state.loading = true
    state.error = null
    client
    .updateAddress(address)
    .then(() => {
      showSuccess('Address Updated Successfully');
    })
    .catch((error) => {
      showError(error);
      state.error = error.message || 'An error occurred'
    })
    .finally(() => {
      fetchAddressesByCustomerId(selectedCustomerId.value!)
      state.loading = false
    })
}

//emails
async function deleteEmail(emailID: number) {
    state.loading = true
    state.error = null
    await client
    .deleteEmail(emailID)
    .then(() => {
        showSuccess('Email Deleted Successfully');
    })
    .catch((error) => {
      showError(error);
      state.error = error.message || 'An error occurred'
    })
    .finally(() => {
      fetchEmailsByCustomerId(selectedCustomerId.value!)
      state.loading = false
    })
}

async function createEmail(email: EmailModel) {
  state.loading = true
  state.error = null
  await client
  .createEmail(email)
  .then(() => {
        showSuccess('Email Created Successfully');
    })
    .catch((error) => {
      showError(error);
      state.error = error.message || 'An error occurred'
    })
    .finally(() => {
      fetchEmailsByCustomerId(selectedCustomerId.value!)
      state.loading = false
    })
}

async function updateEmail(email: EmailModel) {
  state.loading = true
  state.error = null
  await client
  .updateEmail(email)
  .then(() => {
      showSuccess('Email Updated Successfully');
  })
  .catch((error) => {
    showError(error);
    state.error = error.message || 'An error occurred'
  })
  .finally(() => {
    fetchEmailsByCustomerId(selectedCustomerId.value!)
    state.loading = false
  })
}

function updateCustomerInformation(currentID: number | undefined, newCustomer: CustomerModel, newAddress: AddressModel[], removedAddresses: number[], newEmails: EmailModel[], removedEmails: number[]){
  //create new customer
  if(currentID == null){
    createCustomer(newCustomer, newAddress, newEmails);
  }
  //update existing customer
  else{
      updateCustomer(currentID, newCustomer, newAddress, newEmails);
  }

  if (removedAddresses.length > 1){
    for (let i = 1; i < removedAddresses.length; i++){
      deleteAddress(removedAddresses[i]!);
    }
  }

  if (removedEmails.length > 1){
    for (let i = 1; i < removedEmails.length; i++){
      deleteEmail(removedEmails[i]!);
    }
  }

  fetchCustomers();
  fetchAddresses();
  displayCustomerDetails = ref(false);
}

async function createCustomer(newCustomer: CustomerModel, newAddress: AddressModel[], newEmails: EmailModel[]){
    newCustomer.customerId = 0;
    if (newCustomer.firstName != undefined && newCustomer.lastName != undefined && newCustomer.customerType != undefined){
      try {
        await client
        .createCustomer(newCustomer)
        .then(() => {
          showSuccess('Customer Created Successfully');
        })
        .catch((error) => {
          showError(error);
          state.error = error.message || 'An error occurred'
        })
        .finally(() => {
          fetchCustomers();
          state.loading = false
        })

        await fetchCustomers();

        for (let i = 0; i < newAddress.length; i++){
          newAddress[i].customerId = state.customer[state.customer.length - 1].customerId;
          await createAddress(newAddress[i]);
        }

        for (let i = 0; i < newEmails.length; i++){
          newEmails[i].customerId = state.customer[state.customer.length - 1].customerId;
          await createEmail(newEmails[i]);
          fetchEmails();

        }
      }
      catch (error) {
        console.error('Update failed:', error)
      }
    }
}

async function updateCustomer(currentID: number, newCustomer: CustomerModel, newAddress: AddressModel[], newEmails: EmailModel[]){
    state.customer[currentCustomerIndex.value - 1] = newCustomer;
    state.customer[currentCustomerIndex.value - 1].customerId = currentID;
    state.loading = true;

    try {
      if (newCustomer.firstName != undefined && newCustomer.lastName != undefined && newCustomer.customerType != undefined){
        await client
        .updateCustomer(state.customer[currentCustomerIndex.value - 1])
         .then(() => {
          showSuccess('Customer Updated Successfully');
        })
        .catch((error) => {
          showError(error)
          state.error = error.message || 'An error occurred'
        })
        .finally(() => {
          fetchCustomers();
          state.loading = false
        })
      }

      for (let i = 0; i < newAddress.length; i++){
        if (newAddress[i].city != undefined && newAddress[i].state != undefined && newAddress[i].postalCode != undefined && newAddress[i].addressType != undefined){
          if (newAddress[i].addressId! == undefined){
            newAddress[i].customerId = state.customer[state.customer.length - 1].customerId;
            await createAddress(newAddress[i]);
          }
          else{
            await updateAddress(newAddress[i]);
          }
        }
      }
      for (let i = 0; i < newEmails.length; i++){
        if (newEmails[i].emailID! == undefined){
          newEmails[i].customerId = state.customer[state.customer.length - 1].customerId;
          await createEmail(newEmails[i]);

        }
        else{
          await updateEmail(newEmails[i])
        }
        fetchEmails();

      }
    }
    catch (error) {
      console.error('Update failed:', error)
    }

    state.loading = false;
    state.error = null
}

function deleteCustomer(currentID: number){
    console.log(currentID);
    state.loading = true
    state.error = null
    client
    .deleteCustomer(currentID)
    .then(() => {
      showSuccess('Customer Deleted Successfully');
    })
    .catch((error) => {
      showError(error);
      state.error = error.message || 'An error occurred'
    })
    .finally(() => {
      fetchCustomers();
      fetchAddresses();
      fetchEmails();
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

.pageHeader {
  height: 15vh;
  width: 100%;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1rem;
}

.leftPanel {
  flex-grow: 5;
}

.rightPanel {
  flex-grow: 1;
  display: flex;
  justify-content: flex-end;
  align-items: center;
}

/* Add Customers Button */
.addButton {
  border: none;
  height: 50%;
  width: 150px;
  border-radius: 7px;
  padding: 0.5rem 1rem;
  margin-left: 1rem;
  cursor: pointer;

  /* Theme colors */
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
  display: flex;
  align-items: center;
  padding: 0.5rem;
  border: solid 1px rgb(222, 222, 222);
  border-radius: 10px;
  margin-bottom: 1rem;
}

.searchBar {
  width: 100%;
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
}

@media (max-width: 768px) {
  .mb-3 {
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
