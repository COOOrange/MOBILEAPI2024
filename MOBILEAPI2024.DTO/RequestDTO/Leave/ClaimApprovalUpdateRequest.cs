using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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

        public decimal CURR_RATE { get; set; }

        public string Approval_Date { get; set; }

        public int APPROVED_PETROL_KM { get; set; }

        public int Max_Limit { get; set; }

        public string CLAIM_ATTACHMENT { get; set; }

        public decimal APPLICATION_AMOUNT { get; set; }

        public int CLAIM_ID { get; set; }

        public string FOR_DATE { get; set; }

        public string DESCRIPTION { get; set; }

        public int CMP_ID { get; set; }

        public int PETROL_KM { get; set; }

        public string CLAIM_NAME { get; set; }

        public string Rpt_Level { get; set; }

        public string Claim_Status { get; set; }

        public int Emp_ID { get; set; }

        public decimal TOTALAMOUNT { get; set; }

        public decimal Claim_Apr_Amnt { get; set; }

        public int CLAIM_APP_DETAIL_ID { get; set; }

        public int ClaimIDSum { get; set; }

        public int CLAIM_APP_ID { get; set; }

        public int CLAIM_ALLOW_BEYOND_LIMIT { get; set; }

    }

}
