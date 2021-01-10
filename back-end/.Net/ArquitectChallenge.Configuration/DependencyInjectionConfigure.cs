using ArquitectChallenge.Interfaces.Repository.Events;
using ArquitectChallenge.Interfaces.Services.Events;
using ArquitectChallenge.Services.Implementation.Events;
using ArquitectChallenge.Services.Repository;
using ArquitectChallenge.Services.Repository.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArquitectChallenge.Configuration
{
    public static class DependencyInjectionConfigure
    {
        public static void ConfigureDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDatabase(configuration);
            services.ConfigureRepositoryDependencies();
            services.ConfigureServiceDependencies();
        }

        private static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(opt => opt.UseMySql(connectionString));
        }

        private static void ConfigureRepositoryDependencies(this IServiceCollection services)
        {
            services.AddScoped<IEventRepository, EventRepository>();
        }

        private static void ConfigureServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();
        }
    }
}