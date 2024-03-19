using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.ResponseDTO
{
    public class FilterLeaveResponse
    {
        public int Cmp_ID { get; set; }
        public int Emp_ID { get; set; }
        public DateTime For_Date { get; set; }
        public int Leave_Opening { get; set; }
        public int Leave_Credit { get; set; }
        public int Leave_Used { get; set; }
        public int Leave_Closing { get; set; }
        public int Leave_ID { get; set; }
        public string Leave_Type { get; set; }
        public string Leave_Name { get; set; }
        public string Emp_Full_Name { get; set; }
        public int Emp_Code { get; set; }
        public string Alpha_Emp_Code { get; set; }
        public string Emp_First_Name { get; set; }
        public string Grd_Name { get; set; }
        public string Comp_Name { get; set; }
        public string Branch_Name { get; set; }
        public string Dept_Name { get; set; }
        public string Desig_Name { get; set; }
        public string Cmp_Name { get; set; }
        public string Cmp_Address { get; set; }
        public DateTime P_From_Date { get; set; }
        public DateTime P_To_Date { get; set; }
        public int Branch_Id { get; set; }
        public string Type_Name { get; set; }
        public int Desig_Dis_No { get; set; }
        public string Vertical_Name { get; set; }
        public string SubVertical_Name { get; set; }
        public string SubBranch_Name { get; set; }
        public string Leave_Code { get; set; }
        public string Gender { get; set; }
    }
}
