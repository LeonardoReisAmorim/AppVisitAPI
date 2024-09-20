using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppVisitAPI.DTOs.UsuarioDTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; } = false;
        [NotMapped]
        public string Password { get; set; }
    }
}
