using AppVisitAPI.Data.Context;
using AppVisitAPI.DTOs.PaisDTO;
using AppVisitAPI.Models;
using AutoMapper;

namespace AppVisitAPI.Services
{
    public class PaisService
    {
        private Context _context;
        private IMapper _mapper;

        public PaisService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public LerPaisDTO CreatePais(CriarPaisDTO paisDTO)
        {
            try
            {
                Pais pais = _mapper.Map<Pais>(paisDTO);
                _context.Paises.Add(pais);
                _context.SaveChanges();
                return _mapper.Map<LerPaisDTO>(pais);
            }
            catch
            {
                return null;
            }
        }

        public List<LerPaisDTO> GetPais(int? id = null)
        {
            var paisesDTO = new List<LerPaisDTO>();

            if(id.HasValue)
            {
                paisesDTO = _mapper.Map<List<LerPaisDTO>>(_context.Paises.Where(pais => pais.Id == id).ToList());
            }
            else
            {
                paisesDTO = _mapper.Map<List<LerPaisDTO>>(_context.Paises.ToList());
            }

            return paisesDTO;
        }

        public bool UpdatePais(int id, EditarPaisDTO updatePaisDTO)
        {
            var pais = _context.Paises.FirstOrDefault(pais => pais.Id == id);

            if(pais != null)
            {
                _mapper.Map(updatePaisDTO, pais);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeletePais(int id)
        {
            var pais = _context.Paises.FirstOrDefault(pais => pais.Id == id);

            if (pais != null)
            {
                _context.Remove(pais);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
