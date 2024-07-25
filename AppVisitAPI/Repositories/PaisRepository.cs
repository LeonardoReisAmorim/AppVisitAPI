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
            _context.Entry(pais).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePais(Pais pais)
        {
            _context.Remove(pais);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Pais> GetPaisById(int id)
        {
            return await _context.Paises.AsNoTracking().FirstOrDefaultAsync(pais => pais.Id == id);
        }
    }
}
