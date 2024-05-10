using AppVisitAPI.Data.Context;
using AppVisitAPI.Interfaces.Repositories;
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

        public Arquivo Create(Arquivo arquivo)
        {
            _context.Arquivos.Add(arquivo);
            _context.SaveChanges();
            return arquivo;
        }

        public bool Update(Arquivo arquivoParam)
        {
            var arquivo = _context.Arquivos.AsNoTracking().SingleOrDefault(arquivo => arquivo.Id == arquivoParam.Id);

            if (arquivo is null)
            {
                return false;
            }

            arquivo.ArquivoConteudo = arquivoParam.ArquivoConteudo;
            arquivo.NomeArquivo = arquivoParam.NomeArquivo;
            _context.Entry(arquivo).State = EntityState.Modified;
            _context.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var arquivo = _context.Arquivos.AsNoTracking().SingleOrDefault(arquivo => arquivo.Id == id);

            if (arquivo is null)
            {
                return false;
            }

            _context.Remove(arquivo);
            _context.SaveChanges();

            return true;
        }
    }
}
