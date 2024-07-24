using AppVisitAPI.DTOs.CidadeDTO;

namespace AppVisitAPI.Interfaces.ICidade
{
    public interface ICidadeService
    {
        Task<LerCidadeDTO> CreateCidade(CriarCidadeDTO cidadeDTO);
        Task<List<LerCidadeDTO>> GetCidade(int? id = null);
        Task<bool> UpdateCidade(int id, EditarCidadeDTO updateCidadeDTO);
        Task<bool> DeleteCidade(int id);
    }
}
