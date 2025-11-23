import { test, expect } from '@playwright/test';

const expectedCustomers = [
  {
    name: "Alice Smith",
    customerType: "Residential",
    prefferedContactMethod: "Email",
    companyName: "Smith Home Services",
    status: "Active",
    customerNotes: "VIP client - prefers email.",

    street: "123 Maple St",
    city: "Menomonie",
    state: "WI",
    postalCode: 54751,
    country: "USA",
    addressType: "Home",

    emailAddress: "alice@example.com",
    emailType: "main",
    createdOn: "2025-11-10T16:25:47.8400046"
    
  },
  {
    name: "Cameron Scott",
    customerType: "Commercial",
    prefferedContactMethod: "Email",
    companyName: "Google",
    status: "Active",
    customerNotes: "Frequent customer.",

    street: "456 Oak Ave",
    city: "Eau Claire",
    state: "WI",
    postalCode: 54701,
    country: "USA",
    addressType: "Business",

    emailId: 2,
    customerId: 2,
    emailAddress: "cscott@gmail.com",
    emailType: "main",
    createdOn: "2024-11-10T16:25:47.8400046"
  }
];

test.beforeEach(async ({ page }) => {
  await page.goto('/customers', { waitUntil: 'networkidle' });
});

test('CRM customers page loads successfully', async ({ page }) => {
  await expect(page.getByRole('heading', { name: "Customers" })).toBeVisible();
});

for (const c of expectedCustomers) {
  test(`Customer card for ${c.name} loads successfully`, async ({ page }) => {

    const card = page.getByTestId('customerCard').filter({ hasText: c.name });
    await expect(card.getByTestId('customerName')).toHaveText(c.name);
    if (c.prefferedContactMethod === 'email') {
         await expect(card.getByTestId('customerEmail')).toHaveText(c.emailAddress);
    }
    else {
        //TODO add when phone entity is implemented.
    }
    await expect(card.getByTestId('customerAddress')).toHaveText("Address: " + c.street + " " + c.city + ", " + c.state);
    await expect(card.getByTestId('customerType')).toHaveText("Type: " + c.customerType);
    await expect(card.getByTestId('customerCompany')).toContainText("Company: " + c.companyName);
    await expect(card.getByTestId('customerStatus')).toHaveText("Status: " + c.status);
    await expect(card.getByTestId('customerNotes')).toHaveText("Notes: " + c.customerNotes);
  });
}