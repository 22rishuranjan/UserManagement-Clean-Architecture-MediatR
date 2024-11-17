using UserManagement.Api.Middleware;

namespace UserManagement.Api.Extensions;

public static class AppMiddlewareExtension
{
    public static void AddAppMiddleware(this WebApplication app)
    {
        // Add middleware to the request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwaggerMiddleware();
        }

        app.UseCors("AllowSpecificOrigins");
        app.UseRateLimiter();
        app.UseResponseCompression();
        
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAntiforgeryMiddleware();
  
    }


    public static void UseAntiforgeryMiddleware(this IApplicationBuilder app)
    {
       // app.UseMiddleware<AntiforgeryMiddleware>();
    }

    public static void UseSwaggerMiddleware(this IApplicationBuilder app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API V1");
            c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
        });
    }
}

