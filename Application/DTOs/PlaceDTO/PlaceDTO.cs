using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.PlaceDTO
{
    public class PlaceDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int FileVRId { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string UsageInstructionsVR { get; set; }
    }
}
