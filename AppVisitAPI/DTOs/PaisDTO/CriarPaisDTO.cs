using System.ComponentModel.DataAnnotations;

namespace AppVisitAPI.DTOs.PaisDTO
{
    public class CriarPaisDTO
    {
        [Required]
        public string Nome { get; set; }
    }
}
