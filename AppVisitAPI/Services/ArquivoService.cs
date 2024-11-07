using AppVisitAPI.DTOs.ArquivoDTO;
using AppVisitAPI.Interfaces.IArquivo;
using AppVisitAPI.Models;

namespace AppVisitAPI.Services
{
    public class ArquivoService : IArquivoService
    {
        private readonly IArquivoRepository _IArquivoRepository;

        public ArquivoService(IArquivoRepository iarquivoRepository)
        {
            _IArquivoRepository = iarquivoRepository;
        }

        public Stream GetArquivoById(int id)
        {
            var filebytes = _IArquivoRepository.GetArquivoConteudoById(id);
            return new MemoryStream(filebytes);
        }

        public async Task<IEnumerable<LerDadosArquivoDTO>> GetDadosArquivo(int? id = null)
        {
            var result = await _IArquivoRepository.GetDadosArquivo(id);
            return result.Select(a => new LerDadosArquivoDTO
            {
                Id = a.Id,
                NomeArquivo = a.NomeArquivo,
                DataCriacao = a.DataCriacao.ToString("dd/MM/yyyy HH:mm:ss")
            });
            
        }

        public async Task<LerArquivoDTO> CreateArquivoAsync(byte[] arquivoDTO, InserirArquivoDTO arquivodados)
        {
            var arquivo = new Arquivo
            {
                ArquivoConteudo = arquivoDTO,
                NomeArquivo = arquivodados.NomeArquivo,
                DataCriacao = arquivodados.DataCriacao
            };

            var result = await _IArquivoRepository.CreateArquivo(arquivo);

            return new LerArquivoDTO
            {
                Id = result.Id
            };
        }

        public async Task<bool> UpdateArquivo(int id, EditarArquivo editarArquivoDTO)
        {
            return await _IArquivoRepository.UpdateArquivo(id, editarArquivoDTO);
        }

        public async Task<bool> DeleteArquivo(int id)
        {
            return await _IArquivoRepository.DeleteArquivo(id);
        }
    }
}
