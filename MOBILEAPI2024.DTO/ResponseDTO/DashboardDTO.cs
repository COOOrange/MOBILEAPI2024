using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.ResponseDTO
{
    public class DashboardDTO
    {
        public EmployeeCount? employeeCount { get; set; }
        public EmployeeData? employeeData { get; set; }

    }
    public class EmployeeCount
    {
        public decimal emp_id { get; set; }
        public decimal Present { get; set; }
        public decimal WO { get; set; }
        public decimal HO { get; set; }
        public decimal OD { get; set; }
        public decimal Absent { get; set; }
        public decimal Leave { get; set; }
        public decimal Total { get; set; }
        public decimal D_Present { get; set; }
    }

    public class EmployeeData
    {
        public int IO_Tran_DetailsID { get; set; }
        public string In_Out_Flag { get; set; }
        public DateTime IO_Datetime { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
    }
}
