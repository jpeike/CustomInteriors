<template>
    <div class="flex row defaultColor scrollBar">
        <div class="flex column customerInfoWindow">
            <div class="flex row customerInfoBar">
                <div class="flex column customerInfoTitle">
                    <h2 class="titleText">{{ title }}</h2>
                </div>
            </div>

            <div class="title">
                <h3 class="fieldTitle">Title *</h3>
                <InputText v-model="job.jobDescription" required="true" type="text" class="inputValue" :placeholder="props.currentJobInformation?.jobDescription"></InputText>
            </div>
            
            <div class="flex row multipleFields">
              <div style="width: 50%;">
                  <h3 class="fieldTitle">Start Date</h3>
                  <DatePicker v-model="job.startDate" type="text" class="inputValue"></DatePicker>
              </div>
              <div style="width: 50%;">
                  <h3 class="fieldTitle">End Date</h3>
                  <DatePicker v-model="job.endDate" type="text" class="inputValue"></DatePicker>
              </div>
            </div>

            <div class="flex row multipleFields">
              <div style="width: 50%;">
                  <h3 class="fieldTitle">Customer Name *</h3>
                  <Select v-model="job.customerId" 
                    :options="props.customers" :optionLabel="getCustomerFullName" 
                    optionValue="customerId"
                    placeholder="Select a Customer"
                    style="width: 100%;">
                  </Select>
              </div>
              <div style="width: 50%;">
                  <h3 class="fieldTitle">Status</h3>
                  <Select v-model="job.status" 
                    :options="statusOptions"
                    optionValue="status"
                    optionLabel="status"
                    placeholder="Pending"
                    style="width: 100%;">
                  </Select>
              </div>
            </div>

            <div class="flex row buttons">
                <button class = "cancelUpdateButton" @click="$emit('closePage')">
                    <p class="buttonText">Cancel</p>
                </button>
                <button class = "updateInfoButton" @click="testInfo()">
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
.title button,
.addAddress button,
.editButton {
  display: flex;
  justify-content: center;
  align-items: center;
  text-align: center;
}

.title{
  margin-bottom: 2vh;
}
.cancelUpdateButton p.buttonText,
.updateInfoButton p.buttonText,
.title button p,
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
  gap: 1rem;
  margin-bottom: 2vh;
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
    import { Client, JobModel, AddressModel, EmailModel, CustomerModel} from '../../client/client'
    import InputText from 'primevue/inputtext';
    import {reactive, ref} from 'vue'
    import { useToast } from '@/composables/useToast.ts'
    import { DatePicker } from 'primevue';
    import JobsCard from './JobsCard.vue';
    import Select from 'primevue/select';

    const { showWarning } = useToast()

    const props = defineProps<{
        currentJobInformation: JobModel | undefined,
        customers: CustomerModel[] | undefined,
        title: String,
        description: String,
        buttonDesctipnion: String
    }>();

    const emit = defineEmits<{
        closePage: []
        updateJobInformation: [job: JobModel]
    }>()

    const state = reactive({
        loading: false,
    })    

    const message = ref('');
    const job = ref(new JobModel);
    const statusOptions = ref([
      { status: 'Completed'},
      { status: 'In Progress'},
      { status: 'Past Due'},
      { status: 'Pending'}
    ]);
    
    const customersList = ref([new CustomerModel])
    const testDate = ref(new Date)

    if (props.currentJobInformation != undefined){
      var temp = JSON.stringify(props.currentJobInformation);
      job.value = JSON.parse(temp);
      job.value.startDate = new Date(job.value.startDate!);
      job.value.endDate = new Date(job.value.endDate!)

    }
    if (props.customers != undefined){
      for (let i = 0; i < props.customers.length; i++){
        customersList.value.push(props.customers[i])
      }
    }

    function testInfo(){
      const todaysDate = new Date();
      
      if (!job.value.jobDescription){
        showWarning('This job needs a title');
        return;
      }

      if (job.value.endDate! <= job.value.startDate!){
        showWarning('End date must be after the start date');
        return;
      }

      if (job.value.status == "In Progress" && job.value.endDate! < todaysDate){
        showWarning("End date must be after today's date for in progress jobs")
        return
      }

      if (job.value.status == "In Progress" && job.value.startDate! > todaysDate){
        showWarning("Start date must be before today's date for in progress jobs")
        return
      }

      if (job.value.status == undefined){
        job.value.status = "Pending";
      }

      checkForNoChanges();
      message.value = 'Job Successfully Created'
      emit('updateJobInformation', job.value);
    }

    function checkForNoChanges(){
      if (props.currentJobInformation != undefined){
        if (JSON.stringify(job.value) == JSON.stringify(props.currentJobInformation)){
          job.value.jobId = -1;
        }
      }
    }

    const getCustomerFullName = (option: { firstName: any; lastName: any; }) => {
      return `${option.firstName} ${option.lastName}`;   
    }
</script>

