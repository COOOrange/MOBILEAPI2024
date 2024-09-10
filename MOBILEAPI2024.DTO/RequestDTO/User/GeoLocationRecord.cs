using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.User
{
    public class GeoLocationRecord
    {
        public int EmpId { get; set; }
        public int CmpId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime TrackingDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
    }
}
