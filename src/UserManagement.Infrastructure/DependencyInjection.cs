using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Infrastructure.Data;
using UserManagement.Infrastructure.EmailSender;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Infrastructure
{
    public static class DependencyInjection
    {
      public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            // Add services to the container.
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<IEmailSender, EmailSenderService>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
