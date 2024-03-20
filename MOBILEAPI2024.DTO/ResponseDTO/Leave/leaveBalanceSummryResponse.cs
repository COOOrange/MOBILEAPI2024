using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.ResponseDTO.Leave
{
    public class LeaveBalanceSummryResponse
    {
        public List<LeaveBalanceSummry> leaveBalanceSummries { get; set; }
    }
    public class LeaveBalanceSummry
    {
        public int Leave_Opening { get; set; }
        public int Leave_Used { get; set; }
        public int leave_credit { get; set; }
        public int LEAVE_CLOSING { get; set; }
        public string Leave_Code { get; set; }
        public string LEAVE_NAME { get; set; }
        public int LEAVE_ID { get; set; }
        public int Display_leave_balance { get; set; }
        public int Actual_Leave_Closing { get; set; }
        public string Leave_Type { get; set; }
    }
}
