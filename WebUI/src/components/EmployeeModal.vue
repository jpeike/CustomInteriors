<script setup lang="ts">
import 'primeicons/primeicons.css'
import { EmployeeModel } from '../client/client'
import { InputText, InputNumber } from 'primevue'

defineEmits(['closePage', 'updateFunction', 'deleteFunction'])
const props = defineProps({
  selectedEmployee: EmployeeModel,
})

// make copy of passed in employee data so when you change the fields it doesnt
// change the card data (until you save)
let employee = new EmployeeModel()
if (props.selectedEmployee) employee = new EmployeeModel(props.selectedEmployee)
</script>

<template>
  <div class="modalBorder">
    <div class="infoDiv">
      <div class="customerInfoBar">
        <div class="customerInfoTitle">
          <h2 style="margin: 0">Edit employee</h2>
          <p style="margin: 0">Update employee information.</p>
        </div>
      </div>
      <div>
        <h3 class="fieldTitle">Name</h3>
        <InputText v-model="employee.name" type="text" class="inputValue"></InputText>
      </div>
      <div>
        <h3 class="fieldTitle">Role</h3>
        <InputText v-model="employee.role" type="text" class="inputValue"></InputText>
      </div>
      <div>
        <h3 class="fieldTitle">Email Id</h3>
        <InputNumber v-model="employee.emailId" type="number" class="inputValue"></InputNumber>
        <InputNumber v-model="employee.emailId" type="number" class="inputValue"></InputNumber>
      </div>
      <div>
        <h3 class="fieldTitle">Account Id</h3>
        <InputNumber v-model="employee.accountId" type="number" class="inputValue"></InputNumber>
      </div>
      <div class="flex row buttons">
        <button class="cancelUpdateButton" @click="$emit('closePage')">
          <p class="buttonText">Close</p>
        </button>
        <button
          class="updateInfoButton"
          @click="$emit('updateFunction', selectedEmployee?.employeeId, employee)"
        >
          <p class="buttonText">Update</p>
        </button>
        <button
          class="updateInfoButton destructive"
          @click="$emit('deleteFunction', selectedEmployee?.employeeId)"
        >
          <p class="buttonText">Delete</p>
        </button>
      </div>
    </div>
  </div>
</template>

<style scoped>
.titleText {
  margin: 0;
  font-weight: var(--font-weight-medium);
}

.subtitleText {
  margin: 0;
  font-weight: var(--font-weight-normal);
}

.buttonText {
  margin: 0;
  text-align: center;
  font-weight: var(--font-weight-medium);
}

.destructive {
  background-color: var(--destructive);
  color: var(--destructive-foreground);
}

.modalBorder {
  display: flex;
  flex-direction: row;
  padding: 4%;
  width: 50vw;
  height: 80vh;
  margin-left: 25vw;
  margin-top: 10vh;
  border-radius: 5vh;
}

.infoDiv {
  display: flex;
  flex-direction: column;
  overflow-x: hidden;
  overflow-y: auto;
  height: 100%;
  width: 100%;
  padding-right: 5%;
}
.cancelUpdateButton,
.updateInfoButton {
  transition:
    background-color 0.2s ease,
    color 0.2s ease,
    transform 0.1s ease;
}

.cancelUpdateButton:hover {
  background-color: var(--secondary);
  color: var(--foreground);
  cursor: pointer;
}

.updateInfoButton:hover {
  background-color: var(--primary);
  color: var(--primary-foreground);
  cursor: pointer;
}

.updateInfoButton.destructive:hover {
  background-color: var(--destructive);
  color: var(--destructive-foreground);
}

.cancelUpdateButton:active,
.updateInfoButton:active,
.updateInfoButton.destructive:active {
  transform: scale(0.97);
}
</style>
