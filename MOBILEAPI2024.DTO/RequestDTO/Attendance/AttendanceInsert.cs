using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Attendance
{
    public class AttendanceInsert
    {
        public int EmpID { get; set; }
        public int CmpID { get; set; }
        public int VerticalID { get; set; }
        public int SubVerticalID { get; set; }
        public string FromDate { get; set; }
        public string Todate { get; set; }
        public string IOFlage { get; set; }
        public string Reason { get; set; }
        public string IMEINO { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }
        public string ImageName { get; set; }
        public string strAttendance { get; set; }
        public string subVerticalName { get; set; }
        public string Type { get; set; }
    }
}
