using AppVisitAPI.Data.Context;
using AppVisitAPI.DTOs.EstadoDTO;
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
            _context.SaveChanges();
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

        public async Task<bool> UpdateEstado(int id, EditarEstadoDTO updateEstadoDTO)
        {
            var estado = await _context.Estados.AsNoTracking().FirstOrDefaultAsync(estado => estado.Id == id);

            if (estado is null)
            {
                return false;
            }

            estado.Id = id;
            estado.Nome = updateEstadoDTO.Nome;
            estado.PaisId = updateEstadoDTO.PaisId;

            _context.Entry(estado).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteEstado(int id)
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
