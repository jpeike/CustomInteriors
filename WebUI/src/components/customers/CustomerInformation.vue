<template>
    <div class="flex row defaultColor scrollBar">
        <div class="flex column customerInfoWindow" data-testid="customerForm">
            <div class="flex row customerInfoBar">
                <div class="flex column customerInfoTitle">
                    <h2 class="titleText">{{ title }}</h2>
                    <p class="subtitleText">{{ description }}</p>
                </div>
            </div>

            <div class="flex row multipleFields">
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
            <div class="sectionMargin">
                <h3 class="fieldTitle">Preffered Contact Method</h3>
                <div class="contactMethod">
                    <div>
                        <input :checked="currentCustomerInformation?.prefferedContactMethod == 'Email'" type="radio"
                            class="contactMethodRadio" v-model="customer.prefferedContactMethod" id="Email"
                            name="contactMethod" value="Email" variant="filled" data-testid="customerFormEmailRadioButton" />
                        <label for="Email">Email</label>
                    </div>
                    <div>
                        <input :checked="currentCustomerInformation?.prefferedContactMethod == 'Phone'" type="radio"
                            class="contactMethodRadio" v-model="customer.prefferedContactMethod" id="Phone"
                            name="contactMethod" value="Phone" data-testid="customerFormPhoneRadioButton" />
                        <label for="Phone">Phone</label>
                    </div>
                </div>
            </div>

            <div class="sectionMargin" v-for="(emailsAdresses, index) in state.listOfEmails" data-testid="customerEmailForm" :data-email-id="emailsAdresses.emailId">

                <div class="flex row addressHeader">
                    <h2 class="sectionHeader">Email {{ index + 1 }}</h2>
                    <i class="pi pi-trash editButton" @click="deleteEmail(state.listOfEmails[index]); state.listOfEmails.splice(index, 1);" data-testid="removeEmailButton"></i>
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

            <div class="addEmail">
                <button @click="addEmail" class="cancelUpdateButton">
                    <p class="buttonText" data-testid="addEmailButton">Add Email</p>
                </button>
            </div>

        <!--Address Section-->
            <div class="addressField" v-for="(address, index) in state.listOfAddresses" data-testid="customerAddressForm" :data-address-id="address.addressId">
                <div class="flex row addressHeader">
                    <h2 class="sectionHeader">Address {{ index + 1 }}</h2>
                    <i class="pi pi-trash editButton" @click="deleteAddress(state.listOfAddresses[index], index); " data-testid="removeAddressButton"></i>
                </div>

                <div>
                    <h3 class="fieldTitle">Street Address *</h3>
                    <InputText v-model="address.street" required type="text" class="inputValue"
                        :placeholder="address.street ?? 'Street'" data-testid="customerFormStreet" />
                </div>
                <div>
                    <h3 class="fieldTitle">Country</h3>
                    <InputText v-model="address.country" type="text" class="inputValue"
                        :placeholder="address.country ?? 'Country'" data-testid="customerFormCountry" />
                </div>

                <div class="flex row multipleFields">
                    <div class="tripleField">
                        <h3 class="fieldTitle">City *</h3>
                        <InputText v-model="address.city" required type="text" class="inputValue"
                            :placeholder="address.city ?? 'City'" data-testid="customerFormCity" />
                    </div>
                    <div class="tripleField">
                        <h3 class="fieldTitle">State *</h3>
                        <InputText v-model="address.state" required type="text" class="inputValue"
                            :placeholder="address.state ?? 'State'" data-testid="customerFormState" />
                    </div>
                    <div class="tripleField">
                        <h3 class="fieldTitle">Zip *</h3>
                        <input v-model="address.postalCode" required class="p-inputtext p-component inputValue"
                            :placeholder="address.postalCode?.toString()" data-testid="customerFormPostalCode" />
                    </div>
                </div>

                <div>
                    <h3 class="fieldTitle">Address Type *</h3>
                    <InputText v-model="address.addressType" required type="text" class="inputValue"
                        :placeholder="address.addressType ?? 'Address Type'" data-testid="customerFormAddressType" />
                </div>
            </div>

            <div class="addAddress">
                <button @click="addAddress" class="cancelUpdateButton">
                    <p class="buttonText" data-testid="addAddressButton">Add Address</p>
                </button>
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
                <button class = "cancelUpdateButton" @click="$emit('closePage')" data-testid="cancelFormButton">
                    <p class="buttonText">Cancel</p>
                </button>
                <button class = "updateInfoButton" @click="testInfo()" data-testid="updateFormButton">
                    <p class="buttonText">{{buttonDesctipnion}}</p>
                </button>
            </div>
        </div>

        <button class="exitButton" @click="$emit('closePage')">
            <h4 class="exitText">X</h4>
        </button>
    </div>
</template>

<style scoped>
  /* Message Styles */
  .message {
  padding: 0.875rem 1rem;
  border-radius: var(--radius-sm);
  font-size: 0.875rem;
  font-weight: var(--font-weight-medium);
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

  /* Flex Helpers */
  .flex {
  display: flex;
  }
  .row {
  flex-direction: row;
  }
  .column {
  flex-direction: column;
  }

  /* Scroll & Layout */
  .scrollBar {
  padding: 4%;
  width: 50vw;
  height: 80vh;
  border-radius: var(--radius-lg);
  }
  .customerInfoWindow {
  overflow: auto;
  height: 100%;
  width: 100%;
  padding-right: 5%;
  }
  .customerInfoBar {
  justify-content: space-between;
  }
  .customerInfoTitle {
  margin-bottom: 2vh;
  }

  /* Address & Email Headers */
  .addressHeader {
  margin-bottom: 2%;
  justify-content: space-between;
  align-items: center;
  }

  /* Buttons & Controls */
  .exitButton {
  background: none;
  border: none;
  height: fit-content;
  }
  .editButton {
  font-size: 1.1rem;
  height: 100%;
  }
  .editButton:hover {
  transform: scale(1.25);
  }
  .addAddress, .addEmail {
  width: 100%;
  margin-top: 0;
  }
.addressField:last-child h3.fieldTitle {
  margin-bottom: 0;
}
.buttons {
  display: flex;
  gap: 1rem;
  width: 100%;
}
.buttons button {
  flex: 1;
  height: 3rem;
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 0;
  border-radius: 5px;
  font-size: 1rem;
  cursor: pointer;
  border: none;
}
.buttons button p.buttonText {
  margin: 0;
  text-align: center;
  line-height: 1;
  width: 100%;
}
  .updateInfoButton {
  width: 20%;
  height: 5vh;
  border: none;
  border-radius: var(--radius-md);
  background-color: var(--primary);
  color: var(--primary-foreground);
  }
.cancelUpdateButton {
  width: 100%;
  padding: 0.75rem 1rem;
  border-radius: 5px;
  text-align: center;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: var(--primary);
  color: var(--primary-foreground);
  cursor: pointer;
  border: none;
  font-size: 1rem;
}
.buttons {
  margin-top: 1rem; /* adjust as needed */
}
.cancelUpdateButton p.buttonText {
  margin: 0;
}
.cancelUpdateButton,
.updateInfoButton,
.addEmail button,
.addAddress button,
.editButton {
  display: flex;
  justify-content: center;
  align-items: center;
  text-align: center;
}
.cancelUpdateButton p.buttonText,
.updateInfoButton p.buttonText,
.addEmail button p,
.addAddress button p {
  margin: 0;
  width: 100%;
  text-align: center;
  line-height: 1;
  display: block; /* or flex if you want */
}
  /* Input & Form Fields */
  .multipleFields {
  justify-content: space-between;
  }
  .fieldTitle {
  margin: 0vh;
  margin-bottom: 1vh;
  }
  .inputValue {
  width: 100%;
  }
  .p-inputnumber-input {
  width: 25px;
  }
  .tripleField {
  width: 30%;
  }
  .notes {
  height: 10vh;
  resize: none;
  }

  /* Contact Method */
  .contactMethod {
  display: flex;
  justify-content: start;
  gap: 5%;
  margin-bottom: 2vh;
  }

  /* Addresses */
  .addressField {
  margin-bottom: 5%;
  }

  /* Theme Colors */
  .defaultColor {
  background-color: var(--card);
  color: var(--foreground);
  }
  .invertColor {
  background-color: var(--foreground);
  color: var(--card);
  }

  /* Dark Mode Overrides */
  @media (prefers-color-scheme: dark) {
  .defaultColor {
  background-color: var(--card);
  color: var(--foreground);
  }
  .invertColor {
  background-color: var(--foreground);
  color: var(--card);
  }
  }

</style>


<script setup lang="ts">
    import 'primeicons/primeicons.css';
    import { CustomerModel, AddressModel, EmailModel} from '../../client/client'
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
                for (let j = i; j < props.currentAddresses.length; j++){
                    if (JSON.stringify(state.listOfAddresses[i]) == JSON.stringify(props.currentAddresses[j])){
                        state.listOfAddresses.splice(i, 1);
                    }
                }
            }
        }

        for (let i = 0; i < state.listOfEmails.length; i++){
            if (props.currentEmails != undefined){
                for (let j = i; j < props.currentEmails.length; j++){
                    if (JSON.stringify(state.listOfEmails[i]) == JSON.stringify(props.currentEmails[j])){
                        state.listOfEmails.splice(i, 1);
                    }
                }
            }
        }
    }
</script>

