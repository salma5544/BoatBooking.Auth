# BoatBooking Authentication Module

## Overview
This project implements the authentication and authorization module for a boat and trip booking system.
It includes:
- JWT-based authentication
- Role-based access control (Admin, Owner, Customer)
- Admin approval flow for Owners
- Logging using Serilog
- Global error handling
- CQRS pattern with Commands and Queries

## Requirements
- .NET 9
- SQL Server
- Entity Framework Core

## Setup
1. Clone the repository
2. Update `appsettings.json` with your SQL Server connection string and JWT settings
3. Apply migrations:
dotnet ef database update
Or run the SQL Script in `Database/DatabaseScript.sql`
4. Run the application:
dotnet run --project BoatBooking.API

## API Endpoints
- `POST /auth/register` → register a new user
- `POST /auth/login` → login and get JWT token
- `GET /admin/pending-owners` → get pending owners (Admin only)
- `POST /admin/approve-owner/{id}` → approve an owner (Admin only)
- `POST /admin/reject-owner/{id}` → reject an owner (Admin only)

### Example Request/Response
```json
POST /auth/register
{
"fullName": "Salma Galhoum",
"email": "salma@example.com",
"password": "Password123!",
"role": 2
}

Response:
{
"message": "User registered successfully"
}

```
## Notes
1- Owner accounts need Admin approval before logging in.
2- Logging is done with Serilog (console + file)
3- Errors are handled via global middleware and returned as JSON
