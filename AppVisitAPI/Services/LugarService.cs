using AppVisitAPI.DTOs.LugarDTO;
using AppVisitAPI.Interfaces.Repositories;
using AppVisitAPI.Interfaces.Services;
using AppVisitAPI.Models;
using AutoMapper;

namespace AppVisitAPI.Services
{
    public class LugarService : ILugarService
    {
        private readonly IMapper _mapper;
        private readonly ILugarRepository _iLugarRepository;

        public LugarService(IMapper mapper, ILugarRepository iLugarRepository)
        {
            _mapper = mapper;
            _iLugarRepository = iLugarRepository;
        }

        public async Task<LerLugarDTO> CreateLugar(InserirLugarDTO lugarDTO)
        {
            Lugar lugar = _mapper.Map<Lugar>(lugarDTO);
            var lugarCriado = await _iLugarRepository.Create(lugar);
            return _mapper.Map<LerLugarDTO>(lugarCriado);
        }

        public async Task<List<LerLugarDTO>> GetLugar(int? id = null)
        {
            var lugares = await _iLugarRepository.Get(id);
            
            return _mapper.Map<List<LerLugarDTO>>(lugares.Select(lugar => new LerLugarDTO
            {
                ArquivoId = lugar.ArquivoId,
                Cidade = lugar.Cidade.Nome,
                Descricao = lugar.Descricao,
                Id = lugar.Id,
                Imagem = Convert.ToBase64String(lugar.Imagem),
                Nome = lugar.Nome,
                NomeArquivo = lugar.Arquivo.NomeArquivo,
                CidadeId = lugar.CidadeId,
                InstrucoesUtilizacaoVR = lugar.InstrucoesUtilizacaoVR
            }));
        }

        public async Task<string?> GetUtilizacaoLugarVRById(int id)
        {
            return await _iLugarRepository.GetInstrucaoUtilizarVRById(id);
        }

        public async Task<bool> UpdateLugar(int id, EditarLugarDTO updateLugarDTO)
        {
            var lugar = _mapper.Map<Lugar>(updateLugarDTO);
            lugar.Id = id;
            return await _iLugarRepository.Update(lugar);
        }

        public async Task<bool> DeleteLugar(int id)
        {
            return await _iLugarRepository.Delete(id);
        }
    }
}
