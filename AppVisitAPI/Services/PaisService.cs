using AppVisitAPI.DTOs.PaisDTO;
using AppVisitAPI.Interfaces.IPais;
using AppVisitAPI.Models;
using AutoMapper;

namespace AppVisitAPI.Services
{
    public class PaisService : IPaisService
    {
        private readonly IPaisRepository _IPaisRepository;
        private readonly IMapper _mapper;

        public PaisService(IPaisRepository iPaisRepository, IMapper mapper)
        {
            _IPaisRepository = iPaisRepository;
            _mapper = mapper;
        }

        public async Task<LerPaisDTO> CreatePais(CriarPaisDTO paisDTO)
        {
            Pais pais = _mapper.Map<Pais>(paisDTO);
            return _mapper.Map<LerPaisDTO>(await _IPaisRepository.CreatePais(pais));
        }

        public async Task<List<LerPaisDTO>> GetPais(int? id = null)
        {
            return _mapper.Map<List<LerPaisDTO>>(await _IPaisRepository.GetPais(id));
        }

        public async Task<bool> UpdatePais(int id, EditarPaisDTO updatePaisDTO)
        {
            var pais = await _IPaisRepository.GetPaisById(id);

            if (pais is null)
            {
                return false;
            }

            _mapper.Map(updatePaisDTO, pais);
            return await _IPaisRepository.UpdatePais(id, pais);
        }

        public async Task<bool> DeletePais(int id)
        {
            var pais = await _IPaisRepository.GetPaisById(id);

            if (pais is null)
            {
                return false;
            }

            return await _IPaisRepository.DeletePais(pais);
        }
    }
}
