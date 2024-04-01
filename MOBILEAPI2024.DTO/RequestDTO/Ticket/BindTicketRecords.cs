using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Ticket
{
    public class BindTicketRecords
    {
        public int TicketAppID { get; set; }
        public int TicketAprID { get; set; }
        public int CmpID { get; set; }
        public int EmpID { get; set; }
        public int TypeID { get; set; }
        public int DeptID { get; set; }
        public int PriorityID { get; set; }
        public string Description { get; set; }
        public string Attachment { get; set; }
        public string Solution { get; set; }
        public int SEmpID { get; set; }
        public int TicketStatus { get; set; }
        public int LoginID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int TicketSendTo { get; set; }
        public string StrType { get; set; }
    }
}
