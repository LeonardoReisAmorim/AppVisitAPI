using AppVisitAPI.Data.Context;
using AppVisitAPI.DTOs.CidadeDTO;
using AppVisitAPI.DTOs.PaisDTO;
using AppVisitAPI.Models;
using AutoMapper;

namespace AppVisitAPI.Services
{
    public class CidadeService
    {
        private Context _context;
        private IMapper _mapper;

        public CidadeService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public LerCidadeDTO CreateCidade(CriarCidadeDTO cidadeDTO)
        {
            try
            {
                Cidade cidade = _mapper.Map<Cidade>(cidadeDTO);
                _context.Cidades.Add(cidade);
                _context.SaveChanges();
                return _mapper.Map<LerCidadeDTO>(cidade);
            }
            catch
            {
                return null;
            }
        }

        public List<LerCidadeDTO> GetCidade(int? id = null)
        {
            var cidadesDTO = new List<LerCidadeDTO>();

            if (id.HasValue)
            {
                cidadesDTO = _mapper.Map<List<LerCidadeDTO>>(_context.Cidades.Where(pais => pais.Id == id).ToList());
            }
            else
            {
                cidadesDTO = _mapper.Map<List<LerCidadeDTO>>(_context.Cidades.ToList());
            }

            return cidadesDTO;
        }

        public bool UpdateCidade(int id, EditarCidadeDTO updateCidadeDTO)
        {
            var cidade = _context.Cidades.FirstOrDefault(pais => pais.Id == id);

            if (cidade != null)
            {
                _mapper.Map(updateCidadeDTO, cidade);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeleteCidade(int id)
        {
            var cidade = _context.Cidades.FirstOrDefault(pais => pais.Id == id);

            if (cidade != null)
            {
                _context.Remove(cidade);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
