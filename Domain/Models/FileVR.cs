using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class FileVR
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public byte[] FileContent { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public virtual Place Place { get; set; }

    }
}
