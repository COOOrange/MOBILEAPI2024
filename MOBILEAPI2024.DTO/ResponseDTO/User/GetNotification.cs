using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.ResponseDTO.User
{
    public class GetNotification
    {
        public int EmpID { get; set; }
        public int CmpID { get; set; }
        public int DeptID { get; set; }
        public int GalleryType { get; set; }
        public string strType { get; set; }
        public int Year { get; set; }
        public int NotificationID { get; set; }
        public int NEmpID { get; set; }
        public string Fordate { get; set; }
        public int UCommentID { get; set; }
        public DateTime NotificationDate { get; set; }
        public int Flag { get; set; }
        public string Comment { get; set; }
        public string CommentStatus { get; set; }
        public int RCommentID { get; set; }
        public string ReminderType { get; set; }
    }
}
