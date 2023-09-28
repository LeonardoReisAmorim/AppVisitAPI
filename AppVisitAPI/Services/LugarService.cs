using AppVisitAPI.Data.Context;
using AppVisitAPI.DTOs.LugarDTO;
using AppVisitAPI.DTOs.PaisDTO;
using AppVisitAPI.Models;
using AutoMapper;
using Org.BouncyCastle.Utilities;

namespace AppVisitAPI.Services
{
    public class LugarService
    {
        private Context _context;
        private IMapper _mapper;

        public LugarService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public LerLugarDTO CreateLugar(InserirLugarDTO lugarDTO)
        {
            try
            {
                Lugar lugar = _mapper.Map<Lugar>(lugarDTO);
                _context.Lugares.Add(lugar);
                _context.SaveChanges();
                return _mapper.Map<LerLugarDTO>(lugar);
            }
            catch
            {
                return null;
            }
        }

        public List<LerLugarDTO> GetLugar(int? id = null)
        {
            var lugaresDTO = new List<LerLugarDTO>();

            if (id.HasValue)
            {
                lugaresDTO = _mapper.Map<List<LerLugarDTO>>(_context.Lugares.Where(pais => pais.Id == id).ToList());
            }
            else
            {
                lugaresDTO = _mapper.Map<List<LerLugarDTO>>(_context.Lugares.ToList());
            }

            return lugaresDTO;
        }

        public bool UpdateLugar(int id, EditarLugarDTO updateLugarDTO)
        {
            var lugar = _context.Lugares.FirstOrDefault(pais => pais.Id == id);

            if (lugar != null)
            {
                _mapper.Map(updateLugarDTO, lugar);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeleteLugar(int id)
        {
            var lugar = _context.Lugares.FirstOrDefault(pais => pais.Id == id);

            if (lugar != null)
            {
                _context.Remove(lugar);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
