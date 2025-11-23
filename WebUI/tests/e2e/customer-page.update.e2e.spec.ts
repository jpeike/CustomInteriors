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
        firstName: 'Fake',
        lastName: 'Customer',
        customerType: "Fake Customer",
        prefferedContactMethod: "email",
        companyName: "Fake Inc",
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

test('Update Customer', async ({ page }) => {
    const customerId = await createCustomer();
    const addressId = await createAddress(customerId);
    const emailId = await createEmail(customerId);
    
    //Hardcoded card name. Change later.
    await page.reload({ waitUntil: 'networkidle' });
    const card = page.getByTestId('customerCard').filter({ hasText: "Fake Customer" });
    await expect(card).toBeVisible(); 
    await card.getByTestId('updateCustomerButton').click();

    await page.getByTestId('customerFormFirstName').fill('Gabe');
    await page.getByTestId('customerFormLastName').fill('Newell');
    await page.getByTestId('customerFormType').fill('Individual');
    await page.getByTestId('customerFormEmailRadioButton').check();
    await page.getByTestId('customerFormEmailAddress').fill('gnewell@steam.com');
    await page.getByTestId('customerFormEmailType').fill('primary');
    await page.getByTestId('customerFormStreet').fill('1234 South Ave');
    await page.getByTestId('customerFormCountry').fill('USA');
    await page.getByTestId('customerFormCity').fill('Miami');
    await page.getByTestId('customerFormState').fill('Florida');
    await page.getByTestId('customerFormPostalCode').fill('12345');
    await page.getByTestId('customerFormAddressType').fill('Primary');
    await page.getByTestId('customerFormCompany').fill("Steam");
    await page.getByTestId('customerFormStatus').fill("Active");
    await page.getByTestId('customerFormNotes').fill("Gabe!");

    await page.getByTestId('updateFormButton').click();

    //Verify successful update
    await expect(page.getByText('Customer Updated Successfully')).toBeVisible();
    await expect(page.getByText('Address Updated Successfully')).toBeVisible();
    //await expect(page.getByText('Email Updated Successfully')).toBeVisible();

    await page.reload({ waitUntil: 'networkidle' });

    //TODO Modify this piece of code to be resuable
    await expect(card.getByTestId('customerName')).toHaveText('Gabe Newell');
    await expect(card.getByTestId('customerEmail')).toHaveText('gnewell@steam.com');
    await expect(card.getByTestId('customerAddress')).toHaveText('Address: 1234 South Ave Miami, Florida');
    await expect(card.getByTestId('customerType')).toHaveText('Type: Individual');
    await expect(card.getByTestId('customerCompany')).toContainText('Company: Steam');
    await expect(card.getByTestId('customerStatus')).toHaveText('Status: Active');
    await expect(card.getByTestId('customerNotes')).toHaveText('Notes: Gabe!');

    // Cleanup: delete the created customer
    const api = await request.newContext({
        baseURL: 'https://localhost:44351'
    });

    const del = await api.delete(`api/customers/${customerId}`);
    // console.log('DELETE status:', del.status());
    // console.log('DELETE body:', await del.text());
    // console.log('Deleting customer:',
    // customerId, typeof customerId);

    expect(del.ok()).toBeTruthy();

    await page.reload({ waitUntil: 'networkidle' });
    await expect(page.getByText('Gabe Newell')).toHaveCount(0);

    await api.dispose();

});