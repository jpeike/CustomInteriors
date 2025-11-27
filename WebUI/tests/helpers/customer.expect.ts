// helpers/customer.expect.ts
import { Page, expect } from '@playwright/test';

/**
 * Verify that a customer card displays the expected values
 */
export async function expectCustomerCard(
  page: Page,
  customer: {
    firstName: string;
    lastName: string;
    emailAddress: string;
    emailType?: string;
    street: string;
    city: string;
    state: string;
    postalCode: number;
    country: string;
    customerType: string;
    companyName: string;
    status: string;
    customerNotes: string;
  },  
  customerId: number
) {
  const fullName = `${customer.firstName} ${customer.lastName}`;

  // Select the correct card
  const card = page.locator(`[data-customer-id="${customerId}"]`);

  await expect(card).toBeVisible();

  // Name
  await expect(card.getByTestId('customerName')).toHaveText(fullName);

  // Email
  await expect(card.getByTestId('customerEmail')).toHaveText(customer.emailAddress);

  // Address (matches your UI’s formatting)
  const formattedAddress =
    `Address: ${customer.street} ${customer.city}, ${customer.state}, ${customer.postalCode} ${customer.country}`;

  await expect(card.getByTestId('customerAddress')).toHaveText(formattedAddress);

  // Customer Type
  await expect(card.getByTestId('customerType')).toHaveText(`Type: ${customer.customerType}`);

  // Company
  await expect(card.getByTestId('customerCompany')).toContainText(`Company: ${customer.companyName}`);

  // Status
  await expect(card.getByTestId('customerStatus')).toHaveText(`Status: ${customer.status}`);

  // Notes
  await expect(card.getByTestId('customerNotes')).toHaveText(`Notes: ${customer.customerNotes}`);
}
