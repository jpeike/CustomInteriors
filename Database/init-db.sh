#!/bin/bash
# Database Initialization Script for CustomInteriors

set -e

echo "================================"
echo "CustomInteriors Database Setup"
echo "================================"

# Database configuration
DB_NAME="CustomInteriors"
SA_PASSWORD="YourStrong@Passw0rd123"

# Function to execute SQL
run_sql() {
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -d master -Q "$1" -C
}

run_sql_file() {
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -d "$DB_NAME" -i "$1" -C
}

echo "Step 1: Creating database '$DB_NAME'..."
run_sql "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'$DB_NAME') CREATE DATABASE [$DB_NAME];"

echo "Step 2: Creating tables..."

# Create tables in dependency order
echo "  - Creating Customer table..."
run_sql_file "/usr/src/app/tables/Customers.sql"

echo "  - Creating User table..."
run_sql_file "/usr/src/app/tables/Users.sql"

echo "  - Creating Address table..."
run_sql_file "/usr/src/app/tables/Addresses.sql"

echo "  - Creating Email table..."
run_sql_file "/usr/src/app/tables/Emails.sql"

echo "  - Creating Phone table..."
run_sql_file "/usr/src/app/tables/Phone.sql"

echo "  - Creating Job table..."
run_sql_file "/usr/src/app/tables/Job.sql"

echo "  - Creating QuoteRequest table..."
run_sql_file "/usr/src/app/tables/QuoteRequest.sql"

echo "  - Creating JobAssignment table..."
run_sql_file "/usr/src/app/tables/JobAssignment.sql"

echo "  - Creating Invoice table..."
run_sql_file "/usr/src/app/tables/Invoice.sql"

echo "  - Creating JobInvoice table..."
run_sql_file "/usr/src/app/tables/JobInvoice.sql"

echo "  - Creating Payment table..."
run_sql_file "/usr/src/app/tables/Payment.sql"

echo "  - Creating InvoicePayment table..."
run_sql_file "/usr/src/app/tables/InvoicePayment.sql"

echo "  - Creating InvoiceItem table..."
run_sql_file "/usr/src/app/tables/InvoiceItem.sql"

echo "Step 3: Running post-deployment scripts..."

# Create Version table
echo "  - Creating Version tracking table..."
run_sql "USE [$DB_NAME]; IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Version]') AND type = 'U') CREATE TABLE [dbo].[Version] ([ScriptName] NVARCHAR(255) NOT NULL PRIMARY KEY, [AppliedOn] DATETIME NOT NULL);"

# Run versioned scripts
echo "  - Running versioned migration scripts..."
run_sql_file "/usr/src/app/scripts/2025.001.sql"

echo "================================"
echo "Database setup completed successfully!"
echo "Database: $DB_NAME"
echo "Server: localhost,1433"
echo "================================"
