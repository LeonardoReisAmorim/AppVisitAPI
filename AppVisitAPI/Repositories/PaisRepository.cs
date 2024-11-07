using AppVisitAPI.Data.Context;
using AppVisitAPI.Interfaces.IPais;
using AppVisitAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppVisitAPI.Repositories
{
    public class PaisRepository : IPaisRepository
    {
        private readonly Context _context;

        public PaisRepository(Context context)
        {
            _context = context;
        }

        public async Task<Pais> CreatePais(Pais pais)
        {
            await _context.Paises.AddAsync(pais);
            await _context.SaveChangesAsync();
            return pais;
        }

        public async Task<List<Pais>> GetPais(int? id = null)
        {
            if (id.HasValue)
            {
                return await _context.Paises.AsNoTracking().Where(pais => pais.Id == id).ToListAsync();
            }

            return await _context.Paises.AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdatePais(int id, Pais pais)
        {
            var result = await _context.Paises
                         .Where(pais => pais.Id == id)
                         .ExecuteUpdateAsync(setters => setters
                         .SetProperty(p => p.Nome, pais.Nome));
            return result > 0;
        }

        public async Task<bool> DeletePais(int id)
        {
            var result = await _context.Paises
                         .Where(pais => pais.Id == id)
                         .ExecuteDeleteAsync();
            return result > 0;
        }

        public async Task<Pais> GetPaisById(int id)
        {
            return await _context.Paises.AsNoTracking().FirstOrDefaultAsync(pais => pais.Id == id);
        }
    }
}
