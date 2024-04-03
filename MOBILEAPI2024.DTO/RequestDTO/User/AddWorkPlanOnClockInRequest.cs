using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.User
{
    public class AddWorkPlanOnClockInRequest
    {
        public int EmpID { get; set; }
        public int CmpID { get; set; }
        public string WorkPlan { get; set; }
        public string VisitPlan { get; set; }
    }
}
