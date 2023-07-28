using Nest;
using Serdiuk.Rabbit.Core.Models;
using Serdiuk.Rabbit.Services.Interfaces;
using static RabbitMQ.Client.Logging.RabbitMqClientEventSource;

namespace Serdiuk.Rabbit.Services
{
    public class ElasticSearchForumService : ISearchEngineService<Forum>
    {
        private readonly IElasticClient _elasticClient;

        public ElasticSearchForumService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<bool> DeleteIndexAsync(Forum index)
        {
            var result = await _elasticClient.DeleteAsync<Forum>(index.Id);
            return result.IsValid;
        }

        public async Task<bool> IndexAsync(Forum index)
        {
            var result = await _elasticClient.IndexDocumentAsync(index);
            return result.IsValid;
        }

        public async Task<List<Forum>> SearchAsync(string key)
        {
            var result = await _elasticClient.SearchAsync<Forum>(
            s => s.Query(sq =>
                   sq.MultiMatch(mm => mm
                       .Query(key)
                       .Fuzziness(Fuzziness.Auto)
                   )
               )
           );

            return result.Documents.ToList();
        }

        public async Task<bool> UpdateIndexAsync(Forum index)
        {
            var result = await _elasticClient.UpdateAsync<Forum>(index, u=>u.Doc(index));
            return result.IsValid;
        }
    }
}
