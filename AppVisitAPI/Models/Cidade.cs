using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppVisitAPI.Models
{
    public class Cidade
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [JsonIgnore]
        public int EstadoId { get; set; }

        [JsonIgnore]
        public virtual Estado Estado { get; set; }

        public virtual List<Lugar> Lugares { get; set; }
    }
}
