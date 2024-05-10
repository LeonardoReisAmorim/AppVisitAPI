using AppVisitAPI.DTOs.PaisDTO;

namespace AppVisitAPI.Interfaces.Services
{
    public interface IPaisService
    {
        Task<LerPaisDTO> CreatePais(CriarPaisDTO paisDTO);
        Task<List<LerPaisDTO>> GetPais(int? id = null);
        Task<bool> UpdatePais(int id, EditarPaisDTO updatePaisDTO);
        Task<bool> DeletePais(int id);
    }
}
