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
            _context.Entry(cidade).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCidade(Cidade cidade)
        {
            _context.Remove(cidade);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Cidade> GetCidadeById(int id)
        {
            return await _context.Cidades.AsNoTracking().FirstOrDefaultAsync(cidade => cidade.Id == id);
        }
    }
}
