using AppVisitAPI.DTOs.CidadeDTO;
using AppVisitAPI.Models;

namespace AppVisitAPI.Interfaces.ICidade
{
    public interface ICidadeRepository
    {
        Task<Cidade> CreateCidade(Cidade cidade);
        Task<List<Cidade>> GetCidade(int? id = null);
        Task<bool> UpdateCidade(int id, EditarCidadeDTO updateCidadeDTO);
        Task<bool> DeleteCidade(int id);
    }
}
