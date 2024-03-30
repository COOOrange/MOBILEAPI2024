using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.CompOff
{
    public class CompOffApprovalRequest
    {
        public int AppID { get; set; }
        public int EmpID { get; set; }
        public int CmpID { get; set; }
        public int SEmpID { get; set; }
        public string EWorkDate { get; set; }
        public string EWorkHours { get; set; }
        public string EWorkReason { get; set; }
        public string SHours { get; set; }
        public string CompoffType { get; set; }
        public string IMEINo { get; set; }
        public int OTType { get; set; }
        public int LoginID { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string AppStatus { get; set; }
        public string AppComment { get; set; }
    }
}
