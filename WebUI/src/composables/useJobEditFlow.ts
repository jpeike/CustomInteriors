import { useToast } from './useToast'
import { JobModel, AddressModel, EmailModel, Client } from '@/client/client'
import type { JobStore, AddressesStore } from '@/types/jobStore'
import type { useJobModals } from './useJobModals'

export function useJobEditFlow({ jobStore: jobStore, jobModalsStore}: {
  jobStore: JobStore,
  jobModalsStore: ReturnType<typeof useJobModals>
}) {
  const { jobs, fetchJobWithDetails, createJob, updateJob } = jobStore
  const { showInfo } = useToast()
  const { closePage, selectedJobId } = jobModalsStore

  async function updateJobInformation(newJob: JobModel) {
    if (newJob.jobId == -1) {
      showInfo("No changes made");
    }

    else {
      //create new job
      if (newJob.jobId == null) {
        const createdJob = await createJob(newJob);
        
        if (!createdJob) return

        const newJobId = createdJob.jobId

        if (newJobId == null) {
          showInfo("Failed to create customer: missing customerId");
          return
        }
      }

      //update existing customer
      else {
        await updateJob(newJob);      
      }
  }
  await fetchJobWithDetails()
  closePage();
}

async function handleDelete(JobId: number) {
  await jobStore.deleteJob(JobId)
  await fetchJobWithDetails()
  
  jobModalsStore.closeDeleteModal()
}

return {
  updateJobInformation,
  handleDelete
}
}
