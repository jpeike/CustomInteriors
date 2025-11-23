import { test, expect, request } from '@playwright/test';

const customersToDelete = [
  {
    firstName: "Barack",
    lastName: "Obama",
    customerType: "Individual",
    prefferedContactMethod: "email",
    companyName: "Obama Games",
    status: "Inactive",
    customerNotes: "The Former President?",

    street: "123 Washington Ave",
    city: "Washington DC",
    state: "District of columbia",
    postalCode: 12345,
    country: "USA",
    addressType: "Primary",

    emailAddress: "bobama@usa.com",
    emailType: "main",
    createdOn: "2025-11-10T16:25:47.8400046"
  },
];

test.beforeEach(async ({ page }) => {
  await page.goto('/customers', { waitUntil: 'networkidle' });
});

//todo possubly change these to create an customer for every customer in above array.
async function createCustomer() {
  const api = await request.newContext({
    baseURL: 'https://localhost:44351'
  });

  const create = await api.post('api/customers', {
    data: {
        firstName: 'Test',
        lastName: 'User',
        customerType: "Test Customer",
        prefferedContactMethod: "email",
        companyName: "Test Inc",
        status: "Inactive",
        customerNotes: "If you can see this that means the delete e2e test failed.",
    }
  });

  const body = await create.json();
  const customerId = body.customerId;
  await api.dispose();

  return customerId;

}

async function createAddress(customerId: number) {
  const api = await request.newContext({
    baseURL: 'https://localhost:44351'
  });

  const create = await api.post('api/addresses', {
    data: {
        customerId: customerId,
        street: "Test Street",
        city: "Test City",
        state: "Test State",
        postalCode: 12345,
        country: "Test Country",
        addressType: "Test",
    }
  });

  const body = await create.json();
  const addressId = body.addressId;
  await api.dispose();
  return addressId;

}

async function createEmail(customerId: number) {
  const api = await request.newContext({
    baseURL: 'https://localhost:44351'
  });

  const create = await api.post('api/emails', {
    data: {
        customerId: customerId,
        emailAddress: "test@test.com",
        emailType: "main",
        createdOn: "2025-11-10T16:25:47.8400046"
    }
  });

  const body = await create.json();
  const emailId = body.emailId;
  await api.dispose();
  return emailId;

}

test('Delete Customer', async ({ page }) => {
    const customerId = await createCustomer();
    const addressId = await createAddress(customerId);
    const emailId = await createEmail(customerId);
    
    //Hardcoded card name. Change later.
    await page.reload({ waitUntil: 'networkidle' });
    const card = page.getByTestId('customerCard').filter({ hasText: "Test User" });
    await expect(card).toBeVisible(); 
    await card.getByTestId('deleteCustomerButton').click();
    await page.getByTestId('deleteConfirmationButton').click();

    //Verify successful deletion
    await expect(page.getByText('Customer Deleted Successfully')).toBeVisible();
    await expect(page.getByText('Test User')).toHaveCount(0);

});