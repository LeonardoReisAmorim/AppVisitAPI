using AppVisitAPI.Models;

namespace AppVisitAPI.Interfaces.IEstado
{
    public interface IEstadoRepository
    {
        Task<Estado> CreateEstado(Estado estado);
        Task<List<Estado>> GetEstado(int? id = null);
        Task<bool> UpdateEstado(int id, Estado estado);
        Task<bool> DeleteEstado(int id);
        Task<Estado> GetEstadoById(int id);
    }
}
