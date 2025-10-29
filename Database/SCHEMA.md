# CustomInteriors Database Schema Documentation

## Overview
Complete database schema matching the ERD diagram with all tables and relationships.

---

## Tables

### 1. Customer
**Primary Table** - Stores customer information

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| CustomerID | INT | PK, IDENTITY | Unique customer identifier |
| FirstName | NVARCHAR(100) | NOT NULL | Customer first name |
| LastName | NVARCHAR(100) | NOT NULL | Customer last name |
| CustomerType | NVARCHAR(100) | NOT NULL | Type of customer (e.g., Residential, Commercial) |
| PreferredContactMethod | NVARCHAR(100) | NULL | Preferred way to contact |
| CompanyName | NVARCHAR(100) | NULL | Company name if applicable |
| Status | NVARCHAR(100) | NULL | Customer status |
| CustomerNotes | TEXT | NULL | Additional notes |

**Relationships:**
- One Customer → Many Users (1:0..1)
- One Customer → Many Addresses (1:0..*)
- One Customer → Many Emails (1:0..*)
- One Customer → Many Phones (1:0..*)
- One Customer → Many Jobs (1:1..*)
- One Customer → Many QuoteRequests (1:0..*)

---

### 2. User
**Authentication & Authorization** - User accounts linked to customers

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| UserID | INT | PK, IDENTITY | Unique user identifier |
| CustomerID | INT | FK → Customer, NULL | Link to customer record |
| Username | NVARCHAR(100) | NOT NULL, UNIQUE | Login username |
| Email | NVARCHAR(255) | NOT NULL, UNIQUE | User email address |
| PasswordHash | NVARCHAR(512) | NOT NULL | Hashed password |
| Role | NVARCHAR(100) | NOT NULL | User role (e.g., Customer, Admin, Employee) |
| CreatedOn | DATETIME2(7) | NOT NULL, DEFAULT | Account creation timestamp |

**Relationships:**
- Many Users → One Customer (0..1:1)
- Many Users ↔ Many Jobs (via JobAssignment)

---

### 3. Address
**Location Data** - Customer addresses

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| AddressID | INT | PK, IDENTITY | Unique address identifier |
| CustomerID | INT | FK → Customer, NOT NULL | Customer reference |
| Street | NVARCHAR(255) | NOT NULL | Street address |
| City | NVARCHAR(255) | NOT NULL | City name |
| State | NVARCHAR(255) | NOT NULL | State/Province |
| PostalCode | INT | NOT NULL | ZIP/Postal code |
| AddressType | NVARCHAR(100) | NOT NULL | Type (e.g., Billing, Shipping, Job Site) |

**Relationships:**
- Many Addresses → One Customer (0..*:1) CASCADE DELETE

---

### 4. Email
**Contact Information** - Customer email addresses

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| EmailID | INT | PK, IDENTITY | Unique email identifier |
| CustomerID | INT | FK → Customer, NOT NULL | Customer reference |
| EmailAddress | NVARCHAR(255) | NOT NULL | Email address |
| EmailType | NVARCHAR(100) | NOT NULL | Type (e.g., Personal, Work) |

**Unique Constraint:** (CustomerID, EmailAddress) - prevents duplicate emails per customer

**Relationships:**
- Many Emails → One Customer (0..*:1) CASCADE DELETE

---

### 5. Phone
**Contact Information** - Customer phone numbers

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| PhoneNumber | NVARCHAR(100) | PK | Phone number (unique) |
| CustomerID | INT | FK → Customer, NOT NULL | Customer reference |
| PhoneType | NVARCHAR(100) | NOT NULL | Type (e.g., Mobile, Home, Work) |

**Relationships:**
- Many Phones → One Customer (0..*:1) CASCADE DELETE

---

### 6. Job
**Project Management** - Job/project records

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| JobID | INT | PK, IDENTITY | Unique job identifier |
| CustomerID | INT | FK → Customer, NOT NULL | Customer reference |
| JobDescription | NVARCHAR(255) | NOT NULL | Description of work |
| StartDate | DATE | NOT NULL | Job start date |
| EndDate | DATE | NULL | Job completion date |
| Status | NVARCHAR(100) | NOT NULL | Job status |

**Relationships:**
- Many Jobs → One Customer (1..*:1)
- One Job → Many JobInvoices (1:1..*)
- One Job → Many QuoteRequests (1:0..*)
- Many Jobs ↔ Many Users (via JobAssignment)

---

### 7. JobAssignment
**Junction Table** - Links users to jobs with their role on the job

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| JobID | INT | PK, FK → Job | Job reference |
| UserID | INT | PK, FK → User | User reference |
| AssignmentDate | DATE | NOT NULL | Date assigned |
| RoleOnJob | NVARCHAR(100) | NOT NULL | User's role on this job |

**Composite Primary Key:** (JobID, UserID)

**Relationships:**
- Implements Many-to-Many between Job and User

---

### 8. QuoteRequest
**Sales** - Quote requests from customers

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| QuoteID | INT | PK, IDENTITY | Unique quote identifier |
| CustomerID | INT | FK → Customer, NOT NULL | Customer reference |
| JobID | INT | FK → Job, NOT NULL | Related job |
| RequestDate | DATE | NOT NULL | Date of request |
| DescriptionOfWork | NVARCHAR(255) | NOT NULL | Work description |
| EstimatedPrice | DECIMAL(18,2) | NULL | Estimated cost |

**Relationships:**
- Many QuoteRequests → One Customer (0..*:1)
- Many QuoteRequests → One Job (0..*:1)

---

### 9. Invoice
**Billing** - Invoice records

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| InvoiceID | INT | PK, IDENTITY | Unique invoice identifier |
| DateIssued | NVARCHAR(100) | NOT NULL | Issue date |
| Method | NVARCHAR(100) | NOT NULL | Payment method |
| SellerDetails | NVARCHAR(255) | NOT NULL | Seller information |

**Relationships:**
- One Invoice → Many JobInvoices (1:1..*)
- One Invoice → Many InvoiceItems (1:1..*)
- Many Invoices ↔ Many Payments (via InvoicePayment)

---

### 10. JobInvoice
**Junction Table** - Links jobs to invoices with percentage allocation

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| JobID | INT | PK, FK → Job | Job reference |
| InvoiceID | INT | FK → Invoice, NOT NULL | Invoice reference |
| CreatedDate | DATE | NOT NULL | Creation date |
| PercentageOfInvoice | DECIMAL(5,2) | NOT NULL | Percentage of invoice for this job |

**Primary Key:** JobID

**Relationships:**
- Implements One-to-Many between Job and Invoice

---

### 11. InvoiceItem
**Line Items** - Individual items on invoices

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| ItemID | INT | PK, IDENTITY | Unique item identifier |
| InvoiceID | INT | FK → Invoice, NOT NULL | Invoice reference |
| Description | NVARCHAR(255) | NOT NULL | Item description |
| Amount | INT | NOT NULL | Quantity |
| Price | DECIMAL(18,2) | NOT NULL | Unit price |

**Relationships:**
- Many InvoiceItems → One Invoice (1..*:1) CASCADE DELETE

---

### 12. Payment
**Payments** - Payment records

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| PaymentID | INT | PK, IDENTITY | Unique payment identifier |
| PaymentDate | DATE | NOT NULL | Date of payment |
| AmountPaid | DECIMAL(18,2) | NOT NULL | Amount paid |
| Method | NVARCHAR(100) | NOT NULL | Payment method |

**Relationships:**
- Many Payments ↔ Many Invoices (via InvoicePayment)

---

### 13. InvoicePayment
**Junction Table** - Links invoices to payments

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| InvoiceID | INT | PK, FK → Invoice | Invoice reference |
| PaymentID | INT | PK, FK → Payment | Payment reference |

**Composite Primary Key:** (InvoiceID, PaymentID)

**Relationships:**
- Implements Many-to-Many between Invoice and Payment

---

### 14. Version
**Schema Management** - Database migration tracking

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| ScriptName | NVARCHAR(255) | PK | Migration script name |
| AppliedOn | DATETIME | NOT NULL | Application timestamp |

---

## Entity Relationships Summary

```
Customer (Central Entity)
├── User (0..1:1)
├── Address (0..*:1) CASCADE DELETE
├── Email (0..*:1) CASCADE DELETE
├── Phone (0..*:1) CASCADE DELETE
├── Job (1..*:1)
│   ├── JobAssignment (M:N with User)
│   ├── QuoteRequest (0..*:1)
│   └── JobInvoice (1..*:1)
│       └── Invoice
│           ├── InvoiceItem (1..*:1) CASCADE DELETE
│           └── InvoicePayment (M:N with Payment)
└── QuoteRequest (0..*:1)

User
└── JobAssignment (M:N with Job)

Invoice
├── JobInvoice (linked to Job)
├── InvoiceItem (1..*:1) CASCADE DELETE
└── InvoicePayment (M:N with Payment)

Payment
└── InvoicePayment (M:N with Invoice)
```

---

## Sample Data

The schema includes sample data:
- 1 Customer: John Doe (Residential)
- 1 User: demoUser linked to John Doe with Customer role

---

## Notes

- All CASCADE DELETE relationships ensure data integrity
- Unique constraints prevent duplicate emails per customer
- Phone numbers are unique across all customers
- JobAssignment and InvoicePayment are junction tables for many-to-many relationships
- Version table tracks all database migration scripts
