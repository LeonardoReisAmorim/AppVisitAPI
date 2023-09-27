using System.ComponentModel.DataAnnotations;

namespace AppVisitAPI.DTOs.CidadeDTO
{
    public class CriarCidadeDTO
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public int EstadoId { get; set; }
    }
}
