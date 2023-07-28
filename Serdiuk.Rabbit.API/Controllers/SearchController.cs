using Microsoft.AspNetCore.Mvc;
using Serdiuk.Rabbit.Core.Models;
using Serdiuk.Rabbit.Services.Interfaces;

namespace Serdiuk.Rabbit.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchEngineService<Forum> _searchEngineService;

        public SearchController(ISearchEngineService<Forum> searchEngineService)
        {
            _searchEngineService = searchEngineService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string key)
        {
            var result = await _searchEngineService.SearchAsync(key);
            return Ok(result);
        }
    }
}
