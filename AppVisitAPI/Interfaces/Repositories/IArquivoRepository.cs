using AppVisitAPI.Models;

namespace AppVisitAPI.Interfaces.Repositories
{
    public interface IArquivoRepository
    {
        byte[] GetArquivoConteudoById(int id);
        Task<IEnumerable<Arquivo>> GetDadosArquivo(int? id = null);
        Arquivo Create(Arquivo arquivo);
        bool Update(Arquivo arquivoParam);
        bool Delete(int id);
    }
}
