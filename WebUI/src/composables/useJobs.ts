import { ref } from 'vue'
import { JobModel, CustomerUpdateModel, CustomerCreateModel, AddressModel, Client } from '@/client/client'
import { useToast } from '@/composables/useToast.ts'
import type { JobStore } from '@/types/jobStore'

export function useJob() {
    const jobsLoading = ref(false)
    const jobsError = ref<string | null>(null)
    const jobs = ref<JobModel[]>([])
    const client = new Client(import.meta.env.VITE_API_BASE_URL)
    const { showSuccess, showError } = useToast()

    async function fetchJobWithDetails() {
        jobsLoading.value = true
        jobsError.value = null
        client
        .getAllJobs()
        .then((response) => {
            jobs.value = response
        })
        .catch((jobsError) => {
            jobsError.value = jobsError.message || 'A jobsError occurred'
        })
        .finally(() => {
            jobsLoading.value = false
        })
    }

    async function createJob(newJob: JobModel): Promise<JobModel | undefined> {
        jobsLoading.value = true
        jobsError.value = null
        if (newJob.jobDescription) {
            try {
                const created: JobModel = await client.createJob(newJob)
                showSuccess("Customer Created Successfully")
                return created
            } catch (err: any) {
                jobsError.value = err.message
                showError(err.message)
                return undefined
            } finally {
                jobsLoading.value = false
            }
        }
        // ensure function always returns CustomerModel|null and that loading is reset
        jobsLoading.value = false
        jobsError.value = 'Missing required customer fields'
        return undefined
    }

    async function updateJob(newJob: JobModel) {
        jobsLoading.value = true;
        jobsError.value = null

        try {
            if (newJob.jobDescription) {
                await client
                    .updateJob(newJob)
                    .then(() => {
                        showSuccess('Customer Updated Successfully');
                    })
                    .catch((jobsError) => {
                        showError(jobsError)
                        jobsError.value = jobsError.message || 'A jobsError occurred'
                    })
                    .finally(() => {
                        jobsLoading.value = false
                    })
            }

        }
        catch (jobsError) {
            console.error('Update failed:', jobsError)
        }

        jobsLoading.value = false;
        jobsError.value = null
    }

    async function deleteJob(jobId: number) {
        jobsLoading.value = true
        jobsError.value = null
        await client
            .deleteJob(jobId)
            .then(() => {
                showSuccess('Job Deleted Successfully');
            })
            .catch((jobsError) => {
                showError(jobsError);
                jobsError.value = jobsError.message || 'A jobsError occurred'
            })
            .finally(() => {
                jobsLoading.value = false
            })
    }

    return {
        jobs,
        jobsLoading,
        jobsError,
        fetchJobWithDetails,
        createJob,
        updateJob,
        deleteJob
    } satisfies JobStore
}
