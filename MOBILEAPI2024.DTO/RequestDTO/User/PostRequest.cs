using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.User
{
    public class PostRequest
    {
        public int CmpID { get; set; }
        public int EmpID_LoginID { get; set; }
        public int RequestID { get; set; }
        public string Request_Type { get; set; }
        public string Request_Details { get; set; }
        public string Feedback_Details { get; set; }
        public int Request_Status { get; set; }
        public string LoginID { get; set; }
        public string StrType { get; set; }
    }
}
