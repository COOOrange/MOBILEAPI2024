using Azure.Core;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Server;
using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.Common;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.RequestDTO.User;
using MOBILEAPI2024.DTO.ResponseDTO.User;
using System;
using System.Buffers.Text;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Net;
using System.Security.Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MOBILEAPI2024.DAL.Repositories
{
    public class UserRepository : SqlDbRepository<ActiveInActiveUser>, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        public void AddClockIn(ClockIn clockIn)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", clockIn.Emp_Id);
            vParams.Add("@Cmp_ID", clockIn.Cmp_Id);
            vParams.Add("@Vertical_ID", Convert.ToInt32("0"));
            vParams.Add("@SubVertical_ID", Convert.ToInt32("0"));
            vParams.Add("@ForDate ", Convert.ToDateTime(DateTime.UtcNow).ToString("yyyy/MM/dd"));
            vParams.Add("@Time", Convert.ToDateTime(DateTime.UtcNow).ToString("yyyy/MM/dd"));
            vParams.Add("@INOUTFlag", "I");
            vParams.Add("@Reason", "");
            vParams.Add("@IMEINo", "");
            vParams.Add("@Latitude", clockIn.Latitude);
            vParams.Add("@Longitude", clockIn.Longitude);
            vParams.Add("@Address", clockIn.Address);
            vParams.Add("@Emp_Image", clockIn.file.FileName);
            vParams.Add("@strAttendance", "");
            vParams.Add("@Month", "");
            vParams.Add("@Year", "");
            vParams.Add("@Type", "I");
            vParams.Add("@Result", "");
            vParams.Add("@SubVerticalName", "");

            vconn.QueryFirstOrDefault<string>("SP_Mobile_HRMS_WebAPI_Attendance", vParams, commandType: CommandType.StoredProcedure);
        }

        public void AddClockOut(ClockIn clockIn)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", clockIn.Emp_Id);
            vParams.Add("@Cmp_ID", clockIn.Cmp_Id);
            vParams.Add("@Vertical_ID", Convert.ToInt32("0"));
            vParams.Add("@SubVertical_ID", Convert.ToInt32("0"));
            vParams.Add("@ForDate ", Convert.ToDateTime(DateTime.UtcNow).ToString("yyyy/MM/dd"));
            vParams.Add("@Time", Convert.ToDateTime(DateTime.UtcNow).ToString("yyyy/MM/dd"));
            vParams.Add("@INOUTFlag", "O");
            vParams.Add("@Reason", "");
            vParams.Add("@IMEINo", "");
            vParams.Add("@Latitude", clockIn.Latitude);
            vParams.Add("@Longitude", clockIn.Longitude);
            vParams.Add("@Address", clockIn.Address);
            vParams.Add("@Emp_Image", clockIn.file.FileName);
            vParams.Add("@strAttendance", "");
            vParams.Add("@Month", "");
            vParams.Add("@Year", "");
            vParams.Add("@Type", "I");
            vParams.Add("@Result", "");
            vParams.Add("@SubVerticalName", "");

            vconn.QueryFirstOrDefault<string>("SP_Mobile_HRMS_WebAPI_Attendance", vParams, commandType: CommandType.StoredProcedure);

        }

        public void AddTransactionData(Transaction transaction)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", Convert.ToInt32("4"));
            vParams.Add("@Enroll_No", transaction.punchId);
            vParams.Add("@IO_Datetime", transaction.txnDateTime);
            vParams.Add("@Ip_Address", transaction.dvcIP);
            if (transaction.mode == "IN" || transaction.mode == "in" || transaction.mode == "In")
            {
                vParams.Add("@In_Out_flag", "0");
            }
            else if (transaction.mode == "OUT" || transaction.mode == "out" || transaction.mode == "Out")
            {
                vParams.Add("@In_Out_flag", "1");
            }
            else
            {
                vParams.Add("@In_Out_flag", "-1");
            }


            vconn.Execute("P9999_DEVICE_INOUT_DETAIL_DATAINSERT", vParams, commandType: CommandType.StoredProcedure);

        }

        public dynamic AddWorkPlanOnClockIn(AddWorkPlanOnClockInRequest addWorkPlanOnClockInRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", addWorkPlanOnClockInRequest.CmpID);
            vParams.Add("@Emp_ID", addWorkPlanOnClockInRequest.EmpID);
            vParams.Add("@Work_Plan", addWorkPlanOnClockInRequest.WorkPlan);
            vParams.Add("@Visit_Plan", addWorkPlanOnClockInRequest.VisitPlan);
            vParams.Add("@Work_Summary", "");
            vParams.Add("@Visit_Summary", "");
            vParams.Add("@INOUTFlag", "I");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_WorkPlan", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic AddWorkPlanOnClockOut(AddWorkPlanOnClockInRequest addWorkPlanOnClockInRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", addWorkPlanOnClockInRequest.CmpID);
            vParams.Add("@Emp_ID", addWorkPlanOnClockInRequest.EmpID);
            vParams.Add("@Work_Plan", addWorkPlanOnClockInRequest.WorkPlan);
            vParams.Add("@Visit_Plan", addWorkPlanOnClockInRequest.VisitPlan);
            vParams.Add("@Work_Summary", "");
            vParams.Add("@Visit_Summary", "");
            vParams.Add("@INOUTFlag", "O");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_WorkPlan", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic ChangeRequest(ChangeRequest changeRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Row_ID", changeRequest.Row_ID);
            vParams.Add("@Cmp_ID", changeRequest.CmpID);
            vParams.Add("@Emp_Id", changeRequest.Emp_Id);
            vParams.Add("@Request_Type_id", changeRequest.RequestTypeId);
            vParams.Add("@Change_Reason", changeRequest.Change_Reason);
            vParams.Add("@Request_Date", changeRequest.Request_Date);
            vParams.Add("@Shift_From_Date", changeRequest.Shift_From_Date);
            vParams.Add("@Shift_To_Date", changeRequest.Shift_To_Date);
            vParams.Add("@Dependant_Name", changeRequest.Dependant_Name);
            vParams.Add("@Dependant_Relationship", changeRequest.Dependant_Relationship);
            vParams.Add("@Dependant_Gender", changeRequest.Dependant_Gender);
            vParams.Add("@Dependant_DOB", changeRequest.Dependant_DOB);
            vParams.Add("@Dependant_Age", changeRequest.Dependant_Age);
            vParams.Add("@Dependant_Is_Resident", changeRequest.Dependant_Is_Resident);
            vParams.Add("@Dependant_Is_Depended", changeRequest.Dependant_Is_Depended);
            vParams.Add("@Tran_Type", changeRequest.TranType);
            vParams.Add("@Child_Birth_Date", changeRequest.Child_Birth_Date);
            vParams.Add("@DepOccupationID", changeRequest.DepOccupationID);
            vParams.Add("@DepHobbyID", changeRequest.DepHobbyID);
            vParams.Add("@DepHobbyName", changeRequest.DepHobbyName);
            vParams.Add("@DepCompany", changeRequest.DepCompany);
            vParams.Add("@DepCmpCity", changeRequest.DepCmpCity);
            vParams.Add("@DepStandardId", changeRequest.DepStandardId);
            vParams.Add("@DepSchCol", changeRequest.DepSchCol);
            vParams.Add("@DepSchColCity", changeRequest.DepSchColCity);
            vParams.Add("@DepExtAct", changeRequest.DepExtAct);
            vParams.Add("@Image_path", changeRequest.ImageName);
            vParams.Add("@PanCard", changeRequest.PanCard);
            vParams.Add("@AdharCard", changeRequest.AdharCard);
            vParams.Add("@Height", changeRequest.Height);
            vParams.Add("@Weight", changeRequest.Weight);
            vParams.Add("@OtherHobby", changeRequest.OtherHobby);
            vParams.Add("@DepSpecialization", changeRequest.Specialization);
            vParams.Add("@Request_id", dbType: DbType.Decimal, direction: ParameterDirection.Output);
            vParams.Add("@Result", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            var Response = vconn.Query("SP_Mobile_WebService_ChangeRequestApp", vParams, commandType: CommandType.StoredProcedure);
            int requestId = Convert.ToInt32(vParams.Get<string>("@Request_id"));
            string result = vParams.Get<string>("@Result");
            return result;
        }

        public dynamic ChangeRequestBind(int cmpId, int empID, string tranType)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Emp_Id", empID);
            vParams.Add("@Tran_Type", tranType);
            vParams.Add("@Result", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            var Response = vconn.Query("SP_Mobile_WebService_ChangeRequest", vParams, commandType: CommandType.StoredProcedure);
            return Response;

        }

        public dynamic ChangeRequestFav(ChangeRequestFav changeRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Row_ID", changeRequest.Row_ID);
            vParams.Add("@Cmp_ID", changeRequest.CmpID);
            vParams.Add("@Emp_Id", changeRequest.Emp_Id);
            vParams.Add("@Request_Type_id", changeRequest.RequestTypeId);
            vParams.Add("@Change_Reason", changeRequest.Change_Reason);
            vParams.Add("@Request_Date", changeRequest.Request_Date);
            vParams.Add("@EmpFavSportID", changeRequest.EmpFavSportID);
            vParams.Add("@EmpFavSportName", changeRequest.EmpFavSportName);
            vParams.Add("@EmpHobbyID", changeRequest.EmpHobbyID);
            vParams.Add("@EmpHobbyName", changeRequest.EmpHobbyName);
            vParams.Add("@EmpFavFood", changeRequest.EmpFavFood);
            vParams.Add("@EmpFavRestro", changeRequest.EmpFavRestro);
            vParams.Add("@EmpFavTrvDestination", changeRequest.EmpFavTrvDestination);
            vParams.Add("@EmpFavFestival", changeRequest.EmpFavFestival);
            vParams.Add("@EmpFavSportPerson", changeRequest.EmpFavSportPerson);
            vParams.Add("@EmpFavSinger", changeRequest.EmpFavSinger);
            vParams.Add("@CurrEmpFavSportID", changeRequest.CurrEmpFavSportID);
            vParams.Add("@CurrEmpFavSportName", changeRequest.CurrEmpFavSportID);
            vParams.Add("@CurrEmpHobbyID", changeRequest.CurrEmpHobbyID);
            vParams.Add("@CurrEmpHobbyName", changeRequest.CurrEmpHobbyName);
            vParams.Add("@CurrEmpFavFood", changeRequest.CurrEmpFavFood);
            vParams.Add("@CurrEmpFavRestro", changeRequest.CurrEmpFavRestro);
            vParams.Add("@CurrEmpFavTrvDestination", changeRequest.CurrEmpFavTrvDestination);
            vParams.Add("@CurrEmpFavFestival", changeRequest.CurrEmpFavFestival);
            vParams.Add("@CurrEmpFavSportPerson", changeRequest.CurrEmpFavSportPerson);
            vParams.Add("@CurrEmpFavSinger", changeRequest.CurrEmpFavSinger);
            vParams.Add("@Image_path", changeRequest.ImagePath);
            vParams.Add("@tran_type", changeRequest.TranType);
            vParams.Add("@OtherSport", changeRequest.OtherSport);
            vParams.Add("@OtherHobby", changeRequest.OtherHobby);
            vParams.Add("@Request_id", dbType: DbType.Decimal, direction: ParameterDirection.Output);
            vParams.Add("@Result", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            var Response = vconn.Query("SP_Mobile_WebService_ChgReqFavApp", vParams, commandType: CommandType.StoredProcedure);
            //int requestId = Convert.ToInt32(vParams.Get<string>("@Request_id"));
            string result = vParams.Get<string>("@Result");
            return result;
        }

        public bool CheckEnrollNoExixts(Transaction transaction)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", Convert.ToInt32("4"));
            vParams.Add("@Enroll_No", transaction.punchId);

            var checkEnroll = vconn.QueryFirstOrDefault("P0080_EMP_MASTER_CHECK_ENROLL", vParams, commandType: CommandType.StoredProcedure);
            if (checkEnroll != null)
            {
                return true;
            }
            return false;
        }

        public bool CheckTransactionExistence(Transaction transaction)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", Convert.ToInt32("4"));
            vParams.Add("@Enroll_No", transaction.punchId);
            vParams.Add("@IO_Datetime", transaction.txnDateTime);
            vParams.Add("@Ip_Address", transaction.dvcIP);
            if (transaction.mode == "IN" || transaction.mode == "in" || transaction.mode == "In")
            {
                vParams.Add("@In_Out_flag", "0");
            }
            else if (transaction.mode == "OUT" || transaction.mode == "out" || transaction.mode == "Out")
            {
                vParams.Add("@In_Out_flag", "1");
            }
            else
            {
                vParams.Add("@In_Out_flag", "-1");
            }

            var transactionData = vconn.QueryFirstOrDefault("P9999_DEVICE_INOUT_DETAIL_CHECK_EXIXTS", vParams, commandType: CommandType.StoredProcedure);
            if (transactionData != null)
            {
                return true;
            }
            return false;

        }

        public DashboardDTO DashboardData(string empId, string cmpId)
        {
            DashboardDTO dashboardDTO = new DashboardDTO();
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", Convert.ToInt32(empId));
            vParams.Add("@Cmp_ID", Convert.ToInt32(cmpId));

            var employeeCount = vconn.QueryMultiple("SP_Mobile_HRMS_WebService_DASHBOARD", vParams, commandType: CommandType.StoredProcedure);
            var emp = employeeCount.Read().FirstOrDefault();
            var finalEmployee = employeeCount.Read().FirstOrDefault();
            if (finalEmployee != null)
            {
                var employeeCou = new EmployeeCount();

                employeeCou.emp_id = finalEmployee.emp_id;
                employeeCou.Present = finalEmployee.Present;
                employeeCou.WO = finalEmployee.WO;
                employeeCou.HO = finalEmployee.HO;
                employeeCou.OD = finalEmployee.OD;
                employeeCou.Absent = finalEmployee.Absent;
                employeeCou.Leave = finalEmployee.Leave;
                employeeCou.Total = finalEmployee.Total;
                employeeCou.D_Present = finalEmployee.D_Present;

                dashboardDTO.employeeCount = employeeCou;
            }

            vParams.Add("@Vertical_ID", Convert.ToInt32("0"));
            vParams.Add("@SubVertical_ID", Convert.ToInt32("0"));
            vParams.Add("@ForDate", "");
            vParams.Add("@Time", "");
            vParams.Add("@INOUTFlag", "");
            vParams.Add("@Reason", "");
            vParams.Add("@IMEINo", "");
            vParams.Add("@Latitude", "");
            vParams.Add("@Longitude", "");
            vParams.Add("@Address", "");
            vParams.Add("@Emp_Image", "");
            vParams.Add("@strAttendance", "");
            vParams.Add("@Month", "");
            vParams.Add("@Year", "");
            vParams.Add("@Type", "L");
            vParams.Add("@Result", "");
            vParams.Add("@SubVerticalName", "");

            var employeeData = vconn.QueryFirstOrDefault<EmployeeData>("SP_Mobile_HRMS_Attendance", vParams, commandType: CommandType.StoredProcedure);
            dashboardDTO.employeeData = employeeData;
            return dashboardDTO;
        }

        public dynamic Dashboard_backup(int cmpId, int empID)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", empID);
            vParams.Add("@Cmp_ID", cmpId);
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_DASHBOARD", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic EventDetails(int cmpId, int empId, string forDate)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@For_Date", Convert.ToDateTime(forDate));
            vParams.Add("@Emp_ID", empId);
            vParams.Add("@Cmp_ID", cmpId);
            var Response = vconn.Query("P0100_EVENT_DETAIL_GET", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic GeoLocationTracking(GeoLocationRequest geoLocationRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", geoLocationRequest.EmpID);
            vParams.Add("@Cmp_ID", geoLocationRequest.CmpID);
            vParams.Add("@Date", "");
            vParams.Add("@Latitude", geoLocationRequest.Latitude);
            vParams.Add("@Longitude", geoLocationRequest.Longitude);
            vParams.Add("@Address_location", geoLocationRequest.AddressLocation);
            vParams.Add("@Type", "I");
            vParams.Add("@Result", "");
            var geoLocationResponse = vconn.Query("SP_GeoLocationTracing_API", vParams, commandType: CommandType.StoredProcedure);
            return geoLocationResponse;
        }

        public dynamic GeoLocationTrackingList(int cmpId, int empId, DateTime date)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", empId);
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Date", date);
            vParams.Add("@Latitude", "");
            vParams.Add("@Longitude", "");
            vParams.Add("@Address_location", "");
            vParams.Add("@Type", "L");
            vParams.Add("@Result", "");
            var geoLocationResponse = vconn.Query("SP_GeoLocationTracing_API", vParams, commandType: CommandType.StoredProcedure);
            return geoLocationResponse;
        }

        public dynamic GetDashboardApplicationsCount(int cmpId, int empId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Emp_Id", empId);
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_GETCOUNT", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic GetEmployeeOTDetails(int cmpId, int empId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Compoff_App_ID", 0);
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Emp_ID", empId);
            vParams.Add("@SEmp_ID", 0);
            vParams.Add("@ForDate", DateTime.Now);
            vParams.Add("@Extra_Work_Date", DateTime.Now);
            vParams.Add("@Extra_Work_Hours", "");
            vParams.Add("@Extra_Work_Reason", "");
            vParams.Add("@Sanctioned_Hours", "");
            vParams.Add("@CompOff_Type", "");
            vParams.Add("@IMEINo", "");
            vParams.Add("@OT_Type", 0);
            vParams.Add("@Login_ID", 0);
            vParams.Add("@Email", "");
            vParams.Add("@ContactNo", "");
            vParams.Add("@Approval_Status", "");
            vParams.Add("@Approval_Comments", "");
            vParams.Add("@Type", "O");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_CompOff", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic GetHolidayList(int cmpId, int empId, int year)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", empId);
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Dept_ID", 0);
            vParams.Add("@GalleryType", 0);
            vParams.Add("@Type", "H");
            vParams.Add("@Year", year);
            vParams.Add("@Notification_ID", 0);
            vParams.Add("@NEmp_ID", 0);
            vParams.Add("@For_date", DateTime.Now);
            vParams.Add("@U_Comment_Id", 0);
            vParams.Add("@Notification_date", DateTime.Now);
            vParams.Add("@Flag", 0);
            vParams.Add("@Comment", "");
            vParams.Add("@Comment_Status", "");
            vParams.Add("@Reply_Comment_Id", 0);
            vParams.Add("@Reminder_Type", "");
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_Notification", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic GetNewJoiningUpdatedRecords(LeaveFilter getLikeCommentListRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@FROMDATE", getLikeCommentListRequest.FromDate);
            vParams.Add("@TODATE", getLikeCommentListRequest.ToDate);
            vParams.Add("@TYPE", "R");

            var Response = vconn.Query("SP_Common_Webservice_UpdatedRecords", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic GetNewsFeedDetail(int cmpId, int empId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Emp_ID", empId);
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_NewsFeed", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic GetNotification(GetNotification getNotificatioon)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", getNotificatioon.EmpID);
            vParams.Add("@Cmp_ID", getNotificatioon.CmpID);
            vParams.Add("@Dept_ID", getNotificatioon.DeptID);
            vParams.Add("@GalleryType", getNotificatioon.GalleryType);
            vParams.Add("@Type", getNotificatioon.strType);
            vParams.Add("@Year", getNotificatioon.Year);
            vParams.Add("@Notification_ID", getNotificatioon.NotificationID);
            vParams.Add("@NEmp_ID", getNotificatioon.NEmpID);
            vParams.Add("@For_date", Convert.ToDateTime(getNotificatioon.Fordate));
            vParams.Add("@U_Comment_Id", getNotificatioon.UCommentID);
            vParams.Add("@Notification_date", Convert.ToDateTime(getNotificatioon.NotificationDate));
            vParams.Add("@Flag", getNotificatioon.Flag);
            vParams.Add("@Comment", getNotificatioon.Comment);
            vParams.Add("@Comment_Status", getNotificatioon.CommentStatus);
            vParams.Add("@Reply_Comment_Id", getNotificatioon.RCommentID);
            vParams.Add("@Reminder_Type", getNotificatioon.ReminderType);
            var geoLocationResponse1 = vconn.Query("SP_Mobile_HRMS_WebService_Notification", vParams, commandType: CommandType.StoredProcedure);
            return geoLocationResponse1;
        }

        public dynamic GetPostRequestEmployee(int cmpID, int loginId, string request_Type)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpID);
            vParams.Add("@Emp_Login_ID", loginId);
            vParams.Add("@Request_ID", 0);
            vParams.Add("@Request_Type", request_Type);
            vParams.Add("@Request_Date", DateTime.Now);
            vParams.Add("@Request_Detail", "");
            vParams.Add("@Feedback_Detail", "");
            vParams.Add("@Request_Status", 0);
            vParams.Add("@Login_ID", 0);
            vParams.Add("@Type", "R");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_PostRequest", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic GetShiftDeatails(int cmpID, int empID, string forDate)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Leave_Application_ID", 0);
            vParams.Add("@Emp_ID", empID);
            vParams.Add("@Cmp_ID", cmpID);
            vParams.Add("@Leave_ID", 0);
            vParams.Add("@From_Date", Convert.ToDateTime(forDate).ToString("dd/MMM/yyyy"));
            vParams.Add("@To_Date", DateTime.Now);
            vParams.Add("@Period", 0.0);
            vParams.Add("@Leave_Assign_As", "");
            vParams.Add("@Comment", "");
            vParams.Add("@Half_Leave_Date", DateTime.Now);
            vParams.Add("@InTime", DateTime.Now);
            vParams.Add("@OutTime", DateTime.Now);
            vParams.Add("@Login_ID", 0);
            vParams.Add("@Attachment", "");
            vParams.Add("@Type", "T");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_Leave", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic GetSurveyList(int cmpId, int empId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", empId);
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Survey_ID", 0);
            vParams.Add("@Survey_Details", "");
            vParams.Add("@Login_ID", 0);
            vParams.Add("@IMEINo", "");
            vParams.Add("@Type", "B");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_Survey", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic GetSurveyQuestionAnswerList(int cmpID, int empId, int surveyID)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", empId);
            vParams.Add("@Cmp_ID", cmpID);
            vParams.Add("@Survey_ID", surveyID);
            vParams.Add("@Survey_Details", "");
            vParams.Add("@Login_ID", 0);
            vParams.Add("@IMEINo", "");
            vParams.Add("@Type", "Q");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_Survey", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic KilometerRateMaster(KilometerRateMasterRequest kilometerRateMasterRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", kilometerRateMasterRequest.CmpID);
            vParams.Add("@Emp_Category", kilometerRateMasterRequest.EmployeeCategory);
            vParams.Add("@Vehicle_Type", kilometerRateMasterRequest.VehicleType);
            var Response = vconn.Query("SP_Mobile_KilometerRate_Master", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic LiveTrackingTotalDistance(int cmpId, int empId, DateTime createdDate)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Emp_Id", empId);
            vParams.Add("@Created_Date", createdDate);
            var Response = vconn.Query("SP_Mobile_LiveTracking_TotalDistance", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic MatchFingerPrint(int cmpId, int empId)
        {
            byte[] bt = { 0, 0 };

            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Emp_ID", empId);
            //vParams.Add("@Base64", bt);
            vParams.Add("@Type", "S");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_WebService_FingerPrint", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic MobileSalesStockResponse(MobileSalseStockResponseRequest mobileSalseStockResponseRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            //vParams.Add("@Cmp_ID", CmpID, SqlDbType.Int)
            //vParams.Add("@Emp_ID", EmpID, SqlDbType.Int)
            //vParams.Add("@Store_ID", StoreID, SqlDbType.Int)
            //vParams.Add("@Login_ID", LoginID, SqlDbType.Int)
            //vParams.Add("@Mobile_Remark_ID", RemarkID, SqlDbType.Int)
            //vParams.Add("@SaleStockDetails", SaleStockDetails, SqlDbType.NVarChar)
            //vParams.Add("@Type", strType, SqlDbType.VarChar)
            //vParams.Add("@For_Date", Convert.ToDateTime(ForDate).ToString("dd/MMM/yyyy"), SqlDbType.DateTime)
            //vParams.Add("@Result", "", SqlDbType.VarChar, ParameterDirection.Output)
            var Response = vconn.Query("SP_Mobile_LiveTracking_TotalDistance", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic MoodTracker(LeaveBalanceRequest moodTracker)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", moodTracker.CmpId);
            vParams.Add("@Emp_ID", moodTracker.EmpId);
            vParams.Add("@Month", moodTracker.Month);
            vParams.Add("@Year", moodTracker.Year);
            vParams.Add("@Activity", 0);
            vParams.Add("@MoodDetails", "");
            vParams.Add("@Type", "S");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_WebService_MoodTracker", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic MoodTrackerActivityList()
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            var Response = vconn.Query("SP_Mobile_WebService_MoodTracker_ActLists", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic MoodTrackerInsert(MoodTrackerInsertRequest moodTrackerInsertRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", moodTrackerInsertRequest.CmpID);
            vParams.Add("@Emp_ID", moodTrackerInsertRequest.EmpID);
            vParams.Add("@Month", 0);
            vParams.Add("@Year", 0);
            vParams.Add("@Activity", moodTrackerInsertRequest.ActivityId);
            vParams.Add("@MoodDetails", moodTrackerInsertRequest.MoodDetail);
            vParams.Add("@Type", "I");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_WebService_MoodTracker", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic PostFingerPrintDetails(int empID, int cmpId, string base64)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Emp_ID", empID);
            //vParams.Add("@Base64", base64);
            vParams.Add("@Type", "I");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_WebService_FingerPrint", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic PostRequest(PostRequest postRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", postRequest.CmpID);
            vParams.Add("@Emp_Login_ID", postRequest.EmpID_LoginID);
            vParams.Add("@Request_ID", postRequest.RequestID);
            vParams.Add("@Request_Type", postRequest.Request_Type);
            vParams.Add("@Request_Date", DateTime.Now);
            vParams.Add("@Request_Detail", postRequest.Request_Details);
            vParams.Add("@Feedback_Detail", postRequest.Feedback_Details);
            vParams.Add("@Request_Status", postRequest.Request_Status);
            vParams.Add("@Login_ID", postRequest.LoginID);
            vParams.Add("@Type", postRequest.StrType);
            vParams.Add("@Result", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_PostRequest", vParams, commandType: CommandType.StoredProcedure);
            string result = vParams.Get<string>("@Result");
            return result;
        }

        public dynamic SalaryDetails(LeaveBalanceRequest salaryDetails)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", salaryDetails.EmpId);
            vParams.Add("@Cmp_ID", salaryDetails.CmpId);
            vParams.Add("@Month", salaryDetails.Month);
            vParams.Add("@Year", salaryDetails.Year);
            vParams.Add("@Type", "V");
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_Salary", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic TemplateApplicationDetails(TemplateApplicationDetailsRequest templateApplicationDetailsRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@From_Date", templateApplicationDetailsRequest.FromDate);
            vParams.Add("@To_Date", templateApplicationDetailsRequest.ToDate);
            vParams.Add("@Cmp_Id", templateApplicationDetailsRequest.CmpID);
            vParams.Add("@Branch_ID", templateApplicationDetailsRequest.BranchId.ToString());
            vParams.Add("@Cat_ID", templateApplicationDetailsRequest.CatId.ToString());
            vParams.Add("@Grd_ID", templateApplicationDetailsRequest.GrdId.ToString());
            vParams.Add("@Dept_ID", templateApplicationDetailsRequest.DeptId.ToString());
            vParams.Add("@Desig_ID", templateApplicationDetailsRequest.DesgId.ToString());
            vParams.Add("@Emp_ID", templateApplicationDetailsRequest.EmpId);
            vParams.Add("@T_Id", templateApplicationDetailsRequest.TId);
            var Response = vconn.Query("SP_Mobile_TemplateApplication_Data", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic TemplateFieldData(TemplateFieldDataRequest templateFieldDataRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", templateFieldDataRequest.CmpID);
            vParams.Add("@Emp_ID", templateFieldDataRequest.EmpID);
            vParams.Add("@T_Id", templateFieldDataRequest.TID);
            vParams.Add("@From_Date", templateFieldDataRequest.FromDate);
            vParams.Add("@To_Date", templateFieldDataRequest.ToDate);
            vParams.Add("@Flag", templateFieldDataRequest.Flag);
            vParams.Add("@Response_Flag", templateFieldDataRequest.RFlag);
            var Response = vconn.Query("SP_Mobile_TemplateField_Data", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic TemplateFieldDataView(TemplateFieldDataViewRequest templateFieldDataRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", templateFieldDataRequest.CmpID);
            vParams.Add("@T_ID", templateFieldDataRequest.TId);
            vParams.Add("@Emp_Id", templateFieldDataRequest.EmpId);
            vParams.Add("@Response_Flag", templateFieldDataRequest.RFlag);
            var Response = vconn.Query("SP_Mobile_TemplateField_View", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic UnisonMaster(int cmpId, int empId, string master)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@MASTER", master);
            vParams.Add("@EMP_ID", empId);
            var Response = vconn.Query("SP_MOBILE_HRMS_WEBSERVICE_UNISONMASTER", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public void UpdateTransactionData(Transaction transaction)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", Convert.ToInt32("110"));
            vParams.Add("@IO_tran_ID", transaction.txnId);
            vParams.Add("@Enroll_No", transaction.dvcId);
            vParams.Add("@IO_Datetime", transaction.txnDateTime);
            vParams.Add("@Ip_Address", transaction.dvcIP);
            vParams.Add("@In_Out_flag", transaction.mode);
            vParams.Add("@IU_Flag", "U");

            vconn.Execute("SP_Mobile_HRMS_Attendance", vParams, commandType: CommandType.StoredProcedure);

        }
    }
}
