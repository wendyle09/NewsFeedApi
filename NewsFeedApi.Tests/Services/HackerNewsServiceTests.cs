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
	public class HackerNewsServiceTests
	{
        private readonly IMemoryCache _memoryCache;

        public HackerNewsServiceTests()
		{
            _memoryCache = A.Fake<IMemoryCache>();
        }
	}
}