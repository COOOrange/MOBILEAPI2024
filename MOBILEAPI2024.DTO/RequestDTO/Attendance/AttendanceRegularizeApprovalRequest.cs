using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Attendance
{
    public class AttendanceRegularizeApprovalRequest
    {
        public int ApplicationID { get; set; }
        public int? EmpID { get; set; }
        public int? CmpID { get; set; }
        public string Fordate { get; set; }
        public string Reason { get; set; }
        public string HalfFullDay { get; set; }
        public int CancelLateIn { get; set; }
        public int CancelEarlyOut { get; set; }
        public string Intime { get; set; }
        public string OutTime { get; set; }
        public string Comment { get; set; }
        public int SEmpID { get; set; }
        public int RptLevel { get; set; }
        public int FinalApproval { get; set; }
        public int IsFWDRej { get; set; }
        public string AppStatus { get; set; }
    }
}
