import type { AddressModel, JobModel, EmailModel } from "@/client/client"
import type { Ref } from "vue"

export interface JobStore {
    jobs: Ref<JobModel[]>
    jobsLoading: Ref<boolean>
    jobsError: Ref<string | null>
    fetchJobWithDetails: () => Promise<void>
    createJob: (c: JobModel) => Promise<JobModel | undefined>,
    checkPastDueJobs: () => void,
    updateJob: (c: JobModel) => Promise<void>
    deleteJob: (id: number) => Promise<void>
}

export interface AddressesStore {
    addressesLoading: Ref<boolean>
    addressesError: Ref<string | null>
    //fetchAddresses: () => Promise<void>
    formatAddress: (addr: AddressModel | undefined) => string
    createAddress: (a: AddressModel) => Promise<void>
    updateAddress: (a: AddressModel) => Promise<void>
    deleteAddress: (id: number) => Promise<void>
}
