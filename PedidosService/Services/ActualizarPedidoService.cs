using Microsoft.EntityFrameworkCore;
using PedidoDB.Data;
using PedidosService.Repository;

namespace PedidosService.Services
{
    public class ActualizarPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        private static readonly List<string>estadosValidos = new() {"Pendiente" , "Preparando" , "Entregado"};

        public ActualizarPedidoService  (IPedidoRepository pedidoRepository)

        {
            _pedidoRepository = pedidoRepository;

        }

        public async Task<bool>ActualizarEstatusPedidoAsync(int pedidoId, string nuevoEstatus)
        {
            if (!estadosValidos.Contains(nuevoEstatus.ToLower()))
            {
                throw new ArgumentException("Estado no valido. Ingresar 'Pendiente', 'Preparacion' o 'Entregado'");
            }


            var pedido = await  _pedidoRepository.GetByIdAsync(pedidoId);
            if (pedido == null )
            {
                throw new Exception("Pedido no encontrado ");
            }

            pedido.Estatus =nuevoEstatus.ToLower();
            _pedidoRepository.Update(pedido);
            await _pedidoRepository.SaveChangesAsync();

            return true;


        }
    }
}