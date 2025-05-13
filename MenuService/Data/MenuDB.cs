using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MenuService.Models;

namespace MenuService.Data
{
    public class MenuDb : DbContext
    {
        public MenuDb(DbContextOptions<MenuDb>options) : base(options){}
        public DbSet<Menu> menus => Set<Menu>();
    }
}