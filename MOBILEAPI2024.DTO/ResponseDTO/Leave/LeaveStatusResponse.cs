using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.ResponseDTO.Leave
{
    public class LeaveStatusResponse
    {
        public int Leave_Approval_ID { get; set; }
        public int Leave_Application_ID { get; set; }
        public string Application_Status { get; set; }
        public string AppStatus { get; set; }
        public int Cmp_ID { get; set; }
        public int Leave_ID { get; set; }
        public string From_Date { get; set; }
        public string To_Date { get; set; }
        public string Application_Date { get; set; }
        public int LeaveAppDays { get; set; }
        public int LeaveApprDays { get; set; }
        public string Leave_Code { get; set; }
        public string Leave_Assign_As { get; set; }
        public string Leave_Reason { get; set; }
        public string Leave_Name { get; set; }
        public string Senior_Employee { get; set; }
        public string Emp_Full_Name { get; set; }
        public int Emp_Superior { get; set; }
        public string Half_Leave_Date { get; set; }
        public string Application_Comments { get; set; }
        public string Approval_Comments { get; set; }
        public string Leave_CompOff_Dates { get; set; }
        public int LeaveCancelCount { get; set; }
    }
}
