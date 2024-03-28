using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Attendance
{
    public class AllEmployeeAttendanceRequest
    {
        public int? CmpId { get; set; }
        public int? EmpId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
