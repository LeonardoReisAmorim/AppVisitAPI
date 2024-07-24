using AppVisitAPI.DTOs.LugarDTO;

namespace AppVisitAPI.Interfaces.ILugar
{
    public interface ILugarService
    {
        Task<LerLugarDTO> CreateLugar(InserirLugarDTO lugarDTO);
        Task<List<LerLugarDTO>> GetLugar(int? id = null);
        Task<string?> GetUtilizacaoLugarVRById(int id);
        Task<bool> UpdateLugar(int id, EditarLugarDTO updateLugarDTO);
        Task<bool> DeleteLugar(int id);
    }
}
