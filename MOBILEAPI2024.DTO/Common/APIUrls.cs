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

        /// <summary>
        ///  Authentication
        /// </summary>
        public const string LoginCheck = "LoginCheck"; 
        public const string Logout = "Logout";
        public const string ForgotPassword = "forgot/password"; 
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
