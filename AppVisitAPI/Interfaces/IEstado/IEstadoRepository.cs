using AppVisitAPI.DTOs.EstadoDTO;
using AppVisitAPI.Models;

namespace AppVisitAPI.Interfaces.IEstado
{
    public interface IEstadoRepository
    {
        Task<Estado> CreateEstado(Estado estado);
        Task<List<Estado>> GetEstado(int? id = null);
        Task<bool> UpdateEstado(int id, EditarEstadoDTO updateEstadoDTO);
        Task<bool> DeleteEstado(int id);
    }
}
