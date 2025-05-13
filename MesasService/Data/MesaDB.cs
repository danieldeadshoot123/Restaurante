using Microsoft.EntityFrameworkCore;
using MesasService.Models;

namespace MesasService.Data 
{
    public class MesaDb : DbContext 
    {
        public MesaDb(DbContextOptions<MesaDb> options) : base(options) {}
        public DbSet<Mesa> mesas => Set<Mesa>();

        
    }
}