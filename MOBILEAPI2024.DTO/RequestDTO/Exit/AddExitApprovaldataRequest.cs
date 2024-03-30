using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Exit
{
    public class AddExitApprovaldataRequest
    {
        public int Tran_id { get; set; }
        public int ExitID { get; set; }
        public int CmpID { get; set; }
        public int EmpID { get; set; }
        public int BranchID { get; set; }
        public int SEmpID { get; set; }
        public DateTime ResigDate { get; set; }
        public DateTime LastDate { get; set; }
        public int ReasonID { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public int Is_rehirable { get; set; }
        public string Feedback { get; set; }
        public string Sup_ack { get; set; }
        public DateTime Interview_date { get; set; }
        public string Interview_time { get; set; }
        public char Is_Process { get; set; }
        public string Email_ForwardTo { get; set; }
        public string Drivedata_Forwardto { get; set; }
        public string tran_type { get; set; }
        public int Rpt_Mng_Id { get; set; }
        public int Rpt_Level { get; set; }
        public int Final_Approval { get; set; }
        public int Is_Fwd_Reject { get; set; }
        public DateTime Application_Date { get; set; }
        public DateTime Approval_Date { get; set; }
        public string Clearance_ManagerID { get; set; }
    }
}
