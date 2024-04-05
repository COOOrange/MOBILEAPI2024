using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.User
{
    public class MobileSalseStockResponseRequest
    {
        public int CmpID { get; set; }
        public int EmpID { get; set; }
        public int StoreID { get; set; }
        public string SaleStockDetails { get; set; }
        public int LoginID { get; set; }
        public int RemarkID { get; set; }
        public string ForDate { get; set; }
        public string StrType { get; set; }
    }
}
