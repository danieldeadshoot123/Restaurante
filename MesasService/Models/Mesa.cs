namespace MesasService.Models
{

    public class Mesa{

        public int Id{get;set;}
        public string NumeroMesa{get;set;}=string.Empty;
        public string  Disponible { get; set; } = "libre";

    }
}