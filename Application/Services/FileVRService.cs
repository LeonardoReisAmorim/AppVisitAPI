using Application.DTOs.FileVRDTO;
using Application.Interfaces;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class FileVRService : IFileVRService
    {
        private readonly IFileVRRepository _fileVRRepository;

        public FileVRService(IFileVRRepository fileVRRepository)
        {
            _fileVRRepository = fileVRRepository;
        }

        public Stream GetArquivoById(int id)
        {
            var filebytes = _fileVRRepository.GetArquivoConteudoById(id);
            return new MemoryStream(filebytes);
        }

        public async Task<IEnumerable<ReadDataFileVRDTO>> GetDadosArquivo(int? id = null)
        {
            var result = await _fileVRRepository.GetDadosArquivo(id);
            return result.Select(a => new ReadDataFileVRDTO
            {
                Id = a.Id,
                FileName = a.FileName,
                CreatedAt = a.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss")
            });

        }

        public async Task<ReadFileVRDTO> CreateArquivoAsync(byte[] arquivoDTO, AddFileVRDTO arquivodados)
        {
            var arquivo = new FileVR
            {
                FileContent = arquivoDTO,
                FileName = arquivodados.FileName,
                CreatedAt = arquivodados.CreatedAt
            };

            var result = await _fileVRRepository.CreateArquivo(arquivo);

            return new ReadFileVRDTO
            {
                Id = result.Id
            };
        }

        public async Task<bool> UpdateArquivo(int id, UpdateFileVRDTO editarArquivoDTO)
        {
            var arquivo = new FileVR
            {
                FileContent = editarArquivoDTO.Content,
                FileName = editarArquivoDTO.FileName,
                CreatedAt = editarArquivoDTO.CreatedAt
            };

            return await _fileVRRepository.UpdateArquivo(id, arquivo);
        }

        public async Task<bool> DeleteArquivo(int id)
        {
            return await _fileVRRepository.DeleteArquivo(id);
        }
    }
}
