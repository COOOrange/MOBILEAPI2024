using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.User
{
    public class GetNotificationRequest
    {
        public int EmpID { get; set; }
        public int CmpID { get; set; }
        public int DeptID { get; set; }
        public int GalleryType { get; set; }
        public string strType { get; set; }
    }
}
