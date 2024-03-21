using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Claim
{
    public class ClaimAppDetailsRequest
    {
        public int ClaimAppID { get; set; }
        public int ClaimAppDetailID { get; set; }
        public int CmpID { get; set; }
        public string ClaimDetails { get; set; }
    }
}
