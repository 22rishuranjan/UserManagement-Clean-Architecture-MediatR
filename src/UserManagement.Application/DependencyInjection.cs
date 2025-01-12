
using FluentValidation;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Common.Behaviours;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

namespace UserManagement.Application
{
    public  static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });

            var redisOptions = new ConfigurationOptions
            {
                EndPoints = { configuration.GetConnectionString("Redis") }, // Redis endpoint
                AbortOnConnectFail = false,      // Allow retries on connection failure
                ConnectTimeout = 5000,           // Timeout in milliseconds
                SyncTimeout = 5000,              // Sync operation timeout
                KeepAlive = 180,                 // Keep-alive interval in seconds
                                                 // While Redis does not have a true "connection pool", increasing
                                                 // connections is managed by using multiple multiplexers effectively.
            };

            var redis = ConnectionMultiplexer.Connect(redisOptions);

            services.AddSingleton<IConnectionMultiplexer>(redis);
            return services;
        }
    }
}
