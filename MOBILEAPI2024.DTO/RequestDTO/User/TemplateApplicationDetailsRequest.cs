using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.User
{
    public class TemplateApplicationDetailsRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int CmpID { get; set; }
        public int BranchId { get; set; }
        public int CatId { get; set; }
        public int GrdId { get; set; }
        public int DeptId { get; set; }
        public int DesgId { get; set; }
        public int EmpId { get; set; }
        public int TId { get; set; }
    }
}
