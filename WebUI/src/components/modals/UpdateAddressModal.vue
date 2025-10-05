<template>
  <Transition name="modal">
    <div v-if="isOpen" class="modalBackdrop" @click.self="closeModal">
      <div class="modalContainer">
        <div class="modalHeader">
          <div>
            <h2 class="modalTitle">Update Address</h2>
            <p class="modalSubtitle">Add a new address for a customer.</p>
          </div>
          <button @click="closeModal" class="closeButton" type="button">
            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
              <line x1="18" y1="6" x2="6" y2="18"></line>
              <line x1="6" y1="6" x2="18" y2="18"></line>
            </svg>
          </button>
        </div>
        <div class="modalContent">
          <form @submit.prevent="handleSubmit" class="addressForm">
            <div class="formGroup">
              <label for="street">Street *</label>
              <input 
                id="street"
                type="text" 
                required 
                v-model="street"
                placeholder="Enter street address"
              />
            </div>

            <div class="formRow">
              <div class="formGroup">
                <label for="city">City *</label>
                <input 
                  id="city"
                  type="text" 
                  required 
                  v-model="city"
                  placeholder="Enter city"
                />
              </div>

              <div class="formGroup">
                <label for="state">State *</label>
                <input 
                  id="state"
                  type="text" 
                  required 
                  v-model="state"
                  placeholder="Enter state"
                />
              </div>
            </div>

            <div class="formRow">
              <div class="formGroup">
                <label for="zipCode">ZIP Code *</label>
                <input 
                  id="zipCode"
                  type="number" 
                  required 
                  v-model="zipCode"
                  placeholder="Enter zip code"
                />
              </div>

              <div class="formGroup">
                <label for="country">Country</label>
                <input 
                  id="country"
                  type="text" 
                  v-model="country"
                  placeholder="Enter country"
                />
              </div>
            </div>

            <div class="formGroup">
              <label for="addressType">Address Type *</label>
              <input 
                id="addressType"
                type="text" 
                required 
                v-model="addressType"
                placeholder="e.g., Home, Work, Billing"
              />
            </div>

            <div v-if="message" :class="['message', messageType]">
              {{ message }}
            </div>

            <div v-if="props.errorMessage" class="message error">
              {{ props.errorMessage }}
            </div>

            <div class="modalActions">
              <button @click="closeModal" type="button" class="cancelButton">Cancel</button>
              <button @click="handleSubmit" type="button" class="submitButton">Update Address</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </Transition>
</template>

<script setup lang="ts">
import { ref, computed, watch} from 'vue'
import { AddressModel } from '../../client/client'

const props = defineProps<{
  isOpen: boolean
  customerId: number | null
  errorMessage: string | null
  address: AddressModel | null
}>()

const emit = defineEmits<{
  close: []
  updated: [address: AddressModel]
}>()

const street = ref('')
const city = ref('')
const state = ref('')
const zipCode = ref('')
const country = ref('')
const addressType = ref('')

const message = ref('')

const messageType = computed(() => {
  return message.value.includes('successfully') || message.value.includes('Successfully') ? 'success' : 'error'
})

function resetForm() {
  street.value = ''
  city.value = ''
  state.value = ''
  zipCode.value = ''
  country.value = ''
  addressType.value = ''
  message.value = ''
}

watch([() => props.isOpen, () => props.address], ([isOpen, address]) => {
  if (isOpen && address) {
    street.value = address.street || ''
    city.value = address.city || ''
    state.value = address.state || ''
    zipCode.value = String(address.postalCode || '')
    country.value = address.country || ''
    addressType.value = address.addressType || ''
  }
})

function closeModal() {
  resetForm()
  emit('close')
}

async function handleSubmit() {
  if (!props.customerId) {
    message.value = 'Customer ID is required'
    return
  }

  if (!/^\d{5}$/.test(zipCode.value)) {
    message.value = 'ZIP Code must be a 5 digit integer'
    return
  }

  if (Number(zipCode.value) < 0) {
    message.value = 'ZIP Code must be a postitive integer'
    return
  }

  if (street.value.length > 255 || city.value.length > 100 || state.value.length > 100 || zipCode.value.length > 10 || (country.value && country.value.length > 100) || addressType.value.length > 100) {
    message.value = 'One or more fields exceed maximum length'
    return
  }

  const updatedAddress = new AddressModel({
    addressId: props.address?.addressId ?? undefined,
    customerId: props.customerId,
    street: street.value,
    city: city.value,
    state: state.value,
    postalCode: Number(zipCode.value),
    addressType: addressType.value,
    country: country.value || undefined,
  })

  
    message.value = 'Address Successfully Updated'

    emit('updated', updatedAddress)
    
    setTimeout(() => {
      closeModal()
    }, 1500)
}
</script>

<style scoped>
.modal-enter-active,
.modal-leave-active {
  transition: opacity 0.2s ease;
}

.modal-enter-from,
.modal-leave-to {
  opacity: 0;
}

.modal-enter-active .modalContainer,
.modal-leave-active .modalContainer {
  transition: transform 0.2s ease;
}

.modal-enter-from .modalContainer,
.modal-leave-to .modalContainer {
  transform: scale(0.96);
}

.modalBackdrop {
  position: fixed;
  inset: 0;
  background-color: rgba(0, 0, 0, 0.4);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
  padding: 1.5rem;
}

.modalContainer {
  background: white;
  border-radius: 12px;
  max-width: 700px;
  width: 100%;
  max-height: 90vh;
  display: flex;
  flex-direction: column;
  box-shadow: 0 20px 25px -5px rgba(0, 0, 0, 0.1), 0 10px 10px -5px rgba(0, 0, 0, 0.04);
}

.modalHeader {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  padding: 2rem 2rem 1.5rem 2rem;
  border-bottom: 1px solid #e5e7eb;
}

.modalTitle {
  font-size: 1.5rem;
  font-weight: 600;
  color: #000;
  margin: 0 0 0.25rem 0;
}

.modalSubtitle {
  font-size: 0.9375rem;
  color: #737373;
  margin: 0;
  font-weight: 400;
}

.closeButton {
  background: none;
  border: none;
  color: #737373;
  cursor: pointer;
  padding: 0.25rem;
  border-radius: 4px;
  transition: color 0.15s;
  display: flex;
  align-items: center;
  justify-content: center;
}

.closeButton:hover {
  color: #000;
}

.modalContent {
  flex: 1;
  overflow-y: auto;
  padding: 2rem;
}

.addressForm {
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}

.formRow {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 1.5rem;
}

.formGroup {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

label {
  font-size: 0.875rem;
  font-weight: 500;
  color: #000;
}

input {
  padding: 0.75rem;
  border: 1px solid #d1d5db;
  background-color: #fafafa;
  border-radius: 6px;
  font-size: 0.9375rem;
  color: #000;
  transition: border-color 0.15s, box-shadow 0.15s;
}

input:focus {
  outline: none;
  border-color: #000;
  background-color: #fff;
}

input::placeholder {
  color: #a3a3a3;
}

/* Message */
.message {
  padding: 0.875rem 1rem;
  border-radius: 6px;
  font-size: 0.875rem;
  font-weight: 500;
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

.modalActions {
  display: flex;
  gap: 0.75rem;
  justify-content: flex-end;
  margin-top: 0.5rem;
}

.cancelButton {
  padding: 0.75rem 1.5rem;
  background-color: white;
  color: #000;
  border: 1px solid #d1d5db;
  border-radius: 6px;
  font-size: 0.9375rem;
  font-weight: 500;
  cursor: pointer;
  transition: background-color 0.15s, border-color 0.15s;
}

.cancelButton:hover {
  background-color: #f5f5f5;
  border-color: #a3a3a3;
}

.submitButton {
  padding: 0.75rem 1.5rem;
  background-color: #000;
  color: white;
  border: none;
  border-radius: 6px;
  font-size: 0.9375rem;
  font-weight: 500;
  cursor: pointer;
  transition: background-color 0.15s;
}

.submitButton:hover {
  background-color: #262626;
}

.submitButton:active {
  background-color: #0a0a0a;
}

/* Dark mode support */
@media (prefers-color-scheme: dark) {
  .modalBackdrop {
    background-color: rgba(0, 0, 0, 0.7);
  }

  .modalContainer {
    background: #1a1a1a;
    box-shadow: 0 20px 25px -5px rgba(0, 0, 0, 0.5), 0 10px 10px -5px rgba(0, 0, 0, 0.3);
  }

  .modalHeader {
    border-bottom: 1px solid #2d2d2d;
  }

  .modalTitle {
    color: #ffffff;
  }

  .modalSubtitle {
    color: #a3a3a3;
  }

  .closeButton {
    color: #a3a3a3;
  }

  .closeButton:hover {
    color: #ffffff;
  }

  label {
    color: #e5e5e5;
  }

  input {
    border: 1px solid #2d2d2d;
    background-color: #262626;
    color: #ffffff;
  }

  input:focus {
    border-color: #ffffff;
    background-color: #1a1a1a;
  }

  input::placeholder {
    color: #737373;
  }

  .message.success {
    background-color: #064e3b;
    color: #a7f3d0;
    border: 1px solid #065f46;
  }

  .message.error {
    background-color: #7f1d1d;
    color: #fca5a5;
    border: 1px solid #991b1b;
  }

  .cancelButton {
    background-color: #262626;
    color: #ffffff;
    border: 1px solid #2d2d2d;
  }

  .cancelButton:hover {
    background-color: #2d2d2d;
    border-color: #404040;
  }

  .submitButton {
    background-color: #ffffff;
    color: #000;
  }

  .submitButton:hover {
    background-color: #e5e5e5;
  }

  .submitButton:active {
    background-color: #d4d4d4;
  }
}
</style>