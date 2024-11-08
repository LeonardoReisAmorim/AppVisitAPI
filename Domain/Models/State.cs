using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class State
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [JsonIgnore]
        public int CountryId { get; set; }

        [JsonIgnore]
        public virtual Country Country { get; set; }

        public virtual List<City> Cities { get; set; }
    }
}
