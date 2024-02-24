using System.ComponentModel.DataAnnotations;

namespace AppVisitAPI.DTOs.ArquivoDTO
{
    public class EditarArquivo
    {
        [Required]
        public byte[] Arquivo { get; set; }
        [Required]
        public string NomeArquivo { get; set; }
        [Required]
        public DateTime DataCriacao { get; set; }
    }
}
