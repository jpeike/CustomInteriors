import { ref } from 'vue'
import { type CustomerModel, type AddressModel, type EmailModel, type PhoneModel, Client } from '@/client/client'
import type { CustomersStore, AddressesStore, EmailsStore, PhonesStore } from '@/types/customerStores'

export function useCustomerModals({addressesStore, emailsStore, phonesStore, customersStore}: {
  customersStore: CustomersStore
  addressesStore: AddressesStore
  emailsStore: EmailsStore
  phonesStore: PhonesStore
}) {

  const currentCustomer = ref<CustomerModel | undefined>(undefined)
  const currentCustomerAddresses = ref<AddressModel[] | undefined>(undefined)
  const currentEmailAddresses = ref<EmailModel[] | undefined>(undefined)
  const currentPhoneNumbers = ref<PhoneModel[] | undefined>(undefined)
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
  const { emailsLoading, emailsError } = emailsStore
  const { customers, customersLoading, customersError } = customersStore

  function closePage() {
    displayCustomerDetails.value = false;
    currentCustomer.value = undefined;
    currentEmailAddresses.value = undefined;
    currentCustomerAddresses.value = undefined;
    currentPhoneNumbers.value = undefined;
  }

  function createCustomerDisplay() {
    currentCustomerAddresses.value = undefined;
    currentCustomerIndex.value = -1;
    displayCustomerDetails.value = true;

    customerTitle.value = 'Create a Customer';
    customerButtonDesc.value = 'Create';
  }

  async function editCustomerUI(customer: CustomerModel) {
    console.log(customer.customerId + " " + customer.firstName);
    selectedCustomerId.value = customer.customerId!;

    const selected = customersStore.customers.value.find(c => c.customerId === selectedCustomerId.value)

    currentCustomer.value = selected
    currentCustomerAddresses.value = JSON.parse(JSON.stringify(selected!.addresses ?? []))
    currentEmailAddresses.value = JSON.parse(JSON.stringify(selected!.emails ?? []))
    currentPhoneNumbers.value = JSON.parse(JSON.stringify(selected!.phones ?? []))
    console.log(currentPhoneNumbers.value);

    customerModalLoading.value = true;
    displayCustomerDetails.value = true;
    customerModalLoading.value = false;

    customerTitle.value = 'Update a Customer';
    customerButtonDesc.value = 'Update';
  }

  function getCustomerIndex(customerID: number) {
    for (let i = 0; i < customers.value.length; i++) {
      if (customers.value[i].customerId == customerID) {
        currentCustomerIndex.value = i;
      }
    }
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
    currentPhoneNumbers,
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
    closeDeleteModal,
    openDeleteModal,

  }
}
