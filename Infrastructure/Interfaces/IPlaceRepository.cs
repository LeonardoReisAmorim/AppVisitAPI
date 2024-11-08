using Domain.Models;

namespace Infrastructure.Interfaces
{
    public interface IPlaceRepository
    {
        Task<Place> CreateLugar(Place lugar);
        Task<List<Place>> GetLugar(int? id = null);
        Task<string?> GetUtilizacaoLugarVRById(int id);
        Task<bool> UpdateLugar(int id, Place lugar);
        Task<bool> DeleteLugar(int id);
        Task<bool> ExistsArquivo(int arquivoId);
        Task<Place> GetLugarById(int id);
    }
}
