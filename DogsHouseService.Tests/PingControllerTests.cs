using DogsHouseService.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsHouseService.Tests
{
	public class PingControllerTests
	{
		[Fact]
		public void GetPing_WhenCalled_ReturnsCorrectString()
		{
			var controller = new PingController();

			var result = controller.GetPing();

			var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

			okResult.Value.Should().Be("Dogshouseservice.Version1.0.1");
		}
	}
}
