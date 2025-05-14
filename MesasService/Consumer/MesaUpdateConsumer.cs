using MassTransit;
using MesasService.Data;
using MesasService.DTOs;
using MesasService.Repository;

namespace MesasService.Consumer
{
    public class MesaUpdateConsumer : IConsumer<MesaMesaggeUpdate>
    {
        private readonly IMesaRepository _mesaRepository;


        public MesaUpdateConsumer ( IMesaRepository mesaRepository)
        {
            _mesaRepository = mesaRepository;
        }

        public async Task Consume(ConsumeContext<MesaMesaggeUpdate>context )
        {
            var msg = context.Message;
            var mesa = await _mesaRepository.GetByIdAsync(msg.MesaId);
            if(mesa != null)
            {
                mesa.Disponible = msg.NuevoEstado.ToLower();
                _mesaRepository.Update(mesa);
                await _mesaRepository.SaveChangesAsync();
            }
        }
    }
}