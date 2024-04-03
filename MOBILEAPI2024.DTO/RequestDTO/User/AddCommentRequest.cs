using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.User
{
    public class AddCommentRequest
    {
        public int EmpID { get; set; }
        public int CmpID { get; set; }
        public int NotificationID { get; set; }
        public int NEmpID { get; set; }
        public string Fordate { get; set; }
        public string Comment { get; set; }
        public string ReminderType { get; set; }
    }
}
