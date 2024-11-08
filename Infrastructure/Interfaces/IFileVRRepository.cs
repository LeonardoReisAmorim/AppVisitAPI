using Domain.Models;

namespace Infrastructure.Interfaces
{
    public interface IFileVRRepository
    {
        byte[] GetArquivoConteudoById(int id);
        Task<IEnumerable<FileVR>> GetDadosArquivo(int? id = null);
        Task<FileVR> CreateArquivo(FileVR arquivo);
        Task<bool> UpdateArquivo(int id, FileVR arquivo);
        Task<bool> DeleteArquivo(int id);
        FileVR GetArquivoById(int id);
    }
}
