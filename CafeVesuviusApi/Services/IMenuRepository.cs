using CafeVesuviusApi.Entities;

namespace CafeVesuviusApi.Services
{
    public interface IMenuRepository
    {
        Task<bool> DeleteMenu(int id);
        Task<bool> DeleteMenuItem(int id);
        Task<IEnumerable<Menu>> GetActiveMenus();
        Task<IEnumerable<Menu>> GetAllMenus();
        Task<Menu> GetLastChanged();
        Task<Menu> GetMenuById(int id);
        Task<Menu> GetNewestMenu();
        Task<Menu> PostMenu(Menu menu);
        Task<MenuItem> PostMenuItem(MenuItem menuItem);
        Task<bool> UpdateMenu(int id, Menu menu);
        Task<bool> UpdateMenuItem(int id, MenuItem menuItem);
    }
}