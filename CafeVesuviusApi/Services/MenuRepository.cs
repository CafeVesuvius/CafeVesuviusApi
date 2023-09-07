using CafeVesuviusApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeVesuviusApi.Services
{
    public class MenuRepository
    {
        private readonly CafeVesuviusContext _context;

        public MenuRepository(CafeVesuviusContext context)
        {
            _context = context;
        }

        public Menu GetNewestMenu()
        {
            Menu? menu = _context.Menus.OrderBy(Menu => Menu.Id).Last();
            menu.MenuItems = _context.MenuItems.Where(MenuItem => MenuItem.MenuId == menu.Id).ToList();

            return menu;
        }
        public IEnumerable<Menu> GetActiveMenus()
        {
            List<Menu>? menus = _context.Menus.Where(Menu => !Menu.Active).ToList();
            if (menus.Count > 0)
            {
                foreach (Menu menu in menus)
                {
                    menu.MenuItems = _context.MenuItems.Where(MenuItem => MenuItem.MenuId == menu.Id).ToList();
                }
            }

            return menus;
        }
        public Menu GetLastChanged()
        {
            Menu? menu = _context.Menus.OrderBy(menu => menu.Changed).First();
            menu.MenuItems = _context.MenuItems.Where(MenuItem => MenuItem.MenuId == menu.Id).ToList();

            return menu;
        }

        public IEnumerable<Menu> GetAllMenus()
        {
            List<Menu>? menus = _context.Menus.OrderBy(menu => menu.Changed).ToList();
            if (menus.Count > 0)
            {
                foreach (Menu menu in menus)
                {
                    menu.MenuItems = _context.MenuItems.Where(MenuItem => MenuItem.MenuId == menu.Id).ToList();
                }
            }

            return menus;
        }
        public Menu GetMenuById(long id)
        {
            Menu? menu = _context.Menus.Find(id);
            if (menu == null)
            {
                return menu;
            }
            menu.MenuItems = _context.MenuItems.Where(MenuItem => MenuItem.MenuId == menu.Id).ToList();

            return menu;
        }

        public void UpdateMenu(long id, Menu menu)
        {
            _context.Entry(menu).State = EntityState.Modified;

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
                {
                    return;
                }
                else
                {
                    throw;
                }
            }
        }
        private bool MenuExists(long id)
        {
            return (_context.Menus?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
