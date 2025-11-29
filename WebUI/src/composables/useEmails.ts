
import { ref } from 'vue'
import { EmailModel, Client } from '@/client/client'
import { useToast } from '@/composables/useToast.ts'
import type { EmailsStore } from '@/types/customerStores'

export function useEmails() {
    const emailsLoading = ref(false)
    const emailsError = ref<string | null>(null)

    const client = new Client(import.meta.env.VITE_API_BASE_URL)

    const { showSuccess, showError } = useToast()

    async function createEmail(email: EmailModel) {
        emailsLoading.value = true
        emailsError.value = null
        await client
            .createEmail(email)
            .then(() => {
                showSuccess('Email Created Successfully');
            })
            .catch((emailsError) => {
                showError(emailsError);
                emailsError.value = emailsError.message || 'An emailsError occurred'
            })
            .finally(() => {
                emailsLoading.value = false
            })
    }
    async function updateEmail(email: EmailModel) {
        emailsLoading.value = true
        emailsError.value = null
        await client
            .updateEmail(email)
            .then(() => {
                showSuccess('Email Updated Successfully');
            })
            .catch((emailsError) => {
                showError(emailsError);
                emailsError.value = emailsError.message || 'An emailsError occurred'
            })
            .finally(() => {
                emailsLoading.value = false
            })
    }
    async function deleteEmail(emailId: number) {
        emailsLoading.value = true
        emailsError.value = null
        await client
            .deleteEmail(emailId)
            .then(() => {
                showSuccess('Email Deleted Successfully');
            })
            .catch((emailsError) => {
                showError(emailsError);
                emailsError.value = emailsError.message || 'An emailsError occurred'
            })
            .finally(() => {
                emailsLoading.value = false
            })
    }

    function formatEmail(email: EmailModel | undefined): string {
        if (!email) return ''

        return `${email.emailAddress}`
    }

    return {
        emailsLoading,
        emailsError,
        formatEmail,
        createEmail,
        updateEmail,
        deleteEmail,
    } satisfies EmailsStore
}
