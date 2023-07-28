using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Serdiuk.Rabbit.Di
{
    public static class RabbitMQDependencyInjection
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration _configuration)
        {
            var configuration = _configuration.GetSection("RabbitMQ");

            services.AddSingleton<IConnection>(s =>
            {
                var connectionFactory = new ConnectionFactory()
                {
                    Endpoint = new AmqpTcpEndpoint(),
                    DispatchConsumersAsync = true,
                };
                return connectionFactory.CreateConnection();
            });

            return services;
        }
    }
}
