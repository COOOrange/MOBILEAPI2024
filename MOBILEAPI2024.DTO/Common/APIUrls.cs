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
        /// Medical
        /// </summary>
        public const string BindMedicalDepDetails = "BindMedicalDepDetails";
        public const string BindMedicalIncident = "BindMedicalIncident";
        public const string GetMedicalAppDetails = "GetMedicalAppDetails";
        public const string GetMedicalAppIdDet = "GetMedicalAppIdDet";
        public const string MedicalDelete = "MedicalDelete";
        public const string MedicalInsert = "MedicalInsert";
        public const string MedicalUpdate = "MedicalUpdate";

        /// <summary>
        /// Grievance
        /// </summary>
        public const string GrievHearing = "GrievHearing";
        public const string GrievMaster = "GrievMaster";
        public const string GrievanceApplication = "GrievanceApplication";
        public const string GrievanceApplicationDelete = "GrievanceApplicationDelete";
        public const string GetGrievanceRecords = "GetGrievanceRecords";


        /// <summary>
        /// Ticket
        /// </summary>
        public const string AddTicketFeedback = "AddTicketFeedback";
        public const string BindTicketRecords = "BindTicketRecords";
        public const string TicketApplication = "TicketApplication";
        public const string TicketApplicationDelete = "TicketApplicationDelete";
        public const string TicketApplicationRecords = "TicketApplicationRecords";
        public const string TicketApplicationStatus = "TicketApplicationStatus";
        public const string TicketApproval = "TicketApproval";
        public const string TicketAprovalDelete = "TicketAprovalDelete";
        public const string TicketDashboard = "TicketDashboard";
        public const string TicketSendTo = "TicketSendTo";


        /// <summary>
        /// Exit 
        /// </summary>

        public const string AddExitAppilcation = "AddExitAppilcation";
        public const string AddExitApprovaldata = "AddExitApprovaldata";
        public const string ExitAppInsert = "ExitAppInsert";
        public const string ExitApplicationDelete = "ExitApplicationDelete";
        public const string ExitApplicationNoticePeriod = "ExitApplicationNoticePeriod";
        public const string ExitApplicationPreQuestion = "ExitApplicationPreQuestion";
        public const string ExitApplicationValidate = "ExitApplicationValidate";
        public const string GetExitApplicationRecords = "GetExitApplicationRecords";
        public const string GetExitApporvalRecords = "GetExitApporvalRecords";
        public const string GetExitApprovalEMPData = "GetExitApprovalEMPData";
        public const string GetExitInterviewQAInterview = "GetExitInterviewQAInterview";
        public const string GetExitTermsandConditions = "GetExitTermsandConditions";

        /// <summary>
        /// Comp Off
        /// </summary>
        public const string CompOffApplication = "CompOffApplication";
        public const string CompOffApplicationDelete = "CompOffApplicationDelete";
        public const string CompOffApproval = "CompOffApproval";
        public const string CompOffApprovalDelete = "CompOffApprovalDelete";
        public const string GetCompOffApplicationDetails = "GetCompOffApplicationDetails";
        public const string GetCompOffApplicationStatus = "GetCompOffApplicationStatus";
        public const string GetCompoffApplicationRecord = "GetCompoffApplicationRecord";

        /// <summary>
        /// Travel
        /// </summary>
        public const string DisplayTavelType = "DisplayTavelType";
        public const string GetTravelAPIData = "GetTravelAPIData";
        public const string TravelAllDetails = "TravelAllDetails";
        public const string TravelApp = "TravelApp";
        public const string TravelApplicationDelete = "TravelApplicationDelete";
        public const string TravelApprovalDelete = "TravelApprovalDelete";
        public const string TravelAprDetails = "TravelAprDetails";
        public const string TravelProof = "TravelProof";
        public const string TravelProofInsert = "TravelProofInsert";
        public const string TravelProofValidation = "TravelProofValidation";
        public const string Travel_Approval = "Travel_Approval";
        public const string Travel_Approval_AdminSetting = "Travel_Approval_AdminSetting";
        public const string Travel_Mode_Ddl = "Travel_Mode_Ddl";
        public const string Travel_Settlement = "Travel_Settlement";



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
        public const string AllEmployeesList = "AllEmployeesList";
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
        public const string Dashboard = "Dashboard";
        public const string ClockIn = "clockin";
        public const string ClockOut = "clockout";
        public const string GetPresentDayDuration = "GetPresentDayDuration";
        public const string GeoLocationTracking = "GeoLocationTracking";
        public const string GeoLocationTrackingList = "GeoLocationTrackingList";

        public const string AddComment = "AddComment";
        public const string AddLike = "AddLike";
        public const string AddWorkPlanOnClockIn = "AddWorkPlanOnClockIn";
        public const string AddWorkPlanOnClockOut = "AddWorkPlanOnClockOut";
        public const string ChangeRequestDepDelete = "ChangeRequestDepDelete";
        public const string ChangeRequestDepInsert = "ChangeRequestDepInsert";
        public const string ChangeRequestDepUpdate = "ChangeRequestDepUpdate";
        public const string ChangeRequestFavDelete = "ChangeRequestFavDelete";
        public const string ChangeRequestFavInsert = "ChangeRequestFavInsert";
        public const string ChangeRequestFavUpdate = "ChangeRequestFavUpdate";
        public const string ChangeRequestReqType = "ChangeRequestReqType";
        public const string Dashboard_backup = "Dashboard_backup";
        public const string ErrorHandling = "ErrorHandling";
        public const string EventDetails = "EventDetails";
        public const string GeoLocationRecords = "GeoLocationRecords";
        public const string GetBankList = "GetBankList";
        public const string GetBranch = "GetBranch";
        public const string GetCity = "GetCity";
        public const string GetCountry = "GetCountry";
        public const string GetDashboardApplicationsCount = "GetDashboardApplicationsCount";
        public const string GetEmployeeOTDetails = "GetEmployeeOTDetails";
        public const string GetHolidayList = "GetHolidayList";
        public const string GetLikeCommentList = "GetLikeCommentList";
        public const string GetNewJoiningUpdatedRecords = "GetNewJoiningUpdatedRecords";
        public const string GetNewsFeedDetail = "GetNewsFeedDetail";
        public const string GetNotification = "GetNotification";
        public const string GetNotification_Test = "GetNotification_Test";
        public const string GetPostRequestEmployee = "GetPostRequestEmployee";
        public const string GetReason = "GetReason";
        public const string GetReasonforResignation = "GetReasonforResignation";
        public const string GetShiftDeatails = "GetShiftDeatails";
        public const string GetState = "GetState";
        public const string GetSurveyList = "GetSurveyList";
        public const string GetSurveyQuestionAnswerList = "GetSurveyQuestionAnswerList";
        public const string GetVertical = "GetVertical";
        public const string KilometerRateMaster = "KilometerRateMaster";
        public const string LiveTrackingApplication = "LiveTrackingApplication";
        public const string LiveTrackingTotalDistance = "LiveTrackingTotalDistance";
        public const string MatchFingerPrint = "MatchFingerPrint";
        public const string MobileSalseStockResponse = "MobileSalseStockResponse";
        public const string MoodTracker = "MoodTracker";
        public const string MoodTrackerActivityList = "MoodTrackerActivityList";
        public const string MoodTrackerInsert = "MoodTrackerInsert";
        public const string PostFingerPrintDetails = "PostFingerPrintDetails";
        public const string PostRequest = "PostRequest";
        public const string SalaryDetails = "SalaryDetails";
        public const string SalaryDetailsForTally = "SalaryDetailsForTally";
        public const string SendNotification = "SendNotification";
        public const string ServerConnection = "ServerConnection";
        public const string SurveyApplication = "SurveyApplication";
        public const string TemplateApplication = "TemplateApplication";
        public const string TemplateApplicationDetails = "TemplateApplicationDetails";
        public const string TemplateFieldData = "TemplateFieldData";
        public const string TemplateFieldDataView = "TemplateFieldDataView";
        public const string Trigger_FCM = "Trigger_FCM";
        public const string UnisonMaster = "UnisonMaster";
        public const string UpdateBankDetails = "UpdateBankDetails";
        public const string UploadDocument = "UploadDocument";
        public const string get_currency = "get_currency";

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
