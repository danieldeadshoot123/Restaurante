using PedidosService.Models;
using static PedidosService.Models.Pedido;

namespace PedidosService.Repository
{
    public interface IPedidoRepository
    {
        Task <Pedido?>GetByIdAsync( int id);
        Task<List<Pedido>>GetAllAsync();
        Task AddAsync(Pedido pedido);
        Task SaveChangesAsync ();

        void Remove(Pedido pedido);
        void RemovePedidoMenus(IEnumerable<PedidoMenu> pedidoMenus);

        void Update(Pedido pedido);

    }
}