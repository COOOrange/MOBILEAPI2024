using Dapper;
using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.Ticket;
using System.Data;

namespace MOBILEAPI2024.DAL.Repositories
{
    public class TicketRepository : SqlDbRepository<ActiveInActiveUser>, ITicketRepository
    {
        public TicketRepository(string connectionString) : base(connectionString)
        {
        }

        public dynamic AddTicketFeedback(AddTicketFeedbackRequest addTicketFeedbackRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Ticket_App_ID", addTicketFeedbackRequest.TicketAppID);
            vParams.Add("@Cmp_ID", addTicketFeedbackRequest.CmpID);
            vParams.Add("@Emp_ID", addTicketFeedbackRequest.EmpID);
            vParams.Add("@Login_ID", addTicketFeedbackRequest.LoginID);
            vParams.Add("@Rating", addTicketFeedbackRequest.Rating);
            vParams.Add("@Suggestion", addTicketFeedbackRequest.Suggestion);
            vParams.Add("@Trantype", "F");
            vParams.Add("@Result", "");
            var response = vconn.Query("SP_Mobile_HRMS_WebService_Ticket_Feedback", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic BindTicketRecords(BindTicketRecords bindTicketRecords)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Ticket_App_ID", bindTicketRecords.TicketAppID);
            vParams.Add("@Ticket_Apr_ID", bindTicketRecords.TicketAprID);
            vParams.Add("@Cmp_ID", bindTicketRecords.CmpID);
            vParams.Add("@Emp_ID", bindTicketRecords.EmpID);
            vParams.Add("@Ticket_Type_ID", bindTicketRecords.TypeID);
            vParams.Add("@Ticket_Dept_ID", bindTicketRecords.DeptID);
            vParams.Add("@Ticket_Priority_ID", bindTicketRecords.PriorityID);
            vParams.Add("@Ticket_Description", bindTicketRecords.Description);
            vParams.Add("@Ticket_Attachment", bindTicketRecords.Attachment);
            vParams.Add("@Ticket_Solution", bindTicketRecords.Solution);
            vParams.Add("@S_Emp_ID", bindTicketRecords.SEmpID);
            vParams.Add("@Ticket_Status", bindTicketRecords.TicketStatus);
            vParams.Add("@Login_ID", bindTicketRecords.LoginID);
            vParams.Add("@FromDate", bindTicketRecords.FromDate);
            vParams.Add("@ToDate", bindTicketRecords.ToDate);
            vParams.Add("@Send_To", bindTicketRecords.TicketSendTo);
            vParams.Add("@Type", bindTicketRecords.StrType);
            vParams.Add("@Result", "");
            var response = vconn.Query("SP_Mobile_HRMS_WebService_Ticket", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic TicketSendTo(int cmpID, int empId, int deptId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpID);
            vParams.Add("@Emp_ID", empId);
            vParams.Add("@Dept_ID", deptId);
            var response = vconn.Query("SP_Mobile_HRMS_WebService_Ticket_Send_To", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }
    }
}
