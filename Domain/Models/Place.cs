using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Place
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int FileVRId { get; set; }

        [JsonIgnore]
        public virtual FileVR FileVR { get; set; }

        [Required]
        [JsonIgnore]
        public int CityId { get; set; }

        [Required]
        public byte[] Image { get; set; }

        [JsonIgnore]
        public virtual City City { get; set; }

        [Required]
        public string UsageInstructionsVR { get; set; }
    }
}
