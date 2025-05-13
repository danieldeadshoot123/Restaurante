using MenuService.Data;
using MenuService.Models;
using Microsoft.EntityFrameworkCore;

namespace MenuService.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly MenuDb _context;

        public MenuRepository (MenuDb context)
        {
            _context = context;
        }

        public async Task AddAsync(Menu menu)
        {
            await  _context.menus.AddAsync(menu);
        }

        public   Task  RemoveAsync(Menu menu)
        {
            _context.menus.Remove(menu);
            return Task.CompletedTask;
        }

        public async Task<List<Menu>> GetAllAsync()
        {
            return await _context.menus
            .ToListAsync();
        }

        public async Task<Menu?> GetByIdAsync(int id)
        {
            return await _context.menus
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Menu> UpdateAsync(Menu menu)
        {
            _context.menus.Update(menu);
            await _context.SaveChangesAsync();
            return menu;
        }

       
}}