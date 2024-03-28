using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Travel
{
    public class TravelProofRequest
    {
        public int CmpId { get; set; }
        public int EmpId { get; set; }
        public string StartJourney { get; set; }
        public string ReachDestination { get; set; }
        public string Event { get; set; }
        public string EndJourney { get; set; }
    }
}
