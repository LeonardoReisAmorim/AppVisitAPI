using AppVisitAPI.DTOs.CidadeDTO;
using AppVisitAPI.Interfaces.ICidade;
using AppVisitAPI.Models;
using AutoMapper;

namespace AppVisitAPI.Services
{
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _ICidadeRepository;
        private readonly IMapper _mapper;

        public CidadeService(ICidadeRepository icidadeRepository, IMapper mapper)
        {
            _ICidadeRepository = icidadeRepository;
            _mapper = mapper;
        }

        public async Task<LerCidadeDTO> CreateCidade(CriarCidadeDTO cidadeDTO)
        {
            Cidade cidade = _mapper.Map<Cidade>(cidadeDTO);
            return _mapper.Map<LerCidadeDTO>(await _ICidadeRepository.CreateCidade(cidade));
        }

        public async Task<List<LerCidadeDTO>> GetCidade(int? id = null)
        {
            return _mapper.Map<List<LerCidadeDTO>>(await _ICidadeRepository.GetCidade(id));
        }

        public async Task<bool> UpdateCidade(int id, EditarCidadeDTO updateCidadeDTO)
        {
            return await _ICidadeRepository.UpdateCidade(id, updateCidadeDTO);
        }

        public async Task<bool> DeleteCidade(int id)
        {
            return await _ICidadeRepository.DeleteCidade(id);
        }
    }
}
