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
      //Todo when whole entity is implemented.
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
    await expect(card).toContainText(c.status);
    await expect(card).toContainText(c.customerNotes);
  });
}
