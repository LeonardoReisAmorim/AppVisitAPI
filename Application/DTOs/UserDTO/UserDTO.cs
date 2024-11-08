using System.ComponentModel.DataAnnotations.Schema;

namespace Application.DTOs.UserDTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; } = false;
        [NotMapped]
        public string Password { get; set; }
    }
}
