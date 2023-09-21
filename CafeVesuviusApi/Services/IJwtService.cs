using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CafeVesuviusApi.Models;

namespace CafeVesuviusApi.Services
{
    public interface IJwtService
    {
        Task<AuthResponse> GetTokenAsync(AuthRequest authRequest,string ipAddress);
        Task<AuthResponse> GetRefreshTokenAsync(string ipAddress, int userId, string userName);
        Task<bool> IsTokenValid(string accessToken, string ipAddress);
    }
}