using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Attendance
{
    public class AttendanceInsertRequest
    {
        public int? EmpID { get; set; }
        public int? CmpID { get; set; }
        public string ForDate { get; set; }
        public string IOFlage { get; set; }
        public string Reason { get; set; }
        public string IMEINO { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }
        public string ImageName { get; set; }
        public int VerticalID { get; set; }
        public int SubVerticalID { get; set; }
        public string SubVerticalName { get; set; }
    }
}
