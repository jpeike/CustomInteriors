# SQL Server 2022 with CustomInteriors database initialization
FROM mcr.microsoft.com/mssql/server:2022-latest

# Set environment variables
ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=YourStrong@Passw0rd123
ENV MSSQL_PID=Developer

# Create directory for initialization scripts
USER root
RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

# Copy all SQL scripts into the container
COPY ./Tables/*.sql ./tables/
COPY ./Scripts/*.sql ./scripts/
COPY ./PostDeployment.sql ./
COPY ./entrypoint.sh ./
COPY ./init-db.sh ./

# Make scripts executable
RUN chmod +x ./entrypoint.sh
RUN chmod +x ./init-db.sh

# Switch back to mssql user for running SQL Server
USER mssql

# Expose SQL Server port
EXPOSE 1433

# Use custom entrypoint that initializes the database
ENTRYPOINT ["./entrypoint.sh"]
