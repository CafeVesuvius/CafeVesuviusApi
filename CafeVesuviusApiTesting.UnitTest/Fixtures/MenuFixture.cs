using CafeVesuviusApi.Entities;
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
            IsActive = true,
            ChangedDate = DateTime.Now,
            MenuItems = new List<MenuItem>
            {
                new MenuItem
                {
                    Id=1,
                    Name = "MenuItem1",
                    Description = "MenuItem1",
                    IsActive = true,
                    UnitPrice = 100,
                    MenuId = 1,
                }
            }
        };
    }
}
