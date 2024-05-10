using AppVisitAPI.DTOs.ArquivoDTO;
using AppVisitAPI.Interfaces.Repositories;
using AppVisitAPI.Interfaces.Services;
using AppVisitAPI.Models;

namespace AppVisitAPI.Services
{
    public class ArquivoService : IArquivoService
    {
        private readonly IArquivoRepository _iArquivoRepository;

        public ArquivoService(IArquivoRepository iArquivoRepository)
        {
            _iArquivoRepository = iArquivoRepository;
        }

        public byte[] GetArquivoById(int id)
        {
            return _iArquivoRepository.GetArquivoConteudoById(id) ?? throw new Exception();
        }

        public async Task<IEnumerable<LerDadosArquivoDTO>> GetDadosArquivo(int? id = null)
        {
            var arquivoModel = await _iArquivoRepository.GetDadosArquivo(id);

            return arquivoModel.Select(a => new LerDadosArquivoDTO
            {
                Id = a.Id,
                NomeArquivo = a.NomeArquivo,
                DataCriacao = a.DataCriacao.ToString("dd/MM/yyyy HH:mm:ss")
            });
        }

        public LerArquivoDTO CreateArquivo(byte[] arquivoDTO, InserirArquivoDTO arquivodados)
        {
            var arquivo = new Arquivo
            {
                ArquivoConteudo = arquivoDTO,
                NomeArquivo = arquivodados.NomeArquivo,
                DataCriacao = arquivodados.DataCriacao
            };
            var arquivoCriado = _iArquivoRepository.Create(arquivo);

            return new LerArquivoDTO
            {
                Id = arquivoCriado.Id
            };
        }

        public bool UpdateArquivo(int id, EditarArquivo editarArquivoDTO)
        {
            var arquivoModel = new Arquivo
            {
                ArquivoConteudo = editarArquivoDTO.Arquivo,
                DataCriacao = editarArquivoDTO.DataCriacao,
                NomeArquivo = editarArquivoDTO.NomeArquivo,
                Id = id
            };

            return _iArquivoRepository.Update(arquivoModel);
        }

        public bool DeleteArquivo(int id)
        {
            return _iArquivoRepository.Delete(id);
        }
    }
}
