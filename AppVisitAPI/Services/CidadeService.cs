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
            Cidade cidade = _mapper.Map<Cidade>(cidadeDTO);
            await _context.Cidades.AddAsync(cidade);
            _context.SaveChanges();
            return _mapper.Map<LerCidadeDTO>(cidade);
        }

        public async Task<List<LerCidadeDTO>> GetCidade(int? id = null)
        {
            if (id.HasValue)
            {
                return _mapper.Map<List<LerCidadeDTO>>(await _context.Cidades.AsNoTracking().Where(cidade => cidade.Id == id).ToListAsync());
            }

            return _mapper.Map<List<LerCidadeDTO>>(await _context.Cidades.AsNoTracking().ToListAsync());
        }

        public async Task<bool> UpdateCidade(int id, EditarCidadeDTO updateCidadeDTO)
        {
            var cidade = await _context.Cidades.AsNoTracking().FirstOrDefaultAsync(cidade => cidade.Id == id);

            if (cidade is null)
            {
                return false;
            }

            _mapper.Map(updateCidadeDTO, cidade);
            _context.Entry(cidade).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteCidade(int id)
        {
            var cidade = await _context.Cidades.AsNoTracking().FirstOrDefaultAsync(cidade => cidade.Id == id);

            if (cidade is null)
            {
                return false;
            }

            _context.Remove(cidade);
            _context.SaveChanges();
            return true;
        }
    }
}
