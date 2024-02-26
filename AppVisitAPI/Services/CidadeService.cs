using AppVisitAPI.Data.Context;
using AppVisitAPI.DTOs.CidadeDTO;
using AppVisitAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AppVisitAPI.Services
{
    public class CidadeService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public CidadeService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LerCidadeDTO> CreateCidade(CriarCidadeDTO cidadeDTO)
        {
            try
            {
                Cidade cidade = _mapper.Map<Cidade>(cidadeDTO);
                await _context.Cidades.AddAsync(cidade);
                _context.SaveChanges();
                return _mapper.Map<LerCidadeDTO>(cidade);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<LerCidadeDTO>> GetCidade(int? id = null)
        {
            var cidadesDTO = new List<LerCidadeDTO>();

            if (id.HasValue)
            {
                cidadesDTO = _mapper.Map<List<LerCidadeDTO>>(await _context.Cidades.AsNoTracking().Where(cidade => cidade.Id == id).ToListAsync());
            }
            else
            {
                cidadesDTO = _mapper.Map<List<LerCidadeDTO>>(await _context.Cidades.AsNoTracking().ToListAsync());
            }

            return cidadesDTO;
        }

        public async Task<bool> UpdateCidade(int id, EditarCidadeDTO updateCidadeDTO)
        {
            var cidade = await _context.Cidades.AsNoTracking().FirstOrDefaultAsync(cidade => cidade.Id == id);

            if (cidade != null)
            {
                _mapper.Map(updateCidadeDTO, cidade);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteCidade(int id)
        {
            var cidade = await _context.Cidades.AsNoTracking().FirstOrDefaultAsync(cidade => cidade.Id == id);

            if (cidade != null)
            {
                _context.Remove(cidade);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
