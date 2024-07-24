using AppVisitAPI.DTOs.LugarDTO;
using AppVisitAPI.Models;

namespace AppVisitAPI.Interfaces.ILugar
{
    public interface ILugarRepository
    {
        Task<Lugar> CreateLugar(Lugar lugar);
        Task<List<Lugar>> GetLugar(int? id = null);
        Task<string?> GetUtilizacaoLugarVRById(int id);
        Task<bool> UpdateLugar(int id, EditarLugarDTO updateLugarDTO);
        Task<bool> DeleteLugar(int id);
    }
}
