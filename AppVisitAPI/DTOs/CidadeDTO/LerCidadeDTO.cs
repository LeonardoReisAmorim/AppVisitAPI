using AppVisitAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace AppVisitAPI.DTOs.CidadeDTO
{
    public class LerCidadeDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public virtual List<Lugar> Lugares { get; set; }
    }
}
