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
            var arquivo = _context.Arquivos.AsNoTracking().Where(x => x.Id == id).Select(x => x.ArquivoConteudo).First();

            return arquivo;
        }

        public IEnumerable<LerDadosArquivoDTO> GetDadosArquivo()
        {
            var dadosArquivo = _context.Arquivos.AsNoTracking().Select(a => new LerDadosArquivoDTO
            {
                Id = a.Id,
                NomeArquivo = a.NomeArquivo,
                DataCriacao = a.DataCriacao.ToString("dd/MM/yyyy HH:mm:ss")
            });

            return dadosArquivo;
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
            var arquivo = _context.Arquivos.FirstOrDefault(arquivo => arquivo.Id == id);

            if (arquivo == null)
            {
                return false;
            }

            arquivo.ArquivoConteudo = editarArquivoDTO.Arquivo;
            _context.SaveChanges();

            return true;
        }

        public bool DeleteArquivo(int id)
        {
            var arquivo = _context.Arquivos.FirstOrDefault(arquivo => arquivo.Id == id);

            if (arquivo == null)
            {
                return false;
            }

            _context.Remove(arquivo);
            _context.SaveChanges();
            
            return true;
        }
    }
}
