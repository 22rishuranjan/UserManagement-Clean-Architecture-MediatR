# User Management Service with Email Notification

This project provides a clean architecture implementation of a User Management service using .NET 8, MediatR for CQRS, and includes various security features such as rate limiting, XSRF protection, CORS, and authorization.

## Features
- **CQRS Pattern**: Utilizes MediatR for command handling.
- **Email Notification**: Sends email notifications upon user creation.
- **Authorization**: Ensures only authorized users can perform specific actions.
- **Rate Limiting**: Protects endpoints from being overwhelmed by requests.
- **CORS Configuration**: Allows secure cross-origin resource sharing.
- **XSRF Protection**: Safeguards against cross-site request forgery attacks.
- **Logging**: Microsoft.Extensions.Logging is used to add logging to debug window as well as console.

## Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or later / Visual Studio Code
- SMTP service for email (optional, for production)

## Installation and Setup
1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-repo/user-management-service.git
   cd user-management-service
   ```

2. **Configure dependencies** in `Program.cs` or `Startup.cs`:
   ```csharp
   var builder = WebApplication.CreateBuilder(args);

   // Add services to the container
   builder.Services.AddUserManagement(builder.Configuration);

   var app = builder.Build();

   // Configure middleware
   app.UseCors("AllowSpecificOrigins");
   app.UseRateLimiter();
   app.UseAntiforgeryMiddleware();
   app.UseAuthorization();

   app.MapControllers();
   app.Run();
   ```

3. **Configure CORS policy** in `AppServiceExtenstion.cs`:
   ```csharp
   services.AddCors(options =>
   {
       options.AddPolicy("AllowSpecificOrigins", builder =>
       {
           builder.WithOrigins("https://example.com")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
       });
   });
   ```

4. **Configure rate limiting**:
   ```csharp
   services.AddRateLimiter(options =>
   {
       options.GlobalLimiter = RateLimitPartition.GetFixedWindowLimiter<string>(
           partitionKeyProvider: httpContext => "global",
           factory: _ => new FixedWindowRateLimiterOptions
           {
               PermitLimit = 100,
               Window = TimeSpan.FromMinutes(1),
               AutoReplenishment = true
           });
   });
   ```

## How to Use
### User Creation
Send a POST request to the designated endpoint (e.g., `/api/users`) with the required data:
```json
{
   "email": "user@example.com",
   "userName": "NewUser"
}
```

### Email Service
Ensure that the `IEmailService` is configured with a real implementation for production use.

## Security Considerations
- **Rate Limiting**: Prevents abuse by limiting the number of requests per minute.
- **XSRF Protection**: Validates tokens to mitigate CSRF attacks.
- **Authorization**: Uses claims-based checks to control access.

## License
This project is licensed under the MIT License.

## Contributing
Pull requests are welcome. For significant changes, please open an issue first to discuss what you would like to change.

## Contact
For questions or support, please contact [22rishuranjan@gmail.com](mailto:your-email@example.com).

---

This README provides an overview of the setup and configuration for implementing secure and efficient user management using clean architecture in .NET 8.

