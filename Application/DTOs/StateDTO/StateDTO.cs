using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.StateDTO
{
    public class StateDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}
