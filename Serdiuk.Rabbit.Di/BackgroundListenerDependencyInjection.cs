using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serdiuk.Rabbit.Services.Listeners;

namespace Serdiuk.Rabbit.Di
{
    public static class BackgroundListenerDependencyInjection
    {
        public static IServiceCollection AddListener(this IServiceCollection services)
        {
            services.AddHostedService<DbContextListener>();
            services.AddHostedService<ForumSearchEngineListener>();
            //services.AddScoped<DbContextListener>();
            //services.AddScoped<ForumSearchEngineListener>();
            return services;
        }
    }
}
