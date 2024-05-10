using AppVisitAPI.Models;

namespace AppVisitAPI.Interfaces.Repositories
{
    public interface ICidadeRepository
    {
        Task<Cidade> Create(Cidade cidade);
        Task<List<Cidade>> Get(int? id = null);
        Task<bool> Update(Cidade cidadeParam);
        Task<bool> Delete(int id);
    }
}
