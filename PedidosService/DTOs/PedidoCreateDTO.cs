namespace PedidosService.DTOs
{
    public class PedidoCreateDTO
    {
        public string NumeroPedido{get;set;}=string.Empty;
        public int MesaId {get;set;}
        public List<int>MenuIds {get;set;} =new();
    }
}