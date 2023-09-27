using AppVisitAPI.Data.Context;
using AppVisitAPI.DTOs.EstadoDTO;
using AppVisitAPI.DTOs.PaisDTO;
using AppVisitAPI.Models;
using AutoMapper;

namespace AppVisitAPI.Services
{
    public class EstadoService
    {
        private Context _context;
        private IMapper _mapper;

        public EstadoService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public LerEstadoDTO CreateEstado(CriarEstadoDTO estadoDTO)
        {
            try
            {
                Estado estado = _mapper.Map<Estado>(estadoDTO);
                _context.Estados.Add(estado);
                _context.SaveChanges();
                return _mapper.Map<LerEstadoDTO>(estado);
            }
            catch
            {
                return null;
            }
        }

        public List<LerEstadoDTO> GetEstado(int? id = null)
        {
            var estadosDTO = new List<LerEstadoDTO>();

            if (id.HasValue)
            {
                estadosDTO = _mapper.Map<List<LerEstadoDTO>>(_context.Estados.Where(pais => pais.Id == id).ToList());
            }
            else
            {
                estadosDTO = _mapper.Map<List<LerEstadoDTO>>(_context.Estados.ToList());
            }

            return estadosDTO;
        }

        public bool UpdateEstado(int id, EditarEstadoDTO updateEstadoDTO)
        {
            var estado = _context.Estados.FirstOrDefault(pais => pais.Id == id);

            if (estado != null)
            {
                _mapper.Map(updateEstadoDTO, estado);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeleteEstado(int id)
        {
            var estado = _context.Estados.FirstOrDefault(pais => pais.Id == id);

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
