using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Exit
{
    public class AddExitAppilcationRequest
    {
        public int ExitID { get; set; }
        public int CmpID { get; set; }
        public int EmpID { get; set; }
        public int BranchID { get; set; }
        public int SEmpID { get; set; }
        public string ResigDate { get; set; }
        public string LastDate { get; set; }
        public int ReasonID { get; set; }
        public string Comment { get; set; }
        public string ExitAppDoc { get; set; }
    }
}
