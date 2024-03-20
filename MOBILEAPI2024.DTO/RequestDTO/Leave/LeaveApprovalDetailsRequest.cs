using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Leave
{
    public class LeaveApprovalDetailsRequest
    {
        public int CmpId { get; set; }
        public int LeaveId { get; set; }
        public int LoginId { get; set; }
        public int LeaveAppId { get; set; }
        public int LeaveApprId { get; set; }
        public int EmpId { get; set; }
    }
}
