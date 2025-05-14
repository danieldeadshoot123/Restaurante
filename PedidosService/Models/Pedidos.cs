using PedidosService.DTOs;

namespace PedidosService.Models {

    public class Pedido {
        public int Id { get; set; }
        public string NumeroPedido { get; set; } = string.Empty;
        public string Estatus { get; set; } = "pendiente";
        public DateTime DiaPedido { get; set; } = DateTime.UtcNow;

        public int MesaId { get; set; } 
 
        public List<PedidoMenu> PedidoMenus { get; set; } = new(); 

    public class PedidoMenu {
        public int PedidoId { get; set; } 
        public Pedido? Pedido{get;set;}       

        public int MenuId { get; set; } 
      
    }
}}
