using AppVisitAPI.DTOs.LugarDTO;
using AppVisitAPI.Interfaces.ILugar;
using AppVisitAPI.Models;
using AutoMapper;

namespace AppVisitAPI.Services
{
    public class LugarService : ILugarService
    {
        private readonly ILugarRepository _ILugarRepository;
        private readonly IMapper _mapper;

        public LugarService(ILugarRepository ilugarRepository, IMapper mapper)
        {
            _ILugarRepository = ilugarRepository;
            _mapper = mapper;
        }

        public async Task<LerLugarDTO> CreateLugar(InserirLugarDTO lugarDTO)
        {
            Lugar lugar = _mapper.Map<Lugar>(lugarDTO);
            return _mapper.Map<LerLugarDTO>(await _ILugarRepository.CreateLugar(lugar));
        }

        public async Task<List<LerLugarDTO>> GetLugar(int? id = null)
        {
            var result = await _ILugarRepository.GetLugar(id);

            return result.Select(lugar => new LerLugarDTO
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
            }).ToList();
        }

        public async Task<string?> GetUtilizacaoLugarVRById(int id)
        {
            return await _ILugarRepository.GetUtilizacaoLugarVRById(id);
        }

        public async Task<bool> UpdateLugar(int id, EditarLugarDTO updateLugarDTO)
        {
            return await _ILugarRepository.UpdateLugar(id, updateLugarDTO);
        }

        public async Task<bool> DeleteLugar(int id)
        {
            return await _ILugarRepository.DeleteLugar(id);
        }
    }
}
