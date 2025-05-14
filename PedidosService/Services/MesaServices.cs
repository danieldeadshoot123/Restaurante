


using PedidosService.DTOs;

namespace PedidosService.Services
{
    public class MesasService
    {
        private readonly HttpClient _httpClient;

        public MesasService (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<MesaDTO?> GetMesaByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5290/api/mesas/{id}");
            if(!response.IsSuccessStatusCode) return null;


            return await response.Content.ReadFromJsonAsync<MesaDTO>();
        }
    }
}