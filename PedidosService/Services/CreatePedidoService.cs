using PedidoDB.Data;
using PedidosService.DTOs;
using PedidosService.Models;
using PedidosService.Repository;
using static PedidosService.Models.Pedido;
using MassTransit;


    


namespace PedidosService.Services
{
    public class CreatePedidoService{

        private readonly IPedidoRepository _pedidoRepository; 
        private readonly MesasService _mesasService;

        private readonly MenuService _menuService;
 


        public  CreatePedidoService(IPedidoRepository pedidoRepository, MesasService mesasService, MenuService menuService )
        {
            _pedidoRepository = pedidoRepository;
            _mesasService = mesasService;
            _menuService = menuService;
           

        }

        public async Task<PedidoReadDTO> CrearPedido(PedidoCreateDTO pedidoCreateDTO)
        {
            var mesaDto = await _mesasService.GetMesaByIdAsync(pedidoCreateDTO.MesaId);
            if (mesaDto == null)
                throw new Exception("Mesa no encontrada");

            var pedido = new Pedido
            {
                NumeroPedido = pedidoCreateDTO.NumeroPedido,
                Estatus = "pendiente",
                DiaPedido = DateTime.UtcNow,
                MesaId = pedidoCreateDTO.MesaId,
                PedidoMenus = new List<PedidoMenu>()
            };
            var menus = new List<MenuDTO>();

            foreach (var menuId in pedidoCreateDTO.MenuIds)
            {
                var menuDto = await _menuService.GetMenuByIdAsync(menuId);
                if (menuDto == null)
                    throw new Exception($"Menú con ID {menuId} no encontrado");

                pedido.PedidoMenus.Add(new PedidoMenu
                {
                    MenuId = menuDto.Id,
                    Pedido = pedido
                });
                menus.Add(menuDto);
            }

            await _pedidoRepository.AddAsync(pedido);
            await _pedidoRepository.SaveChangesAsync();

            var actualizado = await _mesasService.ActualizarDisponibilidadMesa(pedido.MesaId,false);
            if (!actualizado)
            {
                throw new Exception("No se Pudo actualizar la disponibilidad  de la mesa ");
            }

        


            return new PedidoReadDTO 
            {
                Id = pedido.Id,
                NumeroPedido = pedido.NumeroPedido,
                Estatus = pedido.Estatus,
                DiaPedido = pedido.DiaPedido,
                MesaId = pedido.MesaId,
                Mesa = mesaDto,
                Menus = menus

            };
            
        }

        
    }
}