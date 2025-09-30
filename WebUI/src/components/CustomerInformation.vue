<template>
    <div class="flex row scrollBar">
        <div class = "flex column customerInfoWindow">
            <div class="flex row customerInfoBar">
                <div class = "flex column customerInfoTitle">
                    <h2 style="margin: 0;">{{title}}</h2>
                    <p style="margin: 0;">{{description}}</p>
                </div>
            </div>
            <div class = "flex row multipleFields">
                <div>
                    <h3 class="fieldTitle">First Name*</h3>
                    <InputText v-model="customer.firstName" type="text" class="inputValue" :placeholder="currentCustomerInformation?.firstName"></InputText>
                </div>
                <div>
                    <h3 class="fieldTitle">Last Name*</h3>
                    <InputText v-model="customer.lastName" type="text" class="inputValue" :placeholder="currentCustomerInformation?.lastName"></InputText>
                </div>
            </div>
            <div>
                <h3 class="fieldTitle">Customer Type*</h3>
                <InputText v-model="customer.customerType" type="text" class="inputValue" :placeholder="currentCustomerInformation?.customerType"></InputText>
            </div>
            <div>
                <h3 class="fieldTitle">Email</h3>
                <InputText v-model="newEmail" type="text" class="inputValue" placeholder="placeholder email"></InputText>
            </div>
            <div>
                <h3 class="fieldTitle">Phone</h3>
                <InputText v-model="newPhone" type="text" class="inputValue" placeholder="placeholder phone"></InputText>
            </div>
            <div>
                <h3 class="fieldTitle">Preffered Contact Method</h3>
                <div class="contactMethod">                    
                    <div>
                        <input :checked="currentCustomerInformation?.prefferedContactMethod == 'Email'" type="radio" class = "contactMethodRadio" v-model="customer.prefferedContactMethod" id = "Email" name="contactMethod" value ="Email" variant="filled"/>
                        <label for="Email">Email</label>
                    </div>
                    <div>
                        <input :checked="currentCustomerInformation?.prefferedContactMethod == 'Phone'" type="radio" class = "contactMethodRadio" v-model="customer.prefferedContactMethod" id = "Phone" name="contactMethod" value ="Phone"/>
                        <label for="Phone">Phone</label>
                    </div>
                </div>
            </div>
            <div class="flex row multipleFields">
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
            <div>
                <h3 class="fieldTitle">Company Name</h3>
                <InputText v-model="customer.companyName" type="text" class="inputValue" :placeholder="currentCustomerInformation?.companyName"></InputText>
            </div>
            <div>
                <h3 class="fieldTitle">Status</h3>
                <InputText v-model="customer.status" type="text" class="inputValue" :placeholder="currentCustomerInformation?.status"></InputText>
            </div>
            <div class="notesField">
                <h3 class="fieldTitle">Notes</h3>
                <textarea v-model="customer.customerNotes" type="text" class="p-inputtext p-component inputValue notes" :placeholder="currentCustomerInformation?.customerNotes"></textarea>
            </div>
            <div class="flex row buttons">
                <button class = "cancelUpdateButton" @click="$emit('closePage')">
                    <p style="margin: 0; text-align: center; color: black;">Cancel</p>
                </button>  
                <button class = "updateInfoButton" @click="$emit('updateCustomerInformation', currentCustomerInformation?.customerId, customer)">
                    <p style="margin: 0; text-align: center; color: white;">{{buttonDesctipnion}}</p>
                </button>
            </div>
        </div>
        <button class = "exitButton" @click="$emit('closePage')"><h4 style="margin: 0;">X</h4></button>
    </div>
</template>

<script setup lang="ts">
    import 'primeicons/primeicons.css';
    import { Client, CustomerModel } from '../client/client'
    import InputText from 'primevue/inputtext';
    import { ref } from 'vue'

    const props = defineProps({
       currentCustomerInformation: CustomerModel,
       title: String,
       description: String,
       buttonDesctipnion: String
    });
    
    let customer;
    if (props.currentCustomerInformation != undefined){
        customer =ref(props.currentCustomerInformation);
}
    else{
        customer = ref(new CustomerModel);
    }
    
    const newEmail = ref('');
    const newPhone = ref('');
</script>

<style scoped>
    .flex{
        display: flex;
    }
    .row {
        flex-direction: row;
    }
    .column{
        flex-direction: column;
    }
    .scrollBar{
        padding: 4%;
        width: 50vw;
        height: 80vh;  
        background-color: rgb(255, 255, 255);
        margin-left: 25vw;
        margin-top: 10vh;    
        border-radius: 5vh;
    }
    
    .customerInfoWindow{
        background-color: rgb(255, 255, 255);
        overflow: scroll;
        height: 100%;
        width: 100%;
        padding-right: 5%;
    }
    .customerInfoBar{
        justify-content: space-between;
    }
    .customerInfoTitle{
        margin-bottom: 2vh;
    }
    .exitButton{
        background: none;
        border: none;
        height: fit-content;
    }
    .multipleFields{
        justify-content: space-between;
    }
    .fieldTitle{
        margin: 0vh;
        margin-bottom: 1vh;
    }
    .inputValue{
        margin-bottom: 2vh;
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
    .contactMethod{
        display: flex;
        justify-content:start;
        gap: 5%;
        margin-bottom: 2vh;
    }
    
</style>