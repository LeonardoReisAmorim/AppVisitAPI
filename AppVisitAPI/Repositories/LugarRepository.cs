using AppVisitAPI.Data.Context;
using AppVisitAPI.DTOs.LugarDTO;
using AppVisitAPI.Interfaces.ILugar;
using AppVisitAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppVisitAPI.Repositories
{
    public class LugarRepository : ILugarRepository
    {
        private readonly Context _context;

        public LugarRepository(Context context)
        {
            _context = context;
        }

        public async Task<Lugar> CreateLugar(Lugar lugar)
        {
            await _context.Lugares.AddAsync(lugar);
            _context.SaveChanges();
            return lugar;
        }

        public async Task<List<Lugar>> GetLugar(int? id = null)
        {
            if (id.HasValue)
            {
                return await _context.Lugares.AsNoTracking()
                             .Where(lugar => lugar.Id == id)
                             .Include(lugar => lugar.Arquivo)
                             .Include(lugar => lugar.Cidade)
                             .ToListAsync();
            }

            return await _context.Lugares.AsNoTracking()
                         .Include(lugar => lugar.Arquivo)
                         .Include(lugar => lugar.Cidade)
                         .ToListAsync();
        }

        public async Task<string?> GetUtilizacaoLugarVRById(int id)
        {
            if (id == 0)
            {
                return null;
            }

            return await _context.Lugares
                                         .AsNoTracking()
                                         .Where(lugar => lugar.Id == id)
                                         .Select(lugar => lugar.InstrucoesUtilizacaoVR)
                                         .SingleOrDefaultAsync();
        }

        public async Task<bool> UpdateLugar(int id, EditarLugarDTO updateLugarDTO)
        {
            var lugar = await _context.Lugares.AsNoTracking().FirstOrDefaultAsync(lugar => lugar.Id == id);

            if (lugar is null)
            {
                return false;
            }

            lugar.Id = id;
            lugar.Nome = updateLugarDTO.Nome;
            lugar.Descricao = updateLugarDTO.Descricao;
            lugar.ArquivoId = updateLugarDTO.ArquivoId;
            lugar.CidadeId = updateLugarDTO.CidadeId;
            lugar.Imagem = Convert.FromBase64String(updateLugarDTO.Imagem);
            lugar.InstrucoesUtilizacaoVR = updateLugarDTO.InstrucoesUtilizacaoVR;

            _context.Entry(lugar).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteLugar(int id)
        {
            var lugar = await _context.Lugares.AsNoTracking().FirstOrDefaultAsync(lugar => lugar.Id == id);

            if (lugar is null)
            {
                return false;
            }

            _context.Remove(lugar);
            _context.SaveChanges();
            return true;
        }
    }
}
