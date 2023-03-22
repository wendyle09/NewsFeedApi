using Microsoft.Extensions.Caching.Memory;
using NewsFeedApi.Models;

namespace NewsFeedApi.Services
{
	public class HackerNewsService
	{
        private readonly IMemoryCache _memoryCache;
        private HttpClient client;

		public HackerNewsService(IMemoryCache memoryCache)
		{
			client = new HttpClient();
			client.BaseAddress = new Uri("https://hacker-news.firebaseio.com");

            _memoryCache = memoryCache;
        }

		public async Task<List<int>> GetLatestStoryIds()
		{
            HttpResponseMessage response = await client.GetAsync("v0/newstories.json");

            if (!response.IsSuccessStatusCode)
            {
                return new List<int>();
            }

            return await response.Content.ReadFromJsonAsync<List<int>>() ?? new List<int>();
        }

		public async Task<List<Story>> GetStoryDetails(List<int> storyIds)
		{
            List<Story> stories = new List<Story>();

            foreach (int storyId in storyIds)
            {
                Story? cachedStory = _memoryCache.Get<Story>(key: storyId.ToString());

                if (cachedStory is not null)
                {
                    stories.Add(cachedStory);
                }
                else
                {
                    HttpResponseMessage storyDetails = await client.GetAsync($"v0/item/{storyId}.json");
                    Story? story = await storyDetails.Content.ReadFromJsonAsync<Story>();

                    if (story is not null)
                    {
                        _memoryCache.Set(storyId.ToString(), story, GetCacheItemPolicy());
                        stories.Add(story);
                    }
                }
            }

            return stories;
        }

        private MemoryCacheEntryOptions GetCacheItemPolicy()
        {
            MemoryCacheEntryOptions policy = new MemoryCacheEntryOptions();
            policy.SlidingExpiration = TimeSpan.FromHours(1);

            return policy;
        }
	}
}

