using PedidosService.DTOs;



namespace PedidosService.Services
{
    public  class MenuService
    {
        private readonly HttpClient _httpClient;

        public MenuService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MenuDTO?> GetMenuByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5250/api/menu/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<MenuDTO>();
    }
}
}