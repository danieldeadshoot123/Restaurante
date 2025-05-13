using MesasService.Repository;

namespace MesasService.Services
{
    public class EliminarMesaServices
    {
        private readonly IMesaRepository _mesaRepository;


        public EliminarMesaServices (IMesaRepository mesaRepository)
        {
            _mesaRepository = mesaRepository;
        }

        public async Task<bool> EliminarMesaAsync(int mesaId)
        {
            var mesa = await _mesaRepository.GetByIdAsync(mesaId);

            if (mesa == null)
                throw new Exception("Mesa no encontrada");

            _mesaRepository.Remove(mesa);
            await _mesaRepository.SaveChangesASync();

            return true;
        }
    }
}