using PedidoDB.Data;
using Microsoft.EntityFrameworkCore;
using PedidosService.Repository;

namespace PedidosService.Services
{
    public class EliminarPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public EliminarPedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository =  pedidoRepository;
        }

        public async Task<bool> EliminarPedidoAsync(int pedidoId)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(pedidoId);
                

            if (pedido == null)
                throw new Exception("Pedido no encontrado.");

            _pedidoRepository.RemovePedidoMenus(pedido.PedidoMenus);
            _pedidoRepository.Remove(pedido); 
            await _pedidoRepository.SaveChangesAsync();

            return true;
        }
    }
}
