using System.Net.Http.Json;
using PedidosService.DTOs;
using PedidosService.Repository;

namespace PedidosService.Services
{
    public class TotalServices
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly HttpClient _httpClient;

        public TotalServices(IPedidoRepository pedidoRepository, HttpClient httpClient)
        {
            _pedidoRepository = pedidoRepository;
            _httpClient = httpClient;
        }

        public async Task<decimal> CalcularTotalPedidoAsync(int pedidoId)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(pedidoId);

            if (pedido == null || pedido.PedidoMenus == null)
                return 0;

            decimal total = 0;

            foreach (var pm in pedido.PedidoMenus)
            {
                var menuDto = await _httpClient.GetFromJsonAsync<MenuDTO>($"http://menuservice/api/menu/{pm.MenuId}");
                if (menuDto != null)
                {
                    total += menuDto.Precio;
                }
            }

            return total;
        }
    }
}
