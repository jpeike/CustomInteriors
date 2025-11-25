import { useToast } from './useToast'
import { CustomerModel, AddressModel, EmailModel, Client } from '@/client/client'
import type { CustomersStore, AddressesStore, EmailsStore } from '@/types/customerStores'
import type { useCustomerModals } from './useCustomerModals'

export function useCustomerEditFlow({ customersStore, addressesStore, emailsStore, customerModalsStore }: {
  customersStore: CustomersStore,
  addressesStore: AddressesStore,
  emailsStore: EmailsStore,
  customerModalsStore: ReturnType<typeof useCustomerModals>
}) {
  const { customers, fetchCustomers, createCustomer, updateCustomer } = customersStore
  const { fetchAddresses, createAddress, updateAddress, deleteAddress } = addressesStore
  const { fetchEmails, createEmail, updateEmail, deleteEmail } = emailsStore
  const { showInfo } = useToast()
  const { closePage, selectedCustomerId } = customerModalsStore

  async function updateCustomerInformation(newCustomer: CustomerModel, newAddress: AddressModel[], removedAddresses: number[], newEmails: EmailModel[], removedEmails: number[]) {
    if (newCustomer.customerId == -1 && newAddress.length == 0 && removedAddresses.length == 1 && newEmails.length == 0 && removedEmails.length == 1) {
      showInfo("No changes made");
    }

    else {
      //create new customer
      if (newCustomer.customerId == null) {
        const createdCustomer = await createCustomer(newCustomer);

        if (!createdCustomer) return

        const newCustomerId = createdCustomer.customerId

        for (let i = 0; i < newAddress.length; i++) {
          newAddress[i].customerId = newCustomerId;
          console.log(newCustomerId);
          await createAddress(newAddress[i]);
        }

        console.log(newEmails.length);
        for (let i = 0; i < newEmails.length; i++) {
          newEmails[i].customerId = newCustomerId;
          console.log(newCustomerId);
          await createEmail(newEmails[i]);
        }
      }
      //update existing customer
      else {
        updateCustomer(newCustomer);

        for (let i = 0; i < newAddress.length; i++) {
          if (newAddress[i].city != undefined && newAddress[i].state != undefined && newAddress[i].postalCode != undefined && newAddress[i].addressType != undefined) {
            if (newAddress[i].addressId! == undefined) {
              newAddress[i].customerId = selectedCustomerId.value!;
              await createAddress(newAddress[i]);
            }
            else {
              await updateAddress(newAddress[i]);
            }
          }
        }

        for (let i = 0; i < newEmails.length; i++) {
          if (newEmails[i].emailId! == undefined) {
            newEmails[i].customerId = selectedCustomerId.value!;
            await createEmail(newEmails[i]);

          }
          else {
            await updateEmail(newEmails[i])
          }
        }
      
    }

    if (removedAddresses.length > 1) {
      for (let i = 1; i < removedAddresses.length; i++) {
        deleteAddress(removedAddresses[i]!);
      }
    }

    if (removedEmails.length > 1) {
      for (let i = 1; i < removedEmails.length; i++) {
        deleteEmail(removedEmails[i]!);
      }
    }
  }
  closePage();
}

async function handleDelete(customerId: number) {
  await customersStore.deleteCustomer(customerId)
  await customersStore.fetchCustomers()
  await addressesStore.fetchAddresses()
  await emailsStore.fetchEmails()

  customerModalsStore.closeDeleteModal()
}

return {
  updateCustomerInformation,
  handleDelete
}
}
