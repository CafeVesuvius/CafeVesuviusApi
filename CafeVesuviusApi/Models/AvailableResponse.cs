using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeVesuviusApi.Models
{
    public class AvailableResponse
    {
        public bool IsAvailable { get; set; }
        public string Reason { get; set; }
    }
}
