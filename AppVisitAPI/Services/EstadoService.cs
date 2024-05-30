using AppVisitAPI.Data.Context;
using AppVisitAPI.DTOs.EstadoDTO;
using AppVisitAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AppVisitAPI.Services
{
    public class EstadoService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public EstadoService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LerEstadoDTO> CreateEstado(CriarEstadoDTO estadoDTO)
        {
            Estado estado = _mapper.Map<Estado>(estadoDTO);
            await _context.Estados.AddAsync(estado);
            _context.SaveChanges();
            return _mapper.Map<LerEstadoDTO>(estado);
        }

        public async Task<List<LerEstadoDTO>> GetEstado(int? id = null)
        {
            if (id.HasValue)
            {
                return _mapper.Map<List<LerEstadoDTO>>(await _context.Estados.AsNoTracking().Where(estado => estado.Id == id).ToListAsync());
            }

            return _mapper.Map<List<LerEstadoDTO>>(await _context.Estados.AsNoTracking().ToListAsync());

        }

        public async Task<bool> UpdateEstado(int id, EditarEstadoDTO updateEstadoDTO)
        {
            var estado = await _context.Estados.AsNoTracking().FirstOrDefaultAsync(estado => estado.Id == id);

            if (estado is null)
            {
                return false;
            }

            _mapper.Map(updateEstadoDTO, estado);
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
