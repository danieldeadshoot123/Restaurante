

namespace PedidosService.DTOs
{
    public class PedidoReadDTO
    {
        public int Id{get;set;}
        public string NumeroPedido{get;set;} = string.Empty;

        public string Estatus {get;set; }= string.Empty;

        public DateTime DiaPedido {get;set;}

        public int MesaId {get;set;}

        public MesaDTO? Mesa {get;set;} 


        public List <MenuDTO> Menus{get;set; }= new();
    }
}