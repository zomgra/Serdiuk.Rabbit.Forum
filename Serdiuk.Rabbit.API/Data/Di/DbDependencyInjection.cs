using Microsoft.EntityFrameworkCore;
using Serdiuk.Rabbit.Core.Interfaces;

namespace Serdiuk.Rabbit.API.Data.Di
{
    public static class DbDependencyInjection
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(s => s.UseSqlite(configuration.GetConnectionString("SQLite")), ServiceLifetime.Transient);

            services.AddTransient<IAppDbContext, AppDbContext>();

            return services;
        }
    }
}
