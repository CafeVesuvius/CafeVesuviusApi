using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeVesuviusApi.Models;
using CafeVesuviusApi.Services;

namespace CafeVesuviusApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;

        public MenuController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        // GET: api/Menu
        [HttpGet]
        public async Task<IActionResult> GetNewestMenu()
        {
            if (await _menuRepository.GetAllMenus() == null) return NotFound();
            return Ok(await _menuRepository.GetNewestMenu());
        }
        
        [HttpGet("Active")]
        public async Task<IActionResult> GetActiveMenu()
        {
            if (await _menuRepository.GetAllMenus() == null) return NotFound();
            return Ok(await _menuRepository.GetActiveMenus());
        }
        
        [HttpGet("Changed")]
        public async Task<IActionResult> GetChangedMenu()
        {
            if (await _menuRepository.GetAllMenus() == null) return NotFound();
            return Ok(await _menuRepository.GetLastChanged());
        }
        
        [HttpGet("All")]
        public async Task<IActionResult> GetAllMenu()
        {
            if (await _menuRepository.GetAllMenus() == null) return NotFound();
            return Ok(await _menuRepository.GetAllMenus());
        }
        
        // GET: api/Menu/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuById(long id)
        {
            if (await _menuRepository.GetAllMenus() == null) return NotFound();
            Menu menu = await _menuRepository.GetMenuById(id);
            
            if(menu == null) return NotFound();

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
            bool success = await _menuRepository.UpdateMenu(id, menu);

            if (!success) return NotFound();
            return NoContent();
        }
        
        [HttpPut("Item/{id}")]
        public async Task<IActionResult> PutMenuItem(long id, MenuItem menuItem)
        {
            if (id != menuItem.Id)
            {
                return BadRequest();
            }
            bool success = await _menuRepository.UpdateMenuItem(id, menuItem);

            if (!success) return NotFound();
            return NoContent();
        }

        // POST: api/Menu
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostMenu(Menu menu)
        {
            if (await _menuRepository.GetAllMenus() == null)
            {
                return Problem("Entity set 'CafeVesuviusContext.Menus'  is null.");
            }
            return Ok(await _menuRepository.PostMenu(menu));
        }
        
        // POST: api/Menu
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("Item")]
        [HttpPost]
        public async Task<IActionResult> PostMenuItem(MenuItem menuItem)
        {
            if (await _menuRepository.GetAllMenus() == null)
            {
                return Problem("Entity set 'CafeVesuviusContext.Menus'  is null.");
            }
            return Ok(await _menuRepository.PostMenuItem(menuItem));
        }

        // DELETE: api/Menu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(long id)
        {
            if (await _menuRepository.GetAllMenus() == null) return NotFound();

            bool success = await _menuRepository.DeleteMenu(id);
            if (!success) return NotFound();
            return NoContent();
        }
        
        // DELETE: api/Menu/5
        [HttpDelete("Item/{id}")]
        public async Task<IActionResult> DeleteMenuItem(long id)
        {
            if (await _menuRepository.GetAllMenus() == null) return NotFound();

            bool success = await _menuRepository.DeleteMenuItem(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
