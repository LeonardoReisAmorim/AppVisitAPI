using AppVisitAPI.Data.Context;
using AppVisitAPI.Interfaces.Repositories;
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

        public async Task<Estado> Create(Estado estado)
        {
            await _context.Estados.AddAsync(estado);
            _context.SaveChanges();
            return estado;
        }

        public async Task<List<Estado>> Get(int? id = null)
        {
            if (id.HasValue)
            {
                return await _context.Estados.AsNoTracking().Where(estado => estado.Id == id).ToListAsync();
            }

            return await _context.Estados.AsNoTracking().ToListAsync();
        }

        public async Task<bool> Update(Estado estadoParam)
        {
            var estado = await _context.Estados.AsNoTracking().FirstOrDefaultAsync(estado => estado.Id == estadoParam.Id);

            if (estado is null)
            {
                return false;
            }

            estado.Nome = estadoParam.Nome;
            estado.PaisId = estadoParam.PaisId;
            _context.Entry(estado).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var estado = await _context.Estados.AsNoTracking().FirstOrDefaultAsync(estado => estado.Id == id);

            if (estado is null)
            {
                return false;
            }

            _context.Remove(estado);
            _context.SaveChanges();
            return true;
        }
    }
}
