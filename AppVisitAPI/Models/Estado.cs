using System.ComponentModel.DataAnnotations;

namespace AppVisitAPI.Models
{
    public class Estado
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Nome { get; set; }

        [Required]
        public int PaisId { get; set; }

        public Pais Pais { get; set; }

        public List<Cidade> Cidades { get; set; }
    }
}
