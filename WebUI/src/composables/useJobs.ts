import { ref } from 'vue'
import { JobModel, CustomerUpdateModel, CustomerCreateModel, AddressModel, Client } from '@/client/client'
import { useToast } from '@/composables/useToast.ts'
import type { JobStore } from '@/types/jobStore'

export function useJob() {
    const jobsLoading = ref(false)
    const jobsError = ref<string | null>(null)
    const jobs = ref<JobModel[]>([])
    const client = new Client(import.meta.env.VITE_API_BASE_URL)
    const { showSuccess, showError, showInfo } = useToast()

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
            checkPastDueJobs()
        })
    }

    async function createJob(newJob: JobModel): Promise<JobModel | undefined> {
        jobsLoading.value = true
        jobsError.value = null
        if (newJob.jobDescription) {
            try {
                const created: JobModel = await client.createJob(newJob)
                showSuccess("Job Created Successfully")
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
        jobsError.value = 'Missing required job fields'
        return undefined
    }

    async function updateJob(newJob: JobModel) {
        jobsLoading.value = true;

        try {
            if (newJob.jobDescription) {
                await client
                    .updateJob(newJob)
                    .then(() => {
                        if (jobsError.value == "Change"){
                            showInfo("Job " + newJob.jobDescription + " is past due. The status has been changed to Past Due")
                            jobsError.value = null
                        }
                        else{
                            showSuccess('Job Updated Successfully');
                        }
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

    function checkPastDueJobs(){
        const pastDueDate = new Date();
        pastDueDate.setDate(pastDueDate.getDate() - 1)

        for (let i = 0; i < jobs.value.length; i++){
            if (Date.parse(jobs.value[i].endDate?.toLocaleDateString()!) < Date.parse(pastDueDate.toLocaleDateString()) && jobs.value[i].status == "In Progress"){
                jobsError.value = "Change"
                jobs.value[i].status = "Past Due"
                updateJob(jobs.value[i]);
            }
        }
    }

    return {
        jobs,
        jobsLoading,
        jobsError,
        fetchJobWithDetails,
        checkPastDueJobs,
        createJob,
        updateJob,
        deleteJob
    } satisfies JobStore
}
