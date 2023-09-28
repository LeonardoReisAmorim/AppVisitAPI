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
            try
            {
                Estado estado = _mapper.Map<Estado>(estadoDTO);
                await _context.Estados.AddAsync(estado);
                _context.SaveChanges();
                return _mapper.Map<LerEstadoDTO>(estado);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<LerEstadoDTO>> GetEstado(int? id = null)
        {
            var estadosDTO = new List<LerEstadoDTO>();

            if (id.HasValue)
            {
                estadosDTO = _mapper.Map<List<LerEstadoDTO>>(await _context.Estados.Where(estado => estado.Id == id).ToListAsync());
            }
            else
            {
                estadosDTO = _mapper.Map<List<LerEstadoDTO>>(await _context.Estados.ToListAsync());
            }

            return estadosDTO;
        }

        public async Task<bool> UpdateEstado(int id, EditarEstadoDTO updateEstadoDTO)
        {
            var estado = await _context.Estados.FirstOrDefaultAsync(estado => estado.Id == id);

            if (estado != null)
            {
                _mapper.Map(updateEstadoDTO, estado);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteEstado(int id)
        {
            var estado = await _context.Estados.FirstOrDefaultAsync(estado => estado.Id == id);

            if (estado != null)
            {
                _context.Remove(estado);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
