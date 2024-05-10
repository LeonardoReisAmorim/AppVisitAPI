using AppVisitAPI.Data.Context;
using AppVisitAPI.Interfaces.Repositories;
using AppVisitAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppVisitAPI.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly Context _context;

        public CidadeRepository(Context context)
        {
            _context = context;
        }

        public async Task<Cidade> Create(Cidade cidade)
        {
            await _context.Cidades.AddAsync(cidade);
            _context.SaveChanges();
            return cidade;
        }

        public async Task<List<Cidade>> Get(int? id = null)
        {
            if (id.HasValue)
            {
                return await _context.Cidades.AsNoTracking().Where(cidade => cidade.Id == id).ToListAsync();
            }

            return await _context.Cidades.AsNoTracking().ToListAsync();
        }

        public async Task<bool> Update(Cidade cidadeParam)
        {
            var cidade = await _context.Cidades.AsNoTracking().FirstOrDefaultAsync(cidade => cidade.Id == cidadeParam.Id);

            if (cidade is null)
            {
                return false;
            }

            cidade.Nome = cidadeParam.Nome;
            cidade.EstadoId = cidadeParam.EstadoId;
            _context.Entry(cidade).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var cidade = await _context.Cidades.AsNoTracking().FirstOrDefaultAsync(cidade => cidade.Id == id);

            if (cidade is null)
            {
                return false;
            }

            _context.Remove(cidade);
            _context.SaveChanges();
            return true;
        }
    }
}
