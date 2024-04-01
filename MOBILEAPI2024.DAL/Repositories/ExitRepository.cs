using Dapper;
using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.Exit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MOBILEAPI2024.DAL.Repositories
{
    public class ExitRepository : SqlDbRepository<ActiveInActiveUser>, IExitRepository
    {
        public ExitRepository(string connectionString) : base(connectionString)
        {
        }

        public dynamic AddExitAppilcation(AddExitAppilcationRequest addExitAppilcationRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();

            //vParams.Add("@exit_id", addExitAppilcationRequest.ExitID);
            //vParams.Add("@emp_id", addExitAppilcationRequest.EmpID);
            //vParams.Add("@cmp_id", addExitAppilcationRequest.CmpID);
            //vParams.Add("@branch_id", addExitAppilcationRequest.BranchID);
            //vParams.Add("@desig_id", addExitAppilcationRequest.Desig_ID); // For Desig Id We need to use inline Query 
            //vParams.Add("@s_emp_id", addExitAppilcationRequest.SEmpID);
            //vParams.Add("@resig_date", Convert.ToDateTime(addExitAppilcationRequest.ResigDate).ToString("dd/MMM/yyyy"));
            //vParams.Add("@last_date", Convert.ToDateTime(addExitAppilcationRequest.LastDate).ToString("dd/MMM/yyyy"));
            //vParams.Add("@reason", addExitAppilcationRequest.ReasonID);
            //vParams.Add("@comments", addExitAppilcationRequest.Comment);
            //vParams.Add("@status", "H");
            //vParams.Add("@is_rehirable", 0);
            //vParams.Add("@feedback", "");
            //vParams.Add("@sup_ack", "P");
            //vParams.Add("@interview_date", DBNull.Value);
            //vParams.Add("@interview_time", DBNull.Value);
            //vParams.Add("@Is_Process", "Y");
            //vParams.Add("@Email_ForwardTo", DBNull.Value);
            //vParams.Add("@DriveData_ForwardTo", DBNull.Value);
            //vParams.Add("@left_Id", 0);
            //vParams.Add("@new_employer", "");
            //vParams.Add("@Is_terminate", 0);
            //vParams.Add("@Uniform_return", 0);
            //vParams.Add("@Exit_Interview", 0);
            //vParams.Add("@Notice_Period", 0);
            //vParams.Add("@Is_Death", 0);
            //vParams.Add("@tran_type", "I");
            //vParams.Add("@Rpt_Mng_ID", 0);
            //vParams.Add("@Application_date", Convert.ToDateTime(addExitAppilcationRequest.ResigDate).ToString("dd/MMM/yyyy"));
            //vParams.Add("@Exit_App_Doc", addExitAppilcationRequest.AttachedDocFile);
            //vParams.Add("@Clearance_ManagerID", 0);
            //vParams.Add("@Result", "", ParameterDirection.Output);

            var response = vconn.Query("SP_Mobile_HRMS_WebService_Exit", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic ExitAppInsert(ExitAppInsertRequest exitAppInsertRequest, int qUEST_ID, int answer_rate, string comments)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", exitAppInsertRequest.CmpID);
            vParams.Add("@Emp_ID", exitAppInsertRequest.EmpID);
            vParams.Add("@EXIT_ID", exitAppInsertRequest.ExitID);
            vParams.Add("@QUEST_ID", qUEST_ID);
            vParams.Add("@Answer_rate", answer_rate);
            vParams.Add("@Comments", comments);
            var response = vconn.Query("P0200_EMP_EXIT_APPLICATION_INSERT", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic ExitApplicationDelete(ExitApplicationDeleteRequest exitApplicationDeleteRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();

            vParams.Add("@Cmp_ID", exitApplicationDeleteRequest.CmpID);
            vParams.Add("@Exit_ID", exitApplicationDeleteRequest.ExitID);
            vParams.Add("@Status", exitApplicationDeleteRequest.Status);
            vParams.Add("@Result", "");

            var response = vconn.Query("SP_Mobile_HRMS_WebService_ExitDelete", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic ExitApplicationNoticePeriod(ExitApplicationNoticePeriodRequest exitApplicationNoticePeriodRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@cmp_Id", exitApplicationNoticePeriodRequest.CmpID);
            vParams.Add("@emp_id", exitApplicationNoticePeriodRequest.EmpID);
            vParams.Add("@branch_Id", exitApplicationNoticePeriodRequest.BranchID);
            vParams.Add("@Resign_Date", exitApplicationNoticePeriodRequest.ResignDate);
            vParams.Add("@Left_Date", exitApplicationNoticePeriodRequest.LeftDate);
            var response = vconn.Query("P0200_GetShortFall", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic ExitApplicationPreQuestion(int cmpId, int branchID)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            var response = vconn.Query("P0200_GetShortFall", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic GetExitApporvalRecords(int cmpID, int empID, string status)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@cmp_Id", cmpID);
            vParams.Add("@emp_id", empID);
            vParams.Add("@status", status);
            var response = vconn.Query("SP_Mobile_HRMS_Exit_Approval", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic GetExitApprovalEMPData(GetExitApprovalEMPDataRequest getExitApprovalEMPDataRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@cmp_id", getExitApprovalEMPDataRequest.Cmp_Id);
            vParams.Add("@emp_id", getExitApprovalEMPDataRequest.Emp_Id);
            vParams.Add("@branch_Id", getExitApprovalEMPDataRequest.Branch_Id);
            vParams.Add("@Exit_Id", getExitApprovalEMPDataRequest.Exit_Id);
            vParams.Add("@Rpt_level", 0);
            vParams.Add("@Resign_Date", getExitApprovalEMPDataRequest.Resign_Date);
            vParams.Add("@Left_Date", getExitApprovalEMPDataRequest.Left_Date);
            var response = vconn.Query("Mobile_HRMS_GetShortFall", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic GetExitInterviewQAInterview(int cmpId, int empId, int exitId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Emp_ID", empId);
            vParams.Add("@EXIT_ID", exitId);
            var response = vconn.Query("GET_INTERVIEW_QUESTION_ASSIGNED", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }
    }
}
