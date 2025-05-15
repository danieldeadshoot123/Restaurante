namespace MesasService.Models
{

    public class Mesa{

        public int Id{get;set;}
        public string NumeroMesa{get;set;}=string.Empty;
        public bool  Disponible { get; set; } 

    }
}