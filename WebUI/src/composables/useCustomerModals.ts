import { ref } from 'vue'
import { type CustomerModel, type AddressModel, type EmailModel, Client } from '@/client/client'
import type { CustomersStore, AddressesStore, EmailsStore } from '@/types/customerStores'

export function useCustomerModals({addressesStore, emailsStore, customersStore}: {
  customersStore: CustomersStore
  addressesStore: AddressesStore
  emailsStore: EmailsStore
}) {

  const currentCustomer = ref<CustomerModel | undefined>(undefined)
  const currentCustomerAddresses = ref<AddressModel[] | undefined>(undefined)
  const currentEmailAddresses = ref<EmailModel[] | undefined>(undefined)
  const currentCustomerIndex = ref(0)
  const displayCustomerDetails = ref<boolean | undefined>(undefined)
  const customerModalLoading = ref(false)
  const selectedCustomerId = ref<number | null>(null)
  const deleteConfirmation = ref(false);
  const client = new Client(import.meta.env.VITE_API_BASE_URL)

  const customerTitle = ref('')
  const customerDescription = ref('')
  const customerButtonDesc = ref('')

  const { addressesLoading, addressesError } = addressesStore
  const { emails, emailsLoading, emailsError } = emailsStore
  const { customers, customersLoading, customersError } = customersStore

  function closePage() {
    displayCustomerDetails.value = false;
    currentCustomer.value = undefined;
    currentEmailAddresses.value = undefined;
    currentCustomerAddresses.value = undefined;
  }

  function createCustomerDisplay() {
    currentCustomerAddresses.value = undefined;
    currentCustomerIndex.value = -1;
    displayCustomerDetails.value = true;

    customerTitle.value = 'Create Customer';
    customerDescription.value = 'Create customer information and contact details';
    customerButtonDesc.value = 'Create';
  }

  async function editCustomerUI(customer: CustomerModel) {
    console.log(customer.customerId + " " + customer.firstName);
    selectedCustomerId.value = customer.customerId!;

    await getSelectedCustomer(selectedCustomerId.value);
    await fetchAddressesByCustomerId(selectedCustomerId.value)
    await fetchEmailsByCustomerId(selectedCustomerId.value);

    customerModalLoading.value = true;
    displayCustomerDetails.value = true;
    customerModalLoading.value = false;

    customerTitle.value = 'Update Customer';
    customerDescription.value = 'Update customer information and contact details';
    customerButtonDesc.value = 'Update';
  }

  function getCustomerIndex(customerID: number) {
    for (let i = 0; i < customers.value.length; i++) {
      if (customers.value[i].customerId == customerID) {
        currentCustomerIndex.value = i;
      }
    }
  }

  async function getSelectedCustomer(customerId: number) {
    customersLoading.value = true
    customersError.value = null
    return await client
      .getCustomerById(customerId)
      .then((response) => {
        currentCustomer.value = response;
      })
      .catch((error) => {
        customersError.value = error.message || 'An error occurred'
        return null;
      })
      .finally(() => {
        customersLoading.value = false
      })
  }

  async function fetchAddressesByCustomerId(customerId: number) {
    addressesLoading.value = true
    addressesError.value = null
    await client
      .getAddressesByCustomerId(customerId)
      .then((response) => {
        currentCustomerAddresses.value =response;
      })
      .catch((addressesError) => {
        addressesError.value = addressesError.message || 'An addressesError occurred'
        return null;
      })
      .finally(() => {
        addressesLoading.value = false
      })
  }

  //TODO change function to be similar to other fetch functions.
  function fetchEmailsByCustomerId(customerId: number) {
    emailsLoading.value = true
    emailsError.value = null

    let userEmails = new Array();
    for (let i = 0; i < emails.value.length; i++) {

      if (emails.value[i].customerId == customerId) {
        userEmails.push(emails.value[i]);
      }
    }
    emailsLoading.value = false
    currentEmailAddresses.value = userEmails;
  }

  function closeDeleteModal() {
    deleteConfirmation.value = false
  }

  function openDeleteModal() {
    deleteConfirmation.value = true
  }

  return {
    currentCustomer,
    currentCustomerAddresses,
    currentEmailAddresses,
    currentCustomerIndex,
    displayCustomerDetails,
    deleteConfirmation,
    customerModalLoading,
    customerTitle,
    customerDescription,
    customerButtonDesc,
    selectedCustomerId,

    closePage,
    createCustomerDisplay,
    editCustomerUI,
    getCustomerIndex,
    fetchAddressesByCustomerId,
    fetchEmailsByCustomerId,
    closeDeleteModal,
    openDeleteModal,

  }
}
