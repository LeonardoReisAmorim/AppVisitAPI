using System.ComponentModel.DataAnnotations;

namespace AppVisitAPI.Models
{
    public class Pais
    {
        [Key]
        [Required] 
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public List<Estado> Estados { get; set; }
    }
}
