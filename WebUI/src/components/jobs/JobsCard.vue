<template>
    <Card class="mb-3">
        <template #header>
            <div class="flex row customCardHeader">
                <p style="margin: 0; flex-grow: 2; font-size: 1.2rem; font-weight: bold;">{{ job.jobDescription }}</p>

                <div class="flex row" style="justify-content: left; flex-grow: 0; gap: 15%">
                    <i class="pi pi-pen-to-square editButton" style="font-size: 1.1rem"
                        @click="emit('edit', job)" ></i>
                    <i class="pi pi-trash editButton" style="font-size: 1.1rem"
                        @click="emit('delete', job)" ></i>
                </div>
            </div>
        </template>
        <template #content>
            <p style="margin: 0;">Start Date: 
              {{getStartDate()}}
            </p>
            <br/>
            <p style="margin: 0;">End Date: 
              {{getEndDate()}}
            </p>

            <br/>
            <p style="margin: 0;"> Status: <span :style="{ color: statusColor() }">{{ job.status }}</span></p>
        </template>
    </Card>
</template>

<script setup lang="ts">
import Card from 'primevue/card'
import type {JobModel} from '@/client/client'
import { ref } from 'vue';

const props = defineProps<{
    job: JobModel
}>()

const emit = defineEmits<{
    edit: [job: JobModel]
    delete: [job: JobModel]
}>()

function statusColor() {
  if (props.job.status?.toLocaleLowerCase() == "completed"){
    return getComputedStyle(document.documentElement).getPropertyValue('--chart-2');
  }
  else if (props.job.status?.toLocaleLowerCase() == "in progress"){
    return getComputedStyle(document.documentElement).getPropertyValue('--chart-1');
  }
  else if (props.job.status?.toLocaleLowerCase() == "past due"){
    return getComputedStyle(document.documentElement).getPropertyValue('--chart-4');
  }
  else {
    return getComputedStyle(document.documentElement).getPropertyValue('--chart-3');
  }
};

function getStartDate(){
  if (props.job.startDate != null){
    return props.job.startDate?.toLocaleDateString()
  }

  return ("No Start Date")
}

function getEndDate(){
    if (props.job.endDate != null){
      return props.job.endDate?.toLocaleDateString()
    }
    return ("No End Date")
}

</script>

<style scoped>
  .flex{
    display: flex;
  }

  .row{
    flex-direction: row;
  }

  .mb-3{
    border: solid;
    border-width: 1px;
    border-color: rgb(222, 222, 222);
    border-radius: 10px;
    width: 23.5%;
    height: 50vh;
    margin-bottom: 2%;
    overflow: scroll;
  }
  .customCardHeader{
    padding: 10%;
    padding-bottom: 0;
    gap: 7%;
    justify-content: space-between;
  }
  .editButton:hover{
    scale: 1.25;
  }
</style>
