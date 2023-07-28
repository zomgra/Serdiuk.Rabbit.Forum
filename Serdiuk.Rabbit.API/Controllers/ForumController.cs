using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Serdiuk.Rabbit.Core.DTO.Request.Forums;
using Serdiuk.Rabbit.Core.Enums;
using Serdiuk.Rabbit.Services.Interfaces;
using System.Text;

namespace Serdiuk.Rabbit.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ForumController : ControllerBase
    {
        private readonly IForumService _forumService;
        private readonly IConnection _connection;

        public ForumController(IForumService forumService, IConnection connection)
        {
            _forumService = forumService;
            _connection = connection;
        }

        [HttpGet]
        public async Task<IActionResult> GetForums()
        {
            var forums = await _forumService.GetForumsAsync();
            return Ok(forums);
        }
        [HttpPost]
        public async Task<IActionResult> CreateForum(CreateForumDto dto)
        {
            var channel = _connection.CreateModel();
            var properties = channel.CreateBasicProperties();

            var header = new Dictionary<string, object>();
            header.Add("event_type", (int)(ForumEventType.Create));

            properties.Headers = header;
            channel.BasicPublish("forum.event",
                routingKey: "forum.event.create",
                properties,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto))
                );
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateForum(UpdateForumDto dto)
        {
            var channel = _connection.CreateModel();
            var properties = channel.CreateBasicProperties();

            var header = new Dictionary<string, object>();
            header.Add("event_type", (int)(ForumEventType.Update));

            properties.Headers = header;
            channel.BasicPublish("forum.event",
                routingKey: "forum.event.update",
                properties,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto))
                );
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteForum(DeleteForumDto dto)
        {
            var channel = _connection.CreateModel();
            var properties = channel.CreateBasicProperties();

            var header = new Dictionary<string, object>();
            header.Add("event_type", (int)(ForumEventType.Delete));

            properties.Headers = header;
            channel.BasicPublish("forum.event",
                routingKey: "forum.event.delete",
                properties,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto))
                );
            return Ok();
        }
    }
}
