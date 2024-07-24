using AppVisitAPI.DTOs.ArquivoDTO;
using AppVisitAPI.Interfaces.Arquivo;

namespace AppVisitAPI.Services
{
    public class ArquivoService : IArquivoService
    {
        private readonly IArquivoRepository _IArquivoRepository;

        public ArquivoService(IArquivoRepository iarquivoRepository)
        {
            _IArquivoRepository = iarquivoRepository;
        }

        public byte[] GetArquivoById(int id)
        {
            return _IArquivoRepository.GetArquivoById(id);
        }

        public async Task<IEnumerable<LerDadosArquivoDTO>> GetDadosArquivo(int? id = null)
        {
            return await _IArquivoRepository.GetDadosArquivo(id);
            
        }

        public LerArquivoDTO CreateArquivo(byte[] arquivoDTO, InserirArquivoDTO arquivodados)
        {
            return _IArquivoRepository.CreateArquivo(arquivoDTO, arquivodados);
        }

        public bool UpdateArquivo(int id, EditarArquivo editarArquivoDTO)
        {
            return _IArquivoRepository.UpdateArquivo(id, editarArquivoDTO);
        }

        public bool DeleteArquivo(int id)
        {
            return _IArquivoRepository.DeleteArquivo(id);
        }
    }
}
