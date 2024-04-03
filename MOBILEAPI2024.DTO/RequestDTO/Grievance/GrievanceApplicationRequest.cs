using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Grievance
{
    public class GrievanceApplicationRequest
    {
        public string AppNo { get; set; }
        public int CmpID { get; set; }
        public int EmpIDF { get; set; }
        public int GrievAgainst { get; set; }
        public int EmpIDT { get; set; }
        public string NameT { get; set; }
        public string AddressT { get; set; }
        public string EmailT { get; set; }
        public string ContactT { get; set; }
        public string SubLine { get; set; }
        public string Details { get; set; }
        public int LoginID { get; set; }
        public string Attachement { get; set; }
        public string DocName { get; set; }
    }
}
