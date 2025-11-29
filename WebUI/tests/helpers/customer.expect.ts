// helpers/customer.expect.ts
import { Page, expect } from '@playwright/test';

/**
 * Verify that a customer card displays the expected values
 */
export async function expectCustomerCard(
    page: Page,
    customer: TestCustomer,
    customerId: number
) {
    const fullName = `${customer.firstName} ${customer.lastName}`;

    // Select the correct card
    const card = page.locator(`[data-customer-id="${customerId}"]`);

    await expect(card).toBeVisible();

    // Name
    await expect(card.getByTestId('customerName')).toHaveText(fullName);

    if (customer.perferredContactMethod === 'Email') {
        await expect(card.getByTestId('customerEmail')).toHaveText(customer.emails[0].emailAddress);
    } else {
        //Todo add phone verification later
    }

    // Address (matches your UI’s formatting)
    const formattedAddress =
        `Address: ${customer.addresses[0].street} ${customer.addresses[0].city}, ${customer.addresses[0].state}, ${customer.addresses[0].postalCode} ${customer.addresses[0].country}`;

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

export async function expectCustomerForm(
    page: Page,
    customer: TestCustomer,
    customerId: number
) {
    const form = page.getByTestId('customerForm');
    await expect(form).toBeVisible();

    // Name
    await expect(form.getByTestId('customerFormFirstName')).toHaveValue(customer.firstName);
    await expect(form.getByTestId('customerFormLastName')).toHaveValue(customer.lastName);

    //Type
    await expect(form.getByTestId('customerFormType')).toHaveValue(customer.customerType);

    for (let i = 0; i < customer.emails.length; i++) {
        const email = customer.emails[i];

        const selectedEmail = form.getByTestId('customerEmailForm').nth(i);

        await expect(selectedEmail.getByTestId('customerFormEmailAddress')).toHaveValue(email.emailAddress);
        await expect(selectedEmail.getByTestId('customerFormEmailType')).toHaveValue(email.emailType);
    }

    //TODO when phone is implemented

    for (let i = 0; i < customer.addresses.length; i++) {
        const addr = customer.addresses[i];

        // Select nth address row
        const selectedAddress = page.getByTestId('customerAddressForm').nth(i);

        await expect(selectedAddress.getByTestId('customerFormStreet')).toHaveValue(addr.street);
        await expect(selectedAddress.getByTestId('customerFormCountry')).toHaveValue(addr.country);
        await expect(selectedAddress.getByTestId('customerFormCity')).toHaveValue(addr.city);
        await expect(selectedAddress.getByTestId('customerFormState')).toHaveValue(addr.state);
        await expect(selectedAddress.getByTestId('customerFormPostalCode')).toHaveValue(addr.postalCode.toString());
        await expect(selectedAddress.getByTestId('customerFormAddressType')).toHaveValue(addr.addressType);
    }

    // Customer Type
    await expect(form.getByTestId('customerFormType')).toHaveValue(customer.customerType);

    // Company
    await expect(form.getByTestId('customerFormCompany')).toHaveValue(customer.companyName);

    // Status
    await expect(form.getByTestId('customerFormStatus')).toHaveValue(customer.status);

    // Notes
    await expect(form.getByTestId('customerFormNotes')).toHaveValue(customer.customerNotes);
}
