using Domain.Models;

namespace Infrastructure.Interfaces
{
    public interface ICityRepository
    {
        Task<City> CreateCidade(City cidade);
        Task<List<City>> GetCidade(int? id = null);
        Task<bool> UpdateCidade(int id, City cidade);
        Task<bool> DeleteCidade(int id);
        Task<City> GetCidadeById(int id);
    }
}
