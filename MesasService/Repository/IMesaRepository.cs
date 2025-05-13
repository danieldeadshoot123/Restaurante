using MesasService.Models;

namespace MesasService.Repository
{
    public interface IMesaRepository
    {
        Task <Mesa?>GetByIdAsync (int id);
        Task <List<Mesa>>GetAllAsync();

        Task AddAsync(Mesa mesa );
        Task SaveChangesAsync();
        void Remove(Mesa mesa);
        void Update(Mesa mesa);

    }
}