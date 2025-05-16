# Account Registration API

A .NET Core Web API for user account registration with verification flow, following SOLID principles and Entity Framework Core code-first approach.

## Features

- **User Registration** with IC number validation
- **User Login** with IC number validation
- **4-digit verification code** sent via mobile and email
- **Privacy Policy agreement** handling
- **6-digit PIN setup** with confirmation
- **Biometric registration** flow
- **Account verification** resend functionality
- **Dashboard/Index** page access
  
## Technologies

- .NET Core 8
- Entity Framework Core (Code First)
- SQL Server
- Swagger/OpenAPI documentation

## API Endpoints

### 1. Account Registration

**Endpoint**: `POST /api/account/create-account`

**Request**:
```json
{
  "customerName": "John Doe",
  "icNumber": "123456789012",
  "mobileNumber": "+60123456789",
  "emailAddress": "john@example.com"
}
```
**Responses**:

200 OK: Verification code sent
400 Bad Request: Account already exists or validation error

### 2. Verify Account
**Endpoint**: POST /api/account/verify

**Request**:

```json
{
  "icNumber": "123456789012",
  "verificationCode": "1234"
}
```
**Responses**:

200 OK: Account verified successfully
400 Bad Request: Invalid code or expired

### 3. Resend Verification Code
**Endpoint**: POST /api/account/resend-verification

**Request**:

```json
{
  "icNumber": "123456789012"
}
```
**Responses**:

200 OK: New verification code sent
400 Bad Request: Account not found

### 4. Privacy Policy Agreement
**Endpoint**: POST /api/account/privacy-policy

**Request**:

```json
{
  "icNumber": "123456789012",
  "agreed": true
}
```
### 5. Setup PIN
**Endpoint**: POST /api/account/setup-pin

**Request**:

```json
{
  "icNumber": "123456789012",
  "pin": "123456",
  "confirmPin": "123456"
}
```
### 6. Setup Biometric
**Endpoint**: POST /api/account/setup-biometric

**Request**:

```json
{
  "icNumber": "123456789012",
  "fingerprintData": "<biometric-data>"
}
```

