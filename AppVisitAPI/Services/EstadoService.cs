using AppVisitAPI.Data.Context;
using AppVisitAPI.DTOs.EstadoDTO;
using AppVisitAPI.Interfaces.Repositories;
using AppVisitAPI.Interfaces.Services;
using AppVisitAPI.Models;
using AppVisitAPI.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AppVisitAPI.Services
{
    public class EstadoService : IEstadoService
    {
        private readonly IMapper _mapper;
        private readonly IEstadoRepository _iEstadoRepository;

        public EstadoService(IMapper mapper, IEstadoRepository iEstadoRepository)
        {
            _mapper = mapper;
            _iEstadoRepository = iEstadoRepository;
        }

        public async Task<LerEstadoDTO> CreateEstado(CriarEstadoDTO estadoDTO)
        {
            Estado estado = _mapper.Map<Estado>(estadoDTO);
            var estadoCriado = await _iEstadoRepository.Create(estado);
            return _mapper.Map<LerEstadoDTO>(estadoCriado);
        }

        public async Task<List<LerEstadoDTO>> GetEstado(int? id = null)
        {
            return _mapper.Map<List<LerEstadoDTO>>(await _iEstadoRepository.Get(id));
        }

        public async Task<bool> UpdateEstado(int id, EditarEstadoDTO updateEstadoDTO)
        {
            var estado = new Estado
            {
                Id = id,
                Nome = updateEstadoDTO.Nome,
                PaisId = updateEstadoDTO.PaisId
            };

            return await _iEstadoRepository.Update(estado);
        }

        public async Task<bool> DeleteEstado(int id)
        {
            return await _iEstadoRepository.Delete(id);
        }
    }
}
