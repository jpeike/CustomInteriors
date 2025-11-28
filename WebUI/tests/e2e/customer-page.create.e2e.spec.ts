import { test } from '../fixtures/customer.fixture';
import { expect } from '@playwright/test';
import { deleteCustomerById } from '../helpers/customer.cleanup';
import { fillCustomerFormAndSubmit } from '../helpers/customer.form';
import { expectCustomerCard, expectCustomerForm } from '../helpers/customer.expect';
import { customerFactory } from '../helpers/customer.factory';

test('Create Customer Using Fixture + Factory', async ({ page, goToCustomersPage, cleanupCustomer }) => {
  await page.getByTestId('addCustomerButton').click();

  const data = customerFactory();

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

test('Create Customer - Required field validation should display errors', async ({ page, goToCustomersPage }) => {
  await page.getByTestId('addCustomerButton').click();

  // Try to submit empty form
  await page.getByTestId('updateFormButton').click();

  // Required field errors
  await expect(page.locator('.p-toast-message:has-text("Customer information not valid")')).toBeVisible();
});

test('Create Customer - Invalid email format should show validation error', async ({ page, goToCustomersPage }) => {
  await page.getByTestId('addCustomerButton').click();

  await page.getByTestId('customerFormFirstName').fill('test');
  await page.getByTestId('customerFormLastName').fill('user');
  await page.getByTestId('customerFormType').fill('testing');
  await page.getByTestId('customerFormEmailAddress').fill('not-an-email');
  await page.getByTestId('updateFormButton').click();

  await expect(page.getByText('address is not valid')).toBeVisible();
});

test('Create Customer - Cancel button closes the form and resets fields', async ({ page, goToCustomersPage }) => {
  await page.getByTestId('addCustomerButton').click();

  const form = page.getByTestId('customerForm');

  // Fill one field
  await page.getByTestId('customerFormFirstName').fill('Temp');

  await page.getByTestId('cancelFormButton').click();
  await expect(form).toBeHidden();

  // Re-open the form - ensure fields reset
  await page.getByTestId('addCustomerButton').click();
  await expect(page.getByTestId('customerFormFirstName')).toHaveValue('');
});

test('Create Customer - Backend 500 error should show error toast', async ({ page, goToCustomersPage }) => {
  await page.route('**/api/customers', route =>
    route.fulfill({ status: 500, body: JSON.stringify({ message: 'Server Error' }) })
  );

  await page.getByTestId('addCustomerButton').click();

  const data = customerFactory();
  await fillCustomerFormAndSubmit(page, data, 'POST', { expectFailure: true });

  await expect(page.getByText('Server Error')).toBeVisible();
});

test('Create Customer - Multiple addresses and emails should appear on customer form', async ({ page, goToCustomersPage, cleanupCustomer }) => {
  await page.getByTestId('addCustomerButton').click();

  const data = customerFactory({
    emails: [
      {
        emailAddress: "test@example.com",
        emailType: "main"
      },
      {
        emailAddress: "test2@example.com",
        emailType: "Secondary"
      }
    ],

    addresses: [
      {
        street: "1234 Test Lane",
        city: "Testville",
        state: "Testorado",
        postalCode: 55555,
        country: "USA",
        addressType: "Primary"
      },
      {
        street: "123 Test2 Lane",
        city: "Testville2",
        state: "Testorado2",
        postalCode: 22222,
        country: "Brazil",
        addressType: "Secondary"
      }
    ]
  });

  const customerId = await fillCustomerFormAndSubmit(page, data, 'POST');

  const card = page.locator(`[data-customer-id="${customerId}"]`);
  await expect(card).toBeVisible();

  await card.getByTestId('updateCustomerButton').click();

  await expectCustomerForm(page, data, customerId);

  await cleanupCustomer(customerId);
});


