using AppVisitAPI.Models;

namespace AppVisitAPI.Interfaces.Repositories
{
    public interface ILugarRepository
    {
        Task<Lugar> Create(Lugar lugar);
        Task<List<Lugar>> Get(int? id = null);
        Task<string?> GetInstrucaoUtilizarVRById(int id);
        Task<bool> Update(Lugar lugarParam);
        Task<bool> Delete(int id);
    }
}
