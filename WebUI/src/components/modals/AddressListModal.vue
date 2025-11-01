<template>
  <Transition name="modal">
    <div v-if="isOpen" class="modalBackdrop" @click.self="closeModal">
      <div class="modalContainer">
        <div class="modalHeader">
          <div>
            <h2 class="modalTitle">Customer Addresses</h2>
            <p class="modalSubtitle">View and manage all addresses for this customer.</p>
          </div>
          <button @click="closeModal" class="closeButton" type="button">
            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
              <line x1="18" y1="6" x2="6" y2="18"></line>
              <line x1="6" y1="6" x2="18" y2="18"></line>
            </svg>
          </button>
        </div>
        <div class="modalContent">
          <div class="actionBar">
            <button @click="openCreateModal()" class="addAddressBtn" type="button">
              <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                <line x1="12" y1="5" x2="12" y2="19"></line>
                <line x1="5" y1="12" x2="19" y2="12"></line>
              </svg>
              Add New Address
            </button>
          </div>
          <div v-if="props.addresses.length > 0" class="addressesList">
            <div v-for="address in props.addresses" :key="address.addressId" class="addressCard">
              <div class="addressHeader">
                <div>
                  <h3 class="addressType">{{ address.addressType }}</h3>
                  <p class="addressId">ID: {{ address.addressId }}</p>
                </div>
                <div class="actionButtons">
                  <button @click="handleEdit(address)" class="iconBtn editBtn" type="button" title="Edit">
                    <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                      <path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"></path>
                      <path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"></path>
                    </svg>
                  </button>
                  <button v-if="address.addressId !== undefined" @click="handleDelete(address.addressId)" class="iconBtn deleteBtn" type="button" title="Delete">
                    <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                      <polyline points="3 6 5 6 21 6"></polyline>
                      <path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"></path>
                    </svg>
                  </button>
                </div>
              </div>
              
              <div class="addressDetails">
                <div class="detailRow">
                  <span class="detailLabel">Street:</span>
                  <span class="detailValue">{{ address.street }}</span>
                </div>
                <div class="detailRow">
                  <span class="detailLabel">City:</span>
                  <span class="detailValue">{{ address.city }}</span>
                </div>
                <div class="detailRow">
                  <span class="detailLabel">State:</span>
                  <span class="detailValue">{{ address.state }}</span>
                </div>
                <div class="detailRow">
                  <span class="detailLabel">Postal Code:</span>
                  <span class="detailValue">{{ address.postalCode }}</span>
                </div>
                <div class="detailRow">
                  <span class="detailLabel">Country:</span>
                  <span class="detailValue">{{ address.country }}</span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </Transition>
</template>

<script setup lang="ts">
import { AddressModel } from '../../client/client'

const props = defineProps<{
  isOpen: boolean
  customerId: number | null
  addresses: AddressModel[]
}>()

const emit = defineEmits<{
  close: []
  edit: [address: AddressModel]
  delete: [addressId: number]
  create: []
}>()

function closeModal() {
  emit('close')
}

function openCreateModal() {
  emit('create')
}

function handleEdit(address: AddressModel) {
  emit('edit', address)
}

function handleDelete(addressId: number) {
  if (confirm('Are you sure you want to delete this address? This action cannot be undone.')) {
    emit('delete', addressId)
  }
}
</script>

<style scoped>
/* styling generated with claude */
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
  max-width: 1000px;
  width: 100%;
  max-height: 85vh;
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
  padding: 1.5rem 2rem 2rem 2rem;
}

.actionBar {
  margin-bottom: 1.5rem;
}

.addAddressBtn {
  display: inline-flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.625rem 1.125rem;
  background-color: #000;
  color: white;
  border: none;
  border-radius: 6px;
  font-size: 0.875rem;
  font-weight: 500;
  cursor: pointer;
  transition: background-color 0.15s;
}

.addAddressBtn:hover {
  background-color: #262626;
}

.addressesList {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(320px, 1fr));
  gap: 1.25rem;
}

.addressCard {
  border: 1px solid #e5e7eb;
  border-radius: 8px;
  background: #fafafa;
  overflow: hidden;
}

.addressHeader {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  padding: 1.25rem;
  background: white;
  border-bottom: 1px solid #e5e7eb;
}

.addressType {
  font-size: 1rem;
  font-weight: 600;
  color: #000;
  margin: 0 0 0.25rem 0;
}

.addressId {
  font-size: 0.75rem;
  color: #a3a3a3;
  margin: 0;
  font-weight: 400;
}

.actionButtons {
  display: flex;
  gap: 0.5rem;
}

.iconBtn {
  background: none;
  border: none;
  color: #737373;
  cursor: pointer;
  padding: 0.375rem;
  border-radius: 4px;
  transition: all 0.15s;
  display: flex;
  align-items: center;
  justify-content: center;
}

.editBtn:hover {
  background-color: #f5f5f5;
  color: #000;
}

.deleteBtn:hover {
  background-color: #fee2e2;
  color: #dc2626;
}

.addressDetails {
  padding: 1.25rem;
  display: flex;
  flex-direction: column;
  gap: 0.75rem;
}

.detailRow {
  display: flex;
  font-size: 0.875rem;
}

.detailLabel {
  color: #737373;
  font-weight: 400;
  min-width: 100px;
  flex-shrink: 0;
}

.detailValue {
  color: #000;
  font-weight: 400;
}

.stateContainer {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 3rem 2rem;
  text-align: center;
}

.stateText {
  color: #737373;
  font-size: 0.9375rem;
  margin: 0;
}

.stateContainer.error .stateText {
  color: #dc2626;
}

.spinner {
  width: 40px;
  height: 40px;
  border: 3px solid #f5f5f5;
  border-top-color: #000;
  border-radius: 50%;
  animation: spin 0.8s linear infinite;
  margin-bottom: 1rem;
}

@keyframes spin {
  to { transform: rotate(360deg); }
}

.stateContainer.empty {
  padding: 4rem 2rem;
}

.emptyIcon {
  color: #d4d4d4;
  margin-bottom: 1.25rem;
}

.stateSubtext {
  color: #a3a3a3;
  font-size: 0.875rem;
  margin: 0.5rem 0 1.5rem 0;
}

.addFirstBtn {
  padding: 0.625rem 1.25rem;
  background-color: #000;
  color: white;
  border: none;
  border-radius: 6px;
  font-size: 0.875rem;
  font-weight: 500;
  cursor: pointer;
  transition: background-color 0.15s;
}

.addFirstBtn:hover {
  background-color: #262626;
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

  .addAddressBtn {
    background-color: #ffffff;
    color: #000;
  }

  .addAddressBtn:hover {
    background-color: #e5e5e5;
  }

  .addressCard {
    border: 1px solid #2d2d2d;
    background: #262626;
  }

  .addressHeader {
    background: #1a1a1a;
    border-bottom: 1px solid #2d2d2d;
  }

  .addressType {
    color: #ffffff;
  }

  .addressId {
    color: #737373;
  }

  .iconBtn {
    color: #a3a3a3;
  }

  .editBtn:hover {
    background-color: #2d2d2d;
    color: #ffffff;
  }

  .deleteBtn:hover {
    background-color: #7f1d1d;
    color: #fca5a5;
  }

  .addressDetails {
    background: #262626;
  }

  .detailLabel {
    color: #a3a3a3;
  }

  .detailValue {
    color: #e5e5e5;
  }

  .stateText {
    color: #a3a3a3;
  }

  .stateContainer.error .stateText {
    color: #fca5a5;
  }

  .spinner {
    border: 3px solid #2d2d2d;
    border-top-color: #ffffff;
  }

  .emptyIcon {
    color: #404040;
  }

  .stateSubtext {
    color: #737373;
  }

  .addFirstBtn {
    background-color: #ffffff;
    color: #000;
  }

  .addFirstBtn:hover {
    background-color: #e5e5e5;
  }
}
</style>