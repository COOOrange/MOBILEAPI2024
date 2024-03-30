using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Exit
{
    public class ExitApplicationDeleteRequest
    {
        public int CmpID { get; set; }
        public int ExitID { get; set; }
        public string Status { get; set; }
    }
}
