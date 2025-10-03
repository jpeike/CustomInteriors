<script setup lang="ts">
import 'primeicons/primeicons.css';
import { Client, EmployeeModel } from '../client/client'
import InputText from 'primevue/inputtext';
import { ref } from 'vue'


defineProps({
  selectedEmployee: EmployeeModel
});

const employee = ref(new EmployeeModel);

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
      <div class = "multipleFields">
        <div>
          <h3 class="fieldTitle">First Name</h3>
          <InputText v-model="employee.name" type="text" class="inputValue" :placeholder="selectedEmployee?.name"></InputText>
        </div>
      </div>
      <div>
        <h3 class="fieldTitle">Email Id</h3>
        <InputText v-model="employee.emailId" type="text" class="inputValue" :placeholder="selectedEmployee?.emailId.toString()"></InputText>
      </div>
      <div class="buttons">
        <button class = "cancelUpdateButton" @click="$emit('closePage')">
          <p style="margin: 0; text-align: center; color: black;">Cancel</p>
        </button>
        <button class = "updateInfoButton" @click="$emit('updateFunction', selectedEmployee?.employeeId, employee)">
          <p style="margin: 0; text-align: center; color: white;">Update</p>
        </button>
      </div>
    </div>
    <button class = "exitButton" @click="$emit('closePage')"><h4 style="margin: 0;">X</h4></button>
  </div>
</template>

<style scoped>
.modalBorder{
  display: flex;
  flex-direction: row;
  padding: 4%;
  width: 50vw;
  height: 80vh;
  background-color: rgb(0, 0, 0);
  margin-left: 25vw;
  margin-top: 10vh;
  border-radius: 5vh;
}

.infoDiv{
  display: flex;
  flex-direction: column;
  background-color: rgb(0, 0, 0);
  overflow: scroll;
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
.exitButton{
  background: none;
  border: none;
  height: fit-content;
}
.multipleFields{
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}
.fieldTitle{
  margin: 0vh;
  margin-bottom: 1vh;
}
.inputValue{
  margin-bottom: 3vh;
  width: 100%;
}
.tripleField{
  width: 30%;
}
.notes{
  height: 10vh;
  resize: none;
}
.buttons{
  display: flex;
  flex-direction: row;
  width: 100%;
  justify-content: end;
  gap: 5%;
}
.updateInfoButton{
  width: 20%;
  height: 5vh;
  border: none;
  align-content: center;
  border-radius: 7px;
}
.cancelUpdateButton{
  width: 20%;
  height: 5vh;
  border: none;
  align-content: center;
  border-radius: 7px;
}
</style>
