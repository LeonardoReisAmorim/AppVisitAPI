using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.FileVRDTO
{
    public class UpdateFileVRDTO
    {
        [Required]
        public byte[] Content { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
