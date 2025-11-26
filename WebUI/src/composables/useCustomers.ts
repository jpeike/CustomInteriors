import { ref } from 'vue'
import { CustomerModel, AddressModel, EmailModel, Client } from '@/client/client'
import { useToast } from '@/composables/useToast.ts'
import type { CustomersStore } from '@/types/customerStores'

export function useCustomers() {
    const customersLoading = ref(false)
    const customersError = ref<string | null>(null)
    const customers = ref<CustomerModel[]>([])
    const client = new Client(import.meta.env.VITE_API_BASE_URL)
    const { showSuccess, showError} = useToast()

    async function fetchCustomersWithDetails() {
        customersLoading.value = true
        customersError.value = null
        client
            .getAllCustomers(true)
            .then((response) => {
                customers.value = response
            })
            .catch((customersError) => {
                customersError.value = customersError.message || 'An customersError occurred'
            })
            .finally(() => {
                customersLoading.value = false
            })
    }

    async function createCustomer(newCustomer: CustomerModel): Promise<CustomerModel | undefined> {
        newCustomer.customerId = 0;
        customersLoading.value = true
        customersError.value = null
        if (newCustomer.firstName != undefined && newCustomer.lastName != undefined && newCustomer.customerType != undefined) {
            try {
                const created: CustomerModel = await client.createCustomer(newCustomer)
                showSuccess("Customer Created Successfully")
                return created
            } catch (err: any) {
                customersError.value = err.message
                showError(err.message)
                return undefined
            } finally {
                customersLoading.value = false
            }
        }
        // ensure function always returns CustomerModel|null and that loading is reset
        customersLoading.value = false
        customersError.value = 'Missing required customer fields'
        return undefined
    }

    async function updateCustomer(newCustomer: CustomerModel) {
        customersLoading.value = true;

        try {
            if (newCustomer.customerId != -1 && newCustomer.firstName != undefined && newCustomer.lastName != undefined && newCustomer.customerType != undefined) {
                await client
                    .updateCustomer(newCustomer)
                    .then(() => {
                        showSuccess('Customer Updated Successfully');
                    })
                    .catch((customersError) => {
                        showError(customersError)
                        customersError.value = customersError.message || 'An customersError occurred'
                    })
                    .finally(() => {
                        customersLoading.value = false
                    })
            }

        }
        catch (customersError) {
            console.error('Update failed:', customersError)
        }

        customersLoading.value = false;
        customersError.value = null
    }

    async function deleteCustomer(customerId: number) {
        customersLoading.value = true
        customersError.value = null
        await client
            .deleteCustomer(customerId)
            .then(() => {
                showSuccess('Customer Deleted Successfully');
            })
            .catch((customersError) => {
                showError(customersError);
                customersError.value = customersError.message || 'An customersError occurred'
            })
            .finally(() => {
                customersLoading.value = false
            })
    }

    return {
        customers,
        customersLoading,
        customersError,
        fetchCustomersWithDetails,
        createCustomer,
        updateCustomer,
        deleteCustomer
    } satisfies CustomersStore
}
