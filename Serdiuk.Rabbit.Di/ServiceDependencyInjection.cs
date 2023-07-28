using Microsoft.Extensions.DependencyInjection;
using Serdiuk.Rabbit.Core.Models;
using Serdiuk.Rabbit.Services;
using Serdiuk.Rabbit.Services.Interfaces;

namespace Serdiuk.Rabbit.Di
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IForumService, ForumService>();
            services.AddTransient<ISearchEngineService<Forum>, ElasticSearchForumService>();
            return services;
        }
    }
}
