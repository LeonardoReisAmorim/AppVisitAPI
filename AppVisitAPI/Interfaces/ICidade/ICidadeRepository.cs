using AppVisitAPI.Models;

namespace AppVisitAPI.Interfaces.ICidade
{
    public interface ICidadeRepository
    {
        Task<Cidade> CreateCidade(Cidade cidade);
        Task<List<Cidade>> GetCidade(int? id = null);
        Task<bool> UpdateCidade(int id, Cidade cidade);
        Task<bool> DeleteCidade(Cidade cidade);
        Task<Cidade> GetCidadeById(int id);
    }
}
