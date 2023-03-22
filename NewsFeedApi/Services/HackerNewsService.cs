namespace NewsFeedApi.Services
{
	public class HackerNewsService
	{
		private HttpClient client;

		public HackerNewsService()
		{
			client = new HttpClient();
			client.BaseAddress = new Uri("https://hacker-news.firebaseio.com");
        }

		public async Task<List<int>> GetLatestStoryIds()
		{
            HttpResponseMessage response = await client.GetAsync("v0/newstories.json");

            if (!response.IsSuccessStatusCode)
            {
                return new List<int>();
            }

            return await response.Content.ReadFromJsonAsync<List<int>>();
        }

		public async Task<List<Story>> GetStoryDetails(List<int> storyIds)
		{
            List<Story> stories = new List<Story>();

            foreach (int storyId in storyIds)
            {
                // Check for story in cache before making API call

                HttpResponseMessage storyDetails = await client.GetAsync($"v0/item/{storyId}.json");
                Story story = await storyDetails.Content.ReadFromJsonAsync<Story>();
                stories.Add(story);

                // Add story to cache
            }

            return stories;
        }
	}
}

