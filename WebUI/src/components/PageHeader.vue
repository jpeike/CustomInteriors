<template>
    <div class="pageHeader">
        <div class = "leftPanel">
            <h2 style="margin: 0;">{{title}}</h2>
            <p style="margin: 0;">{{description}}</p>
        </div>
        <div class = "rightPanel">
            <button class = "addButton">
                <p style="margin: 0; text-align: center; color: white;">+ {{add}}</p>
            </button>
        </div>
    </div>
</template>

<script setup lang="ts">
    defineProps(['title', 'description', 'add'])
    import 'primeicons/primeicons.css'
</script>

<style scoped>
    .pageHeader{
        height: 15vh;
        width: 100%;

        display: flex;
        flex-direction: row;
        justify-content: space-between;
    }
    .leftPanel{
        flex-grow: 5;
        display: flex;
        flex-direction: column;
    }
    .rightPanel{
        flex-grow: 1;
        align-content: center;
        text-align: right;
    }
    .addButton{
        background-color: rgb(0, 0, 0);
        border: none;
        height: 50%;
        width: 75%;
        align-content: center;
        border-radius: 7px;
    }
</style>