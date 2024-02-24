using System.ComponentModel.DataAnnotations;

namespace AppVisitAPI.DTOs.ArquivoDTO
{
    public class InserirArquivoDTO
    {
        [Required]
        public string NomeArquivo { get; set; }
        [Required]
        public DateTime DataCriacao { get; set; }
    }
}
