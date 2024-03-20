using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.ResponseDTO.Leave
{
    public class LeaveApprovalDetailsResponse
    {
        public decimal Leave_Application_ID { get; set; }
        public decimal Leave_Approval_ID { get; set; }
        public DateTime From_Date { get; set; }
        public DateTime To_Date { get; set; }
        public decimal Leave_Period { get; set; }
        public string Leave_Name { get; set; }
        public string Leave_Assign_As { get; set; }
        public string Approval_Comments { get; set; }
        public DateTime For_Date { get; set; }
        public decimal Leave_Used { get; set; }
        public decimal Tran_ID { get; set; }
        public byte Is_Approve { get; set; }
        public decimal Actual_Leave_Day { get; set; }
        public decimal LEAVE_CANCEL_PERIOD { get; set; }
        public decimal REMAIN_LEAVE_PERIOD { get; set; }
        public string REMAIN_DAY { get; set; }
        public string Day_Type { get; set; }
        public string Comment { get; set; }
        public string MComment { get; set; }
        public string APPSTATUS { get; set; }
    }
}
