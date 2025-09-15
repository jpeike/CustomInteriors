<template>
  <div class="not-found">
    <h1>404 - Page Not Found</h1>
    <p>Sorry, the page you are looking for does not exist.</p>
    <router-link to="/">Go back home</router-link>
  </div>
</template>

<style scoped>
.not-found {
  text-align: center;
  margin-top: 4rem;
  font-family: Arial, sans-serif;
}
h1 {
  font-size: 3rem;
  margin-bottom: 1rem;
}
p {
  font-size: 1.25rem;
  margin-bottom: 2rem;
}
</style>
