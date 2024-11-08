using Domain.Models;

namespace Infrastructure.Interfaces
{
    public interface ICountryRepository
    {
        Task<Country> CreatePais(Country pais);
        Task<List<Country>> GetPais(int? id = null);
        Task<bool> UpdatePais(int id, Country pais);
        Task<bool> DeletePais(int id);
        Task<Country> GetPaisById(int id);
    }
}
