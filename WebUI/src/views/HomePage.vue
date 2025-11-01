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
          <!--completion bar-->
          <div class="jobCompletionStatus">

          </div>
        </div>

        <div class="flex row bottomBar">
          <div class="flex row completionTitle">
            <div class = "circle completed"></div>
            <p class="compeltionTitle">Completed</p>
          </div>
          <div class="flex row completionTitle">
            <div class = "circle inProgress"></div>
            <p class="compeltionTitle">In Progress</p>
          </div>
          <div class="flex row completionTitle">
            <div class = "circle pending"></div>
            <p class="compeltionTitle">Pending</p>
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
  
  .flex{
    display: flex;
  }
  .row{
    flex-direction: row;
  }

  .column{
    flex-direction: column;
  }
  .pageHeader{
      height: 15vh;
      width: 100%;
      justify-content: space-between;
  }
  .leftPanel{
      flex-grow: 5;
  }
  .rightPanel{
    flex-grow: 1;
    align-content: center;
    text-align: right;
  }
  .homePageBody{
    width: 90vw;
    height: 90%;
    position: absolute;
    padding: 3%;
    flex-grow: 2;
    gap: 10px;
    margin-top: 1vh;
    margin-left: 10%;
    border-radius: 10px;

  }
  .dashboardContainer{
    border: solid;
    border-width: 1px;
    border-color: rgb(222, 222, 222);
    box-shadow: var(--p-card-shadow);
    background-color: rgb(248, 251, 255);

    border-radius: 10px;
    padding: 2%;
    height: 100vh;
    gap: 5%;
  } 
  .elementContainer{
    height: 25vh;
    width: 100%;
    gap: 5%;
  }
  .dashboardElement{
    width: 25%;
    border: solid;
    border-width: 1px;
    border-color: rgb(222, 222, 222);
    border-radius: 10px;
  }
  .header{
    padding: var(--p-card-body-padding);
    padding-bottom: 0;
  }
  .justify-between{
    justify-content: space-between;
  }
  .title{
    margin: 0;
    flex-grow: 2; 
    font-size: 1.2rem; 
    font-weight: bold;
    color: var(--p-card-color);
  }
  .compeltionTitle{
    margin:0%;
    color: var(--p-card-color);
  }
  .icon{
    font-size: 1.4rem;
    font-weight: bold;
  }
  .calloutValue{
    font-size: 2rem;
  }
  .jobCompletion{
    height: 25vh;
    border: solid;
    border-width: 1px;
    border-color: rgb(222, 222, 222);
    background-color: white;
    border-radius: 10px;
    padding: var(--p-card-body-padding);
    box-shadow: var(--p-card-shadow);
    gap: 5%;
  }
  .jobCompletionBar{
    width: 100%;
    height: 10%;
    border: solid;
    background-color: rgb(211, 211, 211);
    border-color: rgb(211, 211, 211);
    border-width: 1px;
    border-radius: 10px;
  }
  .jobCompletionStatus{
    /*width: var(--job-completion-percentage);*/
    width: 10%;
    height: 100%;
    background-color: black;
    border-color: black;
    border-width: 1px;
    border-top-left-radius: 10px;
    border-bottom-left-radius: 10px;
  }
  .bottomBar{
    gap:2%;
    height: 5vh;
  }
  .completionTitle{
    height: 100%;
    gap: 0.5vw;
  }
  .circle{
    margin-top: 5%;
    width: 10px;
    height: 10px;
    border-width: 1px;
    border-radius: 50%;
  } 
  .completed{
    background-color: green;
  }
  .inProgress{
    background-color: blue;
  }
  .pending{
    background-color: orange;
  }

@media (prefers-color-scheme: dark) {
  .jobCompletionBar{
    background-color: black;
  }
  .jobCompletionStatus{
    background-color: rgb(222, 222, 222);
  }
  .dashboardContainer{
    background-color: #18181b;
  }
  .jobCompletion{
    background-color: #1c1b22;
  }
  .dashboardElement{
    background-color: #1c1b22;
  }
}
  
</style>