using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serdiuk.Rabbit.Core.DTO.Request.Forums;
using Serdiuk.Rabbit.Core.Enums;
using Serdiuk.Rabbit.Services.Interfaces;
using System.Text;

namespace Serdiuk.Rabbit.Services.Listeners
{
    public class DbContextListener : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IForumService _forumService;
        private readonly ILogger<DbContextListener> _logger;



        public DbContextListener(IServiceProvider provider) //IForumService forumService)
        {
            var scope = provider.CreateScope();

            _forumService = scope.ServiceProvider.GetRequiredService<IForumService>();
            _logger = scope.ServiceProvider.GetRequiredService<ILogger<DbContextListener>>();
            _connection = scope.ServiceProvider.GetRequiredService<IConnection>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            _logger.LogInformation("Db context listener running on: {Date}", DateTime.UtcNow.ToShortDateString());
            var channel = _connection.CreateModel();

            
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                TakeMessage(sender, e);
                channel.BasicAck(e.DeliveryTag, false);
                return Task.CompletedTask;
            };
            channel.BasicConsume("db.updater", false, consumer);
        }
        private void TakeMessage(object? sender, BasicDeliverEventArgs e)
        {
            _logger.LogInformation("Take message on search engine");
            var typeEvent = Enum.Parse<ForumEventType>(e.BasicProperties.Headers["event_type"].ToString());


            var json = Encoding.UTF8.GetString(e.Body.ToArray());
            switch (typeEvent)
            {
                case ForumEventType.Update:
                    var forumUpdate = JsonConvert.DeserializeObject<UpdateForumDto>(json);
                    _forumService.UpdateForumAsync(forumUpdate);
                    break;
                case ForumEventType.Delete:
                    var forumDelete = JsonConvert.DeserializeObject<DeleteForumDto>(json);
                    _forumService.DeleteForumAsync(forumDelete);
                    break;
                case ForumEventType.Create:
                    var forumCreate = JsonConvert.DeserializeObject<CreateForumDto>(json);
                    _forumService.CreateForumAsync(forumCreate);
                    break;
            }
        }
    }
}
