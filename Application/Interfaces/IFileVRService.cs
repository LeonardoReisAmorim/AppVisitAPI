using Application.DTOs.FileVRDTO;

namespace Application.Interfaces
{
    public interface IFileVRService
    {
        Stream GetArquivoById(int id);
        Task<IEnumerable<ReadDataFileVRDTO>> GetDadosArquivo(int? id = null);
        Task<ReadFileVRDTO> CreateArquivoAsync(byte[] arquivoDTO, AddFileVRDTO arquivodados);
        Task<bool> UpdateArquivo(int id, UpdateFileVRDTO editarArquivoDTO);
        Task<bool> DeleteArquivo(int id);
    }
}
