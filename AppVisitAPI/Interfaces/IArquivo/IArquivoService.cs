using AppVisitAPI.DTOs.ArquivoDTO;

namespace AppVisitAPI.Interfaces.IArquivo
{
    public interface IArquivoService
    {
        Stream GetArquivoById(int id);
        Task<IEnumerable<LerDadosArquivoDTO>> GetDadosArquivo(int? id = null);
        Task<LerArquivoDTO> CreateArquivoAsync(byte[] arquivoDTO, InserirArquivoDTO arquivodados);
        Task<bool> UpdateArquivo(int id, EditarArquivo editarArquivoDTO);
        Task<bool> DeleteArquivo(int id);
    }
}
