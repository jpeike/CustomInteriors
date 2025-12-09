<template>
    <div class = "flex row defaultColor deleteWindow">
        <div class="flex column deleteWindowBody">
            <p style="margin: 0;">Are you sure you want to delete <span class="title">{{title}}</span>?</p>
            <div class="flex row buttons">
              <button class="cancelUpdateButton" @click="$emit('closePage')" data-testid="deleteCancelButton">
                <p class="buttonText">Cancel</p>
              </button>
              <button class="updateInfoButton" @click="$emit('deleteCustomer', currentInfo?.customerId | currentInfo?.jobId)" data-testid="deleteConfirmationButton">
                <p class="buttonText">Delete</p>
              </button>
            </div>
          </div>
      <button class="exitButton" @click="$emit('closePage')"> <h4 class="exitText">X</h4> </button>
    </div>
</template>

<script setup lang="ts">
    import { CustomerModel, JobModel } from '../client/client'

    const props = defineProps({
       currentInfo: Object,
       title: String
    });

    console.log(props.currentInfo?.customerId);
</script>


<style scoped>
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

/* Delete Confirmation Window */
.deleteWindow {
padding: 3%;
gap: 5%;
width: 30vw;
height: 30vh;
border-radius: var(--radius-lg);
justify-content: space-between;
}

/* Window Body */
.deleteWindowBody {
justify-content: space-between;
}

/* Title Text */
.title {
font-weight: var(--font-weight-medium);
}

/* Buttons */
.buttons {
width: 100%;
justify-content: flex-end;
gap: 5%;
}
.updateInfoButton {
width: 50%;
height: 5vh;
border: none;
border-radius: var(--radius-md);
background-color: var(--destructive);
color: var(--destructive-foreground);
}
.cancelUpdateButton {
width: 50%;
height: 5vh;
border: none;
border-radius: var(--radius-md);
background-color: var(--secondary);
color: var(--foreground);
}

/* Exit Button */
.exitButton {
background: none;
border: none;
height: fit-content;
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
.buttonText {
margin: 0;
text-align: center;
font-weight: var(--font-weight-medium);
}

.exitText {
margin: 0;
font-weight: var(--font-weight-medium);
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
