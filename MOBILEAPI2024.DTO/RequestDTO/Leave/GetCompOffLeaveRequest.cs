using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Leave
{
    public class GetCompOffLeaveRequest
    {
        public int EmpId { get; set; }
        public int CmpId { get; set; }
        public int LeaveId { get; set; }
        public string ForDate{ get; set; }
    }
}
