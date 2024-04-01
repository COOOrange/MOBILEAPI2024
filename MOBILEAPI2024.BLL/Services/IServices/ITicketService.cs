using MOBILEAPI2024.DTO.RequestDTO.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.BLL.Services.IServices
{
    public interface ITicketService
    {
        dynamic AddTicketFeedback(AddTicketFeedbackRequest addTicketFeedbackRequest);
        dynamic BindTicketRecords(int v);
        dynamic TicketApplication(TicketApplicationRequest ticketApplicationRequest);
        dynamic TicketApplicationDelete(int cmpID,int empId, int LoginId,int TicketAppID);
        dynamic TicketApplicationRecords(int v1, int v2, int ticketStatus);
        dynamic TicketApplicationStatus(TicketApplicationStatusRequest ticketApplicationStatusRequest);
        dynamic TicketApproval(TicketApprovalRequest ticketApprovalRequest);
        dynamic TicketAprovalDelete(int v1, int v2, int v3, int ticketAppID);
        dynamic TicketDashboard(int v);
        dynamic TicketSendTo(int v1, int v2, int deptId);
    }
}
