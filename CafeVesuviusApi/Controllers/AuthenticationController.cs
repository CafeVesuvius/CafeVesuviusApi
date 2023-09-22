using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CafeVesuviusApi.Models;
using CafeVesuviusApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeVesuviusApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthenticationController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] AuthRequest authRequest)
        {
            var token = _jwtService.GetTokenAsync(authRequest, HttpContext.Connection.RemoteIpAddress.ToString()).Result.Token;
            if (string.IsNullOrEmpty(token))
                return Unauthorized();
            return Ok(token);
        }
    }
}
