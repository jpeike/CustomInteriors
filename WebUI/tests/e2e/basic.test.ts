import { test, expect } from '@playwright/test';

test('CRM customers page loads successfullya', async ({ page }) => {

  await page.goto('/customers');
  
  await expect(page.getByRole('heading', { name: "Customers" })).toBeVisible();
});
