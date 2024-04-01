using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Exit
{
    public class GetExitApprovalEMPDataRequest
    {
        public int Cmp_Id { get; set; }
        public int Emp_Id { get; set; }
        public int Branch_Id { get; set; }
        public int Exit_Id { get; set; }
        public DateTime Resign_Date { get; set; }
        public DateTime Left_Date { get; set; }
    }
}
