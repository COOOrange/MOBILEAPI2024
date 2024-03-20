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
        public string strDetails { get; set; }
        public string CompOffDate { get; set; }
        public int LoginID { get; set; }
        public string IMEINo { get; set; }
    }
}
