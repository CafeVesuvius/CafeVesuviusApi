using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CafeVesuviusApi.Entities;
using CafeVesuviusApi.Services;
using Microsoft.AspNetCore.Authorization;

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
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuById(int id)
        {
            if (await _menuRepository.GetAllMenus() == null) return NotFound();
            Menu menu = await _menuRepository.GetMenuById(id);
            
            if(menu == null) return NotFound();

            return Ok(menu);
        }
        
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(int id, Menu menu)
        {
            if (id != menu.Id)
            {
                return BadRequest();
            }
            bool success = await _menuRepository.UpdateMenu(id, menu);

            if (!success) return NotFound();
            return NoContent();
        }
        
        [Authorize]
        [HttpPut("Item/{id}")]
        public async Task<IActionResult> PutMenuItem(int id, MenuItem menuItem)
        {
            if (id != menuItem.Id)
            {
                return BadRequest();
            }
            bool success = await _menuRepository.UpdateMenuItem(id, menuItem);

            if (!success) return NotFound();
            return NoContent();
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostMenu(Menu menu)
        {
            if (await _menuRepository.GetAllMenus() == null)
            {
                return Problem("Entity set 'CafeVesuviusContext.Menus'  is null.");
            }
            return Ok(await _menuRepository.PostMenu(menu));
        }
        
        [Authorize]
        [HttpPost("Item")]
        public async Task<IActionResult> PostMenuItem(MenuItem menuItem)
        {
            if (await _menuRepository.GetAllMenus() == null)
            {
                return Problem("Entity set 'CafeVesuviusContext.Menus'  is null.");
            }
            return Ok(await _menuRepository.PostMenuItem(menuItem));
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            if (await _menuRepository.GetAllMenus() == null) return NotFound();

            bool success = await _menuRepository.DeleteMenu(id);
            if (!success) return NotFound();
            return NoContent();
        }
        
        [Authorize]
        [HttpDelete("Item/{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            if (await _menuRepository.GetAllMenus() == null) return NotFound();

            bool success = await _menuRepository.DeleteMenuItem(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
