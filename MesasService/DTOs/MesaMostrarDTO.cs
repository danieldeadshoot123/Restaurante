namespace MesasService.DTOs
{
    public class MesaMostrarDTO
    {
        public int Id {get;set;}
        public string NumeroMesa {get;set;} = string.Empty;

        public bool Disponible {get;set;} 
    }
}