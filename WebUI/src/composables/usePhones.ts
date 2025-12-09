import { ref } from 'vue'
import { PhoneModel, Client } from '@/client/client'
import { useToast } from '@/composables/useToast.ts'
import type { PhonesStore } from '@/types/customerStores'

export function usePhones() {
    const phonesLoading = ref(false)
    const phonesError = ref<string | null>(null)

    const client = new Client(import.meta.env.VITE_API_BASE_URL)

    const { showSuccess, showError, showInfo, showWarning } = useToast()

    async function createPhone(phone: PhoneModel) {
        phonesLoading.value = true
        phonesError.value = null
        await client
            .createPhone(phone)
            .then(() => {
                showSuccess('Phone Created Successfully');
            })
            .catch((phonesError) => {
                showError(phonesError);
                phonesError.value = phonesError.message || 'A Phone Error occurred'
            })
            .finally(() => {
                phonesLoading.value = false
            })
    }

    async function updatePhone(phone: PhoneModel) {
        phonesLoading.value = true
        phonesError.value = null
        await client
            .updatePhone(phone)
            .then(() => {
                showSuccess('Phone Updated Successfully');
            })
            .catch((phoneError) => {
                showError(phoneError);
                phoneError.value = phoneError.message || 'A Phone Error occurred'
            })
            .finally(() => {
                phonesLoading.value = false
            })
    }

    async function deletePhone(phoneId: number) {
        phonesLoading.value = true
        phonesError.value = null
        await client
            .deletePhone(phoneId)
            .then(() => {
                showSuccess('Phone Deleted Successfully');
            })
            .catch((phonesError) => {
                showError(phonesError);
                phonesError.value = phonesError.message || 'A Phone Error occurred'
            })
            .finally(() => {
                phonesLoading.value = false
            })
    }

    function formatPhone(phone: PhoneModel | undefined): string {
        if (!phone) return '';

        const parts = [
            phone.phoneNumber
        ].filter(Boolean);

        return parts.join(' ');
    }

    return {
        phonesLoading,
        phonesError,
        formatPhone,
        createPhone,
        updatePhone,
        deletePhone,
    } satisfies PhonesStore
}
