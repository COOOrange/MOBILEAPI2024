using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.ResponseDTO.Leave
{
    public class LeaveCancellationApplicationDetailsResponse
    {
        public List<LeaveCancellationApplicationDetails> LeaveCancellationApplicationDetails {get; set;}
    }
    public class LeaveCancellationApplicationDetails
    {
        public int Leave_Application_ID { get; set; }
        public int Leave_Approval_ID { get; set; }
        public int Tran_ID { get; set; }
        public int Emp_ID { get; set; }
        public int Leave_ID { get; set; }
        public DateTime For_date { get; set; }
        public decimal leave_period_app { get; set; }
        public int Is_Approve { get; set; }
        public string Comment { get; set; }
        public DateTime Request_Date { get; set; }
        public string MComment { get; set; }
        public int A_Emp_ID { get; set; }
        public string Day_Type { get; set; }
        public int Actual_Leave_Day { get; set; }
        public string Leave_Name { get; set; }
        public int Leave_Period { get; set; }
        public DateTime From_Date { get; set; }
        public DateTime To_Date { get; set; }
        public string Leave_Type { get; set; }
        public string CompoffDates { get; set; }
    }
}
