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

            vParams.Add("@exit_id", addExitAppilcationRequest.ExitID);
            vParams.Add("@emp_id", addExitAppilcationRequest.EmpID);
            vParams.Add("@cmp_id", addExitAppilcationRequest.CmpID);
            vParams.Add("@branch_id", addExitAppilcationRequest.BranchID);
            vParams.Add("@desig_id", 0); // For Desig Id We need to use inline Query 
            vParams.Add("@s_emp_id", addExitAppilcationRequest.SEmpID);
            vParams.Add("@resig_date", Convert.ToDateTime(addExitAppilcationRequest.ResigDate).ToString("dd/MMM/yyyy"));
            vParams.Add("@last_date", Convert.ToDateTime(addExitAppilcationRequest.LastDate).ToString("dd/MMM/yyyy"));
            vParams.Add("@reason", addExitAppilcationRequest.ReasonID);
            vParams.Add("@comments", addExitAppilcationRequest.Comment);
            vParams.Add("@status", "H");
            vParams.Add("@is_rehirable", 0);
            vParams.Add("@feedback", "");
            vParams.Add("@sup_ack", "P");
            vParams.Add("@interview_date", DBNull.Value);
            vParams.Add("@interview_time", DBNull.Value);
            vParams.Add("@Is_Process", "Y");
            vParams.Add("@Email_ForwardTo", DBNull.Value);
            vParams.Add("@DriveData_ForwardTo", DBNull.Value);
            vParams.Add("@left_Id", 0);
            vParams.Add("@new_employer", "");
            vParams.Add("@Is_terminate", 0);
            vParams.Add("@Uniform_return", 0);
            vParams.Add("@Exit_Interview", 0);
            vParams.Add("@Notice_Period", 0);
            vParams.Add("@Is_Death", 0);
            vParams.Add("@tran_type", "I");
            vParams.Add("@Rpt_Mng_ID", 0);
            vParams.Add("@Application_date", Convert.ToDateTime(addExitAppilcationRequest.ResigDate).ToString("dd/MMM/yyyy"));
            vParams.Add("@Exit_App_Doc", "");
            vParams.Add("@Clearance_ManagerID", 0);
            vParams.Add("@Result", "");

            var response = vconn.Query("SP_Mobile_HRMS_WebService_Exit", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic AddExitApprovalData(AddExitApprovaldataRequest addExitApprovaldataRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Tran_id", addExitApprovaldataRequest.Tran_id);
            vParams.Add("@exit_id", addExitApprovaldataRequest.ExitID);
            vParams.Add("@emp_id", addExitApprovaldataRequest.EmpID);
            vParams.Add("@cmp_id", addExitApprovaldataRequest.CmpID);
            vParams.Add("@branch_id", addExitApprovaldataRequest.BranchID);
            vParams.Add("@desig_id", 0);
            vParams.Add("@s_emp_id", addExitApprovaldataRequest.SEmpID);
            vParams.Add("@resig_date", addExitApprovaldataRequest.ResigDate);
            vParams.Add("@last_date", addExitApprovaldataRequest.LastDate);
            vParams.Add("@reason", addExitApprovaldataRequest.ReasonID);
            vParams.Add("@comments", addExitApprovaldataRequest.Comment);
            vParams.Add("@status", addExitApprovaldataRequest.Status);
            vParams.Add("@is_rehirable", addExitApprovaldataRequest.Is_rehirable);
            vParams.Add("@feedback", addExitApprovaldataRequest.Feedback);
            vParams.Add("@sup_ack", addExitApprovaldataRequest.Sup_ack);
            vParams.Add("@interview_date", addExitApprovaldataRequest.Interview_date);
            vParams.Add("@interview_time", addExitApprovaldataRequest.Interview_time);
            vParams.Add("@Is_Process", addExitApprovaldataRequest.Is_Process);
            vParams.Add("@Email_ForwardTo", addExitApprovaldataRequest.Email_ForwardTo);
            vParams.Add("@DriveData_ForwardTo", addExitApprovaldataRequest.Drivedata_Forwardto);
            vParams.Add("@tran_type", addExitApprovaldataRequest.tran_type);
            vParams.Add("@Rpt_Mng_ID", addExitApprovaldataRequest.Rpt_Mng_Id);
            vParams.Add("@Rpt_Level", addExitApprovaldataRequest.Rpt_Level);
            vParams.Add("@Final_Approval", addExitApprovaldataRequest.Final_Approval);
            vParams.Add("@Is_Fwd_Reject", addExitApprovaldataRequest.Is_Fwd_Reject);
            vParams.Add("@Tran_id", addExitApprovaldataRequest.Tran_id);
            vParams.Add("@Application_date", addExitApprovaldataRequest.Application_Date);
            vParams.Add("@Approval_Date", addExitApprovaldataRequest.Approval_Date);
            vParams.Add("@Clearance_ManagerID", addExitApprovaldataRequest.Clearance_ManagerID);
            vParams.Add("@Result", "");
            var response = vconn.Query("SP_Mobile_HRMS_Exit_Approval_Level", vParams, commandType: CommandType.StoredProcedure);
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
            string query;

            query = "Select  Is_PreQuestion From T0040_GENERAL_SETTING WITH (NOLOCK) Where Cmp_ID = @CmpID and Branch_ID = @BranchID and For_Date = (select max(for_date) From T0040_General_Setting WITH (NOLOCK) where Cmp_ID = @CmpID and Branch_ID = @BranchID)";
            vParams.Add("@CmpID", cmpId);
            vParams.Add("@BranchID", branchID);
            var response = vconn.Query<dynamic>(query, vParams); // Pass the parameters object
            return response;
        }

        public dynamic ExitApplicationValidate(int cmpID, int empID)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            string query;

            query = "select emp_id,exit_id from T0200_Emp_ExitApplication WITH (NOLOCK) where cmp_id=@CmpID and emp_id=@EmpID";
            vParams.Add("@CmpID", cmpID);
            vParams.Add("@EmpID", empID);
            var response = vconn.Query<dynamic>(query, vParams); // Pass the parameters object
            return response;
        }

        public dynamic GetExitApplicationRecords(int cmpID, int empID)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            string query;

            query = "SELECT EA.exit_id as Exit_ID,EM.Alpha_Emp_Code,CONVERT(varchar(11),ISNULL(EA.resignation_date,''),103) AS Resignation_Date ,TRIM(EA.sup_ack) as sup_ack ,TRIM(EA.[status]) as [status],ISNULL(EM.emp_full_name,'') as Emp_Full_Name,CONVERT(varchar(11),ISNULL(EA.last_date,''),103) AS last_date FROM T0200_Emp_EXITAPPLICATION EA WITH(NOLOCK) INNER JOIN T0080_EMP_MASTER EM WITH(NOLOCK) ON EM.Emp_ID=EA.emp_id WHERE EA.cmp_id = @CmpID AND EA.emp_id = @EmpID AND (EA.[status]='H' OR EA.[Status]='P' OR EA.[Status] = 'LR') ORDER BY EA.exit_id DESC";
            vParams.Add("@CmpID", cmpID);
            vParams.Add("@EmpID", empID);
            var response = vconn.Query<dynamic>(query, vParams); // Pass the parameters object
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

        public dynamic GetExitTermsandConditions(int cmpId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            string query;

            query = "select Cmp_ID,Exit_Terms_Condition from T0011_Company_Other_Setting with(nolock) Where Cmp_ID= @CmpID";
            vParams.Add("@CmpID", cmpId);
            var response = vconn.Query<dynamic>(query, vParams); // Pass the parameters object
            return response;
        }
    }
}
