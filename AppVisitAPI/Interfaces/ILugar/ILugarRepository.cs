using AppVisitAPI.DTOs.LugarDTO;
using AppVisitAPI.Models;

namespace AppVisitAPI.Interfaces.ILugar
{
    public interface ILugarRepository
    {
        Task<Lugar> CreateLugar(Lugar lugar);
        Task<List<LerLugarDTO>> GetLugar(int? id = null);
        Task<string?> GetUtilizacaoLugarVRById(int id);
        Task<bool> UpdateLugar(int id, EditarLugarDTO lugar);
        Task<bool> DeleteLugar(int id);
        Task<bool> ExistsArquivo(int arquivoId);
        Task<Lugar> GetLugarById(int id);
    }
}
