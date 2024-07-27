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
            await _context.SaveChangesAsync();
            return lugar;
        }

        public async Task<List<LerLugarDTO>> GetLugar(int? id = null)
        {
            if (id.HasValue)
            { 
                return await _context.Lugares.AsNoTracking()
                             .Where(lugar => lugar.Id == id)
                             .Include(lugar => lugar.Arquivo)
                             .Include(lugar => lugar.Cidade)
                             .Select(lugar => new LerLugarDTO
                             {
                                 ArquivoId = lugar.ArquivoId,
                                 Cidade = lugar.Cidade.Nome,
                                 Descricao = lugar.Descricao,
                                 Id = lugar.Id,
                                 Imagem = Convert.ToBase64String(lugar.Imagem),
                                 Nome = lugar.Nome,
                                 NomeArquivo = lugar.Arquivo.NomeArquivo,
                                 CidadeId = lugar.CidadeId,
                                 InstrucoesUtilizacaoVR = lugar.InstrucoesUtilizacaoVR
                             })
                             .ToListAsync();
            }

            return await _context.Lugares.AsNoTracking()
                         .Include(lugar => lugar.Arquivo)
                         .Include(lugar => lugar.Cidade)
                         .Select(lugar => new LerLugarDTO
                         {
                             ArquivoId = lugar.ArquivoId,
                             Cidade = lugar.Cidade.Nome,
                             Descricao = lugar.Descricao,
                             Id = lugar.Id,
                             Imagem = Convert.ToBase64String(lugar.Imagem),
                             Nome = lugar.Nome,
                             NomeArquivo = lugar.Arquivo.NomeArquivo,
                             CidadeId = lugar.CidadeId,
                             InstrucoesUtilizacaoVR = lugar.InstrucoesUtilizacaoVR
                         })
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

        public async Task<bool> UpdateLugar(int id, Lugar lugar)
        {
            _context.Entry(lugar).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLugar(Lugar lugar)
        {
            _context.Remove(lugar);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsArquivo(int arquivoId)
        {
            return await _context.Lugares.AnyAsync(lugar => lugar.ArquivoId == arquivoId);
        }

        public async Task<Lugar> GetLugarById(int id)
        {
            return await _context.Lugares.AsNoTracking().FirstOrDefaultAsync(lugar => lugar.Id == id);
        }
    }
}
