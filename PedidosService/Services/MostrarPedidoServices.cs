using PedidoDB.Data;
using PedidosService.DTOs;
using PedidosService.Models;
using Microsoft.EntityFrameworkCore;
using PedidosService.Repository;

namespace PedidosService.Services
{
    public class MostrarPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly MesasService _mesasService;
        private readonly MenuService _menuService;

        public MostrarPedidoService(IPedidoRepository pedidoRepository, MesasService mesasService, MenuService menuService)
        {
            _pedidoRepository = pedidoRepository;
            _mesasService = mesasService;
            _menuService = menuService;
        }

        public async Task<PedidoReadDTO?> GetPedidoByIdAsync(int id)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);
                

            if (pedido == null)
                return null;

            var mesaDto = await _mesasService.GetMesaByIdAsync(pedido.MesaId);

            var menuDtos = new List<MenuDTO>();
            foreach (var pm in pedido.PedidoMenus)
            {
                var menuDto = await _menuService.GetMenuByIdAsync(pm.MenuId);
                if (menuDto != null)
                {
                    menuDtos.Add(menuDto);
                }
            }

            var pedidoReadDto = new PedidoReadDTO
            {
                Id = pedido.Id,
                NumeroPedido = pedido.NumeroPedido,
                Estatus = pedido.Estatus,
                DiaPedido = pedido.DiaPedido,
                MesaId = pedido.MesaId,
                Mesa = mesaDto,
                Menus = menuDtos
            };

            return pedidoReadDto;
        }

        public async Task<List<PedidoReadDTO>> GetAllPedidosAsync()
{
            var pedidos = await _pedidoRepository.GetAllAsync();
                

            var pedidosDto = new List<PedidoReadDTO>();

            foreach (var pedido in pedidos)
            {
                var mesaDto = await _mesasService.GetMesaByIdAsync(pedido.MesaId);

                var menuDtos = new List<MenuDTO>();
                foreach (var pm in pedido.PedidoMenus)
                {
                    var menuDto = await _menuService.GetMenuByIdAsync(pm.MenuId);
                    if (menuDto != null)
                    {
                        menuDtos.Add(menuDto);
                    }
                }

                pedidosDto.Add(new PedidoReadDTO
                {
                    Id = pedido.Id,
                    NumeroPedido = pedido.NumeroPedido,
                    Estatus = pedido.Estatus,
                    DiaPedido = pedido.DiaPedido,
                    MesaId = pedido.MesaId,
                    Mesa = mesaDto,
                    Menus = menuDtos
                });
            }

            return pedidosDto;
        }
    }
}
