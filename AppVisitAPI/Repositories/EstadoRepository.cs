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
            var result = await _context.Estados
                        .Where(estado => estado.Id == id)
                        .ExecuteUpdateAsync(setters => setters
                        .SetProperty(e => e.PaisId, estado.PaisId)
                        .SetProperty(e => e.Nome, estado.Nome));
            return result > 0;
        }

        public async Task<bool> DeleteEstado(int id)
        {
            var result = await _context.Estados
                        .Where(estado => estado.Id == id)
                        .ExecuteDeleteAsync();
            return result > 0;
        }

        public async Task<Estado> GetEstadoById(int id)
        {
            return await _context.Estados.AsNoTracking().FirstOrDefaultAsync(estado => estado.Id == id);
        }
    }
}
