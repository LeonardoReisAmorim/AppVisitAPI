﻿using System.ComponentModel.DataAnnotations;

namespace AppVisitAPI.Models
{
    public class Cidade
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public int EstadoId { get; set; }

        public Estado Estado { get; set; }

        public List<Lugar> Lugares { get; set; }
    }
}