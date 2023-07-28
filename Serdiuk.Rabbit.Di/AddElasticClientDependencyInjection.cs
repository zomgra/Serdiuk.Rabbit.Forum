using Microsoft.Extensions.DependencyInjection;
using Elasticsearch.Net;
using Nest;
using Microsoft.Extensions.Configuration;
using Serdiuk.Rabbit.Core.Models;

namespace Serdiuk.Rabbit.Di
{
    public static class AddElasticClientDependencyInjection
    {
        public static IServiceCollection AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["ElasticConfiguration:Uri"];
            var defaultIndex = configuration["ElasticConfiguration:Index"];

            var settings = new ConnectionSettings(new Uri(url)).PrettyJson().DefaultIndex(defaultIndex);
            settings.DefaultMappingFor<Forum>(x=>x.IdProperty(f=>f.Id));

            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);

            client.Indices.Create(defaultIndex, d => d.Map<Forum>(x => x.AutoMap()));

            return services;
        }

    }
}
