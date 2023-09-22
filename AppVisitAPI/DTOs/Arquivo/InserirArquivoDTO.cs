using System.ComponentModel.DataAnnotations;

namespace AppVisitAPI.DTOs.Arquivo
{
    public class InserirArquivoDTO
    {
        [Required]
        public byte[] Arquivo { get; set; }
    }
}
