using AppVisitAPI.Data.Context;
using AppVisitAPI.DTOs.PaisDTO;
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
            _context.SaveChanges();
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

        public async Task<bool> UpdatePais(int id, EditarPaisDTO updatePaisDTO)
        {
            var pais = await _context.Paises.AsNoTracking().FirstOrDefaultAsync(pais => pais.Id == id);

            if (pais is null)
            {
                return false;
            }

            pais.Id = id;
            pais.Nome = updatePaisDTO.Nome;

            _context.Entry(pais).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> DeletePais(int id)
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
