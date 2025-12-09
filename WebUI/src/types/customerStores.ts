import type { AddressModel, CustomerModel, EmailModel, PhoneModel } from "@/client/client"
import type { Ref } from "vue"

export interface CustomersStore {
    customers: Ref<CustomerModel[]>
    customersLoading: Ref<boolean>
    customersError: Ref<string | null>
    fetchCustomersWithDetails: () => Promise<void>
    createCustomer: (c: CustomerModel) => Promise<CustomerModel | undefined>
    updateCustomer: (c: CustomerModel) => Promise<void>
    deleteCustomer: (id: number) => Promise<void>
}

export interface AddressesStore {
    addressesLoading: Ref<boolean>
    addressesError: Ref<string | null>
    formatAddress: (addr: AddressModel | undefined) => string
    createAddress: (a: AddressModel) => Promise<void>
    updateAddress: (a: AddressModel) => Promise<void>
    deleteAddress: (id: number) => Promise<void>
}

export interface EmailsStore {
    emailsLoading: Ref<boolean>
    emailsError: Ref<string | null>
    formatEmail: (addr: EmailModel | undefined) => string
    createEmail: (e: EmailModel) => Promise<void>
    updateEmail: (e: EmailModel) => Promise<void>
    deleteEmail: (id: number) => Promise<void>
}

export interface PhonesStore {
    phonesLoading: Ref<boolean>
    phonesError: Ref<string | null>
    formatPhone: (addr: PhoneModel | undefined) => string
    createPhone: (e: PhoneModel) => Promise<void>
    updatePhone: (e: PhoneModel) => Promise<void>
    deletePhone: (id: number) => Promise<void>
}

