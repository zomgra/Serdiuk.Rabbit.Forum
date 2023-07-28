using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serdiuk.Rabbit.Core.Enums;
using Serdiuk.Rabbit.Core.Models;
using Serdiuk.Rabbit.Services.Interfaces;
using System.Text;

namespace Serdiuk.Rabbit.Services.Listeners
{
    public class ForumSearchEngineListener : BackgroundService
    {
        private readonly ILogger<ForumSearchEngineListener> _logger;
        private readonly IConnection _connectionFactory;
        private readonly ISearchEngineService<Forum> _searchEngineService;

        public ForumSearchEngineListener(IServiceProvider provider)
        {
            using var scope = provider.CreateScope();

            _logger = scope.ServiceProvider.GetRequiredService<ILogger<ForumSearchEngineListener>>();
            _connectionFactory = scope.ServiceProvider.GetRequiredService<IConnection>();
            _searchEngineService = scope.ServiceProvider.GetRequiredService<ISearchEngineService<Forum>>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            _logger.LogInformation("Search engine listener running on: {Date}", DateTime.UtcNow.ToShortDateString());
            var channel = _connectionFactory.CreateModel();

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += (sender, e) => 
            {
                TakeMessage(sender, e);
                channel.BasicAck(e.DeliveryTag, false);
                return Task.CompletedTask;
            };
            channel.BasicConsume("search.engine", false, consumer);
        }

        private void TakeMessage(object? sender, BasicDeliverEventArgs e)
        {
            _logger.LogInformation("Take message on search engine");
            var typeEvent = Enum.Parse<ForumEventType>(e.BasicProperties.Headers["event_type"].ToString());
            

            var json = Encoding.UTF8.GetString(e.Body.ToArray());
            var forum = JsonConvert.DeserializeObject<Forum>(json);

            switch (typeEvent)
            {
                case ForumEventType.Update:
                    _searchEngineService.UpdateIndexAsync(forum);
                    break;
               case ForumEventType.Delete:
                    _searchEngineService.DeleteIndexAsync(forum);
                    break;
                case ForumEventType.Create:
                    _searchEngineService.IndexAsync(forum);
                    break;
            }
           
        }
    }
}
