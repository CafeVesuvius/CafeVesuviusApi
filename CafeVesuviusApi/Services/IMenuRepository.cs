using CafeVesuviusApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeVesuviusApi.Services
{
    public interface IMenuRepository
    {
        Task<bool> DeleteMenu(long id);
        Task<IEnumerable<Menu>> GetActiveMenus();
        Task<IEnumerable<Menu>> GetAllMenus();
        Task<Menu> GetLastChanged();
        Task<Menu> GetMenuById(long id);
        Task<Menu> GetNewestMenu();
        Task<Menu> PostMenu(Menu menu);
        Task<bool> UpdateMenu(long id, Menu menu);
    }
}