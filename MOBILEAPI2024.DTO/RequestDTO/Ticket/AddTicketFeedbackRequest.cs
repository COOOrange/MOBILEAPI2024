using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Ticket
{
    public class AddTicketFeedbackRequest
    {
        public int TicketAppID { get; set; }
        public int CmpID { get; set; }
        public int EmpID { get; set; }
        public int LoginID { get; set; }
        public int Rating { get; set; }
        public string Suggestion { get; set; }
    }
}
