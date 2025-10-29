# CustomInteriors Database Container

This directory contains the Docker configuration for the SQL Server database used by CustomInteriors.

## Quick Start

### 1. Build and Start Database Container

```powershell
# From the project root
docker-compose -f docker-compose.db.yml up --build
```

### 2. Verify Database is Running

```powershell
# Check container status
docker ps

# View logs
docker-compose -f docker-compose.db.yml logs -f
```

### 3. Connect to Database

**Connection String:**
```
Server=localhost,1433;Database=CustomInteriors;User Id=sa;Password=YourStrong@Passw0rd123;TrustServerCertificate=True;
```

**Using sqlcmd:**
```powershell
docker exec -it custominteriors-sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P 'YourStrong@Passw0rd123' -C
```

**Using Azure Data Studio / SSMS:**
- Server: `localhost,1433`
- Authentication: SQL Server Authentication
- Username: `sa`
- Password: `YourStrong@Passw0rd123`
- Database: `CustomInteriors`

## Testing

### Test 1: Container Health
```powershell
docker inspect custominteriors-sqlserver --format='{{json .State.Health}}'
```

### Test 2: Database Exists
```powershell
docker exec custominteriors-sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P 'YourStrong@Passw0rd123' -Q "SELECT name FROM sys.databases WHERE name = 'CustomInteriors'" -C
```

### Test 3: Tables Created
```powershell
docker exec custominteriors-sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P 'YourStrong@Passw0rd123' -d CustomInteriors -Q "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME" -C
```

### Test 4: Sample Data
```powershell
docker exec custominteriors-sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P 'YourStrong@Passw0rd123' -d CustomInteriors -Q "SELECT * FROM Customer" -C
docker exec custominteriors-sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P 'YourStrong@Passw0rd123' -d CustomInteriors -Q "SELECT * FROM [User]" -C
```

## Database Schema

The following tables are created automatically:
- **User** - Application users with customer linkage and roles
- **Customer** - Customer information
- **Address** - Customer addresses (linked to Customer)
- **Email** - Customer emails (linked to Customer)
- **Phone** - Customer phone numbers (linked to Customer)
- **Job** - Job/project information (linked to Customer)
- **JobAssignment** - Links Users to Jobs with their roles
- **QuoteRequest** - Quote requests (linked to Customer and Job)
- **JobInvoice** - Links Jobs to Invoices
- **Invoice** - Invoice details
- **InvoiceItem** - Line items on invoices
- **Payment** - Payment records
- **InvoicePayment** - Links Invoices to Payments
- **Version** - Database migration tracking

## Data Persistence

Database data is stored in a Docker volume: `custominteriors-sqlserver-data`

To view volumes:
```powershell
docker volume ls | Select-String "custominteriors"
```

To remove all data and start fresh:
```powershell
docker-compose -f docker-compose.db.yml down -v
```

## Troubleshooting

### Container won't start
- Check if port 1433 is already in use
- Verify SA password meets complexity requirements
- Check logs: `docker-compose -f docker-compose.db.yml logs`

### Can't connect to database
- Wait 30-60 seconds for SQL Server to fully initialize
- Check health status: `docker ps` (should show "healthy")
- Verify firewall allows port 1433

### Tables not created
- Check initialization logs: `docker logs custominteriors-sqlserver`
- Rebuild container: `docker-compose -f docker-compose.db.yml up --build --force-recreate`

## Cleanup

```powershell
# Stop and remove containers (keeps data)
docker-compose -f docker-compose.db.yml down

# Stop and remove containers AND data
docker-compose -f docker-compose.db.yml down -v
```
