using AppVisitAPI.Models;

namespace AppVisitAPI.Interfaces.IPais
{
    public interface IPaisRepository
    {
        Task<Pais> CreatePais(Pais pais);
        Task<List<Pais>> GetPais(int? id = null);
        Task<bool> UpdatePais(int id, Pais pais);
        Task<bool> DeletePais(Pais pais);
        Task<Pais> GetPaisById(int id);
    }
}
