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

## Project Structure

### Solution Structure

Models - Domain entities
DTOs - Data Transfer Objects
Services - Business logic
Controllers - API endpoints
DbContext - Database context

### 1. Models
 let's create our domain models:

 ```
// Models/Account.cs

    public class Account
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }
        
        [Required]
        [StringLength(20)]
        public string ICNumber { get; set; }
        
        [Required]
        [StringLength(15)]
        public string MobileNumber { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string EmailAddress { get; set; }
        
        public bool IsVerified { get; set; } = false;
        public string VerificationCode { get; set; }
        public DateTime? VerificationCodeExpiry { get; set; }
        
        public bool PrivacyPolicyAgreed { get; set; } = false;
        public string PinHash { get; set; }
        public bool IsBiometricSet { get; set; } = false;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
```

### 2. DTOs
- RegistrationDto.cs

```
 public class RegistrationDto
    {
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(20)]
        public string ICNumber { get; set; }

        [Required]
        [StringLength(15)]
        public string MobileNumber { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string EmailAddress { get; set; }
    }
```

- VerificationDto.cs

```
 public class VerificationDto
    {
        [Required]
        public string ICNumber { get; set; }
        
        [Required]
        [StringLength(4)]
        public string VerificationCode { get; set; }
    }
```

- PrivacyPolicyDto.cs

```
public class PrivacyPolicyDto
    {
        [Required]
        public string ICNumber { get; set; }
        
        [Required]
        public bool Agreed { get; set; }
    }
```

- PinSetupDto.cs

```
 public class PinSetupDto
    {
        [Required]
        public string ICNumber { get; set; }
        
        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string Pin { get; set; }
        
        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string ConfirmPin { get; set; }
    }
```

- BiometricDto.cs

```
 public class BiometricDto
    {
        [Required]
        public string ICNumber { get; set; }
        
        [Required]
        public string FingerprintData { get; set; }
    }
```
