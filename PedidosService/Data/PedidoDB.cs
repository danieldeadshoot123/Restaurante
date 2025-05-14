using PedidosService.Models;
using Microsoft.EntityFrameworkCore;
using static PedidosService.Models.Pedido;


namespace PedidoDB.Data
{
    public class PedidoDb : DbContext
    {
        public PedidoDb(DbContextOptions<PedidoDb> options) : base(options) { }

        public DbSet<Pedido> Pedidos => Set<Pedido>();
        public DbSet<PedidoMenu> PedidoMenus => Set<PedidoMenu>();
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PedidoMenu>()
                .HasKey(pm => new { pm.PedidoId, pm.MenuId });

             modelBuilder.Entity<PedidoMenu>()
                .HasOne(pm => pm.Pedido)
                .WithMany(p => p.PedidoMenus)
                .HasForeignKey(pm => pm.PedidoId);
    }
}}
