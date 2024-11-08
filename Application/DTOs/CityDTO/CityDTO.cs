using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.CityDTO
{
    public class CityDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int StateId { get; set; }
    }
}
