using Microsoft.AspNetCore.Mvc;
using NewsFeedApi.Helpers;
using NewsFeedApi.Models;
using NewsFeedApi.Pagination;
using NewsFeedApi.Search;
using NewsFeedApi.Services;

namespace NewsFeedApi.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class StoriesController : Controller
    {
        private IHackerNewsService _hackerNewsSvc;

        public StoriesController(IHackerNewsService hackerNewsService)
        {
            _hackerNewsSvc = hackerNewsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 20, string search = "")
        {
            List<Story> storiesWithDetails = await _hackerNewsSvc.GetLatestStoriesWithDetails();

            if (!storiesWithDetails.Any())
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            PagedList<Story> pagedStories;

            if (search == "")
            {
                pagedStories = PagedList<Story>.ToPagedList(storiesWithDetails, pageNumber, pageSize);
            }
            else
            {
                SearchTokens searchTokens = new SearchTokens(search);
                List<Story> filteredStories = storiesWithDetails.Where(story => story.HasSearchTerms(searchTokens.Filtered)).ToList<Story>();
                pagedStories = PagedList<Story>.ToPagedList(filteredStories, pageNumber, pageSize);
            }

            var responseBody = new
            {
                metadata = ResponseHelper.GetMetadata<Story>(pagedStories, pageNumber, pageSize),
                data = pagedStories,
            };

            return Ok(responseBody);
        }
    }
}

