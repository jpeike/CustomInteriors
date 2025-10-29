<template>
  <div class="flex column homePageBody">
    <div class="flex row pageHeader">
        <div class = "flex column leftPanel">
            <h2 style="margin: 0;">Dashboard</h2>
        </div>        
    </div>
    
    <div class="flex column dashboardContainer">
      <div class="elementContainer flex row">

        <Card class="dashboardElement">
          <template #header>
            <div class="flex row justify-between">
              <p style="margin: 0">Total Customers</p>
              <i class="pi pi-users" style="font-size: 1.1rem"></i>
            </div>
          </template>

          <template #content>
            <div>
              <p style="margin: 0">{{ state.totalCustomers }}</p>
            </div>
          </template>
        </Card>

        <Card class="dashboardElement">
          <template #header>
            <div class="flex row justify-between">
              <p style="margin: 0">Active Jobs</p>
              <i class="pi pi-briefcase" style="font-size: 1.1rem"></i>
            </div>
          </template>
        </Card>

        <Card class="dashboardElement">
          <template #header>
            <div class="flex row justify-between">
              <p style="margin: 0">Active Jobs</p>
              <i class="pi pi-clock" style="font-size: 1.1rem"></i>
            </div>
          </template>
        </Card>
        
        <Card class="dashboardElement">
          <template #header>
            <div class="flex row justify-between">
              <p style="margin: 0">Active Jobs</p>
              <i class="pi pi-dollar" style="font-size: 1.1rem"></i>
            </div>
          </template>
        </Card>
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
    background-color: red;
    padding: 2%;
    height: 100vh;
  } 
  .elementContainer{
    height: 25vh;
    width: 100%;
    gap: 5%;
  }
  .dashboardElement{
    background-color: blue;
    width: 25%;
    padding: 2%;
  }
  .justify-between{
    justify-content: space-between;
  }
  
</style>