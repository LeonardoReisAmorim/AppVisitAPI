using AppVisitAPI.Data.Context;
using AppVisitAPI.DTOs.CidadeDTO;
using AppVisitAPI.Interfaces.ICidade;
using AppVisitAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppVisitAPI.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly Context _context;

        public CidadeRepository(Context context)
        {
            _context = context;
        }

        public async Task<Cidade> CreateCidade(Cidade cidade)
        {
            await _context.Cidades.AddAsync(cidade);
            _context.SaveChanges();
            return cidade;
        }

        public async Task<List<Cidade>> GetCidade(int? id = null)
        {
            if (id.HasValue)
            {
                return await _context.Cidades.AsNoTracking().Where(cidade => cidade.Id == id).ToListAsync();
            }

            return await _context.Cidades.AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdateCidade(int id, EditarCidadeDTO updateCidadeDTO)
        {
            var cidade = await _context.Cidades.AsNoTracking().FirstOrDefaultAsync(cidade => cidade.Id == id);

            if (cidade is null)
            {
                return false;
            }

            cidade.Id = id;
            cidade.EstadoId = updateCidadeDTO.EstadoId;
            cidade.Nome = updateCidadeDTO.Nome;

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
