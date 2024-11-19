using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Leave
{
    public class LeaveCancellationApplicationRequest
    {
        public int EmpID { get; set; }
        public int CmpID { get; set; }
        public int LeaveID { get; set; }
        public int LeaveAppID { get; set; }
        public int LeaveApprID { get; set; }
        public List<LeaveDetail> StrDetails { get; set; } // Changed to a list of LeaveDetail objects
        public string CompOffDate { get; set; }
        public int LoginID { get; set; }
        public string IMEINo { get; set; }
    }

    public class LeaveDetail
    {
        public string ForDate { get; set; }
        public string Comment { get; set; }
        public string AssignAs { get; set; }
        public string ActualLeaveDay { get; set; }
        public string Period { get; set; }
    }

}
