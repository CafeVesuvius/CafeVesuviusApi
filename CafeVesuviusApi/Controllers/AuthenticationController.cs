using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CafeVesuviusApi.Entities;
using CafeVesuviusApi.Models;
using CafeVesuviusApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeVesuviusApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        
        public AuthenticationController(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] AuthRequest authRequest)
        {
            var token = _authenticationRepository.GetTokenAsync(authRequest, HttpContext.Connection.RemoteIpAddress.ToString()).Result.Token;
            if (string.IsNullOrEmpty(token))
                return Unauthorized();
            return Ok(token);
        }
        
        [HttpPost("User"), Authorize]
        public async Task<IActionResult> PostUser(AccessUser accessUser)
        {
            await _authenticationRepository.AddUser(accessUser);
            return NoContent();
        }
    }
}
