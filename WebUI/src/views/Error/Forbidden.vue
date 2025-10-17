<template>
  <div class="flex column forbiddenBody">
    <div class="forbiddenContainer">
      <div class="errorIcon">
        <i class="pi pi-exclamation-circle" style="font-size: 7.5rem;"></i>
      </div>
      
      <h1 class="errorCode">403</h1>
      <h2 class="errorTitle">Forbidden</h2>
      <p class="errorDescription">Sorry, you don't have the proper credentials to access the page.</p>
      
      <router-link to="/" class="homeButton"><i class="pi pi-home"></i>Go Back Home</router-link>
    </div>
  </div>
</template>

<script setup lang="ts">
import 'primeicons/primeicons.css'
</script>

<style scoped>
.flex {
  display: flex;
}

.row {
  flex-direction: row;
}

.column {
  flex-direction: column;
}

.forbiddenBody {
  width: auto;
  height: 90vh;
  top: 0;
  left: 0;
  justify-content: center;
  align-items: center;
  background: white;
}

.forbiddenContainer {
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
  max-width: 500px;
  padding: 3rem 2rem;
}

.errorIcon {
  color: #d4d4d4;
  margin-bottom: 1.5rem;
  font-size: 5rem;
}

.errorCode {
  font-size: 4rem;
  font-weight: 700;
  color: #000;
  margin: 0 0 1rem 0;
  letter-spacing: -0.02em;
}

.errorTitle {
  font-size: 1.5rem;
  font-weight: 600;
  color: #000;
  margin: 0 0 0.75rem 0;
}

.errorDescription {
  font-size: 0.9375rem;
  color: #737373;
  margin: 0 0 2rem 0;
  line-height: 1.6;
}

.homeButton {
  display: inline-flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.75rem 1.5rem;
  background-color: #000;
  color: white;
  border: none;
  border-radius: 6px;
  font-size: 0.875rem;
  font-weight: 500;
  text-decoration: none;
  cursor: pointer;
  transition: background-color 0.15s;
}

.homeButton:hover {
  background-color: #262626;
}

/* Dark mode support */
@media (prefers-color-scheme: dark) {
  .forbiddenBody {
    background: #1a1a1a;
  }

  .errorIcon {
    color: #404040;
  }

  .errorCode {
    color: #ffffff;
  }

  .errorTitle {
    color: #ffffff;
  }

  .errorDescription {
    color: #a3a3a3;
  }

  .homeButton {
    background-color: #ffffff;
    color: #000;
  }

  .homeButton:hover {
    background-color: #e5e5e5;
  }
}
</style>