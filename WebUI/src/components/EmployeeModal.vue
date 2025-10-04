<script setup lang="ts">
import 'primeicons/primeicons.css';
import { EmployeeModel } from '../client/client'
import InputText from 'primevue/inputtext';
import Button from 'primevue/button';


defineEmits(['closePage', 'updateFunction', 'deleteFunction']);
const props = defineProps({
  selectedEmployee: EmployeeModel
});


// make copy of passed in employee data so when you change the fields it doesnt
// change the card data (until you save)
let employee = new EmployeeModel;
if (props.selectedEmployee) employee = new EmployeeModel(props.selectedEmployee);

</script>

<template>
  <div class="modalBorder">
    <div class = "infoDiv">
      <div class="customerInfoBar">
        <div class = "customerInfoTitle">
          <h2 style="margin: 0;">Edit employee</h2>
          <p style="margin: 0;">Update employee information.</p>
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
        <InputText v-model="employee.emailId" type="number" class="inputValue"></InputText>
      </div>
      <div>
        <h3 class="fieldTitle">Account Id</h3>
        <InputText v-model="employee.accountId" type="number" class="inputValue"></InputText>
      </div>
      <div class="buttons">
        <Button label="Close" @click="$emit('closePage')"></Button>
        <Button label="Update" @click="$emit('updateFunction', selectedEmployee?.employeeId, employee)"></Button>
        <Button label="Delete" @click="$emit('deleteFunction', selectedEmployee?.employeeId)" severity="danger"></Button>
      </div>
    </div>
  </div>
</template>

<style scoped>
.modalBorder{
  display: flex;
  flex-direction: row;
  padding: 4%;
  width: 50vw;
  height: 80vh;
  background-color: var(--p-overlay-modal-background);
  margin-left: 25vw;
  margin-top: 10vh;
  border-radius: 5vh;
}

.infoDiv{
  display: flex;
  flex-direction: column;

  overflow-x: hidden;
  overflow-y: auto;
  height: 100%;
  width: 100%;
  padding-right: 5%;
}
.customerInfoBar{
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}
.customerInfoTitle{
  display: flex;
  flex-direction: column;
  margin-bottom: 2vh;
}
.fieldTitle{
  margin: 0vh;
  margin-bottom: 1vh;
}
.inputValue{
  margin-bottom: 3vh;
  width: 100%;
}
.buttons{
  display: flex;
  flex-direction: row;
  width: 100%;
  justify-content: end;
  gap: 5%;
}
</style>
