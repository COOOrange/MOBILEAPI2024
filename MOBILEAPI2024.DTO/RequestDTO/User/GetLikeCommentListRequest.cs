using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.User
{
    public class GetLikeCommentListRequest
    {
        public int EmpID { get; set; }
        public int CmpID { get; set; }
        public string Date { get; set; }
        public string ReminderType { get; set; }
    }
}
