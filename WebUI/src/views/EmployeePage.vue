<script setup lang="ts">
import Card from 'primevue/card'
import { Client, EmployeeModel } from '../client/client'
import { onMounted, reactive, ref } from 'vue'
import CrudHeader from '../components/CrudHeader.vue'
import EmployeeModal from '../components/EmployeeModal.vue'
import 'primeicons/primeicons.css'

const client = new Client(import.meta.env.VITE_API_BASE_URL)
let showEmployeeModal = ref(false)
const selectedEmployee = ref(new EmployeeModel);
import { useToast } from '@/composables/useToast.ts'

const { showSuccess, showError } = useToast()

const state = reactive({
  employees: [] as EmployeeModel[],
  loading: false,
  error: null as string | null,
})

onMounted(() => {
  console.log('EmployeePage mounted')
  fetchEmployees()
})

// reload employee list
function fetchEmployees() {
  state.loading = true
  state.error = null
  client
    .getAllEmployees()
    .then((response) => {
      state.employees = response
    })
    .catch((error) => {
      state.error = error.message || 'An error occurred'
    })
    .finally(() => {
      state.loading = false
    })
}

function addDefaultEmployee() {
  state.loading = true;
  const newEmployee = new EmployeeModel();

  // use 0 as the identity to signal to ef core you want it to be a new row
  newEmployee.employeeId = 0;
  newEmployee.accountId = 999;
  newEmployee.emailId = 999;
  newEmployee.name = 'New Employee';
  newEmployee.role = 'New Employee';

  // create employee
  client.createEmployee(newEmployee)
    .then(() => {
        showSuccess('Employee Created Successfully');
      })
    .catch((error) => {
      showError('Failed To Create Employee')
      state.error = error.message || 'An error occurred';
    })
    .finally(() => {
      fetchEmployees();
      state.loading = false;
    })
}

function updateEmployeeInformation(currentID: number, updatedEmployee: EmployeeModel){
  updatedEmployee.employeeId = currentID;

  state.loading = true;
  state.error = null;
  client
    .updateEmployee(updatedEmployee)
    .then(() => {
      showSuccess('Employee Updated Successfully');
    })
    .catch((error) => {
      showError('Failed To Update Employee')
      state.error = error.message || 'An error occurred';
    })
    .finally(() => {
      fetchEmployees();
      state.loading = false;
    })
  showEmployeeModal = ref(false);
}

function deleteEmployee(id: number) {
state.loading = true;
  state.error = null;

  client
    .deleteEmployee(id)
    .then(() => {
      showSuccess('Employee Deleted Successfully');
    })
    .catch((error) => {
      showError('Failed To Delete Employee')
      state.error = error.message || 'An error occurred';
    })
    .finally(() => {
      fetchEmployees();
      state.loading = false;
    })
    showEmployeeModal = ref(false);
}
</script>

<template>
  <CrudHeader title="Employees"></CrudHeader>
  <div v-if="!state.loading">
    <Card v-for="employee in state.employees" :key="employee.employeeId" class="empCard" @click="showEmployeeModal = !showEmployeeModal; selectedEmployee = employee"> 
      <template #title>{{ employee.employeeId }}: {{ employee.name }}</template>
      <template #subtitle></template>
      <template #content>
        <p>AccountId: {{ employee.accountId }}</p>
        <p>EmailId: {{ employee.emailId }}</p>
        <p>Role: {{ employee.role }}</p>
      </template>
    </Card>
    <Card class="empCard" @click="addDefaultEmployee()">
      <template #title>Add</template>
    </Card>
  </div>
  <div v-else-if="state.error">{{ state.error }}</div>
  <div v-else>
    <p>Loading...</p>
  </div>

  <div v-if="showEmployeeModal" class="modalBlur">
    <EmployeeModal :selectedEmployee="selectedEmployee" @closePage="showEmployeeModal = !showEmployeeModal" @updateFunction="updateEmployeeInformation" @deleteFunction="deleteEmployee"></EmployeeModal>
  </div>
</template>

<style scoped>
.empCard{
  border: solid;
  border-width: 1px;
  border-radius: 10px;
  margin-bottom: 2%;
}
.modalBlur{
  position: fixed;
  backdrop-filter: brightness(60%) blur(2px);
  height: 100vh;
  width: 100vw;
  top: 0px;
  left: 0px;
}
</style>
