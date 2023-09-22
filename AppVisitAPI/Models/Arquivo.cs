using System.ComponentModel.DataAnnotations;

namespace AppVisitAPI.Models
{
    public class Arquivo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public byte[] File { get; set; }

        public Lugar Lugar { get; set; }

    }
}
