using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Leave
{
    public class LeaveCancellationApprovalRequest
    {
        public int EmpID { get; set; }
        public int CmpID { get; set; }
        public int LeaveID { get; set; }
        public int LeaveAppID { get; set; }
        public int LeaveApprID { get; set; }
        public string strDetails { get; set; }
        public int LoginID { get; set; }
        public int SEmpID { get; set; }
        public DateTime CompOffDate { get; set; }
        public int IMEINo { get; set; }
    }
}
