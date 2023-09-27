using AppVisitAPI.Data.Context;

namespace AppVisitAPI.Services
{
    public class CidadeService
    {
        private Context _context;
        //private Imapper _mapper;

        public CidadeService(Context context /*Imapper mapper*/)
        {
            _context = context;
            //_mapper = mapper;
        }
    }
}
