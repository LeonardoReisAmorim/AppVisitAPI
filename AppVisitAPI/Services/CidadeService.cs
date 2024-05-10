using AppVisitAPI.DTOs.CidadeDTO;
using AppVisitAPI.Interfaces.Repositories;
using AppVisitAPI.Interfaces.Services;
using AppVisitAPI.Models;
using AppVisitAPI.Repositories;
using AutoMapper;

namespace AppVisitAPI.Services
{
    public class CidadeService : ICidadeService
    {
        private readonly IMapper _mapper;
        private readonly ICidadeRepository _iCidadeRepository;

        public CidadeService(IMapper mapper, ICidadeRepository iCidadeRepository)
        {
            _mapper = mapper;
            _iCidadeRepository = iCidadeRepository;
        }

        public async Task<LerCidadeDTO> CreateCidade(CriarCidadeDTO cidadeDTO)
        {
            Cidade cidadeModel = _mapper.Map<Cidade>(cidadeDTO);
            var cidadeCriada = await _iCidadeRepository.Create(cidadeModel);
            return _mapper.Map<LerCidadeDTO>(cidadeCriada);
        }

        public async Task<List<LerCidadeDTO>> GetCidade(int? id = null)
        {
            return _mapper.Map<List<LerCidadeDTO>>(await _iCidadeRepository.Get(id));
        }

        public async Task<bool> UpdateCidade(int id, EditarCidadeDTO updateCidadeDTO)
        {
            var cidadeModel = new Cidade
            {
                EstadoId = updateCidadeDTO.EstadoId,
                Nome = updateCidadeDTO.Nome,
                Id = id,
            };

            return await _iCidadeRepository.Update(cidadeModel);
        }

        public async Task<bool> DeleteCidade(int id)
        {
            return await _iCidadeRepository.Delete(id);
        }
    }
}
