using MesasService.DTOs;
using MesasService.Models;
using MesasService.Repository;

namespace MesasService.Services
{
    public class CrearMesaService
    {
        private readonly IMesaRepository _mesaRepository;
        
    

    public CrearMesaService (IMesaRepository mesaRepository)
    {
        _mesaRepository = mesaRepository;
    }

    public async Task<MesaMostrarDTO> CrearMesa(MesaCrearDTO mesaCrearDTO)
    {
        var mesa = new Mesa 
        {
            NumeroMesa = mesaCrearDTO.NumeroMEsa,
            Disponible = "libre"
        };
        await _mesaRepository.AddAsync(mesa);
        await _mesaRepository.SaveChangesASync();

        return new MesaMostrarDTO
        {
            Id = mesa.Id,
            NumeroMesa = mesa.NumeroMesa,
            Disponible = mesa.Disponible
        };
    }
}
}