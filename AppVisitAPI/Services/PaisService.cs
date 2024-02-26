using AppVisitAPI.Data.Context;
using AppVisitAPI.DTOs.PaisDTO;
using AppVisitAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AppVisitAPI.Services
{
    public class PaisService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public PaisService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LerPaisDTO> CreatePais(CriarPaisDTO paisDTO)
        {
            try
            {
                Pais pais = _mapper.Map<Pais>(paisDTO);
                await _context.Paises.AddAsync(pais);
                _context.SaveChanges();
                return _mapper.Map<LerPaisDTO>(pais);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<LerPaisDTO>> GetPais(int? id = null)
        {
            var paisesDTO = new List<LerPaisDTO>();

            if(id.HasValue)
            {
                paisesDTO = _mapper.Map<List<LerPaisDTO>>(await _context.Paises.AsNoTracking().Where(pais => pais.Id == id).ToListAsync());
            }
            else
            {
                paisesDTO = _mapper.Map<List<LerPaisDTO>>(await _context.Paises.AsNoTracking().ToListAsync());
            }

            return paisesDTO;
        }

        public async Task<bool> UpdatePais(int id, EditarPaisDTO updatePaisDTO)
        {
            var pais = await _context.Paises.AsNoTracking().FirstOrDefaultAsync(pais => pais.Id == id);

            if(pais != null)
            {
                _mapper.Map(updatePaisDTO, pais);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<bool> DeletePais(int id)
        {
            var pais = await _context.Paises.AsNoTracking().FirstOrDefaultAsync(pais => pais.Id == id);

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
