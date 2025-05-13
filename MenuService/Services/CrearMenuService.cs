using MenuService.DTOs;
using MenuService.Models;
using MenuService.Repository;

namespace MenuService.Services
{
    public class CrearMenuService
    {
        private readonly IMenuRepository _menuRepository;


        public CrearMenuService (IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<MenuMostrarDTO> CrearMenu(MenuCrearDTO menuCrearDTO )
        {
            var menu = new Menu 
            {
                NombreComida = menuCrearDTO.NombreComida,
                Precio = menuCrearDTO.Precio
            };
            await _menuRepository.AddAsync(menu);
            await _menuRepository.SaveChangesAsync();

            return new MenuMostrarDTO
            {
                id = menu.Id,
                NombreComida = menu.NombreComida,
                Precio = menu.Precio


            };
            
        }
    }
}