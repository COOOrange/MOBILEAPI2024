using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.User
{
    public class SurveyApplicationRequest
    {
        public int CmpID { get; set; }
        public int EmpID { get; set; }
        public int SurveyID { get; set; }
        public string SurveyDetails { get; set; }
        public int LoginID { get; set; }
        public string IMEINo { get; set; }
        public string strType { get; set; }
    }
}
