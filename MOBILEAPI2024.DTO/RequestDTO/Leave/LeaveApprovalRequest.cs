using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Leave
{
    public class LeaveApprovalRequest
    {
        public int EmpID { get; set; }
        public int CmpID { get; set; }
        public int LeaveID { get; set; }
        public int LeaveAppID { get; set; }
        public DateTime FromDate { get; set; }
        public decimal Period { get; set; }
        public DateTime ToDate { get; set; }
        public string AssignAs { get; set; }
        public string Comment { get; set; }
        public DateTime HLeaveDate { get; set; }
        public string AppStatus { get; set; }
        public string AppComment { get; set; }
        public int FinalApproval { get; set; }
        public int IsFWDRej { get; set; }
        public int RptLevel { get; set; }
        public int SEmpID { get; set; }
        public DateTime Intime { get; set; }
        public DateTime OutTime { get; set; }
        public int LoginID { get; set; }
        public string Attachment { get; set; }
        public string CompoffLeaveDates { get; set; }
    }
}
