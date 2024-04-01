using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Ticket
{
    public class TicketApplicationStatusRequest
    {
        public int CmpID { get; set; }
        public int EmpID { get; set; }
        public int TicketStatus { get; set; }
        public string FromDate { get; set; }
        public string Todate { get; set; }
    }
}
