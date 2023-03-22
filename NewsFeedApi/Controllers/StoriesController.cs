using System.Text.Json.Serialization;
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
        public async Task<IActionResult> Get(int currentPage = 1, int pageSize = 20)
        {
            List<int> allStoryIds = await hackerNewsSvc.GetLatestStoryIds();

            if (!allStoryIds.Any())
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            PagedList<int> storyIds = PagedList<int>.ToPagedList(allStoryIds, currentPage, pageSize);

            List<Story> storiesWithDetails = await hackerNewsSvc.GetStoryDetails(storyIds);

            if (!storiesWithDetails.Any())
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var responseBody = new
            {
                metadata = GetMetadata<int>(storyIds, currentPage, pageSize, "stories"),
                data = storiesWithDetails,
            };

            return Ok(responseBody);
        }

        private Object GetMetadata<T>(PagedList<T> pagedList, int currentPage, int pageSize, string queryPath)
        {
            var links = GetPaginationLinks<T>(pagedList, currentPage, pageSize, queryPath);

            var metadata = new
            {
                pagedList.CurrentPage,
                pagedList.HasNext,
                pagedList.HasPrevious,
                pagedList.PageSize,
                pagedList.TotalCount,
                pagedList.TotalPages,
                links
            };

            return metadata;
        }

        private Object GetPaginationLinks<T> (PagedList<T> pagedList, int currentPage, int pageSize, string queryPath)
        {
            return new
            {
                self = $"/{queryPath}? page={currentPage}&pageSize={pageSize}",
                previous = pagedList.HasPrevious ? $"/{queryPath}?page={currentPage - 1}&pageSize={pageSize}" : null,
                next = pagedList.HasNext ? $"/{queryPath}?page={currentPage}&pageSize={pageSize}" : null,
                first = $"/{queryPath}?page=1&pageSize={pageSize}",
                last = $"/{queryPath}?page={pagedList.TotalPages}&pageSize={pageSize}"
            };
        }
    }
}

