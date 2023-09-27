using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public int PaisId { get; set; }

        [JsonIgnore]
        public virtual Pais Pais { get; set; }

        public virtual List<Cidade> Cidades { get; set; }
    }
}
