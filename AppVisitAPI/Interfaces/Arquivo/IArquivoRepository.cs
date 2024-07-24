using AppVisitAPI.DTOs.ArquivoDTO;

namespace AppVisitAPI.Interfaces.Arquivo
{
    public interface IArquivoRepository
    {
        byte[] GetArquivoById(int id);
        Task<IEnumerable<LerDadosArquivoDTO>> GetDadosArquivo(int? id = null);
        LerArquivoDTO CreateArquivo(byte[] arquivoDTO, InserirArquivoDTO arquivodados);
        bool UpdateArquivo(int id, EditarArquivo editarArquivoDTO);
        bool DeleteArquivo(int id);
    }
}
