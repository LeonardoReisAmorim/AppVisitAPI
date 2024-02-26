using AppVisitAPI.Data.Context;
using AppVisitAPI.DTOs.LugarDTO;
using AppVisitAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AppVisitAPI.Services
{
    public class LugarService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public LugarService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LerLugarDTO> CreateLugar(InserirLugarDTO lugarDTO)
        {
            try
            {
                Lugar lugar = _mapper.Map<Lugar>(lugarDTO);
                await _context.Lugares.AddAsync(lugar);
                _context.SaveChanges();
                return _mapper.Map<LerLugarDTO>(lugar);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<LerLugarDTO>> GetLugar(int? id = null)
        {
            var lugaresDTO = new List<LerLugarDTO>();

            if (id.HasValue)
            {
                lugaresDTO = _mapper.Map<List<LerLugarDTO>>(await _context.Lugares
                                                           .AsNoTracking()
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
                                                               NomeArquivo = lugar.Arquivo.NomeArquivo
                                                           })
                                                           .ToListAsync());
            }
            else
            {
                lugaresDTO = _mapper.Map<List<LerLugarDTO>>(await _context.Lugares
                                                            .AsNoTracking()
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
                                                               NomeArquivo = lugar.Arquivo.NomeArquivo
                                                            })
                                                            .ToListAsync());
            }

            return lugaresDTO;
        }

        public async Task<bool> UpdateLugar(int id, EditarLugarDTO updateLugarDTO)
        {
            var lugar = await _context.Lugares.AsNoTracking().FirstOrDefaultAsync(lugar => lugar.Id == id);

            if (lugar != null)
            {
                _mapper.Map(updateLugarDTO, lugar);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteLugar(int id)
        {
            var lugar = await _context.Lugares.AsNoTracking().FirstOrDefaultAsync(lugar => lugar.Id == id);

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
