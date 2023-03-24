using System.Net;
using System.Text.Json;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NewsFeedApi.Controllers;
using NewsFeedApi.Models;
using NewsFeedApi.Services;
using Newtonsoft.Json.Linq;

namespace NewsFeedApi.Tests.Controllers
{
	public class StoriesControllerTests
	{
        private readonly IHackerNewsService _hackerNewsSvc;
		private readonly StoriesController _controller;

        public StoriesControllerTests()
		{
			_hackerNewsSvc = A.Fake<IHackerNewsService>();
			_controller = new StoriesController(_hackerNewsSvc);
        }

		[Fact]
		public async void StoriesController_GetStories_ReturnOK()
		{
			var stories = A.CollectionOfFake<Story>(10) as List<Story>;
			A.CallTo(() => _hackerNewsSvc.GetLatestStoriesWithDetails()).Returns(Task.FromResult<List<Story>>(stories));

			var result = await _controller.Get() as OkObjectResult;

			result.Should().NotBeNull();
		}
	}
}