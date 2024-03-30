using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Exit
{
    public class ExitAppInsertRequest
    {
        public int CmpID { get; set; }
        public int EmpID { get; set; }
        public int ExitID { get; set; }
        public List<ExitDetail> ExitDetails { get; set; }
    }
    public class ExitDetail
    {
        public int QUEST_ID { get; set;}
        public string Answer_rate { get; set;}
        public string Comments { get; set; }
    }
}
