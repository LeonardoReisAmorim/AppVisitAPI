using AppVisitAPI.Models;

namespace AppVisitAPI.Interfaces.IArquivo
{
    public interface IArquivoRepository
    {
        byte[] GetArquivoConteudoById(int id);
        Task<IEnumerable<Arquivo>> GetDadosArquivo(int? id = null);
        Task<Arquivo> CreateArquivo(Arquivo arquivo);
        Task<bool> UpdateArquivo(int id, Arquivo arquivo);
        Task<bool> DeleteArquivo(Arquivo arquivo);
        Task<Arquivo> GetArquivoById(int id);
    }
}
