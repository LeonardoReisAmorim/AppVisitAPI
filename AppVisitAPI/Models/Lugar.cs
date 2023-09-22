using System.ComponentModel.DataAnnotations;

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

        public Arquivo Arquivo { get; set; }

        [Required]
        public int CidadeId { get; set; }
        public Cidade Cidade { get; set; }

        [Required]
        public byte[] Imagem { get; set; }
    }
}
