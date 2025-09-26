<template>
  <div class="customer-page">
    <!-- Header Section -->
    <div class="page-header">
      <div class="header-content">
        <div class="header-text">
          <h1 class="page-title">Customers</h1>
          <p class="page-subtitle">Manage your customer information and contact details.</p>
        </div>
        <Button 
          label="Add Customer" 
          icon="pi pi-plus" 
          class="add-customer-btn"
          @click="addCustomer"
        />
      </div>
      
      <!-- Search Bar -->
      <div class="search-section">
        <InputText 
          v-model="searchQuery"
          placeholder="Search customers by name, email, phone, or location..."
          class="search-input"
        />
        <i class="pi pi-search search-icon"></i>
      </div>
    </div>

    <!-- Statistics Cards -->
    <div class="stats-section">
      <div class="stats-grid">
        <Card class="stat-card">
          <template #content>
            <div class="stat-content">
              <h3 class="stat-title">Total Customers</h3>
              <div class="stat-number">{{ state.customer.length }}</div>
            </div>
          </template>
        </Card>
        
        <Card class="stat-card">
          <template #content>
            <div class="stat-content">
              <h3 class="stat-title">Search Results</h3>
              <div class="stat-number">{{ filteredCustomers.length }}</div>
            </div>
          </template>
        </Card>
        
        <Card class="stat-card">
          <template #content>
            <div class="stat-content">
              <h3 class="stat-title">Recent Additions</h3>
              <div class="stat-number">{{ recentCustomersCount }}</div>
              <div class="stat-subtitle">Last 30 days</div>
            </div>
          </template>
        </Card>
      </div>
    </div>

    <!-- Customer Cards Section -->
    <div class="customers-section" v-if="!state.loading">
      <div class="customers-grid">
        <Card v-for="customer in filteredCustomers" :key="customer.customerId" class="customer-card">
          <template #content>
            <div class="customer-header">
              <div class="customer-name">{{ customer.firstName }} {{ customer.lastName }}</div>
              <div class="customer-actions">
                <Button 
                  icon="pi pi-pencil" 
                  class="p-button-text p-button-sm action-btn"
                  @click="editCustomer(customer)"
                />
                <Button 
                  icon="pi pi-trash" 
                  class="p-button-text p-button-sm p-button-danger action-btn"
                  @click="deleteCustomer(customer)"
                />
              </div>
            </div>
            
            <div class="customer-badge">Customer</div>
            
            <div class="customer-details">
              <div class="customer-contact">
                <div class="contact-item" v-if="customer.companyName">
                  <i class="pi pi-building contact-icon"></i>
                  <span>{{ customer.companyName }}</span>
                </div>
                <div class="contact-item" v-if="customer.prefferedContactMethod">
                  <i class="pi pi-phone contact-icon"></i>
                  <span>Preferred: {{ customer.prefferedContactMethod }}</span>
                </div>
                <div class="contact-item" v-if="customer.status">
                  <i class="pi pi-info-circle contact-icon"></i>
                  <span>Status: {{ customer.status }}</span>
                </div>
              </div>
              
              <div v-if="customer.customerNotes" class="customer-notes">
                "{{ customer.customerNotes }}"
              </div>
              
              <div class="customer-meta">
                <span>Type: {{ customer.customerType || 'Standard' }}</span>
                <span>ID: {{ customer.customerId }}</span>
              </div>
            </div>
          </template>
        </Card>
      </div>
    </div>
    
    <div v-else-if="state.error" class="error-message">{{ state.error }}</div>
    <div v-else class="loading-message">
      <p>Loading...</p>
    </div>
  </div>
</template>

<script setup lang="ts">
import Card from 'primevue/card'
import Button from 'primevue/button'
import InputText from 'primevue/inputtext'
import { Client, CustomerModel } from '../client/client'
import { onMounted, reactive, ref, computed } from 'vue'

const client = new Client(import.meta.env.VITE_API_BASE_URL)

const state = reactive({
  customer: [] as CustomerModel[],
  loading: false,
  error: null as string | null,
})

const searchQuery = ref('')

// Computed properties
const filteredCustomers = computed(() => {
  if (!searchQuery.value) return state.customer
  
  const query = searchQuery.value.toLowerCase()
  return state.customer.filter(customer => 
    `${customer.firstName || ''} ${customer.lastName || ''}`.toLowerCase().includes(query) ||
    (customer.companyName && customer.companyName.toLowerCase().includes(query)) ||
    (customer.customerNotes && customer.customerNotes.toLowerCase().includes(query))
  )
})

const recentCustomersCount = computed(() => {
  // For now, return 0 since we don't have createdOn field
  return 0
})

// Functions
function addCustomer() {
  // TODO: Implement add customer functionality
  console.log('Add customer clicked')
}

function editCustomer(customer: CustomerModel) {
  // TODO: Implement edit customer functionality
  console.log('Edit customer:', customer)
}

function deleteCustomer(customer: CustomerModel) {
  // TODO: Implement delete customer functionality
  console.log('Delete customer:', customer)
}

// Remove formatDate function since we don't have createdOn field

onMounted(() => {
  console.log('AboutView mounted')
  fetchCustomers()
})

function fetchCustomers() {
  state.loading = true
  state.error = null
  client
    .customersAll()
    .then((response) => {
      state.customer = response
    })
    .catch((error) => {
      state.error = error.message || 'An error occurred'
    })
    .finally(() => {
      state.loading = false
    })
}
</script>

<style scoped>
.customer-page {
  padding: 2rem;
  max-width: 1200px;
  margin: 0 auto;
}

.page-header {
  margin-bottom: 2rem;
}

.header-content {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 2rem;
}

.header-text {
  flex: 1;
}

.page-title {
  font-size: 2.5rem;
  font-weight: bold;
  margin: 0 0 0.5rem 0;
  color: #1a1a1a;
}

.page-subtitle {
  font-size: 1.1rem;
  color: #666;
  margin: 0;
  line-height: 1.5;
}

.add-customer-btn {
  background-color: #1a1a1a;
  border-color: #1a1a1a;
  color: white;
  font-weight: 600;
  padding: 0.75rem 1.5rem;
  border-radius: 8px;
  font-size: 0.95rem;
}

.add-customer-btn:hover {
  background-color: #333;
  border-color: #333;
}

.search-section {
  position: relative;
  max-width: 600px;
}

.search-input {
  width: 100%;
  padding: 0.875rem 1rem 0.875rem 3rem;
  border-radius: 8px;
  border: 1px solid #ddd;
  font-size: 0.95rem;
  background-color: #f8f9fa;
}

.search-input:focus {
  background-color: white;
  border-color: #007bff;
  box-shadow: 0 0 0 2px rgba(0, 123, 255, 0.25);
}

.search-input::placeholder {
  color: #999;
}

.search-icon {
  position: absolute;
  left: 1rem;
  top: 50%;
  transform: translateY(-50%);
  color: #999;
  font-size: 1rem;
  pointer-events: none;
}

/* Statistics Section */
.stats-section {
  margin-bottom: 2rem;
}

.stats-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  gap: 1rem;
  margin-bottom: 2rem;
}

.stat-card {
  border-radius: 12px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.stat-content {
  padding: 0.5rem;
}

.stat-title {
  font-size: 1rem;
  color: #666;
  margin: 0 0 0.5rem 0;
  font-weight: 500;
}

.stat-number {
  font-size: 2rem;
  font-weight: bold;
  color: #1a1a1a;
  margin-bottom: 0.25rem;
}

.stat-subtitle {
  font-size: 0.875rem;
  color: #888;
}

/* Customers Grid */
.customers-section {
  margin-top: 2rem;
}

.customers-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(350px, 1fr));
  gap: 1.5rem;
}

.customer-card {
  border-radius: 12px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}

.customer-card:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 16px rgba(0, 0, 0, 0.15);
}

.customer-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1rem;
}

.customer-name {
  font-size: 1.25rem;
  font-weight: bold;
  color: #1a1a1a;
}

.customer-actions {
  display: flex;
  gap: 0.5rem;
}

.action-btn {
  width: 32px;
  height: 32px;
  border-radius: 6px;
}

.customer-badge {
  display: inline-block;
  background-color: #f0f0f0;
  color: #666;
  padding: 0.25rem 0.75rem;
  border-radius: 16px;
  font-size: 0.875rem;
  margin-bottom: 1rem;
}

.customer-contact {
  margin-bottom: 1rem;
}

.contact-item {
  display: flex;
  align-items: center;
  margin-bottom: 0.5rem;
  font-size: 0.95rem;
}

.contact-icon {
  width: 16px;
  margin-right: 0.75rem;
  color: #666;
  flex-shrink: 0;
}

.customer-notes {
  font-style: italic;
  color: #666;
  margin-bottom: 1rem;
  padding: 0.75rem;
  background-color: #f8f9fa;
  border-radius: 6px;
  border-left: 3px solid #ddd;
}

.customer-meta {
  display: flex;
  justify-content: space-between;
  font-size: 0.875rem;
  color: #888;
  padding-top: 0.75rem;
  border-top: 1px solid #eee;
}

.error-message, .loading-message {
  text-align: center;
  padding: 2rem;
  color: #666;
}
</style>
