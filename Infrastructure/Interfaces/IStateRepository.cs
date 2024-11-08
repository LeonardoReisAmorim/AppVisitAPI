using Domain.Models;

namespace Infrastructure.Interfaces
{
    public interface IStateRepository
    {
        Task<State> CreateEstado(State estado);
        Task<List<State>> GetEstado(int? id = null);
        Task<bool> UpdateEstado(int id, State estado);
        Task<bool> DeleteEstado(int id);
        Task<State> GetEstadoById(int id);
    }
}
