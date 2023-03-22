using Microsoft.AspNetCore.Mvc;
using NewsFeedApi.Services;

namespace NewsFeedApi.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class StoriesController : Controller
    {
        private HackerNewsService hackerNewsSvc = new HackerNewsService();

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int page = 0, int pageSize = 20)
        {
            List<int> allStoryIds = await hackerNewsSvc.GetLatestStoryIds();

            if (!allStoryIds.Any())
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            List<int> storyIds = allStoryIds.Skip((page + 1) * pageSize).Take(pageSize).ToList();

            List<Story> storiesWithDetails = await hackerNewsSvc.GetStoryDetails(storyIds);

            if (!storiesWithDetails.Any())
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(storiesWithDetails);
        }
    }
}

