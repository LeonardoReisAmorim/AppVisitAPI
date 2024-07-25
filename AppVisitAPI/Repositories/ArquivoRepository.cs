using AppVisitAPI.Data.Context;
using AppVisitAPI.Interfaces.IArquivo;
using AppVisitAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppVisitAPI.Repositories
{
    public class ArquivoRepository : IArquivoRepository
    {
        private readonly Context _context;

        public ArquivoRepository(Context context)
        {
            _context = context;
        }

        public byte[] GetArquivoConteudoById(int id)
        {
            return _context.Arquivos.AsNoTracking().FirstOrDefault(x => x.Id == id).ArquivoConteudo;
        }

        public async Task<IEnumerable<Arquivo>> GetDadosArquivo(int? id = null)
        {
            if (id.HasValue)
            {
                return await _context.Arquivos.AsNoTracking().Where(a => a.Id == id).ToListAsync();
            }

            return await _context.Arquivos.AsNoTracking().ToListAsync();
        }

        public async Task<Arquivo> CreateArquivo(Arquivo arquivo)
        {
            await _context.Arquivos.AddAsync(arquivo);
            await _context.SaveChangesAsync();
            return arquivo;
        }

        public async Task<bool> UpdateArquivo(int id, Arquivo arquivo)
        {
            _context.Entry(arquivo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteArquivo(Arquivo arquivo)
        {
            _context.Remove(arquivo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Arquivo> GetArquivoById(int id)
        {
            return await _context.Arquivos.AsNoTracking().SingleOrDefaultAsync(arquivo => arquivo.Id == id);
        }
    }
}
