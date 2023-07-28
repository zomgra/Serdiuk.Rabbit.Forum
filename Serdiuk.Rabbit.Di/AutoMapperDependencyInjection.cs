using Microsoft.Extensions.DependencyInjection;
using Serdiuk.Rabbit.Core.Mappers;

namespace Serdiuk.Rabbit.Di
{
    public static class AutoMapperDependencyInjection
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(x => x.AddProfile(new ApplicationMapperProfile()));
            return services;
        }
    }
}
