using AppVisitAPI.Data.Context;
using AppVisitAPI.Interfaces.Repositories;
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

        public async Task<Pais> Create(Pais pais)
        {
            await _context.Paises.AddAsync(pais);
            _context.SaveChanges();
            return pais;
        }

        public async Task<List<Pais>> Get(int? id = null)
        {
            if (id.HasValue)
            {
                return await _context.Paises.AsNoTracking().Where(pais => pais.Id == id).ToListAsync();
            }

            return await _context.Paises.AsNoTracking().ToListAsync();
        }

        public async Task<bool> Update(Pais paisParam)
        {
            var pais = await _context.Paises.AsNoTracking().FirstOrDefaultAsync(pais => pais.Id == paisParam.Id);

            if (pais is null)
            {
                return false;
            }

            pais.Nome = paisParam.Nome;
            _context.Entry(pais).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var pais = await _context.Paises.AsNoTracking().FirstOrDefaultAsync(pais => pais.Id == id);

            if (pais is null)
            {
                return false;
            }

            _context.Remove(pais);
            _context.SaveChanges();
            return true;
        }
    }
}
