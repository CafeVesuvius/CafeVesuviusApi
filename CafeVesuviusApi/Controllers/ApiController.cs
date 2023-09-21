using CafeVesuviusApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeVesuviusApi.Controllers
{
    [Route("api/Fox")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        [HttpGet]
        public FileContentResult GetFox()
        {
            Byte[] b = System.IO.File.ReadAllBytes(@"Images/fox.jpg");
            return File(b, "image/jpeg");
        }
    }
}