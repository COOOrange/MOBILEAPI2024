using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Attendance
{
    public class AttendanceRegularizeInsert
    {
        public int IOTranId { get; set; }
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
        public int Is_Approve { get; set; }
        public string Other_Reason { get; set; }
        public string IMEINo { get; set; }
        public int SEmp_ID { get; set; }
        public int RptLevel { get; set; }
        public int FinalApprove { get; set; }
        public int IsFwdLeaveRej { get; set; }
        public string ApprovalStatus { get; set; }
        public string strType { get; set; }
    }
}
