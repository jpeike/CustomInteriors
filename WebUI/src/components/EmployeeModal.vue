<script setup lang="ts">
import 'primeicons/primeicons.css';
import { Client, CustomerModel } from '../client/client'
import InputText from 'primevue/inputtext';
import { ref } from 'vue'


defineProps({
  currentCustomerInformation: CustomerModel
});

const customer = ref(new CustomerModel);
const newEmail = ref('');
const newPhone = ref('');

</script>

<template>
  <div class="scrollBar">
    <div class = "customerInfoWindow">
      <div class="customerInfoBar">
        <div class = "customerInfoTitle">
          <h2 style="margin: 0;">Edit Customer</h2>
          <p style="margin: 0;">Update customer information and contact details.</p>
        </div>
      </div>
      <div class = "multipleFields">
        <div>
          <h3 class="fieldTitle">First Name</h3>
          <InputText v-model="customer.firstName" type="text" class="inputValue" :placeholder="currentCustomerInformation?.firstName"></InputText>
        </div>
        <div>
          <h3 class="fieldTitle">Last Name</h3>
          <InputText v-model="customer.lastName" type="text" class="inputValue" :placeholder="currentCustomerInformation?.lastName"></InputText>
        </div>
      </div>
      <div>
        <h3 class="fieldTitle">Email</h3>
        <InputText v-model="newEmail" type="text" class="inputValue" placeholder="placeholder email"></InputText>
      </div>
      <div>
        <h3 class="fieldTitle">Phone</h3>
        <InputText v-model="newPhone" type="text" class="inputValue" placeholder="placeholder phone"></InputText>
      </div>
      <div class="multipleFields">
        <div class="tripleField">
          <h3 class="fieldTitle">City</h3>
          <InputText type="text" class="inputValue" placeholder="City"></InputText>
        </div>
        <div class="tripleField">
          <h3 class="fieldTitle">State</h3>
          <InputText type="text" class="inputValue" placeholder="State"></InputText>
        </div>
        <div class="tripleField">
          <h3 class="fieldTitle">Zip</h3>
          <InputText type="text" class="inputValue" placeholder="Zip"></InputText>
        </div>
      </div>
      <div class="notesField">
        <h3 class="fieldTitle">Notes</h3>
        <textarea v-model="customer.customerNotes" type="text" class="p-inputtext p-component inputValue notes" :placeholder="currentCustomerInformation?.customerNotes"></textarea>
      </div>
      <div class="buttons">
        <button class = "cancelUpdateButton" @click="$emit('closePage')">
          <p style="margin: 0; text-align: center; color: black;">Cancel</p>
        </button>
        <button class = "updateInfoButton" @click="$emit('updateCustomerInformation', currentCustomerInformation?.customerId, customer)">
          <p style="margin: 0; text-align: center; color: white;">Update</p>
        </button>
      </div>
    </div>
    <button class = "exitButton" @click="$emit('closePage')"><h4 style="margin: 0;">X</h4></button>
  </div>
</template>

<style scoped>
.scrollBar{
  display: flex;
  flex-direction: row;
  padding: 4%;
  width: 50vw;
  height: 80vh;
  background-color: rgb(255, 255, 255);
  margin-left: 25vw;
  margin-top: 10vh;
  border-radius: 5vh;
}

.customerInfoWindow{
  display: flex;
  flex-direction: column;
  background-color: rgb(255, 255, 255);
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
  background-color: rgb(0, 0, 0);
  border: none;
  align-content: center;
  border-radius: 7px;
}
.cancelUpdateButton{
  width: 20%;
  height: 5vh;
  background-color: rgb(233, 233, 233);
  border: none;
  align-content: center;
  border-radius: 7px;
}
</style>
