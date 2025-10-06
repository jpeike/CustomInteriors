<template>
    <div class = "flex row defaultColor deleteWindow">
        <div class="flex column deleteWindowBody">
            <p style="margin: 0;">Are you sure you want to delete <span class="title">{{title}}</span>?</p>
            
            <div class="flex row buttons">
                <button class = "cancelUpdateButton" @click="$emit('closePage')">
                    <p style="margin: 0; text-align: center;">Cancel</p>
                </button>  
                <button class = "updateInfoButton" @click="$emit('deleteCustomer', currentCustomerInformation?.customerId)">
                    <p style="margin: 0; text-align: center;">Delete</p>
                </button>
            </div>
        </div>
        <button class = "exitButton" @click="$emit('closePage')"><h4 style="margin: 0;">X</h4></button>
    </div>
</template>


<script setup lang="ts">
    import { Client, CustomerModel } from '../client/client'

    const props = defineProps({
       currentCustomerInformation: CustomerModel,
       title: String
    });

    console.log(props.currentCustomerInformation?.customerId);
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
    .deleteWindow{
        padding: 3%;
        gap: 5%;
        width: 30vw;
        height: 30vh; 
        border-radius: 5vh;
        justify-content: space-between;
    }
    .deleteWindowBody{
        justify-content: space-between;
    }
    .title{
        font-weight: bold;
    }
    .buttons{
        width: 100%;
        justify-content: end;
        gap: 5%;
    }
    .updateInfoButton{
        width: 50%;
        height: 5vh;
        border: none;
        align-content: center;
        border-radius: 7px;
    }
    .cancelUpdateButton{
        width: 50%;
        height: 5vh;
        border: none;
        align-content: center;
        border-radius: 7px;
    }
    .exitButton{
        background: none;
        border: none;
        height: fit-content;
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