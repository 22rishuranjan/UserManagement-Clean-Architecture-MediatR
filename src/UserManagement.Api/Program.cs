using System.Runtime.CompilerServices;
using UserManagement.Api.Extensions;

namespace UserManagement.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

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
