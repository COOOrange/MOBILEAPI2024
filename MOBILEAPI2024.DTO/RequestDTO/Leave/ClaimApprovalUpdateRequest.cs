using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Leave
{
    public class ClaimApprovalUpdateRequest
    {
        public int ClaimAppID { get; set; }
        public int CmpID { get; set; }
        public int EmpID { get; set; }
        public int SEmpID { get; set; }
        public DateTime ClaimApprovalDate { get; set; }
        public string ClaimStatus { get; set; }
        public string Comment { get; set; }
        public int LoginID { get; set; }
        public int RptLevel { get; set; }
        public List<ClaimDetails> ClaimDetails { get; set; }
        public int FinalApproval { get; set; }
        public DateTime ClaimAppDate { get; set; }
    }

    public class ClaimDetails 
    {

        public int EmpId { get; set; }
        public int CmpId { get; set; }
        public int LoginId { get; set; }
    }

}
