using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AppVisitAPI.Models
{
    public class Arquivo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public byte[] File { get; set; }

        [JsonIgnore]
        public virtual Lugar Lugar { get; set; }

    }
}
