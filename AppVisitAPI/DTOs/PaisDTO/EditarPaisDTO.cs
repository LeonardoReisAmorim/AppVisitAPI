using System.ComponentModel.DataAnnotations;

namespace AppVisitAPI.DTOs.PaisDTO
{
    public class EditarPaisDTO
    {
        [Required]
        public string Nome { get; set; }
    }
}
