import { test } from '../fixtures/customer.fixture';
import { expect } from '@playwright/test';

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
    emailAddress: "cscott@gmail.com",
    emailType: "main",
  }
];

test('CRM customers page loads successfully', async ({ page, goToCustomersPage  }) => {
  await expect(page.getByRole('heading', { name: "Customers" })).toBeVisible();
});

for (const c of expectedCustomers) {

  test(`Customer card for ${c.name} should display correct info`, async ({ page, goToCustomersPage  }) => {

    const card = page.getByTestId('customerCard').filter({ hasText: c.name });

    await expect(card.getByTestId('customerName')).toContainText(c.name);

    // Email
    if (c.prefferedContactMethod === 'Email') {
      await expect(card).toContainText(c.emailAddress);
    }
    else {
      //Todo when phone entity is implemented.
    }

    // Address fields (partial matches = stable)
    await expect(card).toContainText(c.street);
    await expect(card).toContainText(c.city);
    await expect(card).toContainText(c.state);
    await expect(card).toContainText(c.postalCode.toString());
    await expect(card).toContainText(c.country);

    // Other info
    await expect(card).toContainText(c.customerType);
    await expect(card).toContainText(c.companyName);
    await expect(card).toContainText(c.customerNotes);
  });
}

test('Customer search should filter cards correctly', async ({ page, goToCustomersPage }) => {
  await page.getByTestId('customerSearchInput').fill('Alice');
  const cards = page.getByTestId('customerCard');

  //const visibleCards = page.getByTestId('customerCard');
  await expect(cards.filter({ hasText: "Alice Smith" })).toBeVisible();
  await expect(cards.filter({ hasText: "Cameron Scott" })).toHaveCount(0);
});

test('Clearing customer search should reset card list', async ({ page, goToCustomersPage }) => {
  const search = page.getByTestId('customerSearchInput');
  const cards = page.getByTestId('customerCard');

  await search.fill('Alice');
  await expect(cards).toHaveCount(1);

  // Clear the input
  await search.fill('');
  await expect(cards.filter({ hasText: "Alice Smith" })).toBeVisible();
  await expect(cards.filter({ hasText: "Cameron Scott" })).toBeVisible();
});

test('Customers page should persist customer list after page reload', async ({ page, goToCustomersPage }) => {
  await expect(page.getByRole('heading', { name: "Customers" })).toBeVisible();

  await page.reload();

  // Cards should still be visible after refresh
  const cards = page.getByTestId('customerCard');
  await expect(cards.filter({ hasText: "Alice Smith" })).toBeVisible();
  await expect(cards.filter({ hasText: "Cameron Scott" })).toBeVisible();
});

test('"Add Customer" button should open the customer create form', async ({ page, goToCustomersPage }) => {
  await page.getByTestId('addCustomerButton').click();

  const form = page.getByTestId('customerForm');
  await expect(form).toBeVisible();

  await expect(form.getByRole('Heading', { name: 'Create Customer' })).toBeVisible();
});

test('"Edit Customer" button should open the customer update form', async ({ page, goToCustomersPage }) => {
  const card = page.getByTestId('customerCard').filter({ hasText: "Alice Smith" });

  await card.getByTestId('updateCustomerButton').click();

  const form = page.getByTestId('customerForm');
  await expect(form).toBeVisible();

  await expect(form.getByRole('Heading', { name: 'Update Customer' })).toBeVisible();
});
