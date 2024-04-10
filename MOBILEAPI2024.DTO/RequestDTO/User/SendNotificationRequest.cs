using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.User
{
    public class SendNotificationRequest
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int EmpID { get; set; }
        public int DeptID { get; set; }
        public int CmpID { get; set; }
    }
}
