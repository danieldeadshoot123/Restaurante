namespace PedidosService.DTOs
{
    public class MesaMesaggeUpdate
    {
        public int MesaId {get;set;}
        public string NuevoEstado {get;set;} = "Ocupado";
    }
}