using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.User
{
    public class MoodTrackerInsertRequest
    {
        public int CmpID { get; set; }
        public int EmpID { get; set; }
        public int ActivityId { get; set; }
        public string MoodDetail { get; set; }
    }
}
