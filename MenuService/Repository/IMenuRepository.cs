using MenuService.Models;

namespace MenuService.Repository
{
    public interface IMenuRepository 
    {
        Task <Menu?> GetByIdAsync (int id);
        Task <List<Menu>> GetAllAsync();

        Task AddAsync (Menu menu);
        Task SaveChangesAsync();
        Task  RemoveAsync (Menu menu);
        Task <Menu> UpdateAsync (Menu menu);
    }
}