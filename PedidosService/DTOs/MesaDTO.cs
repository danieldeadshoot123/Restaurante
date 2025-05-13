namespace PedidosService.DTOs
{
    public class MesaDTO 
    {
        public int Id {get;set;}

        public string NumeroMesa {get;set;}= string.Empty;

        public string Disponible {get;set;} = "libre";
    }
}