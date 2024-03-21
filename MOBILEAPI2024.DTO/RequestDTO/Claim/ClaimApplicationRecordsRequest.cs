using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Claim
{
    public class ClaimApplicationRecordsRequest
    {
        public int EmpID { get; set; }
        public int CmpID { get; set; }
        public string ClaimStatus { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
