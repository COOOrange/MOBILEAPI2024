using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Leave
{
    public class ClaimLimitRequest
    {
        public int EmpId { get; set; }
        public int CmpId { get; set; }
        public int ClaimId { get; set; }
        public DateTime ForDate { get; set; }
    }
}
