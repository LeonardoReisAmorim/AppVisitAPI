using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FileVRRepository : IFileVRRepository
    {
        private readonly Context _context;

        public FileVRRepository(Context context)
        {
            _context = context;
        }

        public byte[] GetArquivoConteudoById(int id)
        {
            return _context.FilesVr.AsNoTracking().FirstOrDefault(x => x.Id == id).FileContent;
        }

        public async Task<IEnumerable<FileVR>> GetDadosArquivo(int? id = null)
        {
            if (id.HasValue)
            {
                return await _context.FilesVr.AsNoTracking().Where(a => a.Id == id).Select(fileVr => new FileVR
                {
                    Id = fileVr.Id,
                    FileName = fileVr.FileName,
                    CreatedAt = fileVr.CreatedAt,
                    UpdatedAt = fileVr.UpdatedAt
                }).ToListAsync();
            }

            return await _context.FilesVr.AsNoTracking().Select(fileVr => new FileVR
            {
                Id = fileVr.Id,
                FileName = fileVr.FileName,
                CreatedAt = fileVr.CreatedAt,
                UpdatedAt = fileVr.UpdatedAt
            }).ToListAsync();
        }

        public async Task<FileVR> CreateArquivo(FileVR fileVr)
        {
            await _context.FilesVr.AddAsync(fileVr);
            await _context.SaveChangesAsync();
            return fileVr;
        }

        public async Task<bool> UpdateArquivo(int id, FileVR fileVr)
        {
            var result = await _context.FilesVr
                        .Where(fileVr => fileVr.Id == id)
                        .ExecuteUpdateAsync(setters => setters
                        .SetProperty(a => a.FileContent, fileVr.FileContent)
                        .SetProperty(a => a.FileName, fileVr.FileName)
                        .SetProperty(a => a.UpdatedAt, fileVr.UpdatedAt));
            return result > 0;
        }

        public async Task<bool> DeleteArquivo(int id)
        {
            var result = await _context.FilesVr
                .Where(arquivo => arquivo.Id == id)
                .ExecuteDeleteAsync();
            return result > 0;
        }

        public FileVR GetArquivoById(int id)
        {
            return _context.FilesVr.AsNoTracking().SingleOrDefault(fileVr => fileVr.Id == id);
        }
    }
}
