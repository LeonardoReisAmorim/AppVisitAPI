using AppVisitAPI.DTOs.PaisDTO;
using AppVisitAPI.Interfaces.Repositories;
using AppVisitAPI.Interfaces.Services;
using AppVisitAPI.Models;
using AutoMapper;

namespace AppVisitAPI.Services
{
    public class PaisService : IPaisService
    {
        private readonly IMapper _mapper;
        private readonly IPaisRepository _IPaisRepository;

        public PaisService(IMapper mapper, IPaisRepository iPaisRepository)
        {
            _mapper = mapper;
            _IPaisRepository = iPaisRepository;
        }

        public async Task<LerPaisDTO> CreatePais(CriarPaisDTO paisDTO)
        {
            Pais pais = _mapper.Map<Pais>(paisDTO);
            var paisCriado = await _IPaisRepository.Create(pais);
            return _mapper.Map<LerPaisDTO>(paisCriado);
        }

        public async Task<List<LerPaisDTO>> GetPais(int? id = null)
        {
            return _mapper.Map<List<LerPaisDTO>>(await _IPaisRepository.Get(id));
        }

        public async Task<bool> UpdatePais(int id, EditarPaisDTO updatePaisDTO)
        {
            var pais = new Pais
            {
                Id = id,
                Nome = updatePaisDTO.Nome
            };
            return await _IPaisRepository.Update(pais);
        }

        public async Task<bool> DeletePais(int id)
        {
            return await _IPaisRepository.Delete(id);
        }
    }
}
