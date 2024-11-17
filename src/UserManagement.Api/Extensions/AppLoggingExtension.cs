namespace UserManagement.Api.Extensions;

public static class AppLoggingExtension
{
    public static void AddAppLogging(this WebApplicationBuilder builder)
    {
        // Configure logging
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole(); // Add console logging
        builder.Logging.AddDebug(); // Add debug logging (useful in development)
    }
}