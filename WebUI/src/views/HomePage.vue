<template>
  <div class="flex column homePageBody">
    <div class="flex row pageHeader">
        <div class = "flex column leftPanel">
            <h2 style="margin: 0%;">Dashboard</h2>
            <p style="margin: 0;">Welcome back! Here's what's happening with your painting business.</p>
        </div>
    </div>

    <div class="flex column dashboardContainer">
      <div class="elementContainer flex row">
        <!--Total Customers-->
        <Card class="dashboardElement">
          <template #header>
            <div class="flex row justify-between header">
              <p class="title">Total Customers</p>
              <i class="pi pi-users icon"></i>
            </div>
          </template>

          <template #content>
            <div>
              <p style="margin: 0" class="calloutValue">{{ state.totalCustomers }}</p>
            </div>
          </template>
        </Card>

        <!--Active Jobs-->
        <Card class="dashboardElement">
          <template #header>
            <div class="flex row justify-between header">
              <p class="title">Active Jobs</p>
              <i class="pi pi-briefcase icon"></i>
            </div>
          </template>

          <template #content>
            <div>
              <p style="margin: 0" class="calloutValue">{{ state.activeJobs }}</p>
            </div>
          </template>
        </Card>

        <!--Pending Jobs-->
        <Card class="dashboardElement">
          <template #header>
            <div class="flex row justify-between header">
              <p class="title">Pending Jobs</p>
              <i class="pi pi-clock icon"></i>
            </div>
          </template>

          <template #content>
            <div>
              <p style="margin: 0" class="calloutValue">{{ state.pendingJobs }}</p>
            </div>
          </template>
        </Card>

        <!--Revenue-->
        <Card class="dashboardElement">
          <template #header>
            <div class="flex row justify-between header">
              <p class="title">Total Revenue</p>
              <i class="pi pi-dollar icon"></i>
            </div>
          </template>

          <template #content>
            <div>
              <p style="margin: 0" class="calloutValue">{{ state.totalRevenue }}</p>
            </div>
          </template>
        </Card>
      </div>

      <div class="flex column jobCompletion justify-between">
        <div class="flex row justify-between">
          <div class="title">
            Job Completion Progress
          </div>
          <div>
            <p class="compeltionTitle">Total: {{ state.totalJobs }}</p>
          </div>
        </div>
        <!--Status bar-->
        <div class="jobCompletionBar">
          <div
            class="jobCompletionStatus completed"
            :style="{ width: completedPercent + '%' }"
          ></div>
          <div
            class="jobCompletionStatus inProgress bar"
            :style="{ width: inProgressPercent + '%' }"
          ></div>
          <div
            class="jobCompletionStatus pending bar"
            :style="{ width: pendingPercent + '%' }"
          ></div>
        </div>


        <div class="flex row bottomBar">
          <div class="flex row completionTitle">
            <div class = "circle completed"></div>
            <p class="completionTitle">Completed</p>
          </div>
          <div class="flex row completionTitle">
            <div class = "circle inProgress"></div>
            <p class="completionTitle">In Progress</p>
          </div>
          <div class="flex row completionTitle">
            <div class = "circle pending"></div>
            <p class="completionTitle">Pending</p>
          </div>
        </div>
      </div>
    </div>

  </div>

</template>

<script setup lang="ts">
import Card from 'primevue/card'
import { Client, CustomerModel, AddressModel} from '../client/client'
import { onMounted, reactive } from 'vue'
import { computed } from 'vue'

const client = new Client(import.meta.env.VITE_API_BASE_URL)
const state = reactive({
  totalCustomers: 0,
  activeJobs: 0,
  pendingJobs: 0,
  totalRevenue: 0,
  totalJobs: 0,
  loading: false,
  error: null as string | null,
})

const completedPercent = computed(() =>
  state.totalJobs ? ((state.totalJobs - state.activeJobs - state.pendingJobs) / state.totalJobs) * 100 : 0
)

const inProgressPercent = computed(() =>
  state.totalJobs ? (state.activeJobs / state.totalJobs) * 100 : 0
)

const pendingPercent = computed(() =>
  state.totalJobs ? (state.pendingJobs / state.totalJobs) * 100 : 0
)

onMounted(() => {
  console.log('AboutView mounted')
  fetchCustomers()
})

function fetchCustomers() {
  state.loading = true
  state.error = null
  client
    .getAllCustomers()
    .then((response) => {
      state.totalCustomers = response.length;
    })
    .catch((error) => {
      state.error = error.message || 'An error occurred'
    })
    .finally(() => {
      state.loading = false
    })
}
</script>

<style scoped>
/* Flex utilities */
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
.homePageBody {
  width: 80vw;
  min-height: 90vh;
  padding: 2rem;
  margin: 1vh auto 0 auto;
  margin-left: 15vw;

  border-radius: var(--radius-lg);
  gap: 1rem;
  box-sizing: border-box;
  background-color: var(--background);
  color: var(--foreground);
}

/* Header */
.pageHeader {
  width: 100%;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1rem;
}

.leftPanel h2,
.leftPanel p {
  margin: 0;
}

/* Dashboard container */
.dashboardContainer {
  display: flex;
  flex-direction: column;
  gap: 2rem;
  padding: 2rem;
  border: 1px solid var(--border);
  border-radius: var(--radius-lg);
  background-color: var(--card);
  box-shadow: var(--p-card-shadow);
}

/* Cards row */
.elementContainer {
  display: flex;
  flex-wrap: wrap;
  gap: 1.5rem;
  justify-content: space-between;
}

/* Individual dashboard element */
.dashboardElement {
  flex: 1 1 23%;
  min-width: 200px;
  border: 1px solid var(--border);
  border-radius: var(--radius-md);
  padding: 1rem;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  background-color: var(--card);
  color: var(--foreground);
}

/* Card header */
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 0.5rem;
}

.title {
  margin: 0;
  font-size: 1.2rem;
  font-weight: var(--font-weight-medium);
  color: var(--foreground);
  flex-grow: 1;
}

.icon {
  font-size: 1.4rem;
  flex-shrink: 0;
  color: var(--primary);
}

/* Callout values */
.calloutValue {
  font-size: 2rem;
  margin-top: 0.5rem;
  color: var(--foreground);
}

/* Job completion section */
.jobCompletion {
  display: flex;
  flex-direction: column;
  gap: 1rem;
  border: 1px solid var(--border);
  border-radius: var(--radius-md);
  padding: 1rem;
  background-color: var(--card);
  box-shadow: var(--p-card-shadow);
  color: var(--foreground);
}

/* Completion bar */
.jobCompletionBar {
  width: 100%;
  height: 20px;
  border-radius: var(--radius-md);
  background-color: var(--secondary);
  overflow: hidden;
  display: flex;
  flex-direction: row;
}

.jobCompletionStatus {
  width: 10%; /* dynamically adjust */
  height: 100%;
  transition: width 0.6s ease;
  background-color: var(--primary);
  border-radius: var(--radius-md) 0 0 var(--radius-md);
}

/* Bottom bar legend */
.bottomBar {
  display: flex;
  gap: 2rem;
  margin-top: 0.5rem;
}

.completionTitle {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  color: var(--foreground);
}

.circle {
  width: 12px;
  height: 12px;
  border-radius: 50%;
}

.completed {
  background-color: var(--chart-2);
}

.inProgress {
  background-color: var(--chart-1);
}

.pending {
  background-color: var(--chart-3);
}

.bar{
  border-top-left-radius: 0px;
  border-bottom-left-radius: 0px;
}

/* Responsive adjustments */
@media (max-width: 1200px) {
  .dashboardElement {
    flex: 1 1 48%;
  }
}

@media (max-width: 768px) {
  .elementContainer {
    flex-direction: column;
  }
  .dashboardElement {
    flex: 1 1 100%;
  }
}

/* Dark mode support via theme.css */
.dark .dashboardContainer,
.dark .dashboardElement,
.dark .jobCompletion {
  background-color: var(--card);
  color: var(--foreground);
}

.dark .jobCompletionBar {
  background-color: var(--secondary);
}

.dark .jobCompletionStatus {
  background-color: var(--primary);
}

.dark .icon {
  color: var(--primary);
}

</style>
