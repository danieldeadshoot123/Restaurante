using MenuService.DTOs;
using MenuService.Repository;

namespace MenuService.Services
{
    public class MostrarMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MostrarMenuService (IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<MenuMostrarDTO?> GetMenuByIdAsync(int id)
        {
            var menu = await _menuRepository.GetByIdAsync(id);

            if (menu == null)
                return null;

            return new MenuMostrarDTO
            {
                id = menu.Id,
                NombreComida = menu.NombreComida,
                Precio = menu.Precio
            };
        }

        public async Task<List<MenuMostrarDTO>>GetAllMenuAsync()
        {
            var menus = await _menuRepository.GetAllAsync();

            var menusDto = menus.Select(m => new MenuMostrarDTO
            {
                id = m.Id,
                NombreComida = m.NombreComida,
                Precio = m.Precio
            }).ToList();

            return menusDto;
        }
    }
}