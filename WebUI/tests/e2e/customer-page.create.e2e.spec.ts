import { test, expect, request } from '@playwright/test';

const customerToCreate = [
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

test('Create New Customer', async ({ page }) => {
  let customerId: number | null = null;

  page.on('response', async (response) => {
    if (response.url().includes('/api/customers') && response.request().method() === 'POST') {
      const body = await response.json();
      customerId = body.customerId;
      //console.log('Captured customerId:', customerId);

    }
  });

  await page.getByTestId('addCustomerButton').click();

  await page.getByTestId('customerFormFirstName').fill('Barack');
  await page.getByTestId('customerFormLastName').fill('Obama');
  await page.getByTestId('customerFormType').fill('Individual');
  await page.getByTestId('customerFormEmailRadioButton').check();
  await page.getByTestId('customerFormEmailAddress').fill('bobama@gmail.com');
  await page.getByTestId('customerFormEmailType').fill('primary');
  await page.getByTestId('customerFormStreet').fill('123 Washington Ave');
  await page.getByTestId('customerFormCountry').fill('USA');
  await page.getByTestId('customerFormCity').fill('Washington DC');
  await page.getByTestId('customerFormState').fill('District of Columbia');
  await page.getByTestId('customerFormPostalCode').fill('12345');
  await page.getByTestId('customerFormAddressType').fill('Primary');
  await page.getByTestId('customerFormCompany').fill("Obama Games");
  await page.getByTestId('customerFormStatus').fill("Inactive");
  await page.getByTestId('customerFormNotes').fill("The former president?!?!");

  await page.getByTestId('updateFormButton').click();

  //Verify successful creation
  await expect(page.getByText('Customer Created Successfully')).toBeVisible();
  await expect(page.getByText('Address Created Successfully')).toBeVisible();
  await expect(page.getByText('Email Created Successfully')).toBeVisible();
  await expect(page.getByText('Barack Obama')).toBeVisible();
    
  // Cleanup: delete the created customer
const api = await request.newContext({
    baseURL: 'https://localhost:44351'
});

const del = await api.delete(`api/customers/${customerId}`);
// console.log('DELETE status:', del.status());
// console.log('DELETE body:', await del.text());
// console.log('Deleting customer:', customerId, typeof customerId);

expect(del.ok()).toBeTruthy();

await page.reload({ waitUntil: 'networkidle' });
await expect(page.getByText('Barack Obama')).toHaveCount(0);

await api.dispose();
});

// test('Create customer popup visual layout', async ({ page }) => {
//    await expect(page).toHaveScreenshot();
// });