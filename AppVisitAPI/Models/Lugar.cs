using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppVisitAPI.Models
{
    public class Lugar
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public int ArquivoId { get; set;}

        [JsonIgnore]
        public virtual Arquivo Arquivo { get; set; }

        [Required]
        [JsonIgnore]
        public int CidadeId { get; set; }

        [Required]
        public byte[] Imagem { get; set; }

        [JsonIgnore]
        public virtual Cidade Cidade { get; set; }
    }
}
