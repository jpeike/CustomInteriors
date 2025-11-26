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
                    <InputText v-model="customer.firstName" required="true" type="text" class="inputValue" :placeholder="props.currentCustomerInformation?.firstName" data-testid="customerFormFirstName"></InputText>
                </div>
                <div>
                    <h3 class="fieldTitle">Last Name*</h3>
                    <InputText v-model="customer.lastName" required="true" type="text" class="inputValue" :placeholder="props.currentCustomerInformation?.lastName" data-testid="customerFormLastName"></InputText>
                </div>
            </div>
            <div>
                <h3 class="fieldTitle">Customer Type*</h3>
                <InputText v-model="customer.customerType" required="true" type="text" class="inputValue" :placeholder="props.currentCustomerInformation?.customerType" data-testid="customerFormType"></InputText>
            </div>
                        
        <!--Contact-->
            <div style="margin-bottom: 5%;">
                <h3 class="fieldTitle">Preffered Contact Method</h3>
                <div class="contactMethod">                    
                    <div>
                        <input :checked="currentCustomerInformation?.prefferedContactMethod == 'Email'" type="radio" class = "contactMethodRadio" v-model="customer.prefferedContactMethod" id = "Email" name="contactMethod" value ="Email" variant="filled" data-testid="customerFormEmailRadioButton"/>
                        <label for="Email">Email</label>
                    </div>
                    <div>
                        <input :checked="currentCustomerInformation?.prefferedContactMethod == 'Phone'" type="radio" class = "contactMethodRadio" v-model="customer.prefferedContactMethod" id = "Phone" name="contactMethod" value ="Phone" data-testid="customerFormPhoneRadioButton"/>
                        <label for="Phone">Phone</label>
                    </div>
                </div>
            </div>
            
            <div style="margin-bottom: 5%;" v-for="(emailsAdresses, index) in state.listOfEmails">
                
                <div class="flex row addressHeader">
                    <h2 style="margin: 0;">Email {{ index + 1 }}</h2>
                    <i class="pi pi-trash editButton" @click="deleteEmail(state.listOfEmails[index]); state.listOfEmails.splice(index, 1);"></i>
                    </div>
                <div class="flex row multipleFields">
                    <div>
                        <h3 class="fieldTitle">Address * </h3>
                        <InputText v-model="emailsAdresses.emailAddress" type="text" class="inputValue" :placeholder="state.listOfEmails[index].emailAddress" data-testid="customerFormEmailAddress"></InputText>
                    </div>
                    <div>
                        <h3 class="fieldTitle">Type * </h3>
                        <InputText v-model="emailsAdresses.emailType" type="text" class="inputValue" :placeholder="state.listOfEmails[index].emailType" data-testid="customerFormEmailType"></InputText>
                    </div>  
                </div>                  
            </div>
            <div class="addAddress">
                <button @click="addEmail" class="cancelUpdateButton"> <p style="margin: 0; text-align: center;">Add Email</p></button>
            </div>

        <!--Address Section-->
            <div class="addressField" v-for="(address, index) in state.listOfAddresses">
                <div class="flex row addressHeader">
                    <h2 style="margin: 0;">Address {{ index + 1 }}</h2>
                    <i class="pi pi-trash editButton" @click="deleteAddress(state.listOfAddresses[index], index); "></i>
                </div>
                <div>
                    <h3 class="fieldTitle">Street Address *</h3>
                    <InputText v-model="address.street" required="true" type="text" class="inputValue" :placeholder="address.street ?? 'Street'" data-testid="customerFormStreet"></InputText>
                </div>
                <div>
                    <h3 class="fieldTitle">Country</h3>
                    <InputText v-model="address.country" type="text" class="inputValue" :placeholder="address.country ?? 'Country'" data-testid="customerFormCountry"></InputText>
                </div>
                <div class="flex row multipleFields">
                    <div class="tripleField">
                        <h3 class="fieldTitle">City *</h3>
                        <InputText v-model="address.city" required="true" type="text" class="inputValue" :placeholder="address.city ?? 'City'" data-testid="customerFormCity"></InputText>
                    </div>
                    <div class="tripleField">
                        <h3 class="fieldTitle">State *</h3>
                        <InputText v-model="address.state" required="true" type="text" class="inputValue" :placeholder="address.state ?? 'State'" data-testid="customerFormState"></InputText>
                    </div>
                    <div class="tripleField">
                        <h3 class="fieldTitle">Zip *</h3>
                        <input v-model="address.postalCode" required="true" class="p-inputtext p-component inputValue" :placeholder="address.postalCode?.toString()" data-testid="customerFormPostalCode"></input>
                    </div>
                </div> 
                 <div>
                    <h3 class="fieldTitle">Address Type *</h3>
                    <InputText v-model="address.addressType" required="true" type="text" class="inputValue" :placeholder="address.addressType ?? 'Address Type'" data-testid="customerFormAddressType"></InputText>
                </div>               
            </div>

            <div class="addAddress">
                <button @click="addAddress" class="cancelUpdateButton"> <p style="margin: 0; text-align: center;">Add Address</p></button>
            </div>

        <!--Company Name-->
            <div>
                <h3 class="fieldTitle">Company Name</h3>
                <InputText v-model="customer.companyName" type="text" class="inputValue" :placeholder="props.currentCustomerInformation?.companyName" data-testid="customerFormCompany"></InputText>
            </div>
            <div>
                <h3 class="fieldTitle">Status</h3>
                <InputText v-model="customer.status" type="text" class="inputValue" :placeholder="props.currentCustomerInformation?.status" data-testid="customerFormStatus"></InputText>
            </div>
            <div class="notesField">
                <h3 class="fieldTitle">Notes</h3>
                <textarea v-model="customer.customerNotes" type="text" class="p-inputtext p-component inputValue notes" :placeholder="props.currentCustomerInformation?.customerNotes" data-testid="customerFormNotes"></textarea>
            </div>

            <div class="flex row buttons">
                <button class = "cancelUpdateButton" @click="$emit('closePage')">
                    <p style="margin: 0; text-align: center;">Cancel</p>
                </button>  
                <button class = "updateInfoButton" @click="testInfo()" data-testid="updateFormButton">
                    <p style="margin: 0; text-align: center;">{{buttonDesctipnion}}</p>
                </button>
            </div>
            
        </div>
        <button class = "exitButton" @click="$emit('closePage')"><h4 style="margin: 0;">X</h4></button>
    </div>
</template>

<script setup lang="ts">
    import 'primeicons/primeicons.css';
    import { Client, CustomerModel, AddressModel, EmailModel} from '../../client/client'
    import InputText from 'primevue/inputtext';
    import {reactive, ref} from 'vue'
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
        updateCustomerInformation: [customer: CustomerModel, listOfAddresses: AddressModel[], removedAddresses: number[], listOfEmails: EmailModel[], removedEmails: number[]]
    }>()

    const state = reactive({
        loading: false,
        listOfEmails: ref([new EmailModel]),
        listOfAddresses: ref([new AddressModel])
    })

    const newEmail = ref('');
    const newPhone = ref('');
    const message = ref('');
    const customer = ref(new CustomerModel);;
    
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    let removedAddresses = [0];    
    let removedEmails = [0];

    if (props.currentCustomerInformation != undefined){
        var temp = JSON.stringify(props.currentCustomerInformation);
        customer.value = JSON.parse(temp);
    }

    if (props.currentAddresses != undefined){
        var temp = JSON.stringify(props.currentAddresses);
        state.listOfAddresses = JSON.parse(temp);
    }

    if (props.currentEmails != undefined){
        var temp = JSON.stringify(props.currentEmails);
        state.listOfEmails = JSON.parse(temp);
    }
    
    function addAddress(){
        state.listOfAddresses.push(new AddressModel);
    }

    function deleteAddress(address: AddressModel, index: number){
        state.listOfAddresses.splice(index, 1);
        if (address.addressId! != undefined){
            removedAddresses.push(address.addressId);
        }    }

    function addEmail(){
        state.listOfEmails.push(new EmailModel);
    }

    function deleteEmail(email: EmailModel){
        if (email.emailId! != undefined){
            removedEmails.push(email.emailId);
        }
    }

    function testInfo(){
        if (!customer.value.firstName || !customer.value.lastName || !customer.value.customerType){
            showWarning('Customer information not valid');
            return;
        }
        
        for (let i = 0; i < state.listOfEmails.length; i++){
            if (!emailRegex.test(state.listOfEmails[i].emailAddress!)){
                showWarning("Email " + (i+1) + " 's address is not valid")
                return;
            }
            if (!state.listOfEmails[i].emailAddress || !state.listOfEmails[i].emailType){
                showWarning("Email " + (i+1) + " has one or more fields that are not valid")
                return;
            }
        }
    
        for (let i = 0; i < state.listOfAddresses.length; i++){
            if (!state.listOfAddresses[i].city || !state.listOfAddresses[i].postalCode || !state.listOfAddresses[i].addressType || !state.listOfAddresses[i].state){
                showWarning("Address " + (i+1) + " has one or more fields that are not valid");
                return;
            }

            if (!/^\d{5}$/.test(state.listOfAddresses[i].postalCode?.toString()!) || state.listOfAddresses[i].postalCode! < 10000){
                showWarning("Address " + (i + 1) + " ZIP Code must be a 5 digit integer");
                return;
            }
        }
        
        checkForNoChanges();
        
        message.value = 'Address Successfully Created'
        console.log(customer.value);
        emit('updateCustomerInformation', customer.value, state.listOfAddresses, removedAddresses, state.listOfEmails, removedEmails);
    }

    function checkForNoChanges(){
        if (props.currentCustomerInformation != undefined){
            if (JSON.stringify(customer.value) == JSON.stringify(props.currentCustomerInformation)){
                customer.value.customerId = -1;
            }
        }

        for (let i = 0; i < state.listOfAddresses.length; i++){
            if (props.currentAddresses != undefined){
                if (JSON.stringify(state.listOfAddresses[i]) == JSON.stringify(props.currentAddresses[i])){
                    state.listOfAddresses.splice(i, 1);
                }
            }
        }

        for (let i = 0; i < state.listOfEmails.length; i++){
            if (props.currentEmails != undefined){
                if (JSON.stringify(state.listOfEmails[i]) == JSON.stringify(props.currentEmails[i])){
                    state.listOfEmails.splice(i, 1);
                }
            }
        }
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