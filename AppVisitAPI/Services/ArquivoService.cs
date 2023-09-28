using AppVisitAPI.Data.Context;
using AppVisitAPI.DTOs.ArquivoDTO;
using AppVisitAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppVisitAPI.Services
{
    public class ArquivoService
    {
        private readonly Context _context;

        public ArquivoService(Context context)
        {
            _context = context;
        }

        public async Task<LerArquivoDTO> GetArquivoById(int id)
        {
            var arquivo = await _context.Arquivos.FirstOrDefaultAsync(x => x.Id == id);

            if (arquivo == null)
            {
                return null;
            }

            var arquivoDto = new LerArquivoDTO
            {
                Id = id,
                Arquivo = Convert.ToBase64String(arquivo.File)
            };

            return arquivoDto;
        }

        public async Task<LerArquivoDTO> CreateArquivo(byte[] arquivoDTO)
        {
            var arquivo = new Arquivo
            {
                File = arquivoDTO
            };

            await _context.Arquivos.AddAsync(arquivo);
            _context.SaveChanges();

            var returnArquivoDTO = new LerArquivoDTO
            {
                Id = arquivo.Id
            };
           
            return returnArquivoDTO;
        }

        public async Task<bool> UpdateArquivo(int id, EditarArquivo editarArquivoDTO)
        {
            var arquivo = await _context.Arquivos.FirstOrDefaultAsync(arquivo => arquivo.Id == id);

            if (arquivo == null)
            {
                return false;
            }

            arquivo.File = editarArquivoDTO.Arquivo;
            _context.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteArquivo(int id)
        {
            var arquivo = await _context.Arquivos.FirstOrDefaultAsync(arquivo => arquivo.Id == id);

            if (arquivo == null)
            {
                return false;
            }

            _context.Remove(arquivo);
            _context.SaveChanges();
            
            return true;
        }
    }
}
