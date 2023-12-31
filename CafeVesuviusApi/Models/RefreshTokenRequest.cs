﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CafeVesuviusApi.Models
{
    public class RefreshTokenRequest
    {
        [Required]
        public string ExpiredToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
