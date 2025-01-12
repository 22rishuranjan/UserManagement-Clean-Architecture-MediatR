# User Management Service with Email Notification

This project provides a clean architecture implementation of a User Management service using .NET 8, MediatR for CQRS, and includes various security features such as rate limiting, XSRF protection, CORS, and authorization.

## Features
- **CQRS Pattern**: Utilizes MediatR for command handling.
- **Email Notification**: Sends email notifications
- **Rate Limiting**: Protects endpoints from being overwhelmed by requests.
- **CORS Configuration**: Allows secure cross-origin resource sharing.
- **XSRF Protection**: Safeguards against cross-site request forgery attacks. //Disabled for now.
- **Logging**: Microsoft.Extensions.Logging is used to add logging to debug window as well as console.

## Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or later / Visual Studio Code
- SMTP service for email (optional, for production)
- PaperCut SMTP Server ( https://github.com/ChangemakerStudios/Papercut-SMTP/releases)

## Installation and Setup
1. **Clone the repository**:
   ```bash
   git clone https://github.com/22rishuranjan/UserManagement-Clean-Architecture-MediatR.git
   cd user-management-service
   ```

2. **Configure MOCK SMPTP SERVER** in `Program.cs` or `Startup.cs`:
   ```csharp
    Download PaperCut to receive email. 
      https://github.com/ChangemakerStudios/Papercut-SMTP/releases
   ```


## How to Use
### Email Send
Send a POST request to the designated endpoint (e.g., `https://localhost:44353/send`) with the required data:
```json
{
   "email": "user@example.com",
   "userName": "NewUser"
}

Validations:
  a. Email must not be blank
  b.Email format should be correct
  c. Email must contain @dso.org.sg domain.
```

Send a POST request to the designated endpoint (e.g., `https://localhost:44353/validate`) with the required data:
```json
{
   "email": "user@example.com",
   "code": "Otp"
}

Validations:
  a. Email must not be blank
  b. Email format should be correct
  c. Email must contain @dso.org.sg domain.
  d. Otp must not be blank
  e. Otp lenght should be 6 digit
```

### Email Service
Install Papercut https://github.com/ChangemakerStudios/Papercut-SMTP/releases to mock the SMTP server.

## Security Considerations
- **Rate Limiting**: Prevents abuse by limiting the number of requests per minute.
- **XSRF Protection**: Validates tokens to mitigate CSRF attacks.


## License
This project is licensed under the MIT License.

## Contributing
Pull requests are welcome. For significant changes, please open an issue first to discuss what you would like to change.

## Contact
For questions or support, please contact [22rishuranjan@gmail.com](mailto:your-email@example.com).

---

This README provides an overview of the setup and configuration for implementing secure and efficient user management using clean architecture in .NET 8.




