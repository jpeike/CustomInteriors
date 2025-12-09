import { ref } from 'vue'
import { CustomerModel, CustomerUpdateModel, CustomerCreateModel, AddressModel, EmailModel } from '@/client/client'
import { Client as client } from '@/client/apiClient'
import { useToast } from '@/composables/useToast.ts'
import type { CustomersStore } from '@/types/customerStores'

export function useCustomers() {
    const customersLoading = ref(false)
    const customersError = ref<string | null>(null)
    const customers = ref<CustomerModel[]>([])
    const { showSuccess, showError } = useToast()

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
                const created: CustomerModel = await client.createCustomer(mapToCreateModel(newCustomer))
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
        customersError.value = null

        try {
            if (newCustomer.customerId != -1 && newCustomer.firstName != undefined && newCustomer.lastName != undefined && newCustomer.customerType != undefined) {
                await client
                    .updateCustomer(mapToUpdateModel(newCustomer))
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

    function mapToCreateModel(customer: CustomerModel): CustomerCreateModel {
        const model = new CustomerCreateModel();
        model.firstName = customer.firstName;
        model.lastName = customer.lastName;
        model.customerType = customer.customerType;
        model.prefferedContactMethod = customer.prefferedContactMethod;
        model.companyName = customer.companyName;
        model.status = customer.status;
        model.customerNotes = customer.customerNotes;
        return model;
    }

    function mapToUpdateModel(customer: CustomerModel): CustomerUpdateModel {
        const model = new CustomerUpdateModel();
        console.log("Mapping Customer ID: " + customer.customerId);
        model.customerId = customer.customerId!;
        model.firstName = customer.firstName;
        model.lastName = customer.lastName;
        model.customerType = customer.customerType;
        model.prefferedContactMethod = customer.prefferedContactMethod;
        model.companyName = customer.companyName;
        model.status = customer.status;
        model.customerNotes = customer.customerNotes;
        return model;
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
