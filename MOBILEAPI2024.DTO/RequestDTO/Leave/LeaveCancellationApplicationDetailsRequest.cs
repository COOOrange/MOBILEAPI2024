using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Leave
{
    public class LeaveCancellationApplicationDetailsRequest
    {
        public int EmpID { get; set; }
        public int CmpID { get; set; }
        public int LeaveApprID { get; set; }
        public int LeaveID { get; set; }
    }
}
