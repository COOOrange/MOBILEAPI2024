using Dapper;
using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.Claim;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using System.Data;
using System.Xml.Linq;

namespace MOBILEAPI2024.DAL.Repositories
{
    public class ClaimRepository : SqlDbRepository<ActiveInActiveUser>, IClaimRepository
    {
        public ClaimRepository(string connectionString) : base(connectionString)
        {
        }

        public dynamic ClaimAdminSetting(int cmpId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
           
            vParams.Add("@Cmp_ID", cmpId);
            var response = vconn.Query("SP_Mobile_HRMS_ADMIN_SETTINGS", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic ClaimApprovalRecords(ClaimRecords claimRecords)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();

            vParams.Add("@Claim_App_ID", claimRecords.ClaimAppID);
            vParams.Add("@Claim_App_Detail_ID", claimRecords.ClaimAppDetailsID);
            vParams.Add("@Emp_ID", claimRecords.EmpID);
            vParams.Add("@Cmp_ID", claimRecords.CmpID);
            vParams.Add("@Claim_ID", claimRecords.ClaimID);
            vParams.Add("@SEmp_ID", claimRecords.SEmpID);
            vParams.Add("@ForDate", Convert.ToDateTime(claimRecords.ForDate));
            vParams.Add("@ToDate", Convert.ToDateTime(claimRecords.ToDate));
            vParams.Add("@Claim_App_Amount", claimRecords.AppAmount);
            vParams.Add("@Claim_Description", claimRecords.Description);
            vParams.Add("@Claim_Attachment", claimRecords.Attachment);
            vParams.Add("@Claim_App_Status", claimRecords.AppStatus);
            vParams.Add("@Flag", claimRecords.Flag);
            vParams.Add("@Claim_Details", claimRecords.ClaimDetails);
            vParams.Add("@Rpt_Level", claimRecords.RptLevel);
            vParams.Add("@Final_Approve", claimRecords.FinalApproval);
            vParams.Add("@Is_Fwd_Leave_Rej", claimRecords.IsFwdRej);
            vParams.Add("@IMEINO", claimRecords.IMEINo);
            vParams.Add("@Login_ID", claimRecords.LoginID);
            vParams.Add("@Type", claimRecords.Type);
            vParams.Add("@Result", "");

            var response = vconn.Query("SP_Mobile_HRMS_WebService_Claim_Approval", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public object ClaimApprovalRecordsFinalUpdateInsert(ClaimApprovalUpdateRequest claimApprovalUpdateRequest, XDocument claimDetails)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Claim_Apr_ID", 0);
            vParams.Add("@Claim_App_ID", claimApprovalUpdateRequest.ClaimAppID);
            vParams.Add("@Cmp_ID", claimApprovalUpdateRequest.CmpID);
            vParams.Add("@Emp_ID", claimApprovalUpdateRequest.EmpID);
            vParams.Add("@S_Emp_ID", claimApprovalUpdateRequest.SEmpID);
            vParams.Add("@Approval_Date", Convert.ToDateTime(claimApprovalUpdateRequest.ClaimApprovalDate));
            vParams.Add("@Claim_App_Date", Convert.ToDateTime(claimApprovalUpdateRequest.ClaimAppDate));
            vParams.Add("@Claim_App_Status", claimApprovalUpdateRequest.ClaimStatus);
            vParams.Add("@Claim_Apr_Comments", claimApprovalUpdateRequest.Comment);
            vParams.Add("@Login_ID", claimApprovalUpdateRequest.LoginID);
            vParams.Add("@Claim_Details", claimDetails);
            vParams.Add("@Tran_Type", "I");
            vParams.Add("@Result", "");
            
            var response = vconn.Query("SP_Mobile_HRMS_WebService_Claim_Approval_Final_Update", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic ClaimApprovalRecordsUpdateInsert(ClaimApprovalUpdateRequest claimApprovalUpdateRequest, XDocument claimDetails)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();

            vParams.Add("@Tran_ID", 0);
            vParams.Add("@Claim_App_ID", claimApprovalUpdateRequest.ClaimAppID);
            vParams.Add("@Cmp_ID", claimApprovalUpdateRequest.CmpID);
            vParams.Add("@Emp_ID", claimApprovalUpdateRequest.EmpID);
            vParams.Add("@S_Emp_ID", claimApprovalUpdateRequest.SEmpID);
            vParams.Add("@Approval_Date", Convert.ToDateTime(claimApprovalUpdateRequest.ClaimApprovalDate));
            vParams.Add("@Approval_Status", claimApprovalUpdateRequest.ClaimStatus);
            vParams.Add("@Approval_Comments", claimApprovalUpdateRequest.Comment);
            vParams.Add("@Login_ID", claimApprovalUpdateRequest.LoginID);
            vParams.Add("@Rpt_Level", claimApprovalUpdateRequest.RptLevel);
            vParams.Add("@Claim_Details", claimDetails);
            vParams.Add("@Tran_Type", "I");
            vParams.Add("@Result", "");

            var response = vconn.Query("SP_Mobile_HRMS_WebService_Claim_Approval_Update", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic ClaimRecords(ClaimRecords claimRecords)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();

            vParams.Add("@Claim_App_ID", claimRecords.ClaimAppID);
            vParams.Add("@Claim_App_Detail_ID", claimRecords.ClaimAppDetailsID);
            vParams.Add("@Emp_ID", claimRecords.EmpID);
            vParams.Add("@Cmp_ID", claimRecords.CmpID);
            vParams.Add("@Claim_ID", claimRecords.ClaimID);
            vParams.Add("@SEmp_ID", claimRecords.SEmpID);
            vParams.Add("@ForDate", Convert.ToDateTime(claimRecords.ForDate));
            vParams.Add("@ToDate", Convert.ToDateTime(claimRecords.ToDate));
            vParams.Add("@Claim_App_Amount", claimRecords.AppAmount);
            vParams.Add("@Claim_Description", claimRecords.Description);
            vParams.Add("@Claim_Attachment", claimRecords.Attachment);
            vParams.Add("@Claim_App_Status", claimRecords.AppStatus);
            vParams.Add("@Flag", claimRecords.Flag);
            vParams.Add("@Claim_Details", claimRecords.ClaimDetails);
            vParams.Add("@Rpt_Level", claimRecords.RptLevel);
            vParams.Add("@Final_Approve", claimRecords.FinalApproval);
            vParams.Add("@Is_Fwd_Leave_Rej", claimRecords.IsFwdRej);
            vParams.Add("@IMEINO", claimRecords.IMEINo);
            vParams.Add("@Login_ID", claimRecords.LoginID);
            vParams.Add("@Type", claimRecords.Type);
            vParams.Add("@Result", "");

            var response = vconn.Query("SP_Mobile_HRMS_WebService_Claim", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }
    }
}
