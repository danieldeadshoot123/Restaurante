using MesasService.Data;
using MesasService.Models;
using Microsoft.EntityFrameworkCore;


namespace MesasService.Repository
{
    public class MesaRepository : IMesaRepository
    {
        private readonly MesaDb _context;


        public MesaRepository (MesaDb context)
        {
            _context = context;
        }

        public async Task<Mesa?> GetByIdAsync (int id)
        {
            return await _context.mesas
                .FirstOrDefaultAsync( p => p.Id == id);

        }

        public async Task<List<Mesa>> GetAllAsync()
        {
            return await _context.mesas
            .ToListAsync();
        }

        public async Task AddAsync (Mesa mesa )
        {
            await _context.mesas.AddAsync(mesa);

        }




        public void Remove (Mesa mesa)
        {
            _context.mesas.Remove(mesa);
        }

        public void Update(Mesa mesa)
        {
            _context.mesas.Update(mesa);
        }

        public Task SaveChangesASync()
        {
            throw new NotImplementedException();
        }
    }
}