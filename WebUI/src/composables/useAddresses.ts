import { ref } from 'vue'
import { AddressModel } from '@/client/client'
import { Client as client} from '@/client/apiClient'
import { useToast } from '@/composables/useToast.ts'
import type { AddressesStore } from '@/types/customerStores'

export function useAddresses() {
    const addressesLoading = ref(false)
    const addressesError = ref<string | null>(null)


    const { showSuccess, showError, showInfo, showWarning } = useToast()

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
            })
    }

    function formatAddress(addr: AddressModel | undefined): string {
        if (!addr) return ''

        const parts = [
            addr.street,
            addr.city + ',',
            addr.state + ',',
            addr.postalCode,
            addr.country
        ].filter(Boolean)

        return parts.join(' ')
    }

    return {
        addressesLoading,
        addressesError,
        formatAddress,
        createAddress,
        updateAddress,
        deleteAddress,
    } satisfies AddressesStore
}
