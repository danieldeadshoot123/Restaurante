using MesasService.Repository;
using MesasService.Models;
using System.Threading.Tasks;

namespace MesasService.Services
{
    public class ActualizarMesaService
    {
        private readonly IMesaRepository _mesaRepository;

        // Constructor
        public ActualizarMesaService(IMesaRepository mesaRepository)
        {
            _mesaRepository = mesaRepository;
        }

        
        public async Task<bool> ActualizarEstadoMesaAsync(int mesaId, string nuevoEstado)
        {
            
            if (nuevoEstado != "Libre" && nuevoEstado != "Ocupado")
            {
                throw new ArgumentException("El estado debe ser 'Libre' o 'Ocupado'.");
            }

            // Obtén la mesa
            var mesa = await _mesaRepository.GetByIdAsync(mesaId);
            if (mesa == null)
            {
                return false; 
            }

            
            mesa.Disponible = false;

           
            _mesaRepository.Update(mesa);
            await _mesaRepository.SaveChangesAsync();

            return true; 
        }
    }
}
