using Application.DTOs.TypePlaceDTO;

namespace Application.Interfaces
{
    public interface ITypePlaceService
    {
        Task<TypePlaceDTO> CreateTypePlace(TypePlaceDTO typePlaceDTO);
        Task<List<TypePlaceDTO>> GetTypePlace(int? id = null);
        Task<bool> UpdateTypePlace(int id, TypePlaceDTO updateTypePlaceDTO);
        Task<bool> DeleteTypePlace(int id);
    }
}
