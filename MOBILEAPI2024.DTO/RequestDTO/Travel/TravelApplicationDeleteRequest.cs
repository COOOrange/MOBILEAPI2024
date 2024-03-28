using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Travel
{
    public class TravelApplicationDeleteRequest
    {
        public int Travel_Application_Id { get; set; }
        public int Emp_Id { get; set; }
        public int Cmp_Id { get; set; }
        public int Login_ID { get; set; }
        public char Tran_Type { get; set; }
    }
}
