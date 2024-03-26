using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Employee
{
    public class EmployeeListRequest
    {
        public int EmpId { get; set; }
        public int CmpId { get; set; }
        public int BranchID { get; set; }
        public int DepartmentID { get; set; }
    }
}
