
namespace MenuService.Models{


    public class Menu{

        public int Id {get;set;}
        public string NombreComida  {get;set;}=string.Empty;

        public decimal Precio {get;set;}
    }
}