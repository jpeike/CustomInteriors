#!/bin/bash
# SQL Server Docker Entrypoint with Database Initialization

echo "Starting SQL Server..."

# Start SQL Server in the background
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to start
echo "Waiting for SQL Server to start..."
sleep 30s

# Run the initialization script
echo "Running database initialization..."
/usr/src/app/init-db.sh

echo "Database initialization complete. SQL Server is ready."

# Keep the container running
wait
