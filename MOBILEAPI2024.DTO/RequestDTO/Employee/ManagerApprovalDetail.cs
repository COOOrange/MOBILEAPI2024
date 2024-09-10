using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Employee
{
    public class ManagerApprovalDetail
    {
        public int Cmp_Id1 { get; set; }
        public string Cmp_Name { get; set; }
        public string Cmp_Address { get; set; }
        public int Emp_Id1 { get; set; }
        public int Branch_Id { get; set; }
        public string Alpha_Emp_Code { get; set; }
        public string Emp_Full_Name { get; set; }
        public int Scheme_Id { get; set; }
        public string Leave { get; set; }
        public string Scheme_Type { get; set; }
        public string Scheme_Name { get; set; }
        public DateTime Effective_Date { get; set; }
        public string Rpt_Level { get; set; }
        public string Rpt_Mgr_1 { get; set; }
        public string Rpt_Mgr_2 { get; set; }
        public string Rpt_Mgr_3 { get; set; }
        public string Rpt_Mgr_4 { get; set; }
        public string Rpt_Mgr_5 { get; set; }
        public string Rpt_Mgr_6 { get; set; }
        public string Rpt_Mgr_7 { get; set; }
        public string Rpt_Mgr_8 { get; set; }
        public string Emp_First_Name { get; set; }
        public int Max_Level { get; set; }
    }

    public class ApplicationDetail
    {
        public string Application_Date { get; set; }
        public string Application_Status { get; set; }
        public int Rpt_Level { get; set; }
        public string Application_Date1 { get; set; }
        public string Name { get; set; }
    }

    public class ManagerApprovalData
    {
        public ManagerApprovalDetail? Data { get; set; }
        public List<ApplicationDetail>? Data1 { get; set; }
    }
}
