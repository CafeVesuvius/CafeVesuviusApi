using CafeVesuviusApi.Controllers;
using CafeVesuviusApi.Entities;
using CafeVesuviusApi.Services;
using CafeVesuviusApiTesting.UnitTest.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeVesuviusApiTesting.UnitTest.Systems.Controllers
{
    public class TestMenuController
    {
        [Fact]
        public async Task Get_OnSuccess_InvokesMenuServiceExactlyOnce()
        {
            //Arrange
            var mockMenuService = new Mock<IMenuRepository>();
            mockMenuService
                .Setup(service => service.GetNewestMenu())
                .ReturnsAsync(new Menu());
            var sut = new MenuController(mockMenuService.Object);

            //Act
            var result = await sut.GetNewestMenu();

            //Assert
            mockMenuService.Verify(service => service.GetNewestMenu(), Times.Once);
        }
        [Fact]
        public async Task Get_OnSuccess_ReturnsNewestMenu()
        {
            //Arrange
            var mockMenuService = new Mock<IMenuRepository>();
            mockMenuService
                .Setup(service => service.GetNewestMenu())
                .ReturnsAsync(MenuFixture.GetOneMenu());
            var sut = new MenuController(mockMenuService.Object);

            //Act
            var result = await sut.GetNewestMenu();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = result as OkObjectResult;
            objectResult.Value.Should().BeOfType<Menu>();
        }
    }
}
