using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeVesuviusApi.Models
{
    public class ReservationResponse
    {
        public string Name { get; set; }
        public List<string> DiningTableNumber { get; set; }
        public DateTime Time { get; set; }
    }
}
