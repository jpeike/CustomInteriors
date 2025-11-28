// tests/helpers/customer.form.ts

import type { Page } from '@playwright/test';
import type { TestCustomer } from './customer.factory';

export async function fillCustomerFormAndSubmit(page: Page, c: TestCustomer, requestMethod: 'POST' | 'PUT', opts: { expectFailure?: boolean } = {}): Promise<number | undefined> {
    // Fill all fields
    await page.getByTestId('customerFormFirstName').fill(c.firstName);
    await page.getByTestId('customerFormLastName').fill(c.lastName);
    await page.getByTestId('customerFormType').fill(c.customerType);

    if (c.prefferedContactMethod === 'Email') {
        await page.getByTestId('customerFormEmailRadioButton').check();
        for (let i = 0; i < c.emails.length; i++) {
            const email = c.emails[i];

            // Add extra rows if needed
            if (i > 0) {
                await page.getByTestId('addEmailButton').click();
            }

            const form = page.getByTestId('customerEmailForm').nth(i);

            await form.getByTestId('customerFormEmailAddress').fill(email.emailAddress);
            await form.getByTestId('customerFormEmailType').fill(email.emailType);
        }
    } else {
        // TODO Add phone handling later
    }

    for (let i = 0; i < c.addresses.length; i++) {
        const addr = c.addresses[i];

        // Add additional rows if needed
        if (i > 0) {
            await page.getByTestId('addAddressButton').click();
        }

        // Select nth address row
        const form = page.getByTestId('customerAddressForm').nth(i);

        await form.getByTestId('customerFormStreet').fill(addr.street);
        await form.getByTestId('customerFormCountry').fill(addr.country);
        await form.getByTestId('customerFormCity').fill(addr.city);
        await form.getByTestId('customerFormState').fill(addr.state);
        await form.getByTestId('customerFormPostalCode').fill(addr.postalCode.toString());
        await form.getByTestId('customerFormAddressType').fill(addr.addressType);
    }


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

    // page.on('request', r => console.log('➡', r.url(), r.method()));
    // page.on('response', r => console.log('⬅', r.url(), r.status()));

    if (opts.expectFailure) {
        // Do NOT parse JSON
        // Just return undefined and allow the test to check the toast
        return;
    }

    if (requestMethod === 'POST') {
        const body = await response.json();
        return body.customerId as number;
    }
}
