using AppVisitAPI.DTOs.PaisDTO;
using AppVisitAPI.Models;

namespace AppVisitAPI.Interfaces.IPais
{
    public interface IPaisRepository
    {
        Task<Pais> CreatePais(Pais pais);
        Task<List<Pais>> GetPais(int? id = null);
        Task<bool> UpdatePais(int id, EditarPaisDTO updatePaisDTO);
        Task<bool> DeletePais(int id);
    }
}
