using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppVisitAPI.Models
{
    public class Arquivo
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public byte[] ArquivoConteudo { get; set; }

        [Required]
        public string NomeArquivo { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; }

        [JsonIgnore]
        public virtual Lugar Lugar { get; set; }

    }
}
