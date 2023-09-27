using System.ComponentModel.DataAnnotations;

namespace AppVisitAPI.DTOs.ArquivoDTO
{
    public class InserirArquivoDTO
    {
        [Required]
        public byte[] Arquivo { get; set; }
    }
}
