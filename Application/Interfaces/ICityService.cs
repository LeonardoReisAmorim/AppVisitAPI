using Application.DTOs.CityDTO;

namespace Application.Interfaces
{
    public interface ICityService
    {
        Task<ReadCityDTO> CreateCidade(CityDTO cidadeDTO);
        Task<List<ReadCityDTO>> GetCidade(int? id = null);
        Task<bool> UpdateCidade(int id, CityDTO updateCidadeDTO);
        Task<bool> DeleteCidade(int id);
    }
}
