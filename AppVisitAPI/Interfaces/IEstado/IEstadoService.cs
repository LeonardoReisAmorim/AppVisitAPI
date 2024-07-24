using AppVisitAPI.DTOs.EstadoDTO;

namespace AppVisitAPI.Interfaces.IEstado
{
    public interface IEstadoService
    {
        Task<LerEstadoDTO> CreateEstado(CriarEstadoDTO estadoDTO);
        Task<List<LerEstadoDTO>> GetEstado(int? id = null);
        Task<bool> UpdateEstado(int id, EditarEstadoDTO updateEstadoDTO);
        Task<bool> DeleteEstado(int id);
    }
}
