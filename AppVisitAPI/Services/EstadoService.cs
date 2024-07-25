using AppVisitAPI.DTOs.EstadoDTO;
using AppVisitAPI.Interfaces.IEstado;
using AppVisitAPI.Models;
using AutoMapper;

namespace AppVisitAPI.Services
{
    public class EstadoService : IEstadoService
    {
        private readonly IEstadoRepository _IEstadoRepository;
        private readonly IMapper _mapper;

        public EstadoService(IEstadoRepository iestadoRepository, IMapper mapper)
        {
            _IEstadoRepository = iestadoRepository;
            _mapper = mapper;
        }

        public async Task<LerEstadoDTO> CreateEstado(CriarEstadoDTO estadoDTO)
        {
            Estado estado = _mapper.Map<Estado>(estadoDTO);
            return _mapper.Map<LerEstadoDTO>(await _IEstadoRepository.CreateEstado(estado));
        }

        public async Task<List<LerEstadoDTO>> GetEstado(int? id = null)
        {
            return _mapper.Map<List<LerEstadoDTO>>(await _IEstadoRepository.GetEstado(id));
        }

        public async Task<bool> UpdateEstado(int id, EditarEstadoDTO updateEstadoDTO)
        {
            var estado = await _IEstadoRepository.GetEstadoById(id);

            if (estado is null)
            {
                return false;
            }
            _mapper.Map(updateEstadoDTO, estado);
            return await _IEstadoRepository.UpdateEstado(id, estado);
        }

        public async Task<bool> DeleteEstado(int id)
        {
            var estado = await _IEstadoRepository.GetEstadoById(id);

            if (estado is null)
            {
                return false;
            }

            return await _IEstadoRepository.DeleteEstado(estado);
        }
    }
}
