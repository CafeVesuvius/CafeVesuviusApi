using CafeVesuviusApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace CafeVesuviusApi.Services
{
    public class MenuRepository : IMenuRepository
    {
        private readonly CafeVesuviusContext _context;

        public MenuRepository(CafeVesuviusContext context)
        {
            _context = context;
        }

        public async Task<Menu> GetNewestMenu()
        {
            Menu? menu = await _context.Menus.OrderBy(Menu => Menu.Id).LastAsync();
            menu.MenuItems = await _context.MenuItems.Where(MenuItem => MenuItem.MenuId == menu.Id).ToListAsync();

            return menu;
        }
        public async Task<IEnumerable<Menu>> GetActiveMenus()
        {
            List<Menu>? menus = await _context.Menus.Where(Menu => Menu.Active).ToListAsync();
            if (menus.Count > 0)
            {
                foreach (Menu menu in menus)
                {
                    menu.MenuItems = await _context.MenuItems.Where(MenuItem => MenuItem.MenuId == menu.Id).ToListAsync();
                }
            }

            return menus;
        }
        public async Task<Menu> GetLastChanged()
        {
            Menu? menu = await _context.Menus.OrderBy(menu => menu.Changed).FirstAsync();
            menu.MenuItems = await _context.MenuItems.Where(MenuItem => MenuItem.MenuId == menu.Id).ToListAsync();

            return menu;
        }

        public async Task<IEnumerable<Menu>> GetAllMenus()
        {
            List<Menu>? menus = await _context.Menus.OrderBy(menu => menu.Changed).ToListAsync();
            if (menus.Count > 0)
            {
                foreach (Menu menu in menus)
                {
                    menu.MenuItems = await _context.MenuItems.Where(MenuItem => MenuItem.MenuId == menu.Id).ToListAsync();
                }
            }
            return menus;
        }
        public async Task<Menu> GetMenuById(long id)
        {
            Menu? menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return menu;
            }
            menu.MenuItems = await _context.MenuItems.Where(MenuItem => MenuItem.MenuId == menu.Id).ToListAsync();

            return menu;
        }

        public async Task<bool> UpdateMenu(long id, Menu menu)
        {
            _context.Entry(menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(_context.Menus?.Any(e => e.Id == id)).GetValueOrDefault())
                {
                    return false;
                }
            }
            return true;
        }
        public async Task<Menu> PostMenu(Menu menu)
        {

            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();

            return menu;
        }
        public async Task<bool> DeleteMenu(long id)
        {
            Menu? menu = await _context.Menus.FindAsync(id);

            if (menu == null) return false;

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
