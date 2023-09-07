using CafeVesuviusApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeVesuviusApiTesting.UnitTest.Fixtures
{
    public static class MenuFixture
    {
        public static Menu GetOneMenu() => new()
        {
            Id = 1,
            Name = "Menu1",
            Season = "Winter",
            Active = true,
            Changed = DateTime.Now,
            MenuItems = new List<MenuItem>
            {
                new MenuItem
                {
                    Id=1,
                    Name = "MenuItem1",
                    Description = "MenuItem1",
                    Active = true,
                    UnitPrice = 100,
                    MenuId = 1,
                }
            }
        };
    }
}
