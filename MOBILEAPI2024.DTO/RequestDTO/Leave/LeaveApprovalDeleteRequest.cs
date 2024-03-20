using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Leave
{
    public class LeaveApprovalDeleteRequest
    {
        public int EmpID { get; set; }
        public int CmpID { get; set; }
        public int LeaveAppID { get; set; }
        public DateTime Approval_Date { get; set; }
        public string Approval_Status { get; set; }
        public int Is_Backdated_App { get; set; }
        public int SEmpID { get; set; }
        public string User_Id { get; set; }
        public int LoginID { get; set; }
        public string Tran_Type { get; set; }
    }
}
