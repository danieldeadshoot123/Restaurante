
using Microsoft.EntityFrameworkCore;
using PedidoDB.Data;
using PedidosService.Models;
using static PedidosService.Models.Pedido;


namespace PedidosService.Repository
{
    public class PedidoRepository : IPedidoRepository
    {   
        private readonly PedidoDb _context;
    
    public PedidoRepository (PedidoDb context )
    {
        _context = context;
    }

        public async Task<Pedido?> GetByIdAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.PedidoMenus)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Pedido>> GetAllAsync()
        {
            return await _context.Pedidos
                .Include(p => p.PedidoMenus)
                .ToListAsync();
        }

        public async Task AddAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Remove(Pedido pedido)
        {
            _context.Pedidos.Remove(pedido);
        }   

        public void RemovePedidoMenus(IEnumerable<PedidoMenu> pedidoMenus)
        {
            _context.PedidoMenus.RemoveRange(pedidoMenus);
        }

        public void Update(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
        }


    }
    }