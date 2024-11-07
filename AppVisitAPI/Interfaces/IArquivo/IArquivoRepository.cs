using AppVisitAPI.DTOs.ArquivoDTO;
using AppVisitAPI.Models;

namespace AppVisitAPI.Interfaces.IArquivo
{
    public interface IArquivoRepository
    {
        byte[] GetArquivoConteudoById(int id);
        Task<IEnumerable<Arquivo>> GetDadosArquivo(int? id = null);
        Task<Arquivo> CreateArquivo(Arquivo arquivo);
        Task<bool> UpdateArquivo(int id, EditarArquivo arquivo);
        Task<bool> DeleteArquivo(int id);
        Arquivo GetArquivoById(int id);
    }
}
