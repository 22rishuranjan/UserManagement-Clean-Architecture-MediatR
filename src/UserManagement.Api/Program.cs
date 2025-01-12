
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
using UserManagement.Api.Extensions;

namespace UserManagement.Api;

public class Program
{
    public static void Main(string[] args)
    {

        // Set minimum threads in the thread pool
        ThreadPool.SetMinThreads(5000, 5000);

        var builder = WebApplication.CreateBuilder(args);

        // Configure Kestrel server
        builder.WebHost.ConfigureKestrel(serverOptions =>
        {
            serverOptions.Limits.MaxConcurrentConnections = 5000;
            serverOptions.Limits.MaxConcurrentUpgradedConnections = 5000;
            serverOptions.Limits.MaxRequestBodySize = 10 * 1024 * 1024; // 10 MB
            
            serverOptions.ListenAnyIP(5002); // Listen on port 5000 for all network interfaces
        });


        builder.AddAppLogging();
        builder.Services.AddAppServices(builder.Configuration);
        // Customise default API behaviour
        builder.Services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        var app = builder.Build();

        app.AddAppMiddleware();


        app.MapControllers();

        app.Run();
    }
}
