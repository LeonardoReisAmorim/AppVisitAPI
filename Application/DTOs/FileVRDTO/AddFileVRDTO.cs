using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.FileVRDTO
{
    public class AddFileVRDTO
    {
        [Required]
        public string FileName { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
