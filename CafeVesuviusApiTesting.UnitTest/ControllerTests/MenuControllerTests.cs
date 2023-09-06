using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using System.Threading.Tasks;
using CafeVesuviusApi.Models;
using CafeVesuviusApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeVesuviusApiTesting.UnitTest.ControllerTests
{
    public class MenuControllerTests
    {

        [Fact]
        public void Get_Always_NewestMenuByLargestId()
        {
            //Arrange
            var mockMenus = new Menu[]
            {
                new Menu{Id = 2, Active = false, Name = "MockTest1", Season = "Fall", Changed = DateTime.Now, MenuItems = new List<MenuItem>()},
                new Menu{Id = 3, Active = false, Name = "MockTest1", Season = "Fall", Changed = DateTime.Now, MenuItems = new List<MenuItem>()}
            }.AsQueryable();

            var mockRepository = new Mock<CafeVesuviusContext>();

            mockRepository.Setup(m => m.Menus);

            var controller = new MenuController(mockRepository.Object);

            //Act
            var result = controller.GetNewestMenu().Result as OkObjectResult;


            //Assert
            //Assert.NotNull(result);

            Assert.Equal(3, result.Value.Id);
        }
    }
}
