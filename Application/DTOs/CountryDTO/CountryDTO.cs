using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.CountryDTO
{
    public class CountryDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
