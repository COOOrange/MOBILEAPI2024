using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Attendance
{
    public class AttendanceRegularizeInsertRequest
    {
        public int EmpID { get; set; }
        public int CmpID { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Fordate { get; set; }
        public string Reason { get; set; }
        public string HalfFullDay { get; set; }
        public int CancelLateIn { get; set; }
        public int CancelEarlyOut { get; set; }
        public string Intime { get; set; }
        public string OutTime { get; set; }
        public int IsApprove { get; set; }
        public string OtherReason { get; set; }
        public string IMEINo { get; set; }
    }
}
