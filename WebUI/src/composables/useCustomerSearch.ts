import { ref, computed, type Ref, } from 'vue'
import type { CustomerModel } from '@/client/client'

export function useCustomerSearch(customers: Ref<CustomerModel[]>) {
    const searchValue = ref('')

    const filteredCustomers = computed(() => {
        return customers.value.filter((customers) =>
            customers.firstName?.toLowerCase().includes(searchValue.value.toLowerCase())
            ||
            customers.lastName?.toLowerCase().includes(searchValue.value.toLowerCase())
            ||
            customers.prefferedContactMethod?.toLowerCase().includes(searchValue.value.toLowerCase())
        );
    });

    return {
        searchValue,
        filteredCustomers
    }
}
