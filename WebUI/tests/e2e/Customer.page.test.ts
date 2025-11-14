import { test, expect } from '@playwright/test';

const mockCustomers = [
  {
    customerId: 1,
    firstName: "Alice",
    lastName: "Smith",
    customerType: "Residential",
    prefferedContactMethod: "Email",
    companyName: "Custom Interiors LLC",
    status: "Active",
    customerNotes: "VIP client — prefers morning calls.",
    addresses: [
      {
        addressId: 1,
        customerId: 1,
        street: "123 Maple St",
        city: "Menomonie",
        state: "WI",
        postalCode: 54751,
        country: "USA",
        addressType: "Home"
      }
    ]
  },
  {
    customerId: 2,
    firstName: "Cameron",
    lastName: "Scott",
    customerType: "Large Business",
    prefferedContactMethod: "Email",
    companyName: "Google",
    status: "Active",
    customerNotes: "VIP",
    addresses: [
      {
        addressId: 2,
        customerId: 2,
        street: "1234 7th Avenue South",
        city: "Minneapolis",
        state: "MN",
        postalCode: 55378,
        country: "USA",
        addressType: "Corporate Office"
      }
    ]
  }
];

const mockAddresses = [
  {
    addressId: 1,
    customerId: 1,
    street: "123 Maple St",
    city: "Menomonie",
    state: "WI",
    postalCode: 54751,
    country: "USA",
    addressType: "Home"
  },
  {
    addressId: 2,
    customerId: 2,
    street: "1234 7th Avenue South",
    city: "Minneapolis",
    state: "MN",
    postalCode: 55378,
    country: "USA",
    addressType: "Corporate Office"
  }
]

const mockEmails = [
  {
    emailId: 1,
    customerId: 1,
    emailAddress: "asmith@gmail.com",
    emailType: "main",
    createdOn: "2025-11-10T16:25:47.8400046"
  },
  {
    emailId: 2,
    customerId: 2,
    emailAddress: "cscott@gmail.com",
    emailType: "main",
    createdOn: "2024-11-10T16:25:47.8400046"
  }
]

test.beforeEach(async ({ page }) => {
    // Mock the API route before navigation
    await page.route('**/api/customers', async route => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify(mockCustomers),
      });
    });

  await page.route('**/api/addresses', async route => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify(mockAddresses),
      });
    });

  await page.route('**/api/emails', async route => {
      await route.fulfill({
        status: 200,
        contentType: 'application/json',
        body: JSON.stringify(mockEmails),
      });
    });
  
  await page.goto('/customers', { waitUntil: 'networkidle' });
});

test('CRM customers page loads successfully', async ({ page }) => {
  await expect(page.getByRole('heading', { name: "Customers" })).toBeVisible();
});

test('Customer Card Loads Successfully', async ({ page }) => {
  
  await expect(page).toHaveScreenshot();
  const aliceCard = page.getByTestId('customerCard').filter({ hasText: 'Alice Smith' });
  await expect(aliceCard.getByTestId('customerName')).toHaveText("Alice Smith");
  await expect(aliceCard.getByTestId('customerEmail')).toHaveText("asmith@gmail.com");
  await expect(aliceCard.getByTestId('customerAddress')).toHaveText("Address: 123 Maple St Menomonie, WI");
  await expect(aliceCard.getByTestId('customerType')).toHaveText("Type: Residential");
  await expect(aliceCard.getByTestId('customerCompany')).toHaveText("Company: Custom Interiors LLC");
  await expect(aliceCard.getByTestId('customerStatus')).toHaveText("Status: Active");
  await expect(aliceCard.getByTestId('customerNotes')).toHaveText("Notes: VIP client — prefers morning calls.");
});

test('Multiple Customer Cards Loads Successfully', async ({ page }) => {
  const cameronCard = page.getByTestId('customerCard').filter({ hasText: 'Cameron Scott' });
  await expect(cameronCard.getByTestId('customerName')).toHaveText("Cameron Scott");
  await expect(cameronCard.getByTestId('customerEmail')).toHaveText("cscott@gmail.com");
  await expect(cameronCard.getByTestId('customerAddress')).toHaveText("Address: 1234 7th Avenue South Minneapolis, MN");
  await expect(cameronCard.getByTestId('customerType')).toHaveText("Type: Large Business");
  await expect(cameronCard.getByTestId('customerCompany')).toHaveText("Company: Google");
  await expect(cameronCard.getByTestId('customerStatus')).toHaveText("Status: Active");
  await expect(cameronCard.getByTestId('customerNotes')).toHaveText("Notes: VIP");
});

test('Create New Customer', async ({ page }) => {

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
  await expect(page).toHaveScreenshot();
});
