using MenuService.Repository;

namespace MenuService.Services
{
    public class ELiminarMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public ELiminarMenuService (IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }


        public async Task<bool> ELiminarMenuAsync(int menuId)
        {
            var menu = await _menuRepository.GetByIdAsync(menuId);

            if (menu == null)
                throw new Exception("Menu no encontrado");

            await  _menuRepository.RemoveAsync(menu);
            await _menuRepository.SaveChangesAsync();


            return true;
        }


    }
}