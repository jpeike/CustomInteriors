<template>
  <div id="quote-request-page">
    <h2>Create Quote Request</h2>

    <form @submit.prevent="createQuoteRequest">

      <div class="form-field">
        <label for="descriptionOfWork">Description of Work</label>
        <InputText
          id="descriptionOfWork"
          v-model="form.descriptionOfWork"
          placeholder="Describe the work needed..."
        />
      </div>

      <div class="form-field">
        <label for="estimatedPrice">Estimated Price</label>
        <InputNumber
          id="estimatedPrice"
          v-model="form.estimatedPrice"
          placeholder="Enter estimated price"
          mode="currency"
          currency="USD"
        />
      </div>

      <Button
        label="Submit Request"
        :loading="state.loading"
        type="submit"
      />
    </form>
  </div>
</template>

<script setup lang="ts">
import { reactive } from 'vue';
import { Client, CreateQuoteRequestModel } from '@/client/client';
import InputText from 'primevue/inputtext';
import InputNumber from 'primevue/inputnumber';
import Button from 'primevue/button';
import { useToast } from '@/composables/useToast.ts';

const client = new Client(import.meta.env.VITE_API_BASE_URL);
const { showSuccess, showError } = useToast();

const state = reactive({
  loading: false,
  error: null as string | null,
});

const form = reactive<Partial<CreateQuoteRequestModel>>({
  descriptionOfWork: '',
  estimatedPrice: undefined,
});

async function createQuoteRequest() {
  state.loading = true;
  state.error = null;

  try {
    const newRequest = new CreateQuoteRequestModel();
    newRequest.descriptionOfWork = form.descriptionOfWork || '';
    newRequest.estimatedPrice = form.estimatedPrice ?? undefined;

    console.log('Creating Quote Request:', newRequest);
    await client.createQuoteRequest(newRequest);
    showSuccess('Quote Request Created Successfully');

    form.descriptionOfWork = '';
    form.estimatedPrice = undefined;
  } catch (error: any) {
    state.error = error.message || 'An error occurred while creating the quote request';
    showError(state.error);
  } finally {
    state.loading = false;
  }
}
</script>

<style scoped>
/* Clean CSS styling for the form */
#quote-request-page {
  max-width: 600px;
  margin: 0 auto;
  padding: 24px;
}

h2 {
  font-size: 1.5rem;
  font-weight: bold;
  margin-bottom: 16px;
}

.form-field {
  margin-bottom: 16px;
  display: flex;
  flex-direction: column;
}

label {
  margin-bottom: 8px;
  font-weight: 500;
}

.error-message {
  color: red;
  margin-top: 12px;
}
</style>
