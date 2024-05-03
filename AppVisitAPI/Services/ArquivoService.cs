using AppVisitAPI.Data.Context;
using AppVisitAPI.DTOs.ArquivoDTO;
using AppVisitAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppVisitAPI.Services
{
    public class ArquivoService
    {
        private readonly Context _context;

        public ArquivoService(Context context)
        {
            _context = context;
        }

        public byte[] GetArquivoById(int id)
        {
            return _context.Arquivos.AsNoTracking().FirstOrDefault(x => x.Id == id).ArquivoConteudo;
        }

        public async Task<IEnumerable<LerDadosArquivoDTO>> GetDadosArquivo(int? id = null)
        {
            if(id.HasValue)
            {
                return await _context.Arquivos.AsNoTracking().Where(a => a.Id == id).Select(a => new LerDadosArquivoDTO
                {
                    Id = a.Id,
                    NomeArquivo = a.NomeArquivo,
                    DataCriacao = a.DataCriacao.ToString("dd/MM/yyyy HH:mm:ss")
                }).ToListAsync();
            }
            
            return await _context.Arquivos.AsNoTracking().Select(a => new LerDadosArquivoDTO
            {
                Id = a.Id,
                NomeArquivo = a.NomeArquivo,
                DataCriacao = a.DataCriacao.ToString("dd/MM/yyyy HH:mm:ss")
            }).ToListAsync();
        }

        public LerArquivoDTO CreateArquivo(byte[] arquivoDTO, InserirArquivoDTO arquivodados)
        {
            var arquivo = new Arquivo
            {
                ArquivoConteudo = arquivoDTO,
                NomeArquivo = arquivodados.NomeArquivo,
                DataCriacao = arquivodados.DataCriacao
            };

            _context.Arquivos.Add(arquivo);
            _context.SaveChanges();

            var lerArquivo = new LerArquivoDTO
            {
                Id = arquivo.Id
            };

            return lerArquivo;
        }

        public bool UpdateArquivo(int id, EditarArquivo editarArquivoDTO)
        {
            var arquivo = _context.Arquivos.AsNoTracking().SingleOrDefault(arquivo => arquivo.Id == id);

            if (arquivo is null)
            {
                return false;
            }

            arquivo.ArquivoConteudo = editarArquivoDTO.Arquivo;
            arquivo.NomeArquivo = editarArquivoDTO.NomeArquivo;
            _context.Entry(arquivo).State = EntityState.Modified;
            _context.SaveChanges();

            return true;
        }

        public bool DeleteArquivo(int id)
        {
            var arquivo = _context.Arquivos.AsNoTracking().SingleOrDefault(arquivo => arquivo.Id == id);

            if (arquivo is null)
            {
                return false;
            }

            _context.Remove(arquivo);
            _context.SaveChanges();
            
            return true;
        }
    }
}
