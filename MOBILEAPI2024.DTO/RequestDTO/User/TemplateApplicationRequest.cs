using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.User
{
    public class TemplateApplicationRequest
    {
        public int CmpID { get; set; }
        public int EmpID { get; set; }
        public int TID { get; set; }
        public List<TemplateDetails> TemplateDetails { get; set; }
        public int LoginID { get; set; }
        public string strType { get; set; }
    }

    public class TemplateDetails
    {
        public int T_ID { get; set; }
        public string Answer { get; set; }
        public string Field_Name { get; set; }
        public int F_ID { get; set; }
        public string Field_Type { get; set; }
    }
}
