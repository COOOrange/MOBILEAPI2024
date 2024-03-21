using Azure;
using Dapper;
using Microsoft.SqlServer.Server;
using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.ResponseDTO.Leave;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DAL.Repositories
{
    public class LeaveRepository : SqlDbRepository<ActiveInActiveUser>, ILeaveRepository
    {
        public LeaveRepository(string connectionString) : base(connectionString)
        {
        }

        public string AddLeaveAplication(LeaveFilter leaveFilter, ApplyLeaveRequest applyLeaveRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Leave_Application_ID", Convert.ToInt32("0"));
            vParams.Add("@Emp_ID", leaveFilter.Emp_Id);
            vParams.Add("@Cmp_ID", leaveFilter.Cmp_Id);
            vParams.Add("@Leave_ID", applyLeaveRequest.Leave_ID);
            vParams.Add("@From_Date", applyLeaveRequest.FromDate);
            vParams.Add("@To_Date", applyLeaveRequest.ToDate);
            vParams.Add("@Period", applyLeaveRequest.Period);
            vParams.Add("@Leave_Assign_As", applyLeaveRequest.Leave_Assign_As);
            vParams.Add("@Comment", applyLeaveRequest.Comment);
            vParams.Add("@Half_Leave_Date", "");
            vParams.Add("@InTime", "");
            vParams.Add("@OutTime", "");
            vParams.Add("@Login_ID", leaveFilter.Login_ID);
            vParams.Add("@strLeaveCompOff_Dates", "");
            vParams.Add("@Attachment", "");
            vParams.Add("@Type", "I");
            vParams.Add("@Result", "");

            var data = vconn.QueryFirst("SP_Mobile_HRMS_WebAPI_Leave", vParams, commandType: CommandType.StoredProcedure);
            return data.Result;
        }

        public LeaveApplicationResponse CheckLeaveStatus(LeaveFilter leaveFilter, int leaveAppID)
        {
            LeaveApplicationResponse leaveApplicationResponse = new LeaveApplicationResponse();
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();

            vParams.Add("@Leave_Approval_ID", Convert.ToInt32("0"));
            vParams.Add("@Leave_Application_ID", leaveAppID);
            vParams.Add("@Leave_ID", Convert.ToInt32("0"));
            vParams.Add("@Emp_ID", leaveFilter.Emp_Id);
            vParams.Add("@Cmp_ID", leaveFilter.Cmp_Id);
            vParams.Add("@Approval_Date", DateTime.Now.ToString("dd/MMM/yyyy"));
            vParams.Add("@From_Date", "");
            vParams.Add("@To_Date", "");
            vParams.Add("@Leave_Period", Convert.ToDecimal("0.0"));
            vParams.Add("@Leave_AssignAs", "");
            vParams.Add("@Leave_Reason", "");
            vParams.Add("@Half_Leave_Date", "");
            vParams.Add("@Approval_Status", "");
            vParams.Add("@Approval_Comments", "");
            vParams.Add("@Final_Approve", Convert.ToInt32("0"));
            vParams.Add("@Is_Fwd_Leave_Rej", Convert.ToInt32("0"));
            vParams.Add("@Rpt_Level", Convert.ToInt32("0"));
            vParams.Add("@SEmp_ID", Convert.ToInt32("0"));
            vParams.Add("@InTime", "");
            vParams.Add("@OutTime", "");
            vParams.Add("@Login_ID", Convert.ToInt32("0"));
            vParams.Add("@strLeaveCompOff_Dates", "");
            vParams.Add("@Status", "");
            vParams.Add("@Type", "E");
            vParams.Add("@Result", "");

            var response = vconn.QueryMultiple("SP_Mobile_HRMS_WebAPI_Leave_Approve", vParams, commandType: CommandType.StoredProcedure);
            var leaveApplicationData = response.Read().FirstOrDefault();
            var leaveApplication = response.Read().FirstOrDefault();
            LeaveApplicationData LeaveApp = new LeaveApplicationData();

            LeaveApp.EmployeeFullName = leaveApplicationData.EmployeeFullName;
            LeaveApp.EmployeeName = leaveApplicationData.EmployeeName;
            LeaveApp.Emp_ID = leaveApplicationData.Emp_ID;
            LeaveApp.Cmp_ID = leaveApplicationData.Cmp_ID;
            LeaveApp.S_Emp_ID = leaveApplicationData.S_Emp_ID;
            LeaveApp.Leave_Application_ID = leaveApplicationData.Leave_Application_ID;
            LeaveApp.Application_Date = leaveApplicationData.Application_Date;
            LeaveApp.Application_Code = leaveApplicationData.Application_Code;
            LeaveApp.Application_Status = leaveApplicationData.Application_Status;
            LeaveApp.Application_Comments = leaveApplicationData.Application_Comments;
            LeaveApp.Leave_ID = leaveApplicationData.Leave_ID;
            LeaveApp.Leave_Name = leaveApplicationData.Leave_Name;
            LeaveApp.Leave_Code = leaveApplicationData.Leave_Code;
            LeaveApp.FromDate = leaveApplicationData.FromDate;
            LeaveApp.ToDate = leaveApplicationData.ToDate;
            LeaveApp.Leave_Period = leaveApplicationData.Leave_Period;
            LeaveApp.Leave_Assign_As = leaveApplicationData.Leave_Assign_As;
            LeaveApp.Half_Leave_Date = leaveApplicationData.Half_Leave_Date;
            LeaveApp.Leave_Reason = leaveApplicationData.Leave_Reason;
            LeaveApp.NightHalt = leaveApplicationData.NightHalt;
            LeaveApp.leave_Out_time = leaveApplicationData.leave_Out_time;
            LeaveApp.leave_In_time = leaveApplicationData.leave_In_time;
            LeaveApp.Image_Name = leaveApplicationData.Image_Name;
            LeaveApp.Image_Path = leaveApplicationData.Image_Path;
            LeaveApp.Leave_CompOff_Dates = leaveApplicationData.Leave_CompOff_Dates;
            LeaveApp.Leave_Type = leaveApplicationData.Leave_Type;

            LeaveApplication leave = new LeaveApplication();
            leave.From_Date = leaveApplication.From_Date;
            leave.To_Date = leaveApplication.To_Date;
            leave.Leave_Period = leaveApplication.Leave_Period;
            leave.Comment = leaveApplication.Comment;
            leave.Application_Status = leaveApplication.Application_Status;
            leave.System_Date = leaveApplication.System_Date;
            leave.Rpt_Level = leaveApplication.Rpt_Level;

            leaveApplicationResponse.leaveApplicationData = LeaveApp;
            leaveApplicationResponse.leaveApplication = leave;

            return leaveApplicationResponse;

        }

        public string CheckPeriod(LeaveFilter leaveFilter, CheckPeriod checkPeriod)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Leave_Application_ID", Convert.ToInt32("0"));
            vParams.Add("@Emp_ID", leaveFilter.Emp_Id);
            vParams.Add("@Cmp_ID", leaveFilter.Cmp_Id);
            vParams.Add("@Leave_ID", checkPeriod.Leave_ID);
            vParams.Add("@From_Date", leaveFilter.FromDate);
            vParams.Add("@To_Date", "");
            vParams.Add("@Period", checkPeriod.Period);
            vParams.Add("@Leave_Assign_As", "");
            vParams.Add("@Comment", "");
            vParams.Add("@Half_Leave_Date", DateTime.Now);
            vParams.Add("@InTime", DateTime.Now);
            vParams.Add("@OutTime", DateTime.Now);
            vParams.Add("@Login_ID", Convert.ToInt32("0"));
            vParams.Add("@strLeaveCompOff_Dates", "");
            vParams.Add("@Attachment", "");
            vParams.Add("@Type", "V");
            vParams.Add("@Result", "");

            var dateTime = vconn.QueryFirst("SP_Mobile_HRMS_WebAPI_Leave", vParams, commandType: CommandType.StoredProcedure);
            return dateTime.Result;
        }

        public dynamic GetCompOffLeave(GetCompOffLeaveRequest getCompOffLeaveRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@For_Date", Convert.ToDateTime(getCompOffLeaveRequest.ForDate));
            vParams.Add("@Cmp_ID", getCompOffLeaveRequest.CmpId);
            vParams.Add("@Emp_ID", getCompOffLeaveRequest.EmpId);
            vParams.Add("@leave_ID", getCompOffLeaveRequest.LeaveId);
            vParams.Add("@Leave_Application_ID", 0);
            vParams.Add("@Leave_Encash_App_ID", 0);
            vParams.Add("@Exec_For", 0);
            vParams.Add("@Leave_Period", 0);
            var response = vconn.Query("GET_COMPOFF_DETAILS", vParams, commandType: CommandType.StoredProcedure).ToList();
            return response;
        }

        public List<FilterLeaveResponse> GetLeaveBalance(LeaveFilter leaveFilter)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Leave_Application_ID", 0);
            vParams.Add("@Emp_ID", Convert.ToInt32(leaveFilter.Emp_Id));
            vParams.Add("@Cmp_ID", Convert.ToInt32(leaveFilter.Cmp_Id));
            vParams.Add("@Leave_ID", Convert.ToInt32("0"));
            vParams.Add("@From_Date", leaveFilter.FromDate);
            vParams.Add("@To_Date", leaveFilter.ToDate);
            vParams.Add("@Period", Convert.ToInt32("0"));
            vParams.Add("@Leave_Assign_As", "");
            vParams.Add("@Comment", "");
            vParams.Add("@Half_Leave_Date", DateTime.Now);
            vParams.Add("@InTime", DateTime.Now);
            vParams.Add("@OutTime", DateTime.Now);
            vParams.Add("@Login_ID", Convert.ToInt32("0"));
            vParams.Add("@strLeaveCompOff_Dates", "");
            vParams.Add("@Attachment", "");
            vParams.Add("@Type", "R");
            vParams.Add("@Result", "");
            
            var filteredData = vconn.Query<FilterLeaveResponse>("SP_Mobile_HRMS_WebAPI_Leave", vParams, commandType: CommandType.StoredProcedure).ToList();
            return filteredData;
        }

        public List<LeaveStatusResponse> GetLeaveStatus(LeaveFilter leaveFilter)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Leave_Application_ID", 0);
            vParams.Add("@Emp_ID", Convert.ToInt32(leaveFilter.Emp_Id));
            vParams.Add("@Cmp_ID", Convert.ToInt32(leaveFilter.Cmp_Id));
            vParams.Add("@Leave_ID", Convert.ToInt32("0"));
            vParams.Add("@From_Date", leaveFilter.FromDate);
            vParams.Add("@To_Date", leaveFilter.ToDate);
            vParams.Add("@Period", Convert.ToInt32("0"));
            vParams.Add("@Leave_Assign_As", "");
            vParams.Add("@Comment", "");
            vParams.Add("@Half_Leave_Date", DateTime.Now);
            vParams.Add("@InTime", DateTime.Now);
            vParams.Add("@OutTime", DateTime.Now);
            vParams.Add("@Login_ID", Convert.ToInt32("0"));
            vParams.Add("@strLeaveCompOff_Dates", "");
            vParams.Add("@Attachment", "");
            vParams.Add("@Type", "S");
            vParams.Add("@Result", "");

            var filteredData = vconn.Query<LeaveStatusResponse>("SP_Mobile_HRMS_WebAPI_Leave", vParams, commandType: CommandType.StoredProcedure).ToList();
            return filteredData;
        }

        public dynamic GetLeavetransactionRecords(GetLeaveTransactionRequest getLeaveTransactionRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();

            vParams.Add("@Month", getLeaveTransactionRequest.Month);
            vParams.Add("@Year", getLeaveTransactionRequest.Year);
            vParams.Add("@CMP_ID", getLeaveTransactionRequest.CmpId);
            vParams.Add("@Emp_Code",getLeaveTransactionRequest.EmpCode);
            var response = vconn.Query("SP_GET_LEAVE_APPLICATION_DETAILS_CommonWebservice", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public List<LeaveTypeBind> GetLeaveTypeBind(LeaveFilter leaveFilter)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Leave_Application_ID", 0);
            vParams.Add("@Emp_ID", Convert.ToInt32(leaveFilter.Emp_Id));
            vParams.Add("@Cmp_ID", Convert.ToInt32(leaveFilter.Cmp_Id));
            vParams.Add("@Leave_ID", Convert.ToInt32("0"));
            vParams.Add("@From_Date", "");
            vParams.Add("@To_Date", "");
            vParams.Add("@Period", Convert.ToInt32("0"));
            vParams.Add("@Leave_Assign_As", "");
            vParams.Add("@Comment", "");
            vParams.Add("@Half_Leave_Date", DateTime.Now);
            vParams.Add("@InTime", DateTime.Now);
            vParams.Add("@OutTime", DateTime.Now);
            vParams.Add("@Login_ID", Convert.ToInt32("0"));
            vParams.Add("@strLeaveCompOff_Dates", "");
            vParams.Add("@Attachment", "");
            vParams.Add("@Type", "B");
            vParams.Add("@Result", "");

            var LeaveBindType = vconn.Query<LeaveTypeBind>("SP_Mobile_HRMS_WebAPI_Leave", vParams, commandType: CommandType.StoredProcedure).ToList();
            return LeaveBindType;
        }

        public LeaveApplicationRecordsResponse LeaveApplicationRecords(LeaveApplicationRecordsRequest leaveApplicationRecordsRequest)
        {
            LeaveApplicationRecordsResponse leaveApplicationRecordsResponse = new LeaveApplicationRecordsResponse();
            if (leaveApplicationRecordsRequest.StrType == "Application" || leaveApplicationRecordsRequest.StrType == "application" || leaveApplicationRecordsRequest.StrType == "APPLICATION")
            {
                using var vconn = GetOpenConnection();
                var vParams = new DynamicParameters();
                vParams.Add("@Leave_Approval_ID", 0);
                vParams.Add("@Leave_Application_ID", 0);
                vParams.Add("@Leave_ID", 0);
                vParams.Add("@Emp_ID", leaveApplicationRecordsRequest.EmpId);
                vParams.Add("@Cmp_ID", leaveApplicationRecordsRequest.CmpId);
                vParams.Add("@Approval_Date", DateTime.Now);
                vParams.Add("@From_Date", DateTime.Now);
                vParams.Add("@To_Date", DateTime.Now);
                vParams.Add("@Leave_Period", 0.0);
                vParams.Add("@Leave_AssignAs", "");
                vParams.Add("@Leave_Reason", "");
                vParams.Add("@Half_Leave_Date", DateTime.Now);
                vParams.Add("@Approval_Status", "");
                vParams.Add("@Approval_Comments", "");
                vParams.Add("@Final_Approve", 0);
                vParams.Add("@Is_Fwd_Leave_Rej", 0);
                vParams.Add("@Rpt_Level", 0);
                vParams.Add("@SEmp_ID", 0);
                vParams.Add("@InTime", DateTime.Now);
                vParams.Add("@OutTime", DateTime.Now);
                vParams.Add("@Login_ID", 0);
                vParams.Add("@strLeaveCompOff_Dates", "");
                vParams.Add("@Type", "S");
                vParams.Add("@Status", leaveApplicationRecordsRequest.StrStatus);
                vParams.Add("@Result", "");

                leaveApplicationRecordsResponse.ApplicationResponses = vconn.Query<ApplicationResponse>("SP_Mobile_HRMS_WebService_Leave_Approve", vParams, commandType: CommandType.StoredProcedure).ToList();
                return leaveApplicationRecordsResponse;
            }
            else if(leaveApplicationRecordsRequest.StrType == "Cancellation" || leaveApplicationRecordsRequest.StrType == "cancellation" || leaveApplicationRecordsRequest.StrType == "CANCELLATION")
            {
                using var vconn = GetOpenConnection();
                var vParams = new DynamicParameters();
                vParams.Add("@Tran_ID", 0);
                vParams.Add("@Emp_ID", leaveApplicationRecordsRequest.EmpId);
                vParams.Add("@Cmp_ID", leaveApplicationRecordsRequest.CmpId);
                vParams.Add("@Leave_Application_ID", 0);
                vParams.Add("@Leave_Approval_ID", 0);
                vParams.Add("@Leave_ID", 0);
                vParams.Add("@For_date", DateTime.Now);
                vParams.Add("@Leave_period", 0.0);
                vParams.Add("@Actual_Leave_period", 0.0);
                vParams.Add("@Day_Type", "");
                vParams.Add("@Comment", "");
                vParams.Add("@Login_ID", 0);
                vParams.Add("@Compoff_Work_Date", "");
                vParams.Add("@IMEINo", "");
                vParams.Add("@AEmp_ID", 0);
                vParams.Add("@MComment", "");
                vParams.Add("@Is_Approve", 0);
                vParams.Add("@Type", "L");
                vParams.Add("@Result", "");

                leaveApplicationRecordsResponse.CancellationResponses = vconn.Query<CancellationResponse>("SP_Mobile_HRMS_WebService_Leave_Cancellation", vParams, commandType: CommandType.StoredProcedure).ToList();
                return leaveApplicationRecordsResponse;

            }
            return null;

        }

        public dynamic LeaveApproval(LeaveApprovalRequest leaveApprovalRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Leave_Approval_ID", 0);
            vParams.Add("@Leave_Application_ID", leaveApprovalRequest.LeaveAppID);
            vParams.Add("@Leave_ID", leaveApprovalRequest.LeaveID);
            vParams.Add("@Emp_ID", leaveApprovalRequest.EmpID);
            vParams.Add("@Cmp_ID", leaveApprovalRequest.CmpID);
            vParams.Add("@Approval_Date", DateTime.Now);
            vParams.Add("@From_Date", Convert.ToDateTime(leaveApprovalRequest.FromDate).ToString("dd/MMM/yyyy"));
            vParams.Add("@TO_Date", Convert.ToDateTime(leaveApprovalRequest.ToDate).ToString("dd/MMM/yyyy"));
            vParams.Add("@Leave_Period", leaveApprovalRequest.Period);
            vParams.Add("@Leave_AssignAs", leaveApprovalRequest.AssignAs);
            vParams.Add("@Leave_Reason", leaveApprovalRequest.Comment);
            vParams.Add("@Half_Leave_Date", Convert.ToDateTime(leaveApprovalRequest.HLeaveDate).ToString("dd/MMM/yyyy"));
            vParams.Add("@Approval_Status", leaveApprovalRequest.AppStatus);
            vParams.Add("@Approval_Comments", leaveApprovalRequest.AppComment);
            vParams.Add("@Final_Approve", leaveApprovalRequest.FinalApproval);
            vParams.Add("@Is_Fwd_Leave_Rej", leaveApprovalRequest.IsFWDRej);
            vParams.Add("@Rpt_Level", leaveApprovalRequest.RptLevel);
            vParams.Add("@SEmp_ID", leaveApprovalRequest.SEmpID);
            vParams.Add("@Intime", Convert.ToDateTime(leaveApprovalRequest.Intime).ToString("dd/MMM/yyyy"));
            vParams.Add("@Outtime", Convert.ToDateTime(leaveApprovalRequest.OutTime).ToString("dd/MMM/yyyy"));
            vParams.Add("@Login_ID", leaveApprovalRequest.LoginID);
            vParams.Add("@strLeaveCompOff_Dates", leaveApprovalRequest.CompoffLeaveDates);
            vParams.Add("@Type", "I");
            vParams.Add("@Result", "");

            var leaveApprove= vconn.QueryFirstOrDefault("SP_Mobile_HRMS_WebService_Leave_Approve", vParams, commandType: CommandType.StoredProcedure);
            return leaveApprove;
        }

        public dynamic LeaveApprovalDelete(LeaveApprovalDeleteRequest leaveApprovalDeleteRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", leaveApprovalDeleteRequest.EmpID);
            vParams.Add("@Cmp_ID", leaveApprovalDeleteRequest.CmpID);
            vParams.Add("@Leave_Application_ID", leaveApprovalDeleteRequest.LeaveAppID);
            vParams.Add("@Approval_Date", leaveApprovalDeleteRequest.Approval_Date);
            vParams.Add("@Approval_Status", leaveApprovalDeleteRequest.Approval_Status);
            vParams.Add("@Is_BackDated_app", leaveApprovalDeleteRequest.Is_Backdated_App);
            vParams.Add("@SEmp_ID", leaveApprovalDeleteRequest.SEmpID);
            vParams.Add("@User_Id", leaveApprovalDeleteRequest.User_Id);
            vParams.Add("@Login_ID", leaveApprovalDeleteRequest.LoginID);
            vParams.Add("@tran_type", leaveApprovalDeleteRequest.Tran_Type);
            vParams.Add("@Result", "");
            var leaveApproveDelete = vconn.Query("SP_Mobile_HRMS_WebService_Leave_Approve_Delete", vParams, commandType: CommandType.StoredProcedure);
            return leaveApproveDelete;

        }

        public LeaveApprovalDetailsResponse LeaveApprovalDetails(LeaveApprovalDetailsRequest leaveApprovalDetailsRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Tran_ID", 0);
            vParams.Add("@Emp_ID", leaveApprovalDetailsRequest.EmpId);
            vParams.Add("@Cmp_ID", leaveApprovalDetailsRequest.CmpId);
            vParams.Add("@Leave_Application_ID", leaveApprovalDetailsRequest.LeaveAppId);
            vParams.Add("@Leave_Approval_ID", leaveApprovalDetailsRequest.LeaveApprId);
            vParams.Add("@Leave_ID", leaveApprovalDetailsRequest.LeaveId);
            vParams.Add("@For_date", "");
            vParams.Add("@Leave_period",0);
            vParams.Add("@Actual_Leave_period",0);
            vParams.Add("@Day_Type", "");
            vParams.Add("@Comment", "");
            vParams.Add("@Login_ID", leaveApprovalDetailsRequest.LoginId);
            vParams.Add("@Compoff_Work_Date", "");
            vParams.Add("@IMEINo", "");
            vParams.Add("@AEmp_ID", 0);
            vParams.Add("@MComment", "");
            vParams.Add("@Is_Approve",0);
            vParams.Add("@Type", "E");
            vParams.Add("@Result", "");

            var leaveApprovalDetails = vconn.QueryFirst<LeaveApprovalDetailsResponse>("SP_Mobile_HRMS_WebService_Leave_Cancellation", vParams, commandType: CommandType.StoredProcedure);
            return leaveApprovalDetails;
        }

        public LeaveBalanceSummryResponse LeaveBalanceSummary(LeaveFilter leaveFilter, DateTime forDate)
        {
            LeaveBalanceSummryResponse leaveBalanceSummryResponse = new();
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@CMP_ID", leaveFilter.Cmp_Id);
            vParams.Add("@EMP_ID", leaveFilter.Emp_Id);
            vParams.Add("@FOR_DATE", forDate);
            vParams.Add("@Leave_Application", 0);
            vParams.Add("@Leave_Encash_App_ID", 0);
            vParams.Add("@Leave_ID", 0);
            var response = vconn.Query<LeaveBalanceSummry>("SP_LEAVE_CLOSING_AS_ON_DATE_ALL", vParams, commandType: CommandType.StoredProcedure).ToList();
            leaveBalanceSummryResponse.leaveBalanceSummries = response;
            return leaveBalanceSummryResponse;

        }

        public dynamic LeaveCancellationApplication(LeaveCancellationApplicationRequest leaveCancellationApplicationRequest,string Type)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Tran_ID", 0);
            vParams.Add("@Emp_ID", leaveCancellationApplicationRequest.EmpID);
            vParams.Add("@Cmp_ID", leaveCancellationApplicationRequest.CmpID);
            vParams.Add("@Leave_Application_ID", leaveCancellationApplicationRequest.LeaveAppID);
            vParams.Add("@Leave_Approval_ID", leaveCancellationApplicationRequest.LeaveApprID);
            vParams.Add("@Leave_ID", leaveCancellationApplicationRequest.LeaveID);
            vParams.Add("@For_date", Convert.ToDateTime(DateTime.Now).ToString("dd/MMM/yyyy"));
            vParams.Add("@Leave_period", 0);
            vParams.Add("@Actual_Leave_period", 0);
            vParams.Add("@Day_Type", "");
            vParams.Add("@Comment", "");
            vParams.Add("@Login_ID", leaveCancellationApplicationRequest.LoginID);
            vParams.Add("@Compoff_Work_Date", leaveCancellationApplicationRequest.CompOffDate);
            vParams.Add("@IMEINo", leaveCancellationApplicationRequest.IMEINo);
            vParams.Add("@AEmp_ID", 0);
            vParams.Add("@MComment", "");
            vParams.Add("@Is_Approve", 0);
            vParams.Add("@Type", Type);
            vParams.Add("@Result", "");

            var response = vconn.Query("SP_Mobile_HRMS_WebService_Leave_Cancellation", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public LeaveCancellationApplicationDetailsResponse LeaveCancellationApplicationDetails(LeaveCancellationApplicationDetailsRequest leaveCancellationApplicationDetailsRequest)
        {
            LeaveCancellationApplicationDetailsResponse leaveCancellationApplicationDetailsResponse = new();
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Tran_ID", 0);
            vParams.Add("@Emp_ID", leaveCancellationApplicationDetailsRequest.EmpID);
            vParams.Add("@Cmp_ID", leaveCancellationApplicationDetailsRequest.CmpID);
            vParams.Add("@Leave_Application_ID", 0);
            vParams.Add("@Leave_Approval_ID", leaveCancellationApplicationDetailsRequest.LeaveApprID);
            vParams.Add("@Leave_ID", leaveCancellationApplicationDetailsRequest.LeaveID);
            vParams.Add("@For_date", DateTime.Now);
            vParams.Add("@Leave_period", 0.0);
            vParams.Add("@Actual_Leave_period", 0.0);
            vParams.Add("@Day_Type", "");
            vParams.Add("@Comment", "");
            vParams.Add("@Login_ID", 0);
            vParams.Add("@Compoff_Work_Date", "");
            vParams.Add("@IMEINo", "");
            vParams.Add("@AEmp_ID", 0);
            vParams.Add("@MComment", "");
            vParams.Add("@Is_Approve", 0);
            vParams.Add("@Type", "D");
            vParams.Add("@Result", "");
            var response = vconn.Query<LeaveCancellationApplicationDetails>("SP_Mobile_HRMS_WebService_Leave_Cancellation", vParams, commandType: CommandType.StoredProcedure).ToList();
            leaveCancellationApplicationDetailsResponse.LeaveCancellationApplicationDetails = response;

            return leaveCancellationApplicationDetailsResponse;
        }
    }
}
