using System.Net;
using System.Text.Json;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NewsFeedApi.Controllers;
using NewsFeedApi.Helpers;
using NewsFeedApi.Models;
using NewsFeedApi.Pagination;
using NewsFeedApi.Services;
using Newtonsoft.Json.Linq;

namespace NewsFeedApi.Tests.Controllers
{
	public class ResponseHelperTests
	{
		[Fact]
		public void GetMetadata_ReturnsObject()
		{
			List<Story> stories = A.CollectionOfFake<Story>(10) as List<Story>;
			PagedList<Story> pagedStories = PagedList<Story>.ToPagedList(stories, 1, 5);

			var result = ResponseHelper.GetMetadata<Story>(pagedStories, 1, 5);

			result.Should().NotBeNull();
		}
	}
}