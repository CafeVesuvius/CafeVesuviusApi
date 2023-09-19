using CafeVesuviusApi.Models;

namespace CafeVesuviusApi.Services
{
    public interface IMenuRepository
    {
        Task<bool> DeleteMenu(long id);
        Task<bool> DeleteMenuItem(long id);
        Task<IEnumerable<Menu>> GetActiveMenus();
        Task<IEnumerable<Menu>> GetAllMenus();
        Task<Menu> GetLastChanged();
        Task<Menu> GetMenuById(long id);
        Task<Menu> GetNewestMenu();
        Task<Menu> PostMenu(Menu menu);
        Task<MenuItem> PostMenuItem(MenuItem menuItem);
        Task<bool> UpdateMenu(long id, Menu menu);
        Task<bool> UpdateMenuItem(long id, MenuItem menuItem);
    }
}