using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CafeVesuviusApi.Models
{
    public class ReservationRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public byte People { get; set; }
        [Required]
        public DateTime Time { get; set; }
    }
}
