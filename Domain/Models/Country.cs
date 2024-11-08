using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Country
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual List<State> States { get; set; }
    }
}
