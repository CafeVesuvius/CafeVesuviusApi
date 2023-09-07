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
        private readonly CafeVesuviusContext _context;
        private readonly IMenuRepository _menuRepository;

        public MenuController(CafeVesuviusContext context, IMenuRepository menuRepository)
        {
            _context = context;
            _menuRepository = menuRepository;
        }

        // GET: api/Menu
        [HttpGet]
        public async Task<ActionResult<Menu>> GetNewestMenu()
        {
          if (await _menuRepository.GetAllMenus() == null) return NotFound();
          return Ok(await _menuRepository.GetNewestMenu());
        }
        
        [Route("Active")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetActiveMenu()
        {
            if (await _menuRepository.GetAllMenus() == null) return NotFound();
            return Ok(await _menuRepository.GetActiveMenus());
        }
        
        [Route("Changed")]
        [HttpGet]
        public async Task<ActionResult<Menu>> GetChangedMenu()
        {
            if (await _menuRepository.GetAllMenus() == null) return NotFound();
            return Ok(await _menuRepository.GetLastChanged());
        }

        [Route("All")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetAllMenu()
        {
            if (await _menuRepository.GetAllMenus() == null) return NotFound();
            return Ok(await _menuRepository.GetAllMenus());
        }
        
        // GET: api/Menu/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenuById(long id)
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
            bool succes = await _menuRepository.UpdateMenu(id, menu);

            if (!succes) return NotFound();
            return NoContent();
        }

        // POST: api/Menu
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Menu>> PostMenu(Menu menu)
        {
            if (await _menuRepository.GetAllMenus() == null)
            {
                return Problem("Entity set 'CafeVesuviusContext.Menus'  is null.");
            }
            return await _menuRepository.PostMenu(menu); ;
        }

        // DELETE: api/Menu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(long id)
        {
            if (await _menuRepository.GetAllMenus() == null) return NotFound();

            bool succes = await _menuRepository.DeleteMenu(id);
            if (!succes) return NotFound();
            return NoContent();
        }
    }
}
