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
