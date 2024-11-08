using Application.DTOs.StateDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class StateService : IStateService
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;

        public StateService(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        public async Task<ReadStateDTO> CreateEstado(StateDTO estadoDTO)
        {
            State estado = _mapper.Map<State>(estadoDTO);
            return _mapper.Map<ReadStateDTO>(await _stateRepository.CreateEstado(estado));
        }

        public async Task<List<ReadStateDTO>> GetEstado(int? id = null)
        {
            return _mapper.Map<List<ReadStateDTO>>(await _stateRepository.GetEstado(id));
        }

        public async Task<bool> UpdateEstado(int id, StateDTO updateEstadoDTO)
        {
            State estado = _mapper.Map<State>(updateEstadoDTO);
            return await _stateRepository.UpdateEstado(id, estado);
        }

        public async Task<bool> DeleteEstado(int id)
        {
            return await _stateRepository.DeleteEstado(id);
        }
    }
}
