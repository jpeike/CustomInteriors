import { test as base, request, expect } from '@playwright/test';

//
// Types
//
export interface TestCustomerInput {
    firstName?: string;
    lastName?: string;
    customerType?: string;
    prefferedContactMethod?: string;
    companyName?: string;
    status?: string;
    customerNotes?: string;

    street?: string;
    city?: string;
    state?: string;
    postalCode?: number;
    country?: string;
    addressType?: string;

    emailAddress?: string;
    emailType?: string;
}

export interface CreatedCustomer {
    customerId: number;
    addressId?: number;
    emailId?: number;
}

//
// Utility: Create a fresh API context
//
async function apiContext() {
    return await request.newContext({
        baseURL: 'https://localhost:44351'
    });
}

//
// Utility: Create only the customer
//
async function apiCreateCustomer(input: TestCustomerInput = {}) {
    const api = await apiContext();

    const response = await api.post('api/customers', {
        data: {
            firstName: input.firstName ?? `Test`,
            lastName: input.lastName ?? 'User',
            customerType: input.customerType ?? 'Individual',
            prefferedContactMethod: input.prefferedContactMethod ?? 'email',
            companyName: input.companyName ?? 'Test Inc',
            status: input.status ?? 'Active',
            customerNotes: input.customerNotes ?? 'Test Customer Notes'
        }
    });

    expect(response.ok()).toBeTruthy();
    const body = await response.json();
    await api.dispose();

    return body.customerId as number;
}

//
// Utility: Create a single address
//
async function apiCreateAddress(customerId: number, input: TestCustomerInput = {}) {
    const api = await apiContext();

    const response = await api.post('api/addresses', {
        data: {
            customerId,
            street: input.street ?? '123 Test St',
            city: input.city ?? 'Test City',
            state: input.state ?? 'WI',
            postalCode: input.postalCode ?? 54751,
            country: input.country ?? 'USA',
            addressType: input.addressType ?? 'Primary'
        }
    });

    expect(response.ok()).toBeTruthy();
    const body = await response.json();
    await api.dispose();

    return body.addressId as number;
}

//
// Utility: Create a single email
//
async function apiCreateEmail(customerId: number, input: TestCustomerInput = {}) {
    const api = await apiContext();

    const response = await api.post('api/emails', {
        data: {
            customerId,
            emailAddress: input.emailAddress ?? 'test@example.com',
            emailType: input.emailType ?? 'main',
        }
    });

    if (!response.ok()) {
        const body = await response.json();
        console.log("Validation Errors:", body);
    }
    expect(response.ok()).toBeTruthy();
    const body = await response.json();
    await api.dispose();

    return body.emailId as number;
}

//
// Utility: Delete the customer (cascades delete address/email)
//
async function apiDeleteCustomer(customerId: number) {
    const api = await apiContext();
    const response = await api.delete(`api/customers/${customerId}`);
    expect(response.ok()).toBeTruthy();
    await api.dispose();
}

//
// FIXTURE DEFINITION
//
export const test = base.extend<{
    goToCustomersPage: void;
    createTestCustomer: () => Promise<number>;
    createPopulatedCustomer: () => Promise<CreatedCustomer>;
    seedCustomers: (count: number) => Promise<number[]>;
    cleanupCustomer: (id: number) => Promise<void>;
}>({

    goToCustomersPage: async ({ page }, use) => {
        await page.goto('/customers', { waitUntil: 'networkidle' });
        await use(page);
    },
    //
    // Create only the customer (used for Create form tests)
    //
    createTestCustomer: async ({ }, use) => {
        await use(async () => {
            return await apiCreateCustomer();
        });
    },

    //
    // Create customer + address + email (used for Update/Delete tests)
    //
    createPopulatedCustomer: async ({ }, use) => {
        await use(async () => {
            const customerId = await apiCreateCustomer();
            const addressId = await apiCreateAddress(customerId);
            const emailId = await apiCreateEmail(customerId);

            return { customerId, addressId, emailId };
        });
    },

    //
    // Create many customers (list page testing)
    //
    seedCustomers: async ({ }, use) => {
        await use(async (count: number) => {
            const ids: number[] = [];

            for (let i = 0; i < count; i++) {
                const id = await apiCreateCustomer({
                    firstName: `Seed${i}`,
                    lastName: `Customer${i}`
                });
                ids.push(id);
            }

            return ids;
        });
    },

    //
    // Cleanup helper so your tests can easily delete customers
    //
    cleanupCustomer: async ({ }, use) => {
        await use(async (id: number) => {
            await apiDeleteCustomer(id);
        });
    }
});
