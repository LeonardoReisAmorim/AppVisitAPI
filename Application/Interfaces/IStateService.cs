using Application.DTOs.StateDTO;

namespace Application.Interfaces
{
    public interface IStateService
    {
        Task<ReadStateDTO> CreateEstado(StateDTO estadoDTO);
        Task<List<ReadStateDTO>> GetEstado(int? id = null);
        Task<bool> UpdateEstado(int id, StateDTO updateEstadoDTO);
        Task<bool> DeleteEstado(int id);
    }
}
