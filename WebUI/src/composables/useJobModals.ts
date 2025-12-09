import { ref } from 'vue'
import { type JobModel, type AddressModel, type EmailModel } from '@/client/client'
import { Client as client} from '@/client/apiClient'
import type { JobStore, AddressesStore } from '@/types/jobStore'

export function useJobModals({jobStore}: {
  jobStore: JobStore
}) {

  const currentJob = ref<JobModel | undefined>(undefined)
  const currentJobIndex = ref(0)
  const displayJobDetails = ref<boolean | undefined>(undefined)
  const jobModalLoading = ref(false)
  const selectedJobId = ref<number | null>(null)
  const deleteConfirmation = ref(false);

  const jobTitle = ref('')
  const jobDescription = ref('')
  const jobButtonDesc = ref('')

  const { jobs, jobsLoading, jobsError } = jobStore

  function closePage() {
    displayJobDetails.value = false;
    currentJob.value = undefined;
  }

  function createJobDisplay() {
    currentJobIndex.value = -1;
    displayJobDetails.value = true;

    jobTitle.value = 'Create a Job';
    jobButtonDesc.value = 'Create';
  }

  async function editJobUI(job: JobModel) {
    selectedJobId.value = job.jobId!;

    const selected = jobStore.jobs.value.find(c => c.jobId === selectedJobId.value)

    currentJob.value = selected

    jobModalLoading.value = true;
    displayJobDetails.value = true;
    jobModalLoading.value = false;

    jobTitle.value = 'Update a Job';
    jobButtonDesc.value = 'Update';
  }

  function getJobIndex(jobID: number) {
    for (let i = 0; i < jobs.value.length; i++) {
      if (jobs.value[i].jobId == jobID) {
        currentJobIndex.value = i;
      }
    }
  }

  function closeDeleteModal() {
    deleteConfirmation.value = false
  }

  function openDeleteModal() {
    deleteConfirmation.value = true
  }

  return {
    currentJob,
    currentJobIndex,
    displayJobDetails,
    deleteConfirmation,
    jobModalLoading,
    jobTitle,
    jobDescription,
    jobButtonDesc,
    selectedJobId,

    closePage,
    createJobDisplay,
    editJobUI,
    getJobIndex,
    closeDeleteModal,
    openDeleteModal,
  }
}
