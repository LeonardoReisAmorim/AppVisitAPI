using AppVisitAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace AppVisitAPI.DTOs.LugarDTO
{
    public class LerLugarDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int ArquivoId { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
    }
}
