using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Travel
{
    public class GetTravelAPIDataRequest
    {
        public int TravelApplicationID { get; set; }
        public int EmpID { get; set; }
        public int RptLevel { get; set; }
        public string Type { get; set; }
    }
}
