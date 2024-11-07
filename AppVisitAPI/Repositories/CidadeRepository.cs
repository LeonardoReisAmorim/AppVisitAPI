using AppVisitAPI.Data.Context;
using AppVisitAPI.Interfaces.ICidade;
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

        public async Task<Cidade> CreateCidade(Cidade cidade)
        {
            await _context.Cidades.AddAsync(cidade);
            await _context.SaveChangesAsync();
            return cidade;
        }

        public async Task<List<Cidade>> GetCidade(int? id = null)
        {
            if (id.HasValue)
            {
                return await _context.Cidades.AsNoTracking().Where(cidade => cidade.Id == id).ToListAsync();
            }

            return await _context.Cidades.AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdateCidade(int id, Cidade cidade)
        {
            var result = await _context.Cidades
                        .Where(cidade => cidade.Id == id)
                        .ExecuteUpdateAsync(setters => setters
                        .SetProperty(c => c.EstadoId, cidade.EstadoId)
                        .SetProperty(c => c.Nome, cidade.Nome));
            return result > 0;
        }

        public async Task<bool> DeleteCidade(int id)
        {
            var result = await _context.Cidades
                .Where(cidade => cidade.Id == id)
                .ExecuteDeleteAsync();
            return result > 0;
        }

        public async Task<Cidade> GetCidadeById(int id)
        {
            return await _context.Cidades.AsNoTracking().FirstOrDefaultAsync(cidade => cidade.Id == id);
        }
    }
}
