// tests/helpers/customer.form.ts

import type { Page } from '@playwright/test';
import type { TestCustomer } from './customer.factory';

export async function fillCustomerFormAndSubmit(page: Page, c: TestCustomer, requestMethod: 'POST' | 'PUT'): Promise<number> | void {
  // Fill all fields
  await page.getByTestId('customerFormFirstName').fill(c.firstName);
  await page.getByTestId('customerFormLastName').fill(c.lastName);
  await page.getByTestId('customerFormType').fill(c.customerType);

  await page.getByTestId('customerFormEmailRadioButton').check();
  await page.getByTestId('customerFormEmailAddress').fill(c.emailAddress);
  await page.getByTestId('customerFormEmailType').fill(c.emailType);

  await page.getByTestId('customerFormStreet').fill(c.street);
  await page.getByTestId('customerFormCountry').fill(c.country);
  await page.getByTestId('customerFormCity').fill(c.city);
  await page.getByTestId('customerFormState').fill(c.state);
  await page.getByTestId('customerFormPostalCode').fill(c.postalCode.toString());
  await page.getByTestId('customerFormAddressType').fill(c.addressType);

  await page.getByTestId('customerFormCompany').fill(c.companyName);
  await page.getByTestId('customerFormStatus').fill(c.status);
  await page.getByTestId('customerFormNotes').fill(c.customerNotes);

  // Submit + wait for POST response
  const [response] = await Promise.all([
    page.waitForResponse(r =>
      r.url().includes('/api/customers') &&
      r.request().method() === requestMethod
    ),
    page.getByTestId('updateFormButton').click()
  ]);

  if (requestMethod === 'POST') {
    const body = await response.json();
    return body.customerId as number;
  }
}
