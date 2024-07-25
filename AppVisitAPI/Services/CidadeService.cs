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
            var cidade = await _ICidadeRepository.GetCidadeById(id);

            if (cidade == null)
            {
                return false;
            }

            _mapper.Map(updateCidadeDTO, cidade);
            return await _ICidadeRepository.UpdateCidade(id, cidade);
        }

        public async Task<bool> DeleteCidade(int id)
        {
            var cidade = await _ICidadeRepository.GetCidadeById(id);

            if (cidade is null)
            {
                return false;
            }
            return await _ICidadeRepository.DeleteCidade(cidade);
        }
    }
}
