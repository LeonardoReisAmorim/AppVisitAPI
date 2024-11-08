using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class City
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [JsonIgnore]
        public int StateId { get; set; }

        [JsonIgnore]
        public virtual State State { get; set; }

        public virtual List<Place> Places { get; set; }
    }
}
