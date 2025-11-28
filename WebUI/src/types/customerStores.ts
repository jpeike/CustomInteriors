import type { AddressModel, CustomerModel, EmailModel } from "@/client/client"
import type { Ref } from "vue"

export interface CustomersStore {
    customers: Ref<CustomerModel[]>
    customersLoading: Ref<boolean>
    customersError: Ref<string | null>
    fetchCustomers: () => Promise<void>
    createCustomer: (c: CustomerModel) => Promise<CustomerModel | undefined>
    updateCustomer: (c: CustomerModel) => Promise<void>
    deleteCustomer: (id: number) => Promise<void>
}

export interface AddressesStore {
    addresses: Ref<AddressModel[]>
    addressesLoading: Ref<boolean>
    addressesError: Ref<string | null>
    fetchAddresses: () => Promise<void>
    createAddress: (a: AddressModel) => Promise<void>
    updateAddress: (a: AddressModel) => Promise<void>
    deleteAddress: (id: number) => Promise<void>
    getAddressString: (customerId: number) => string
}

export interface EmailsStore {
    emails: Ref<EmailModel[]>
    emailsLoading: Ref<boolean>
    emailsError: Ref<string | null>
    fetchEmails: () => Promise<void>
    createEmail: (e: EmailModel) => Promise<void>
    updateEmail: (e: EmailModel) => Promise<void>
    deleteEmail: (id: number) => Promise<void>
    getEmailString: (customerId: number) => string
}

