// tests/helpers/customer.factory.ts

export interface TestCustomer {
  firstName: string;
  lastName: string;
  customerType: string;
  prefferedContactMethod: string;
  companyName: string;
  status: string;
  customerNotes: string;

  emails: TestEmail[];
  addresses: TestAddress[];

  //Todo add phone fields later
}

export interface TestEmail {
  emailAddress: string;
  emailType: string;
}

export interface TestAddress {
  street: string;
  city: string;
  state: string;
  postalCode: number | string;
  country: string;
  addressType: string;
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

    emails: [
      {
        emailAddress: "test@example.com",
        emailType: "main"
      }
    ],

    addresses: [
      {
        street: "123 Test Lane",
        city: "Testville",
        state: "Testorado",
        postalCode: 55555,
        country: "USA",
        addressType: "Primary"
      }
    ],

    //Todo add phone fields later

    ...overrides
  };
}

