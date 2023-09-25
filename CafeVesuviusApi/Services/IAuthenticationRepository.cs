using CafeVesuviusApi.DTOs;
using CafeVesuviusApi.Entities;
using CafeVesuviusApi.Models;

namespace CafeVesuviusApi.Services
{
    public interface IAuthenticationRepository
    {
        Task<AuthResponse> GetTokenAsync(AuthRequest authRequest, string ipAddress);
        Task<AuthResponse> GetRefreshTokenAsync(string ipAddress, int userId, string userName);
        Task<bool> IsTokenValid(string accessToken, string ipAddress);
        Task<AccessUser> AddUser(AccessUser accessUser);
    }
}