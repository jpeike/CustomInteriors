import { ref } from 'vue'
import { AddressModel, Client } from '@/client/client'
import { useToast } from '@/composables/useToast.ts'
import type { AddressesStore } from '@/types/customerStores'

export function useAddresses() {
    const addressesLoading = ref(false)
    const addressesError = ref<string | null>(null)

    const addresses = ref<AddressModel[]>([])

    const client = new Client(import.meta.env.VITE_API_BASE_URL)

    const { showSuccess, showError, showInfo, showWarning } = useToast()

    async function fetchAddresses() {
        addressesLoading.value = true
        addressesError.value = null
        client
            .getAllAddresses()
            .then((response) => {
                addresses.value = response
            })
            .catch((addressesError) => {
                addressesError.value = addressesError.message || 'An addressesError occurred'
            })
            .finally(() => {
                addressesLoading.value = false
            })
    }

    async function createAddress(address: AddressModel) {
        addressesLoading.value = true
        addressesError.value = null
        await client
            .createAddress(address)
            .then(() => {
                showSuccess('Address Created Successfully');
            })
            .catch((addressesError) => {
                showError(addressesError);
                addressesError.value = addressesError.message || 'An addressesError occurred'
            })
            .finally(() => {
                addressesLoading.value = false
                fetchAddresses();
            })
    }

    async function updateAddress(address: AddressModel) {
        addressesLoading.value = true
        addressesError.value = null
        await client
            .updateAddress(address)
            .then(() => {
                showSuccess('Address Updated Successfully');
            })
            .catch((addressesError) => {
                showError(addressesError);
                addressesError.value = addressesError.message || 'An addressesError occurred'
            })
            .finally(() => {
                addressesLoading.value = false
                fetchAddresses();
            })
    }

    async function deleteAddress(addressId: number) {
        addressesLoading.value = true
        addressesError.value = null
        await client
            .deleteAddress(addressId)
            .then(() => {
                showSuccess('Address Deleted Successfully');
            })
            .catch((addressesError) => {
                showError(addressesError);
                addressesError.value = addressesError.message || 'An addressesError occurred'
            })
            .finally(() => {
                addressesLoading.value = false
                fetchAddresses();
            })
    }

    function getAddressString(customerId: number): string {
        for (let i = 0; i < addresses.value.length; i++) {
            if (addresses.value[i].customerId === customerId) {
                return addresses.value[i].street + " " + addresses.value[i].city + ", " + addresses.value[i].state;
            }
        }
        return '';
    }

    return {
        addresses,
        addressesLoading,
        addressesError,
        fetchAddresses,
        createAddress,
        updateAddress,
        deleteAddress,
        getAddressString
    } satisfies AddressesStore
}
