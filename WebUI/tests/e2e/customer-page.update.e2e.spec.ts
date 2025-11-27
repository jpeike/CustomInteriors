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
    emailAddress: 'gnewell@gmail.com',
    emailType: 'primary',
    street: '1234 South Ave',
    city: 'Miami',
    state: 'Florida',
    postalCode: 12345,
    addressType: 'Primary',
    country: 'USA',
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
