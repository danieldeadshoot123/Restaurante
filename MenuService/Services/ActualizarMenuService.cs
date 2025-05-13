using MenuService.Repository;

namespace MenuService.Services
{
    public class ActualizarMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public ActualizarMenuService (IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<bool> ActualizarMenuAsync(int menuId,string nombreComida,decimal precio)
        {
            var menuExistente = await _menuRepository.GetByIdAsync(menuId);
            if (menuExistente == null)
            {
                return false;
            }

            menuExistente.NombreComida = nombreComida;
            menuExistente.Precio = precio;

            await _menuRepository.UpdateAsync(menuExistente);
            await _menuRepository.SaveChangesAsync();


            return true;
        }
    }
}