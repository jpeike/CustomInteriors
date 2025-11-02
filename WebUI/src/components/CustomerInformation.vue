<template>
    <div class="flex row defaultColor scrollBar">
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
                    <InputText v-model="customer.firstName" required="true" type="text" class="inputValue" :placeholder="currentCustomerInformation?.firstName"></InputText>
                </div>
                <div>
                    <h3 class="fieldTitle">Last Name*</h3>
                    <InputText v-model="customer.lastName" required="true" type="text" class="inputValue" :placeholder="currentCustomerInformation?.lastName"></InputText>
                </div>
            </div>
            <div>
                <h3 class="fieldTitle">Customer Type*</h3>
                <InputText v-model="customer.customerType" required="true" type="text" class="inputValue" :placeholder="currentCustomerInformation?.customerType"></InputText>
            </div>
                        
        <!--Contact-->
            <div style="margin-bottom: 5%;">
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
            
            <div style="margin-bottom: 5%;" v-for="(emailsAdresses, index) in listOfEmails">
                
                <div class="flex row addressHeader">
                    <h2 style="margin: 0;">Email {{ index + 1 }}</h2>
                    <i class="pi pi-trash editButton" @click="deleteEmail(listOfEmails[index]); listOfEmails.splice(index, 1);"></i>
                    </div>
                <div class="flex row multipleFields">
                    <div>
                        <h3 class="fieldTitle">Address * </h3>
                        <InputText v-model="emailsAdresses.emailAddress" type="text" class="inputValue" :placeholder="listOfEmails[index].emailAddress"></InputText>
                    </div>
                    <div>
                        <h3 class="fieldTitle">Type * </h3>
                        <InputText v-model="emailsAdresses.emailType" type="text" class="inputValue" :placeholder="listOfEmails[index].emailType"></InputText>
                    </div>  
                </div>                  
            </div>
            <div class="addAddress">
                <button @click="addEmail" class="cancelUpdateButton"> <p style="margin: 0; text-align: center;">Add Email</p></button>
            </div>

        <!--Address Section-->
            <div class="addressField" v-for="(address, index) in listOfAddresses">
                <div class="flex row addressHeader">
                    <h2 style="margin: 0;">Address {{ index + 1 }}</h2>
                    <i class="pi pi-trash editButton" @click="deleteAddress(listOfAddresses[index]); listOfAddresses.splice(index, 1);"></i>
                </div>
                <div>
                    <h3 class="fieldTitle">Street Address *</h3>
                    <InputText v-model="address.street" required="true" type="text" class="inputValue" :placeholder="address.street ?? 'Street'"></InputText>
                </div>
                <div>
                    <h3 class="fieldTitle">Country</h3>
                    <InputText v-model="address.country" type="text" class="inputValue" :placeholder="address.country ?? 'Country'"></InputText>
                </div>
                <div class="flex row multipleFields">
                    <div class="tripleField">
                        <h3 class="fieldTitle">City *</h3>
                        <InputText v-model="address.city" required="true" type="text" class="inputValue" :placeholder="address.city ?? 'City'"></InputText>
                    </div>
                    <div class="tripleField">
                        <h3 class="fieldTitle">State *</h3>
                        <InputText v-model="address.state" required="true" type="text" class="inputValue" :placeholder="address.state ?? 'State'"></InputText>
                    </div>
                    <div class="tripleField">
                        <h3 class="fieldTitle">Zip *</h3>
                        <input v-model="address.postalCode" required="true" class="p-inputtext p-component inputValue" :placeholder="address.postalCode?.toString()"></input>
                    </div>
                </div> 
                 <div>
                    <h3 class="fieldTitle">Address Type *</h3>
                    <InputText v-model="address.addressType" required="true" type="text" class="inputValue" :placeholder="address.addressType ?? 'Address Type'"></InputText>
                </div>               
            </div>

            <div class="addAddress">
                <button @click="addAddress" class="cancelUpdateButton"> <p style="margin: 0; text-align: center;">Add Address</p></button>
            </div>

        <!--Company Name-->
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
                    <p style="margin: 0; text-align: center;">Cancel</p>
                </button>  
                <button class = "updateInfoButton" @click="testInfo(currentCustomerInformation?.customerId, customer, listOfAddresses, removedAddresses, listOfEmails, removedEmails)">
                    <p style="margin: 0; text-align: center;">{{buttonDesctipnion}}</p>
                </button>
            </div>
            
        </div>
        <button class = "exitButton" @click="$emit('closePage')"><h4 style="margin: 0;">X</h4></button>
    </div>
</template>

<script setup lang="ts">
    import 'primeicons/primeicons.css';
    import { Client, CustomerModel, AddressModel, EmailModel, Email } from '../client/client'
    import InputText from 'primevue/inputtext';
    import {ref} from 'vue'
    import { useToast } from '@/composables/useToast.ts'

    const { showWarning } = useToast()

    const props = defineProps<{
        currentCustomerInformation: CustomerModel | undefined,
        currentAddresses: AddressModel[] | undefined,
        currentEmails: EmailModel[] | undefined,
        title: String,
        description: String,
        buttonDesctipnion: String
    }>();
    
    const emit = defineEmits<{
        closePage: []
        updateCustomerInformation: [customerId: number | undefined, customer: CustomerModel, listOfAddresses: AddressModel[], removedAddresses: number[], listOfEmails: EmailModel[], removedEmails: number[]]
    }>()

    const newEmail = ref('');
    const newPhone = ref('');
    
    let customer = ref(new CustomerModel);;
    let listOfAddresses = ref([new AddressModel]);
    let removedAddresses = [0];
    
    let listOfEmails = ref([new EmailModel]);
    let removedEmails = [0];

    const message = ref('');
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;


    if (props.currentCustomerInformation != undefined){
        customer = ref(props.currentCustomerInformation);
    }

    if (props.currentAddresses != undefined){
        listOfAddresses = ref(props.currentAddresses);
    }

    if (props.currentEmails != undefined){
        listOfEmails = ref(props.currentEmails);
    }
    
    function addAddress(){
        listOfAddresses.value.push(new AddressModel);
    }

    function deleteAddress(address: AddressModel){
        //removedAddresses.push(listOfAddresses[index].addressId!); listOfAddresses.splice(index, 1);
        if (address.addressId! != undefined){
            console.log(removedAddresses);
            removedAddresses.push(address.addressId);
        }
    }

    function addEmail(){
        listOfEmails.value.push(new EmailModel);
    }

    function deleteEmail(email: EmailModel){
        if (email.emailID! != undefined){
            removedEmails.push(email.emailID);
        }
    }

    function testInfo(customerId: number | undefined, customer: CustomerModel, listOfAddresses: AddressModel[], removedAddresses: number[], listOfEmails: EmailModel[], removedEmails: number[]){
        if (!customer.firstName|| !customer.lastName || !customer.customerType){
            showWarning('Customer information not valid')
            return;
        }
        
        for (let i = 0; i < listOfEmails.length; i++){
            if (!emailRegex.test(listOfEmails[i].emailAddress!)){
                showWarning("Email " + (i+1) + " 's address is not valid")
                return;
            }
            if (!listOfEmails[i].emailAddress || !listOfEmails[i].emailType){
                showWarning("Email " + (i+1) + " has one or more fields that are not valid")
                return;
            }
        }

        for (let i = 0; i < listOfAddresses.length; i++){
            if (!listOfAddresses[i].city || !listOfAddresses[i].postalCode || !listOfAddresses[i].addressType){
                showWarning("Address " + (i+1) + " has one or more fields that are not valid")
                return
            }

            if (!/^\d{5}$/.test(listOfAddresses[i].postalCode?.toString()!) || listOfAddresses[i].postalCode! < 10000){
                showWarning("Address " + (i + 1) + " ZIP Code must be a 5 digit integer")
                return
            }
        }
        
        message.value = 'Address Successfully Created'
        emit('updateCustomerInformation', customerId, customer, listOfAddresses, removedAddresses, listOfEmails, removedEmails);

    }
</script>

<style scoped>

    .message {
        padding: 0.875rem 1rem;
        border-radius: 6px;
        font-size: 0.875rem;
        font-weight: 500;
    }

    .message.success {
        background-color: #d1fae5;
        color: #065f46;
        border: 1px solid #a7f3d0;
    }

    .message.error {
        background-color: #fee2e2;
        color: #991b1b;
        border: 1px solid #fecaca;
    }

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
        border-radius: 5vh;
    }
    .customerInfoWindow{
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
    .addressHeader{
        margin-bottom: 2%;
        justify-content: space-between;
        align-items: center;
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
    .p-inputnumber-input{
        width: 25px;
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
    .addressField{
        margin-bottom: 5%;
        height: 100vh;    
    }
    .addressHeader{
        margin-bottom: 2%;
        justify-content: space-between;
        align-items: center;
    }
    .editButton{
        font-size: 1.1rem;
        height: 100%;
    }
    .editButton:hover{
        scale: 1.25;
    }
    .addAddress{
        height: 5vh;
        margin-bottom: 5%;
    }
    .contactMethod{
        display: flex;
        justify-content:start;
        gap: 5%;
        margin-bottom: 2vh;
    }
    
    .defaultColor{
        background-color: rgb(255, 255, 255);
        color: black;
    }
    .invertColor{
        background-color: rgb(0, 0, 0);  
        color: white;
    }

    @media (prefers-color-scheme: dark) {
        .defaultColor{
            background-color: rgb(16, 16, 16);   
            color: white;
        }
        .invertColor{
            background-color: rgb(255, 255, 255);
            color: black;
        }
    }
    
    
</style>