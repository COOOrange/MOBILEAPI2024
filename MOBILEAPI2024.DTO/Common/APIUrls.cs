using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.Common
{
    public class APIUrls
    {

        /// <summary>
        /// Attendance
        /// </summary>

        public const string AllEmployeeAttendance = "AllEmployeeAttendance";
        public const string AttendanceDetails = "AttendanceDetails";
        public const string AttendanceHistory = "AttendanceHistory";
        public const string AttendanceInsert = "AttendanceInsert";
        public const string AttendanceInsertOffline = "AttendanceInsertOffline";
        public const string AttendanceMissedPunch = "AttendanceMissedPunch";
        public const string AttendanceRegularizeApplicationRecord = "AttendanceRegularizeApplicationRecord";
        public const string AttendanceRegularizeApproval = "AttendanceRegularizeApproval";
        public const string AttendanceRegularizeDetails = "AttendanceRegularizeDetails";
        public const string AttendanceRegularizeInsert = "AttendanceRegularizeInsert";
        public const string AttendanceRoute = "AttendanceRoute";
        public const string CheckINOUT = "CheckINOUT";
        public const string GetAttendanceRegularizeApplicationDetails = "GetAttendanceRegularizeApplicationDetails";

        /// <summary>
        /// Employee
        /// </summary>
        public const string EmployeeDetails = "EmployeeDetails";
        public const string EmployeeDetailsForTally = "EmployeeDetailsForTally";
        public const string EmployeeDirectoryData = "EmployeeDirectoryData";
        public const string EmployeeList = "EmployeeList";
        public const string ManagerApprovalDetails = "ManagerApprovalDetails";
        public const string MyTeamAttendance = "MyTeamAttendance";
        public const string MyTeamAttendanceInsert = "MyTeamAttendanceInsert";
        public const string MyTeamDetails = "MyTeamDetails";
        public const string NewJoiningEmployeeDetails = "NewJoiningEmployeeDetails";
        public const string UpdateEmpFavDetails = "UpdateEmpFavDetails";
        public const string UpdateEmployeeDetails = "UpdateEmployeeDetails";

        /// <summary>
        /// Claim
        /// </summary>
        public const string ClaimAdminSetting = "ClaimAdminSetting";
        public const string ClaimAppDetails = "ClaimAppDetails";
        public const string ClaimApplication = "ClaimApplication";
        public const string ClaimApplicationDelete = "ClaimApplicationDelete";
        public const string ClaimApplicationDetails = "ClaimApplicationDetails";
        public const string ClaimApplicationRecords = "ClaimApplicationRecords";
        public const string ClaimApplicationStatus = "ClaimApplicationStatus";
        public const string ClaimApprovalDetailRecords = "ClaimApprovalDetailRecords";
        public const string ClaimApprovalRecords = "ClaimApprovalRecords";
        public const string ClaimApprovalUpdate = "ClaimApprovalUpdate";
        public const string ClaimType = "ClaimType";
        public const string ClaimLimit = "ClaimLimit";

        /// <summary>
        ///  Authentication
        /// </summary>
        public const string LoginCheck = "LoginCheck";
        public const string Logout = "Logout";
        public const string ForgotPassword = "ForgotPassword";
        public const string OTPVarification = "otp/verification";
        public const string ChangePassword = "ChangePassword";

        /// <summary>
        ///  User
        /// </summary>
        public const string Dashboard = "dashboard";
        public const string ClockIn = "clockin";
        public const string ClockOut = "clockout";

        /// <summary>
        /// Leave
        /// </summary>
        public const string GetLeaveRecords = "GetLeaveRecords";
        public const string LeaveApplication = "LeaveApplication";
        public const string LeaveApplicationDetails = "LeaveApplicationDetails";
        public const string LeaveApproval = "LeaveApproval";
        public const string LeaveApprovalDelete = "LeaveApprovalDelete";
        public const string LeaveBalanceSummary = "LeaveBalanceSummary";
        public const string LeaveApprovalDetails = "LeaveApprovalDetails";
        public const string GetLeaveApplicationRecords = "GetLeaveApplicationRecords";
        public const string LeaveBalance = "LeaveBalance";
        public const string LeaveApplicationRecords = "LeaveApplicationRecords";
        public const string LeaveCancellationApplication = "LeaveCancellationApplication";
        public const string LeaveCancellationApplicationDetails = "LeaveCancellationApplicationDetails";
        public const string LeaveCancellationApproval = "LeaveCancellationApproval";
        public const string LeaveTravelTypeDdl = "LeaveTravelTypeDdl";
        public const string GetLeavetransactionRecords = "GetLeavetransactionRecords";
        public const string GetCompOffLeave = "GetCompOffLeave";
        public const string AllLeaveBalance = "all/leavebalance";
        public const string FilterLeaveStatus = "filter/leavestatus";
        public const string AddAttendanceData = "add/attendance";
        public const string CheckPeriod = "check/period";



    }
}
