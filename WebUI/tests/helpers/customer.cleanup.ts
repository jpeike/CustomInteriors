// tests/helpers/customer.cleanup.ts

import { request, expect } from '@playwright/test';

export async function deleteCustomerById(customerId: number) {
  const api = await request.newContext({
    baseURL: 'https://localhost:44351'
  });

  const res = await api.delete(`/api/customers/${customerId}`);
  expect(res.ok()).toBeTruthy();

  await api.dispose();
}
