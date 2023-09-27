using System.ComponentModel.DataAnnotations;

namespace AppVisitAPI.DTOs.EstadoDTO
{
    public class EditarEstadoDTO
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public int PaisId { get; set; }
    }
}
