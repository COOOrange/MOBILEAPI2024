using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Claim
{
    public class ClaimApplicationRequest
    {
        public int ClaimAppID { get; set; }
        public int ClaimID { get; set; }
        public int EmpID { get; set; }
        public int CmpID { get; set; }
        public int SEmpID { get; set; }
        public DateTime ForDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Description { get; set; }
        public string Attachment { get; set; }
        public int Flag { get; set; }
        public string ClaimDetails { get; set; }
        public string IMEINo { get; set; }
        public int LoginID { get; set; }
        public string Ext { get; set; }
    }
}
