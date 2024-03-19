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
        ///  Authentication
        /// </summary>
        public const string Login = "user/login"; 
        public const string Logout = "user/logout";
        public const string ForgotPassword = "forgot/password"; 
        public const string OTPVarification = "otp/verification"; 
        public const string ResetPassword = "reset/password";

        /// <summary>
        ///  User
        /// </summary>
        public const string Dashboard = "dashboard";
        public const string ClockIn = "clockin";
        public const string ClockOut = "clockout";

        /// <summary>
        /// Leave
        /// </summary>
        public const string FilterLeaveBalance = "filter/leavebalance";
        public const string LeaveBalance = "leavebalance";
        public const string FilterLeaveStatus = "filter/leavestatus";
        public const string LeaveStatus = "leavestatus";
        public const string LeaveTypeBind = "leave/typebind";
        public const string AddAttendanceData = "add/attendance";
        public const string CheckPeriod = "check/period";
        public const string ApplyLeave = "apply/leave";
        public const string CheckLeaveStatus = "check/leavestatus";

    }
}
