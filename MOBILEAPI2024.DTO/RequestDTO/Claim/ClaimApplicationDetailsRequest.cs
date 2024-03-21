using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Claim
{
    public class ClaimApplicationDetailsRequest
    {
        public int EmpID { get; set; }
        public int CmpID { get; set; }
        public int ClaimAppID { get; set; }
        public string ClaimStatus { get; set; }
    }
}
