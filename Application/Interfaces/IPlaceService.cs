using Application.DTOs.PlaceDTO;

namespace Application.Interfaces
{
    public interface IPlaceService
    {
        Task<ReadPlaceDTO> CreateLugar(PlaceDTO lugarDTO);
        Task<List<ReadPlaceDTO>> GetLugar(int? id = null);
        Task<string?> GetUtilizacaoLugarVRById(int id);
        Task<bool> UpdateLugar(int id, PlaceDTO updateLugarDTO);
        Task<bool> DeleteLugar(int id);
    }
}
