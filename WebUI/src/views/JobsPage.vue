<template>
  <div v-if="isLoading" class="skeletonPage">
  <Skeleton width="12rem" height="2rem" class="mb-3" />

  <div class="flex row pageHeader">
    <Skeleton height="2.8rem" class="flex-grow-1" style="border-radius: 10px;" />
    <Skeleton width="150px" height="2.8rem" style="margin-left: 1rem; border-radius: 10px;" />
  </div>

  <div class="flex row customerDisplay">
    <div v-for="n in 8" :key="n" class="customerSkeleton">
      <Skeleton height="100%" width="100%" borderRadius="10px" />
    </div>
  </div>

</div>
  <div v-else>
    <div class="flex column customerBody">
      <div class="flex row pageHeader">
      <div class="flex column leftPanel">
          <h2 style="margin: 0;">Jobs</h2>
      </div>

      <div class="searchBarWrapper">
          <i class="pi pi-search"></i>
          <InputText type="text" class="searchBar" placeholder="Search" />
      </div>

      <div class="rightPanel">
          <button class="addButton" @click="createJobDisplay()" data-testid="addCustomerButton">
              <p style="margin: 0; text-align: center;">+New Job</p>
          </button>
          <ToggleButton
            onLabel="Active Only"
            offLabel="All Jobs"
            onIcon="pi pi-check"
            offIcon="pi pi-filter"
            class="ml-3"
            />
      </div>
  </div>
      <div v-if="!isLoading" class="flex row customerDisplay">
          <JobsCard
            v-for="job in jobs"
            :key="job.customerId"
            :job="job"
            @edit="editJobUI"
            @delete="openDeleteModal(); getJobIndex(job.jobId ?? 0);"
          />

      </div>
      <div v-else-if="isError">{{ isError }}</div>
      <div v-else>
        <p>Loading...</p>
      </div>
    </div>
    
    
    <div v-if="displayJobDetails" class="flex row customerWindowBlur">
      <JobsInformation
        :currentJobInformation="currentJob"
        :customers="customers"
        :title="jobTitle"
        :description="jobDescription"
        :buttonDesctipnion="jobButtonDesc"
        @closePage="closePage"
        @updateJobInformation="updateJobInformation">
      </JobsInformation>
    </div>

    
    <div v-if="deleteConfirmation" class="flex row customerWindowBlur">
      <deleteConfirmation>
      </deleteConfirmation>
    </div>
    
  </div>
</template>

<script setup lang="ts">

import DeleteConfirmation from '../components/DeleteConfirmation.vue';
import JobsCard from '../components/jobs/JobsCard.vue'
import { JobAssignmentModel} from '../client/client'
import { ref, computed } from 'vue'
import { onMounted } from 'vue'
import ToggleButton from 'primevue/togglebutton';
import InputText from 'primevue/inputtext';
import 'primeicons/primeicons.css'
import { useJobModals } from '@/composables/useJobModals'
import { useJob } from '@/composables/useJobs';
import { useJobEditFlow } from '@/composables/useJobEditFlow'
import JobsInformation from '../components/jobs/JobsInformation.vue';

import { useCustomerModals } from '@/composables/useCustomerModals'
import { useCustomers } from '@/composables/useCustomers.ts'

const jobStore = useJob()
const customersStore = useCustomers()

const {
  jobs,
  jobsLoading,
  jobsError,
  fetchJobWithDetails,
} = jobStore

const {
  customers,
  fetchCustomersWithDetails
} = customersStore

const jobModalsStore = useJobModals({
  jobStore
})

const {
    currentJob,
    currentJobIndex,
    displayJobDetails,
    jobModalLoading,
    jobTitle,
    jobDescription,
    jobButtonDesc,
    deleteConfirmation,

    closePage,
    createJobDisplay,
    editJobUI,
    getJobIndex,
    openDeleteModal,
    closeDeleteModal
} = jobModalsStore

const jobEditFlow = useJobEditFlow({
  jobStore,
  jobModalsStore,
})

const {updateJobInformation, handleDelete}  = jobEditFlow

const showActiveOnly = ref(false)

onMounted(() => {
  console.log('AboutView mounted')
  fetchJobWithDetails()
  fetchCustomersWithDetails()
})

const isLoading = computed(() =>
  jobsLoading.value
)

const isError = computed(() =>
  jobsError.value
)

</script>

<style scoped>
.skeletonPage {
  width: 85vw;
  margin-left: 15vw;
  padding: 3%;
}

.customerSkeleton {
  width: 23.5%;
  height: 50vh;
  margin-bottom: 2%;
  border-radius: 10px;
}

 /* Flex helpers */
.flex {
  display: flex;
}

.row {
  flex-direction: row;
}

.column {
  flex-direction: column;
}

/* Page layout */
.customerBody {
  width: 85vw;
  margin-left: 15vw;
  height: 90%;
  position: absolute;
  padding: 3%;
  flex-grow: 2;
  gap: 10px;
  margin-top: 1vh;
  border-radius: 10px;
}

.pageHeader {
  width: 100%;
  display: flex;
  align-items: center;
  gap: 1rem;
  flex-wrap: wrap;
}

.leftPanel {
  flex-basis: 100%;
  margin-bottom: 1rem;}

.rightPanel {
  flex-grow: 0;
  display: flex;
  justify-content: flex-end;
  align-items: center;
}

/* Add Customers Button */
.addButton {
  border: none;
  height: 50%;
  width: 150px;
  border-radius: 7px;
  padding: 0.5rem 1rem;
  margin-left: 1rem;
  cursor: pointer;

  /* Theme colors */
  background-color: var(--primary);
  color: var(--primary-foreground);
  transition: background-color 0.2s ease, transform 0.2s ease;
}

.addButton:hover {
  background-color: var(--secondary);
  color: var(--foreground);
  transform: scale(1.05);
}

/* Search bar */
.searchBarWrapper {
  flex-grow: 2;
  display: flex;
  align-items: center;
  padding: 0.5rem;
  border: solid 1px rgb(222, 222, 222);
  border-radius: 10px;
  margin-bottom: 1rem;
}

.searchBar {
  width: 100%;
  border: none;
  box-shadow: none;
  padding: 0.5rem;
  border-radius: 5px;
}

/* Customer display cards */
.customerDisplay {
  width: 100%;
  padding-right: 2%;
  flex-wrap: wrap;
  justify-content: flex-start;
  column-gap: 2%;
  overflow-y: auto;
  height: calc(100% - 20vh);
}

/* Individual cards */
.mb-3 {
  border: solid 1px rgb(222, 222, 222);
  border-radius: 10px;
  width: 23.5%;
  height: 50vh;
  margin-bottom: 2%;
  overflow: hidden;
  display: flex;
  flex-direction: column;
}

/* Card header */
.customCardHeader {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 1rem;
  margin-top: 1rem;
  gap: 0.5rem;
}

.customCardHeader p {
  flex-grow: 1;
  margin: 0;
  font-size: 1.2rem;
  font-weight: bold;
  overflow-wrap: anywhere;
}

/* Edit/Delete icons */
.editButton {
  font-size: 1.1rem;
  cursor: pointer;
  padding: 0.25rem;
  transition: transform 0.15s ease, color 0.15s ease;
  flex-shrink: 0;
}

.editButton:hover {
  transform: scale(1.25);
  color: var(--primary);
}

/* Blur modal overlay */
.customerWindowBlur {
  position: fixed;
  backdrop-filter: brightness(60%) blur(2px);
  height: 100vh;
  width: 100vw;
  top: 0;
  left: 0;
  justify-content: center;
  align-items: center;
  display: flex;
}

/* Responsive adjustments */
@media (max-width: 1200px) {
  .mb-3 {
    width: 48%;
  }
     .customerSkeleton {
    width: 48%;
  }
}

@media (max-width: 768px) {
  .mb-3 {
    width: 100%;
  }

  .customerSkeleton {
    width: 100%;
  }

  .rightPanel {
    justify-content: center;
    margin-top: 1rem;
  }

  .addButton {
    width: 100%;
  }
}

</style>
