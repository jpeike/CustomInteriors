// tests/helpers/customer.factory.ts

export interface TestCustomer {
  firstName: string;
  lastName: string;
  customerType: string;
  prefferedContactMethod: string;
  companyName: string;
  status: string;
  customerNotes: string;

  street: string;
  city: string;
  state: string;
  postalCode: number;
  country: string;
  addressType: string;

  emailAddress: string;
  emailType: string;
}

export function customerFactory(overrides: Partial<TestCustomer> = {}): TestCustomer {
  return {
    firstName: "Test",
    lastName: "User",
    customerType: "Individual",
    prefferedContactMethod: "Email",
    companyName: "Default Co.",
    status: "Active",
    customerNotes: "Generated via factory",

    street: "123 Test Lane",
    city: "Testville",
    state: "Testorado",
    postalCode: 55555,
    country: "USA",
    addressType: "Primary",

    emailAddress: "testuser@example.com",
    emailType: "main",

    ...overrides
  };
}
