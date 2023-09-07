using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeVesuviusApi.Models;

namespace CafeVesuviusApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly CafeVesuviusContext _context;

        public MenuController(CafeVesuviusContext context)
        {
            _context = context;
        }

        // GET: api/Menu
        [HttpGet]
        public async Task<ActionResult<Menu>> GetNewestMenu()
        {
          if (_context.Menus == null)
          {
              return NotFound();
          }
          
          Menu menu = await _context.Menus.OrderBy(Menu => Menu.Id).LastAsync();
          menu.MenuItems = await _context.MenuItems.Where(MenuItem => MenuItem.MenuId == menu.Id).ToListAsync();

          return Ok(menu);
        }
        
        [Route("Active")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetActiveMenu()
        {
            if (_context.Menus == null)
            {
                return NotFound();
            }

            List<Menu> menus = await _context.Menus.Where(Menu => !Menu.Active).ToListAsync();
            if(menus.Count > 0)
            {
                foreach (Menu menu in menus)
                {
                    menu.MenuItems = await _context.MenuItems.Where(MenuItem => MenuItem.MenuId == menu.Id).ToListAsync();
                }
            } else
            {
                return NotFound();
            }
            return Ok(menus);
        }
        
        [Route("Changed")]
        [HttpGet]
        public async Task<ActionResult<Menu>> GetChangedMenu()
        {
            if (_context.Menus == null)
            {
                return NotFound();
            }
            Menu menu = await _context.Menus.OrderBy(menu => menu.Changed).FirstAsync();
            menu.MenuItems = await _context.MenuItems.Where(MenuItem => MenuItem.MenuId == menu.Id).ToListAsync();

            return Ok(menu);
        }

        [Route("All")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetAllMenu()
        {
            if (_context.Menus == null)
            {
                return NotFound();
            }
            List<Menu> menus = await _context.Menus.OrderBy(menu => menu.Changed).ToListAsync();
            if (menus.Count > 0)
            {
                foreach (Menu menu in menus)
                {
                    menu.MenuItems = await _context.MenuItems.Where(MenuItem => MenuItem.MenuId == menu.Id).ToListAsync();
                }
            } else
            {
                return NotFound();
            }
            return Ok(menus);
        }
        
        // GET: api/Menu/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenuById(long id)
        {
            if (_context.Menus == null)
            {
                return NotFound();
            }
            Menu menu = await _context.Menus.FindAsync(id);

            if (menu == null)
            {
                return NotFound();
            }
            menu.MenuItems = await _context.MenuItems.Where(MenuItem => MenuItem.MenuId == menu.Id).ToListAsync();

            return Ok(menu);
        }

        // PUT: api/Menu/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(long id, Menu menu)
        {
            if (id != menu.Id)
            {
                return BadRequest();
            }

            _context.Entry(menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Menu
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Menu>> PostMenu(Menu menu)
        {
          if (_context.Menus == null)
          {
              return Problem("Entity set 'CafeVesuviusContext.Menus'  is null.");
          }
            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenu", new { id = menu.Id }, menu);
        }

        // DELETE: api/Menu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(long id)
        {
            if (_context.Menus == null)
            {
                return NotFound();
            }
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuExists(long id)
        {
            return (_context.Menus?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
