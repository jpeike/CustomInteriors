import { expect } from '@playwright/test';
import { test } from '../fixtures/customer.fixture';
import { fillCustomerFormAndSubmit } from '../helpers/customer.form';
import { expectCustomerCard } from '../helpers/customer.expect';
import { customerFactory } from '../helpers/customer.factory';

test('Update Customer', async ({ page, goToCustomersPage, createPopulatedCustomer, cleanupCustomer }) => {

  const { customerId, addressId, emailId } = await createPopulatedCustomer();

  //reload page
  await page.reload({ waitUntil: 'networkidle' });

  const card = page.locator(`[data-customer-id="${customerId}"]`);
  await expect(card).toBeVisible();

  await card.getByTestId('updateCustomerButton').click();

  const updated = customerFactory({
    firstName: 'Gabe',
    lastName: 'Newell',
    customerType: 'Individual',
    perferredContactMethod: 'Email',

    emails: [
      {
        emailAddress: 'gnewell@gmail.com',
        emailType: 'primary',
      }
    ],
    addresses: [
      {
        street: '1234 South Ave',
        city: 'Miami',
        state: 'Florida',
        postalCode: 12345,
        country: 'USA',
        addressType: 'Primary'
      }
    ],

    companyName: 'Steam',
    status: 'Active',
    customerNotes: 'Gabe!',
  });

  await fillCustomerFormAndSubmit(page, updated, 'PUT');

  //verify toast
  await expect(
    page.locator('.p-toast-message-success:has-text("Customer Updated Successfully")')
  ).toBeVisible();

  //await page.reload({ waitUntil: 'networkidle' });

  //verify updated card
  await expectCustomerCard(page, updated, customerId);

  cleanupCustomer(customerId);
});

test('Update Customer - Cancel should discard changes', async ({ page, goToCustomersPage, createPopulatedCustomer, cleanupCustomer }) => {
  const { customerId } = await createPopulatedCustomer();
  await page.reload({ waitUntil: 'networkidle' });

  const card = page.locator(`[data-customer-id="${customerId}"]`);
  await expect(card).toBeVisible();

  // Open update form
  await card.getByTestId('updateCustomerButton').click();

  // Modify first name
  await page.getByTestId('customerFormFirstName').fill('DiscardMe');

  // Cancel
  await page.getByTestId('cancelFormButton').click();

  // Reopen update form
  await card.getByTestId('updateCustomerButton').click();

  // Ensure old data is still present
  await expect(page.getByTestId('customerFormFirstName')).not.toHaveValue('DiscardMe');

  await cleanupCustomer(customerId);
});

test('Update Customer - Backend 500 error', async ({ page, goToCustomersPage, createPopulatedCustomer, cleanupCustomer }) => {

  // Register mocks BEFORE any page interactions
  await page.route('**/api/addresses', route =>
    route.fulfill({ status: 500, body: JSON.stringify({ message: 'Server Error' }) })
  );

  await page.route('**/api/emails', route =>
    route.fulfill({ status: 500, body: JSON.stringify({ message: 'Server Error' }) })
  );

  const { customerId } = await createPopulatedCustomer();

  await page.reload({ waitUntil: 'networkidle' });

  const card = page.locator(`[data-customer-id="${customerId}"]`);
  await card.getByTestId('updateCustomerButton').click();

  const updated = customerFactory();

  await fillCustomerFormAndSubmit(page, updated, 'PUT', { expectFailure: true });

  await expect(page.getByText('Server Error')).toBeVisible();

  await cleanupCustomer(customerId);
});

test('Update Customer - Removing email should update card', async ({ page, goToCustomersPage, createPopulatedCustomer, cleanupCustomer }) => {
  const { customerId } = await createPopulatedCustomer();

  await page.reload({ waitUntil: 'networkidle' });

  const card = page.locator(`[data-customer-id="${customerId}"]`);
  await card.getByTestId('updateCustomerButton').click();

  // Remove email row
  await page.getByTestId('removeEmailButton').click();

  const updated = customerFactory({
    emails: []
  });

  await fillCustomerFormAndSubmit(page, updated, 'PUT');

  await expect(
    page.locator('.p-toast-message-success:has-text("Customer Updated Successfully")')
  ).toBeVisible();

  await expect(card).not.toContainText('@');

  await cleanupCustomer(customerId);
});

test('Update Customer - Removing address should update card', async ({ page, goToCustomersPage, createPopulatedCustomer, cleanupCustomer }) => {
  const { customerId } = await createPopulatedCustomer();

  await page.reload({ waitUntil: 'networkidle' });

  const card = page.locator(`[data-customer-id="${customerId}"]`);
  await card.getByTestId('updateCustomerButton').click();

  // Remove address row
  await page.getByTestId('removeAddressButton').click();

  const updated = customerFactory({
    addresses: []
  });

  await fillCustomerFormAndSubmit(page, updated, 'PUT');

  await expect(card).not.toContainText('USA'); // Or original street/city/state

  await cleanupCustomer(customerId);
});