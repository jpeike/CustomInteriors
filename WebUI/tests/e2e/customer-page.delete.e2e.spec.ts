import { test } from '../fixtures/customer.fixture';
import { expect } from '@playwright/test';

test('Delete Customer', async ({ page, goToCustomersPage, createTestCustomer }) => {
  const customerId = await createTestCustomer();

  await page.reload({ waitUntil: 'networkidle' });

  const card = page.locator(`[data-customer-id="${customerId}"]`);
  await expect(card).toBeVisible();

  // Delete
  await card.getByTestId('deleteCustomerButton').click();
  await page.getByTestId('deleteConfirmationButton').click();

  // Confirm REST API DELETE call succeeded
  const deleteResponse = await page.waitForResponse(res =>
    res.url().includes(`/api/customers/${customerId}`) &&
    res.request().method() === 'DELETE'
  );

  expect(deleteResponse.ok()).toBeTruthy();

  // Confirm toast
  await expect(
    page.locator('.p-toast-message-success:has-text("Customer Deleted Successfully")')
  ).toBeVisible();

  // Confirm customer card disappears from UI
  await page.reload({ waitUntil: 'networkidle' });
  await expect(
    page.locator(`[data-customer-id="${customerId}"]`)
  ).toHaveCount(0);
});

test('Delete Customer - Canceling delete should keep the customer', async ({ page, goToCustomersPage, createTestCustomer, cleanupCustomer }) => {
  const customerId = await createTestCustomer();

  await page.reload({ waitUntil: 'networkidle' });

  const card = page.locator(`[data-customer-id="${customerId}"]`);
  await expect(card).toBeVisible();

  // Start delete flow
  await card.getByTestId('deleteCustomerButton').click();

  // CANCEL deletion
  await page.getByTestId('deleteCancelButton').click();

  // Customer should still be present
  await expect(
    page.locator(`[data-customer-id="${customerId}"]`)
  ).toBeVisible();

  await cleanupCustomer(customerId);
});

test('Delete Customer - Backend 500 error should show error toast', async ({ page, goToCustomersPage, createTestCustomer, cleanupCustomer }) => {
  const customerId = await createTestCustomer();

  await page.reload({ waitUntil: 'networkidle' });

  // Mock DELETE failure
  await page.route(`**/api/customers/${customerId}`, route =>
    route.fulfill({
      status: 500,
      body: JSON.stringify({ message: 'Server Error' })
    })
  );

  const card = page.locator(`[data-customer-id="${customerId}"]`);
  await expect(card).toBeVisible();

  await card.getByTestId('deleteCustomerButton').click();
  await page.getByTestId('deleteConfirmationButton').click();

  // Verify error toast
  await expect(
    page.locator('.p-toast-message-error:has-text("Server Error")')
  ).toBeVisible();

  // Customer should still remain in the UI
  await expect(card).toBeVisible();

  await cleanupCustomer(customerId);
});