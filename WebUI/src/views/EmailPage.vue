<template>
  <div>
    <h1>Email Page</h1>
    <p>This is the email page.</p>
  </div>

  <div v-if="!state.loading" class="container">
    <div class="row">
      <div
        v-for="group in groupedEmails"
        :key="group.customerId"
        class="col-md-4 mb-4 d-flex"
      >
        <Card class="w-100" style="border: 1px solid black">
          <template #title>
            Customer ID: {{ group.customerId }}
          </template>
          <template #content>
            <div v-for="email in group.emails" :key="email.emailID" class="mb-2">
              <p><strong>Email ID:</strong> {{ email.emailID }}</p>
              <p><strong>Email Address:</strong> {{ email.emailAddress }}</p>
              <p><strong>Email Type:</strong> {{ email.emailType }}</p>
              <p><strong>Created On:</strong> {{ email.createdOn }}</p>
              <hr v-if="group.emails.length > 1" />
            </div>
          </template>
        </Card>
      </div>
    </div>
  </div>

  <div v-else-if="state.error">{{ state.error }}</div>
  <div v-else>
    <p>Loading...</p>
  </div>
</template>

<script setup lang="ts">
import Card from 'primevue/card'

import { Client, EmailModel } from '../client/client'
// If the file does not exist, create it or correct the path as needed.
import { onMounted, reactive, computed } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const client = new Client(import.meta.env.VITE_API_BASE_URL)

const state = reactive({
  emails: [] as EmailModel[],
  loading: false,
  error: null as string | null,
})

// Group emails by customerId
const groupedEmails = computed(() => {
  const map: Record<number, EmailModel[]> = {}
  state.emails.forEach(email => {
    if (!email.customerId) return
    if (!map[email.customerId]) {
      map[email.customerId] = []
    }
    map[email.customerId].push(email)
  })
  return Object.keys(map).map(customerId => ({
    customerId,
    emails: map[Number(customerId)]
  }))
})

onMounted(() => {
  fetchEmails()
})

function fetchEmails() {
  state.loading = true
  state.error = null
  client
    .emailsAll()
    .then((response) => {
      state.emails = response
    })
    .catch((error) => {
      state.error = error.message || 'An error occurred'
      if (error.status) {
        redirectToErrorPage(error.status)
      } else {
        redirectToErrorPage(0) // Unknown error
      }
    })
    .finally(() => {
      state.loading = false
    })
}

function redirectToErrorPage(errorCode: number) {
  switch (errorCode) {
    case 401:
      router.push({ name: 'unauthorized' })
      break
    case 403:
      router.push({ name: 'forbidden' })
      break
    case 500:
      router.push({ name: 'InternalServerError' })
      break
    case 502:
      router.push({ name: 'badGateway' })
      break
    case 503:
      router.push({ name: 'serviceUnavailable' })
      break
    case 408:
      router.push({ name: 'requestTimeout' })
      break
    case 404:
      router.push({ name: 'notFound' })
      break
    default:
      router.push({ name: 'generalError' })
      break
  }
}
</script>
