


using System.Text;
using System.Text.Json;
using PedidosService.DTOs;

namespace PedidosService.Services
{
    public class MesasService
    {
        private readonly HttpClient _httpClient;

        public MesasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<MesaDTO?> GetMesaByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5290/api/mesas/{id}");
            if (!response.IsSuccessStatusCode) return null;


            return await response.Content.ReadFromJsonAsync<MesaDTO>();
        }

        public async Task<bool> ActualizarDisponibilidadMesa(int mesaId, bool disponible)
        {
            var payload = new
            {
                Disponible = disponible
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"http://localhost:5290/api/mesas/{mesaId}/disponibilidad", content);
            return response.IsSuccessStatusCode;
        }
        
        public async Task<bool> LiberarMesaAsync(int mesaId)
        {
            var dto = new MesaDisponibilidadDTO { Disponible = true };

            var response = await _httpClient.PutAsJsonAsync(
                $"http://localhost:5290/api/mesas/{mesaId}/disponibilidad", dto);

            return response.IsSuccessStatusCode;
        }
    }
}