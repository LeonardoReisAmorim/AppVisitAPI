using AppVisitAPI.Models;

namespace AppVisitAPI.Interfaces.ILugar
{
    public interface ILugarRepository
    {
        Task<Lugar> CreateLugar(Lugar lugar);
        Task<List<Lugar>> GetLugar(int? id = null);
        Task<string?> GetUtilizacaoLugarVRById(int id);
        Task<bool> UpdateLugar(int id, Lugar lugar);
        Task<bool> DeleteLugar(Lugar lugar);
        Task<bool> ExistsArquivo(int arquivoId);
        Task<Lugar> GetLugarById(int id);
    }
}
