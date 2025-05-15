# Account Registration API

A .NET Core Web API for user account registration with verification flow, following SOLID principles and Entity Framework Core code-first approach.

## Features

- **User Registration** with IC number validation
- **4-digit verification code** sent via mobile and email
- **Privacy Policy agreement** handling
- **6-digit PIN setup** with confirmation
- **Biometric registration** flow
- **Account verification** resend functionality

## Technologies

- .NET Core 6+
- Entity Framework Core (Code First)
- SQL Server
- Swagger/OpenAPI documentation

## API Endpoints

### 1. Account Registration

**Endpoint**: `POST /api/account/register`

**Request**:
```json
{
  "customerName": "John Doe",
  "icNumber": "123456789012",
  "mobileNumber": "+60123456789",
  "emailAddress": "john@example.com"
}

