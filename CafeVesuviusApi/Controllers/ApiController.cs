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
    public class ApiController : ControllerBase
    {
        // GET: api/Menu
        [Route("Fox")]
        [HttpGet]
        public async Task<ActionResult<Menu>> GetFox()
        {
            Byte[] b = System.IO.File.ReadAllBytes(@"Images/fox.jpg");
            return File(b, "image/jpeg");
        }
    }
}
