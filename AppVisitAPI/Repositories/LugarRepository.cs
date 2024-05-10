using AppVisitAPI.Data.Context;
using AppVisitAPI.Interfaces.Repositories;
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

        public async Task<Lugar> Create(Lugar lugar)
        {
            await _context.Lugares.AddAsync(lugar);
            _context.SaveChanges();
            return lugar;
        }

        public async Task<List<Lugar>> Get(int? id = null)
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

        public async Task<string?> GetInstrucaoUtilizarVRById(int id)
        {
            if (id == 0)
            {
                return null;
            }

            return await _context.Lugares.AsNoTracking()
                   .Where(lugar => lugar.Id == id)
                   .Select(lugar => lugar.InstrucoesUtilizacaoVR)
                   .SingleOrDefaultAsync();
        }

        public async Task<bool> Update(Lugar lugarParam)
        {
            var lugar = await _context.Lugares.AsNoTracking().FirstOrDefaultAsync(lugar => lugar.Id == lugarParam.Id);

            if (lugar is null)
            {
                return false;
            }

            lugar.ArquivoId = lugarParam.ArquivoId;
            lugar.Nome = lugarParam.Nome;
            lugar.Descricao = lugarParam.Descricao;
            lugar.CidadeId = lugarParam.CidadeId;
            lugar.Imagem = lugarParam.Imagem;
            lugar.InstrucoesUtilizacaoVR = lugarParam.InstrucoesUtilizacaoVR;
            _context.Entry(lugar).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> Delete(int id)
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
