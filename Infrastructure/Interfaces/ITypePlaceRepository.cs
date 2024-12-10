using Domain.Models;

namespace Infrastructure.Interfaces
{
    public interface ITypePlaceRepository
    {
        Task<TypePlace> CreateTypePlace(TypePlace typePlace);
        Task<List<TypePlace>> GetTypePlaces(int? id = null);
        Task<bool> UpdateTypePlace(int id, TypePlace typePlace);
        Task<bool> DeleteTypePlace(int id);
        Task<TypePlace> GetTypePlaceById(int id);
    }
}
