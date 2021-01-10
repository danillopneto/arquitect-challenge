using ArquitectChallenge.Interfaces.Repository.Events;
using ArquitectChallenge.Interfaces.Services.Events;
using ArquitectChallenge.Services.Implementation.Events;
using ArquitectChallenge.Services.Repository;
using ArquitectChallenge.Services.Repository.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ArquitectChallenge.Configuration
{
    public static class DependencyInjectionConfigure
    {
        public static void ConfigureDependencies(IServiceCollection services, string connectionString)
        {
            services.ConfigureDatabase(connectionString);
            services.ConfigureRepositoryDependencies();
            services.ConfigureServiceDependencies();
        }

        private static void ConfigureDatabase(this IServiceCollection services, string connectionString)
        {
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