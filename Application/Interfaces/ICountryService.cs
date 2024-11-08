using Application.DTOs.CountryDTO;

namespace Application.Interfaces
{
    public interface ICountryService
    {
        Task<ReadCountryDTO> CreatePais(CountryDTO paisDTO);
        Task<List<ReadCountryDTO>> GetPais(int? id = null);
        Task<bool> UpdatePais(int id, CountryDTO updatePaisDTO);
        Task<bool> DeletePais(int id);
    }
}
