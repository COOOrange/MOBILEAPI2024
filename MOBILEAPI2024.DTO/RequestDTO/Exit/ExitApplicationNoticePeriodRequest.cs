using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Exit
{
    public class ExitApplicationNoticePeriodRequest
    {
        public int CmpID { get; set; }
        public int EmpID { get; set; }
        public int BranchID { get; set; }
        public string ResignDate { get; set; }
        public string LeftDate { get; set; }
    }
}
