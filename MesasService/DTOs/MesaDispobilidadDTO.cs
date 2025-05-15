using MassTransit.Futures.Contracts;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MesasService.DTOs
{
    public class MesaDisponibilidadDTO
    {
        public bool Disponible{get;set;}
    }
}