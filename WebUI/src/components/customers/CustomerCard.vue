<template>
    <Card class="mb-3" data-testid="customerCard">
        <template #header>
            <div class="flex row customCardHeader">
                <p style="margin: 0; flex-grow: 2; font-size: 1.2rem; font-weight: bold;" data-testid="customerName">{{ customer.firstName }} {{
                    customer.lastName }}</p>

                <div class="flex row" style="justify-content: left; flex-grow: 0; gap: 15%">
                    <i class="pi pi-pen-to-square editButton" style="font-size: 1.1rem"
                        @click="emit('edit', customer)" data-testid="updateCustomerButton"></i>
                    <i class="pi pi-trash editButton" style="font-size: 1.1rem"
                        @click="emit('delete', customer)" data-testid="deleteCustomerButton"></i>
                </div>
            </div>
        </template>
        <template #content>
            <div class="flex row" style="gap:5%;">
                <div>
                    <p style="margin: 0;">Contact: </p>
                </div>
                <div v-if="customer.prefferedContactMethod == 'Email'" data-testid="customerEmail">
                    {{ email }}
                </div>
                <div v-else-if="customer.prefferedContactMethod == 'Phone'" data-testid="customerPhone">
                    Temp Phone
                </div>
                <div v-else>
                    Not Listed
                </div>
            </div>

            <br />

            <p style="margin: 0;" data-testid="customerAddress">Address: {{ address }}</p>
            <br />
            <p style="margin: 0;" data-testid="customerType">Type: {{ customer.customerType }}</p>
            <br />
            <p style="margin: 0;" data-testid="customerCompany">Company: {{ customer.companyName }}</p>
            <br />
            <p style="margin: 0;" data-testid="customerStatus">Status: {{ customer.status }}</p>
            <br />
            <p style="margin: 0;" data-testid="customerNotes">Notes: {{ customer.customerNotes }}</p>
        </template>
    </Card>
</template>

<script setup lang="ts">
import Card from 'primevue/card'
import type { CustomerModel } from '@/client/client'

const props = defineProps<{
    customer: CustomerModel
    email: string | undefined
    address: string | undefined
}>()

const emit = defineEmits<{
    edit: [customer: CustomerModel]
    delete: [customer: CustomerModel]
}>()

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