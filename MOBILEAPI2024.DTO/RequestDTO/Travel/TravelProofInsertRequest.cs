using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Travel
{
    public class TravelProofInsertRequest
    {
        public int CmpId { get; set; }
        public int EmpId { get; set; }
        public int TravelApplicationCode { get; set; }
        public string AttachmentName { get; set; }
        public string Attachment { get; set; }
        public int TravelProofType { get; set; }
    }
}
