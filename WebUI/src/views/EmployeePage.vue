<script setup lang="ts">
import Card from 'primevue/card'
import { Client, EmployeeModel } from '../client/client'
import { onMounted, reactive, ref } from 'vue'
import CrudHeader from '../components/CrudHeader.vue'
import EmployeeModal from '../components/EmployeeModal.vue'

const client = new Client(import.meta.env.VITE_API_BASE_URL)
let showEmployeeModal = ref(false)
const currentEmployeeId = ref(0)

const state = reactive({
  employees: [] as EmployeeModel[],
  loading: false,
  error: null as string | null,
})

onMounted(() => {
  console.log('EmployeePage mounted')
  fetchEmployees()
})

function fetchEmployees() {
  state.loading = true
  state.error = null
  client
    .getEmployees()
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
  console.log('Adding new employee');

  let newEmployee = new EmployeeModel();
  newEmployee.employeeId = 99;
  newEmployee.accountId = 99;
  newEmployee.emailId = 99;
  newEmployee.name = 'NEW EMPLOYEE';
  newEmployee.role = 'NEW EMPLOYEE';

  // create employee
  client.createEmployee(newEmployee)
    .catch((error) => {
      state.error = error.message || 'An error occurred';
    })
    .finally(() => {
      state.loading = false;
    })
}

function updateEmployeeInformation(currentID: number, updatedEmployee: EmployeeModel){
  const tempType = state.employees[currentEmployeeId.value - 1].customerType;
  state.employees[currentEmployeeId.value - 1] = updatedEmployee;
  state.employees[currentEmployeeId.value - 1].customerType = tempType;
  state.employees[currentEmployeeId.value - 1].customerId = currentID;

  state.loading = true
  state.error = null
  client
    .update(state.employees[currentEmployeeId.value - 1])
    .catch((error) => {
      state.error = error.message || 'An error occurred'
    })
    .finally(() => {
      state.loading = false
    })
  showEmployeeModal = ref(false);
}
</script>

<template>
  <CrudHeader title="Employees"></CrudHeader>
  <div v-if="!state.loading">
    <Card v-for="employee in state.employees" :key="employee.employeeId" class="empCard" @click="showEmployeeModal = !showEmployeeModal; currentEmployeeId = employee.employeeId ?? 0">
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
    <EmployeeModal @closePage="showEmployeeModal = !showEmployeeModal" @updateFunction="updateEmployeeInformation"></EmployeeModal>
  </div>
</template>

<style scoped>
.empCard{
  border: solid;
  border-width: 1px;
  border-color: rgb(222, 222, 222);
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
