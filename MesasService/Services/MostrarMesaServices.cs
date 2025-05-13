using MesasService.DTOs;
using MesasService.Repository;

namespace MesasService.Services
{
    public class MostrarMesaServices
    {
        private readonly IMesaRepository _mesaRepository;

        public MostrarMesaServices(IMesaRepository mesaRepository)
        {
            _mesaRepository = mesaRepository;
        }

        public async Task<MesaMostrarDTO?> GetMesaByIdAsync(int id)
        {
            var mesa = await _mesaRepository.GetByIdAsync(id);

            if (mesa == null)
                return null;

            return new MesaMostrarDTO
            {
                Id = mesa.Id,
                NumeroMesa = mesa.NumeroMesa,
                Disponible = mesa.Disponible
            };
        }

        public async Task<List<MesaMostrarDTO>> GetAllMesaAsync()
        {
            var mesas = await _mesaRepository.GetAllAsync();

            var mesasDto = mesas.Select(m => new MesaMostrarDTO
            {
                Id = m.Id,
                NumeroMesa = m.NumeroMesa,
                Disponible = m.Disponible
            }).ToList();

            return mesasDto;
        }
    }
}
