using DogsHouseService.API;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.Tests
{
	public class RateLimiterTests : IClassFixture<WebApplicationFactory<Program>>
	{
		private readonly WebApplicationFactory<Program> _factory;

		public RateLimiterTests(WebApplicationFactory<Program> factory)
		{
			_factory = factory;
		}

		[Fact]
		public async Task RateLimiter_Should_Return_429_When_Too_Many_Requests()
		{
			var client = _factory.CreateClient();
			const int requestLimit = 10;
			int totalRequests = requestLimit + 2;

			HttpResponseMessage? lastResponse = null;

			for (int i = 0; i < totalRequests; i++)
			{
				lastResponse = await client.GetAsync("/ping");
			}

			Assert.NotNull(lastResponse);
			Assert.Equal(HttpStatusCode.TooManyRequests, lastResponse.StatusCode);
		}
	}
}
