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

        var app = builder.Build();

        app.AddAppMiddleware();


        app.MapControllers();
        app.Run();
    }
}
