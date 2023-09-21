using CafeVesuviusApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CafeVesuviusApi.Controllers
{
    [Route("api/Fox")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<Menu>> GetFox()
        {
            Byte[] b = System.IO.File.ReadAllBytes(@"Images/fox.jpg");
            return File(b, "image/jpeg");
        }
    }
}