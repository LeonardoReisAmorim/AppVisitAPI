using AppVisitAPI.Models;

namespace AppVisitAPI.Interfaces.Repositories
{
    public interface IEstadoRepository
    {
        Task<Estado> Create(Estado estado);
        Task<List<Estado>> Get(int? id = null);
        Task<bool> Update(Estado estadoParam);
        Task<bool> Delete(int id);
    }
}
