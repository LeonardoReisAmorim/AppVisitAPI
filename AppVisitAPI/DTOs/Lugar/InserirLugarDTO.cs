using System.ComponentModel.DataAnnotations;

namespace AppVisitAPI.DTOs.Lugar
{
    public class InserirLugarDTO
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public int ArquivoId { get; set; }
        
        [Required]
        public int CidadeId { get; set; }

        [Required]
        //Base64
        public string Imagem { get; set; }
    }
}
