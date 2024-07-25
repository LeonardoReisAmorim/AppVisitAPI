using AppVisitAPI.Data.Context;
using AppVisitAPI.Interfaces.IEstado;
using AppVisitAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppVisitAPI.Repositories
{
    public class EstadoRepository : IEstadoRepository
    {
        private readonly Context _context;

        public EstadoRepository(Context context)
        {
            _context = context;
        }

        public async Task<Estado> CreateEstado(Estado estado)
        {
            await _context.Estados.AddAsync(estado);
            await _context.SaveChangesAsync();
            return estado;
        }

        public async Task<List<Estado>> GetEstado(int? id = null)
        {
            if (id.HasValue)
            {
                return await _context.Estados.AsNoTracking().Where(estado => estado.Id == id).ToListAsync();
            }

            return await _context.Estados.AsNoTracking().ToListAsync();

        }

        public async Task<bool> UpdateEstado(int id, Estado estado)
        {
            _context.Entry(estado).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEstado(Estado estado)
        {
            _context.Remove(estado);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Estado> GetEstadoById(int id)
        {
            return await _context.Estados.AsNoTracking().FirstOrDefaultAsync(estado => estado.Id == id);
        }
    }
}
