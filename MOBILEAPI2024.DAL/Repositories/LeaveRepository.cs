using Dapper;
using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.ResponseDTO.Leave;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
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
    }
}
