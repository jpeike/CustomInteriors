import { ref, computed, type Ref, } from 'vue'
import type { CustomerModel, JobModel } from '@/client/client'

export function useJobSearch(jobs: Ref<JobModel[]>) {
    const searchValue = ref('')

    const filteredJobs = computed(() => {
        return jobs.value.filter((jobs) =>
            jobs.jobDescription?.toLowerCase().includes(searchValue.value.toLowerCase())
            ||
            jobs.status?.toLowerCase().includes(searchValue.value.toLowerCase())
            ||
            jobs.startDate?.toLocaleDateString().includes(searchValue.value.toLowerCase())
        );
    });

    return {
        searchValue,
        filteredJobs
    }
}
