import { test } from '../fixtures/customer.fixture';
import { expect } from '@playwright/test';
import { deleteCustomerById } from '../helpers/customer.cleanup';
import { fillCustomerFormAndSubmit } from '../helpers/customer.form';
import { expectCustomerCard } from '../helpers/customer.expect';
import { customerFactory } from '../helpers/customer.factory';

test('Create Customer Using Fixture + Factory', async ({ page, goToCustomersPage, cleanupCustomer }) => {
  await page.getByTestId('addCustomerButton').click();

  const data = customerFactory({
    firstName: 'Test',
    lastName: 'User',
    customerType: 'Individual',
    emailAddress: 'test@example.com',
    emailType: 'main',
    street: '123 Test St',
    city: 'Menomonie',
    state: 'WI',
    postalCode: '54751',
    country: 'USA',
    addressType: 'Home',
    companyName: 'Test Inc',
    status: 'Active',
    customerNotes: 'E2E test'
  });

  const customerId = await fillCustomerFormAndSubmit(page, data, 'POST');

  // Toast
  await expect(
    page.locator('.p-toast-message-success:has-text("Customer Created Successfully")')
  ).toBeVisible();

  // UI card verification
  await expectCustomerCard(page, data, customerId);

  // Form should close
  const form = page.getByTestId('customer-form');
  await expect(form).toBeHidden();

  // Cleanup
  await cleanupCustomer(customerId);
});
