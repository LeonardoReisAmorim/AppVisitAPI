﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TypePlaceDTO
{
    public class TypePlaceDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public byte[]? Image { get; set; }
        public string ImageRequest { get; set; }
    }
}
