using AppVisitAPI.Data.Context;
using AppVisitAPI.DTOs.ArquivoDTO;
using AppVisitAPI.Models;
using AutoMapper;

namespace AppVisitAPI.Services
{
    public class ArquivoService
    {
        private Context _context;

        public ArquivoService(Context context)
        {
            _context = context;
        }

        public LerArquivoDTO GetArquivoById(int id)
        {
            var arquivo = _context.Arquivos.FirstOrDefault(x => x.Id == id);

            if (arquivo == null)
            {
                return null;
            }

            var arquivoDto = new LerArquivoDTO
            {
                Id = id,
                Arquivo = Convert.ToBase64String(arquivo.File)
            };

            return arquivoDto;
        }

        public LerArquivoDTO CreateArquivo(byte[] arquivoDTO)
        {
            var arquivo = new Arquivo
            {
                File = arquivoDTO
            };

            _context.Arquivos.Add(arquivo);
            _context.SaveChanges();

            var returnArquivoDTO = new LerArquivoDTO
            {
                Id = arquivo.Id
            };
           
            return returnArquivoDTO;
        }

        public bool UpdateArquivo(int id, EditarArquivo editarArquivoDTO)
        {
            var arquivo = _context.Arquivos.FirstOrDefault(arquivo => arquivo.Id == id);

            if (arquivo == null)
            {
                return false;
            }

            arquivo.File = editarArquivoDTO.Arquivo;
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
