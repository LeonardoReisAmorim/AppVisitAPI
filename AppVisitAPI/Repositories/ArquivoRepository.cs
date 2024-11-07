using AppVisitAPI.Data.Context;
using AppVisitAPI.DTOs.ArquivoDTO;
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
                return await _context.Arquivos.AsNoTracking().Where(a => a.Id == id).Select(arquivo => new Arquivo
                {
                    DataCriacao = arquivo.DataCriacao,
                    Id = arquivo.Id,
                    NomeArquivo = arquivo.NomeArquivo,
                }).ToListAsync();
            }

            return await _context.Arquivos.AsNoTracking().Select(arquivo => new Arquivo
            {
                DataCriacao = arquivo.DataCriacao,
                Id = arquivo.Id,
                NomeArquivo = arquivo.NomeArquivo,
            }).ToListAsync();
        }

        public async Task<Arquivo> CreateArquivo(Arquivo arquivo)
        {
            await _context.Arquivos.AddAsync(arquivo);
            await _context.SaveChangesAsync();
            return arquivo;
        }

        public async Task<bool> UpdateArquivo(int id, EditarArquivo arquivo)
        {
            var result = await _context.Arquivos
                        .Where(arquivo => arquivo.Id == id)
                        .ExecuteUpdateAsync(setters => setters
                        .SetProperty(a => a.ArquivoConteudo, arquivo.Arquivo)
                        .SetProperty(a => a.NomeArquivo, arquivo.NomeArquivo)
                        .SetProperty(a => a.DataCriacao, arquivo.DataCriacao));
            return result > 0;
        }

        public async Task<bool> DeleteArquivo(int id)
        {
            var result = await _context.Arquivos
                .Where(arquivo => arquivo.Id == id)
                .ExecuteDeleteAsync();
            return result > 0;
        }

        public Arquivo GetArquivoById(int id)
        {
            return _context.Arquivos.AsNoTracking().SingleOrDefault(arquivo => arquivo.Id == id);
        }
    }
}
