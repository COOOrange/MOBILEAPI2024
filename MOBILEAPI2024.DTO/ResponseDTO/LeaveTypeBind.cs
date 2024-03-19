using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.ResponseDTO
{
    public class LeaveTypeBind
    {
        public int Leave_ID { get; set; }
        public string Leave_Name { get; set; }
        public string Leave_Code { get; set; }
        public int Attachment_Days { get; set; }
        public int is_Document_Required { get; set; }
        public string Leave_Type { get; set; }
        public int Apply_Hourly { get; set; }
        public string Multi_Branch_ID { get; set; }
    }
}
