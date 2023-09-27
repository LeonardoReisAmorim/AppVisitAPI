using AppVisitAPI.Models;

namespace AppVisitAPI.DTOs.EstadoDTO
{
    public class LerEstadoDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public List<Cidade> Cidades { get; set; }
    }
}
