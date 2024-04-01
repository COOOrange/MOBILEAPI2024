using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DTO.RequestDTO.Ticket;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DAL.Repositories.IRepositories
{
    public interface ITicketRepository : IGenericRepository<ActiveInActiveUser>
    {
        dynamic AddTicketFeedback(AddTicketFeedbackRequest addTicketFeedbackRequest);
        dynamic BindTicketRecords(BindTicketRecords bindTicketRecords);
        dynamic TicketSendTo(int cmpID, int empId, int deptId);
    }
}
