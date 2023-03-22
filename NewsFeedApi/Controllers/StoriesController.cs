using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NewsFeedApi.Helpers;
using NewsFeedApi.Models;
using NewsFeedApi.Services;

namespace NewsFeedApi.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class StoriesController : Controller
    {
        private IMemoryCache _memoryCache;
        private HackerNewsService _hackerNewsSvc;

        public StoriesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _hackerNewsSvc = new HackerNewsService(_memoryCache);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int currentPage = 1, int pageSize = 20, string search = "")
        {
            List<Story> storiesWithDetails = await _hackerNewsSvc.GetLatestStoriesWithDetails();

            if (!storiesWithDetails.Any())
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            PagedList<Story> pagedStories;

            if (search == "")
            {
                pagedStories = PagedList<Story>.ToPagedList(storiesWithDetails, currentPage, pageSize);
            }
            else
            {
                SearchTokens searchTokens = new SearchTokens(search);
                List<Story> filteredStories = storiesWithDetails.Where(story => story.HasSearchTerms(searchTokens.Filtered)).ToList<Story>();
                pagedStories = PagedList<Story>.ToPagedList(filteredStories, currentPage, pageSize);
            }

            var responseBody = new
            {
                metadata = ResponseHelper.GetMetadata<Story>(pagedStories, currentPage, pageSize, "stories"),
                data = pagedStories,
            };

            return Ok(responseBody);
        }
    }
}

