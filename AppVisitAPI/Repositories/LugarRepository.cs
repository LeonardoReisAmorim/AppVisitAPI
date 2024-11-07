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
                             .Include(lugar => lugar.Arquivo)
                             .Include(lugar => lugar.Cidade)
                             .Where(lugar => lugar.Id == id)
                             .AsSplitQuery()
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
                         .AsSplitQuery()
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

        public async Task<bool> UpdateLugar(int id, EditarLugarDTO lugar)
        {
            var result = await _context.Lugares
                .Where(lugar => lugar.Id == id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(l => l.ArquivoId, lugar.ArquivoId)
                .SetProperty(l => l.Descricao, lugar.Descricao)
                .SetProperty(l => l.CidadeId, lugar.CidadeId)
                .SetProperty(l => l.Imagem, Convert.FromBase64String(lugar.Imagem))
                .SetProperty(l => l.InstrucoesUtilizacaoVR, lugar.InstrucoesUtilizacaoVR)
                .SetProperty(l => l.Nome, lugar.Nome));
            return result > 0;
        }

        public async Task<bool> DeleteLugar(int id)
        {
            var result = await _context.Lugares
                .Where(lugar => lugar.Id == id)
                .ExecuteDeleteAsync();
            return result > 0;
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
