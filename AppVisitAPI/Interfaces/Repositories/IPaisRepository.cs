using AppVisitAPI.Models;

namespace AppVisitAPI.Interfaces.Repositories
{
    public interface IPaisRepository
    {
        Task<Pais> Create(Pais pais);
        Task<List<Pais>> Get(int? id = null);
        Task<bool> Update(Pais paisParam);
        Task<bool> Delete(int id);
    }
}
