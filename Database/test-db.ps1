# Database Container Test Script# Database Container Test Script

# This script tests Phase 1: Database Setup# This script tests Phase 1: Database Setup



Write-Host "================================" -ForegroundColor CyanWrite-Host "================================" -ForegroundColor Cyan

Write-Host "CustomInteriors Database Test" -ForegroundColor CyanWrite-Host "CustomInteriors Database Test" -ForegroundColor Cyan

Write-Host "================================" -ForegroundColor CyanWrite-Host "================================" -ForegroundColor Cyan

Write-Host ""Write-Host ""



$containerName = "custominteriors-sqlserver"$containerName = "custominteriors-sqlserver"

$saPassword = "YourStrong@Passw0rd123"$saPassword = "YourStrong@Passw0rd123"

$dbName = "CustomInteriors"$dbName = "CustomInteriors"



# Test 1: Container Running# Test 1: Container Running

Write-Host "Test 1: Checking if container is running..." -ForegroundColor YellowWrite-Host "Test 1: Checking if container is running..." -ForegroundColor Yellow

$containerRunning = docker ps --filter "name=$containerName" --format "{{.Names}}"$containerRunning = docker ps --filter "name=$containerName" --format "{{.Names}}"

if ($containerRunning -eq $containerName) {if ($containerRunning -eq $containerName) {

    Write-Host "[PASS] Container is running" -ForegroundColor Green    Write-Host "✓ PASS: Container is running" -ForegroundColor Green

} else {} else {

    Write-Host "[FAIL] Container is not running" -ForegroundColor Red    Write-Host "✗ FAIL: Container is not running" -ForegroundColor Red

    Write-Host "Run: docker-compose -f docker-compose.db.yml up -d" -ForegroundColor Yellow    Write-Host "Run: docker-compose -f docker-compose.db.yml up -d" -ForegroundColor Yellow

    exit 1    exit 1

}}



# Test 2: Container Health# Test 2: Container Health

Write-Host "`nTest 2: Checking container health..." -ForegroundColor YellowWrite-Host "`nTest 2: Checking container health..." -ForegroundColor Yellow

Start-Sleep -Seconds 5Start-Sleep -Seconds 5

$health = docker inspect $containerName --format='{{.State.Health.Status}}' 2>$null$health = docker inspect $containerName --format='{{.State.Health.Status}}' 2>$null

if ($health -eq "healthy") {if ($health -eq "healthy") {

    Write-Host "[PASS] Container is healthy" -ForegroundColor Green    Write-Host "✓ PASS: Container is healthy" -ForegroundColor Green

} else {} else {

    Write-Host "[WARN] Container health status: $health (may still be starting)" -ForegroundColor Yellow    Write-Host "⚠ WARNING: Container health status: $health (may still be starting)" -ForegroundColor Yellow

    Write-Host "Waiting for container to become healthy..." -ForegroundColor Yellow    Write-Host "Waiting for container to become healthy..." -ForegroundColor Yellow

    Start-Sleep -Seconds 10    Start-Sleep -Seconds 10

}}



# Test 3: SQL Server Responsive# Test 3: SQL Server Responsive

Write-Host "`nTest 3: Testing SQL Server connection..." -ForegroundColor YellowWrite-Host "`nTest 3: Testing SQL Server connection..." -ForegroundColor Yellow

docker exec $containerName /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $saPassword -Q "SELECT @@VERSION" -h -1 2>$null | Out-Nulldocker exec $containerName /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $saPassword -Q "SELECT @@VERSION" -h -1 2>$null | Out-Null

if ($LASTEXITCODE -eq 0) {if ($LASTEXITCODE -eq 0) {

    Write-Host "[PASS] SQL Server is responsive" -ForegroundColor Green    Write-Host "✓ PASS: SQL Server is responsive" -ForegroundColor Green

} else {} else {

    Write-Host "[FAIL] Cannot connect to SQL Server" -ForegroundColor Red    Write-Host "✗ FAIL: Cannot connect to SQL Server" -ForegroundColor Red

    exit 1    exit 1

}}



# Test 4: Database Exists# Test 4: Database Exists

Write-Host "`nTest 4: Checking if CustomInteriors database exists..." -ForegroundColor YellowWrite-Host "`nTest 4: Checking if CustomInteriors database exists..." -ForegroundColor Yellow

$dbExists = docker exec $containerName /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $saPassword -Q "SELECT name FROM sys.databases WHERE name = '$dbName'" -h -1 2>$null$dbExists = docker exec $containerName /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $saPassword -Q "SELECT name FROM sys.databases WHERE name = '$dbName'" -h -1 2>$null

if ($dbExists -match $dbName) {if ($dbExists -match $dbName) {

    Write-Host "[PASS] CustomInteriors database exists" -ForegroundColor Green    Write-Host "✓ PASS: CustomInteriors database exists" -ForegroundColor Green

} else {} else {

    Write-Host "[FAIL] CustomInteriors database not found" -ForegroundColor Red    Write-Host "✗ FAIL: CustomInteriors database not found" -ForegroundColor Red

    exit 1    exit 1

}}



# Test 5: Tables Created# Test 5: Tables Created

Write-Host "`nTest 5: Checking if tables are created..." -ForegroundColor YellowWrite-Host "`nTest 5: Checking if tables are created..." -ForegroundColor Yellow

$tables = docker exec $containerName /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $saPassword -d $dbName -Q "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'" -h -1 2>$null$tables = docker exec $containerName /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $saPassword -d $dbName -Q "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'" -h -1 2>$null



$expectedTables = @("Users", "Customers", "Addresses", "Emails", "Employees", "Version")$expectedTables = @("Users", "Customers", "Addresses", "Emails", "Employees", "Version")

$missingTables = @()$missingTables = @()



foreach ($table in $expectedTables) {foreach ($table in $expectedTables) {

    if ($tables -match $table) {    if ($tables -match $table) {

        Write-Host "  [OK] $table table exists" -ForegroundColor Green        Write-Host "  ✓ $table table exists" -ForegroundColor Green

    } else {    } else {

        Write-Host "  [MISSING] $table table missing" -ForegroundColor Red        Write-Host "  ✗ $table table missing" -ForegroundColor Red

        $missingTables += $table        $missingTables += $table

    }    }

}}



if ($missingTables.Count -eq 0) {if ($missingTables.Count -eq 0) {

    Write-Host "[PASS] All tables created successfully" -ForegroundColor Green    Write-Host "✓ PASS: All tables created successfully" -ForegroundColor Green

} else {} else {

    Write-Host "[FAIL] Missing tables: $($missingTables -join ', ')" -ForegroundColor Red    Write-Host "✗ FAIL: Missing tables: $($missingTables -join ', ')" -ForegroundColor Red

    exit 1    exit 1

}}



# Test 6: Sample Data# Test 6: Sample Data

Write-Host "`nTest 6: Checking for sample data..." -ForegroundColor YellowWrite-Host "`nTest 6: Checking for sample data..." -ForegroundColor Yellow

$userCount = docker exec $containerName /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $saPassword -d $dbName -Q "SELECT COUNT(*) FROM Users" -h -1 2>$null$userCount = docker exec $containerName /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $saPassword -d $dbName -Q "SELECT COUNT(*) FROM Users" -h -1 2>$null

if ($userCount -match "\d+" -and [int]$matches[0] -gt 0) {if ($userCount -match "\d+" -and [int]$matches[0] -gt 0) {

    Write-Host "[PASS] Sample data exists (Users: $($matches[0]))" -ForegroundColor Green    Write-Host "✓ PASS: Sample data exists (Users: $($matches[0]))" -ForegroundColor Green

} else {} else {

    Write-Host "[WARN] No sample data found" -ForegroundColor Yellow    Write-Host "⚠ WARNING: No sample data found" -ForegroundColor Yellow

}}



# Test 7: Version Tracking# Test 7: Version Tracking

Write-Host "`nTest 7: Checking version tracking..." -ForegroundColor YellowWrite-Host "`nTest 7: Checking version tracking..." -ForegroundColor Yellow

$version = docker exec $containerName /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $saPassword -d $dbName -Q "SELECT ScriptName, AppliedOn FROM Version" -h -1 2>$null$version = docker exec $containerName /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $saPassword -d $dbName -Q "SELECT ScriptName, AppliedOn FROM Version" -h -1 2>$null

if ($version -match "2025.001") {if ($version -match "2025.001") {

    Write-Host "[PASS] Migration script 2025.001 applied" -ForegroundColor Green    Write-Host "✓ PASS: Migration script 2025.001 applied" -ForegroundColor Green

} else {} else {

    Write-Host "[WARN] Migration tracking not found" -ForegroundColor Yellow    Write-Host "⚠ WARNING: Migration tracking not found" -ForegroundColor Yellow

}}



# Summary# Summary

Write-Host "`n================================" -ForegroundColor CyanWrite-Host "`n================================" -ForegroundColor Cyan

Write-Host "Test Summary" -ForegroundColor CyanWrite-Host "Test Summary" -ForegroundColor Cyan

Write-Host "================================" -ForegroundColor CyanWrite-Host "================================" -ForegroundColor Cyan

Write-Host "[SUCCESS] All critical tests passed!" -ForegroundColor GreenWrite-Host "✓ All critical tests passed!" -ForegroundColor Green

Write-Host ""Write-Host ""

Write-Host "Connection String:" -ForegroundColor YellowWrite-Host "Connection String:" -ForegroundColor Yellow

Write-Host "Server=localhost,1433;Database=$dbName;User Id=sa;Password=$saPassword;TrustServerCertificate=True;" -ForegroundColor WhiteWrite-Host "Server=localhost,1433;Database=$dbName;User Id=sa;Password=$saPassword;TrustServerCertificate=True;" -ForegroundColor White

Write-Host ""Write-Host ""

Write-Host "To connect via sqlcmd:" -ForegroundColor YellowWrite-Host "To connect via sqlcmd:" -ForegroundColor Yellow

Write-Host "docker exec -it $containerName /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $saPassword -d $dbName" -ForegroundColor WhiteWrite-Host "docker exec -it $containerName /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Passw0rd123 -d $dbName" -ForegroundColor White

Write-Host ""Write-Host ""

