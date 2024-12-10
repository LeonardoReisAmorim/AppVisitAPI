using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TypePlace
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [JsonIgnore]
        public virtual Place Place { get; set; }
    }
}
