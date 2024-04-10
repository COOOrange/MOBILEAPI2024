using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.Common;
using MOBILEAPI2024.DTO.RequestDTO.Attendance;
using MOBILEAPI2024.DTO.RequestDTO.Employee;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.RequestDTO.Ticket;
using MOBILEAPI2024.DTO.RequestDTO.Travel;
using MOBILEAPI2024.DTO.RequestDTO.User;
using MOBILEAPI2024.DTO.ResponseDTO.User;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics.Metrics;
using System.Formats.Tar;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MOBILEAPI2024.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public UserService(IUserRepository userRepository, IMapper mapper, IOptions<AppSettings> appSetting, IAttendanceRepository attendanceRepository, IEmployeeRepository employeeRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _appSettings = appSetting.Value;
            _attendanceRepository = attendanceRepository;
            _employeeRepository = employeeRepository;
        }

        public void AddClockIn(ClockIn clockIn)
        {
            _userRepository.AddClockIn(clockIn);
        }

        public void AddClockOut(ClockIn clockIn)
        {
            _userRepository.AddClockOut(clockIn);
        }

        public dynamic AddComment(AddCommentRequest addCommentRequest)
        {

            var getNotificatioon = _mapper.Map<AddCommentRequest, GetNotification>(addCommentRequest);
            getNotificatioon.NotificationDate = DateTime.Now;

            if (getNotificatioon.Comment == "D")
            {
                getNotificatioon.strType = "CD";
                getNotificatioon.Comment = "";
                var notificationResponse = _userRepository.GetNotification(getNotificatioon);
                if ((notificationResponse as ICollection)?.Count == 0 || notificationResponse == null)
                {
                    return null;
                }
                return notificationResponse;
            }
            getNotificatioon.strType = "CI";
            var notificationResponse1 = _userRepository.GetNotification(getNotificatioon);
            if ((notificationResponse1 as ICollection)?.Count == 0 || notificationResponse1 == null)
            {
                return null;
            }
            return notificationResponse1;

        }

        public dynamic AddLike(strDetails item)
        {
            var getNotificatioon = _mapper.Map<strDetails, GetNotification>(item);
            getNotificatioon.NotificationDate = DateTime.Now;

            getNotificatioon.strType = "LI";
            var notificationResponse = _userRepository.GetNotification(getNotificatioon);
            if ((notificationResponse as ICollection)?.Count == 0 || notificationResponse == null)
            {
                return null;
            }
            return notificationResponse;

        }

        public void AddTransactionData(Transaction transactionRequest)
        {
            _userRepository.AddTransactionData(transactionRequest);
        }

        public dynamic AddWorkPlanOnClockIn(AddWorkPlanOnClockInRequest addWorkPlanOnClockInRequest)
        {
            var Response = _userRepository.AddWorkPlanOnClockIn(addWorkPlanOnClockInRequest);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }

        public dynamic AddWorkPlanOnClockOut(AddWorkPlanOnClockInRequest addWorkPlanOnClockInRequest)
        {
            var Response = _userRepository.AddWorkPlanOnClockOut(addWorkPlanOnClockInRequest);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }

        public dynamic ChangeRequestBind(int cmpId, int empID, string tranType)
        {
            var Response = _userRepository.ChangeRequestBind(cmpId, empID, tranType);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }

        public dynamic ChangeRequestDepDelete(int cmpId, int empID, int row_ID)
        {
            ChangeRequest changeRequest = new();
            changeRequest.Emp_Id = empID;
            changeRequest.CmpID = cmpId;
            changeRequest.Row_ID = row_ID;
            changeRequest.TranType = "D";
            changeRequest.Request_Date = DateTime.Now;
            changeRequest.Dependant_DOB = DateTime.Now;
            var Response = _userRepository.ChangeRequest(changeRequest);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }
        public dynamic ChangeRequestDepInsert(ChangeRequestDepInsertRequest changeRequestDepInsert)
        {
            string imgName = "";
            if (!string.IsNullOrEmpty(changeRequestDepInsert.ImagePath))
            {
                string strImagePath = Path.Combine(_appSettings.DocPath);
                imgName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + changeRequestDepInsert.EmpId + ".png";
                byte[] imgBytes = Convert.FromBase64String(changeRequestDepInsert.ImagePath);
                File.WriteAllBytesAsync(Path.Combine(strImagePath, imgName), imgBytes);
            }
            else
            {
                imgName = "";
            }

            var changeRequest = _mapper.Map<ChangeRequestDepInsertRequest, ChangeRequest>(changeRequestDepInsert);
            changeRequest.TranType = "I";
            changeRequest.ImageName = imgName;
            var Response = _userRepository.ChangeRequest(changeRequest);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }

        public dynamic ChangeRequestDepUpdate(ChangeRequestDepInsertRequest changeRequestDepInsert)
        {
            string imgName = "";
            if (!string.IsNullOrEmpty(changeRequestDepInsert.ImagePath))
            {
                string strImagePath = Path.Combine(_appSettings.DocPath);
                imgName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + changeRequestDepInsert.EmpId + ".png";
                byte[] imgBytes = Convert.FromBase64String(changeRequestDepInsert.ImagePath);
                File.WriteAllBytesAsync(Path.Combine(strImagePath, imgName), imgBytes);
            }
            else
            {
                imgName = "";
            }

            var changeRequest = _mapper.Map<ChangeRequestDepInsertRequest, ChangeRequest>(changeRequestDepInsert);
            changeRequest.TranType = "U";
            changeRequest.ImageName = imgName;
            var Response = _userRepository.ChangeRequest(changeRequest);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }

        public dynamic ChangeRequestFavDelete(int cmpId, int empID, int row_ID)
        {
            ChangeRequestFav changeRequest = new();
            changeRequest.Emp_Id = empID;
            changeRequest.CmpID = cmpId;
            changeRequest.Row_ID = row_ID;
            changeRequest.TranType = "D";
            changeRequest.Request_Date = DateTime.Now;
            var Response = _userRepository.ChangeRequestFav(changeRequest);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }

        public dynamic ChangeRequestFavInsert(ChangeRequestFavInsertRequest changeRequestDepInsert)
        {
            string imgName = "";
            if (!string.IsNullOrEmpty(changeRequestDepInsert.ImagePath))
            {
                string strImagePath = Path.Combine(_appSettings.DocPath);
                imgName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + changeRequestDepInsert.Emp_Id + ".png";
                byte[] imgBytes = Convert.FromBase64String(changeRequestDepInsert.ImagePath);
                File.WriteAllBytesAsync(Path.Combine(strImagePath, imgName), imgBytes);
            }
            else
            {
                imgName = "";
            }

            var changeRequest = _mapper.Map<ChangeRequestFavInsertRequest, ChangeRequestFav>(changeRequestDepInsert);
            changeRequest.TranType = "I";
            changeRequest.ImagePath = imgName;
            var Response = _userRepository.ChangeRequestFav(changeRequest);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }

        public dynamic ChangeRequestFavUpdate(ChangeRequestFavInsertRequest changeRequestDepInsert)
        {
            string imgName = "";
            if (!string.IsNullOrEmpty(changeRequestDepInsert.ImagePath))
            {
                string strImagePath = Path.Combine(_appSettings.DocPath);
                imgName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + changeRequestDepInsert.Emp_Id + ".png";
                byte[] imgBytes = Convert.FromBase64String(changeRequestDepInsert.ImagePath);
                File.WriteAllBytesAsync(Path.Combine(strImagePath, imgName), imgBytes);
            }
            else
            {
                imgName = "";
            }

            var changeRequest = _mapper.Map<ChangeRequestFavInsertRequest, ChangeRequestFav>(changeRequestDepInsert);
            changeRequest.TranType = "U";
            changeRequest.ImagePath = imgName;
            var Response = _userRepository.ChangeRequestFav(changeRequest);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }

        public bool CheckEnrollNoExixts(Transaction transaction)
        {
            bool checkEnroll = _userRepository.CheckEnrollNoExixts(transaction);
            if (checkEnroll)
            {
                return true;
            }
            return false;
        }

        public bool CheckTransactionExistence(Transaction transaction)
        {
            bool transactiondata = _userRepository.CheckTransactionExistence(transaction);
            if (transactiondata)
            {
                return true;
            }
            return false;
        }

        public DashboardDTO DashboardData(string empId, string cmpId)
        {
            var dashboardData = _userRepository.DashboardData(empId, cmpId);
            if (dashboardData != null)
            {
                return dashboardData;
            }
            return null;
        }

        public dynamic Dashboard_backup(int cmpId, int empID)
        {
            var Response = _userRepository.Dashboard_backup(cmpId, empID);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }

        public dynamic EventDetails(int cmpId, int empId, string forDate)
        {
            var Response = _userRepository.EventDetails(cmpId, empId, forDate);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }

        public dynamic GeoLocationRecords(int cmpId, int empId)
        {
            AttendanceDetails attendanceDetails1 = new();
            attendanceDetails1.EmpID = empId;
            attendanceDetails1.CmpID = cmpId;
            attendanceDetails1.Month = DateTime.Now.Month;
            attendanceDetails1.Year = DateTime.Now.Year;
            attendanceDetails1.FromDate = DateTime.Now;
            attendanceDetails1.ToDate = DateTime.Now;
            attendanceDetails1.Type = "G";

            var atendanceResponse = _attendanceRepository.AttendanceDetails(attendanceDetails1);

            if (atendanceResponse != null)
            {
                return atendanceResponse;
            }
            return null;
        }

        public dynamic GeoLocationTracking(GeoLocationRequest geoLocationRequest)
        {
            var geoLocationResponse = _userRepository.GeoLocationTracking(geoLocationRequest);
            if ((geoLocationResponse as ICollection)?.Count == 0 || geoLocationResponse == null)
            {
                return null;
            }
            return geoLocationResponse;
        }

        public dynamic GeoLocationTrackingList(int cmpId, int empId, DateTime date)
        {
            var geoLocationResponse = _userRepository.GeoLocationTrackingList(cmpId, empId, date);
            if ((geoLocationResponse as ICollection)?.Count == 0 || geoLocationResponse == null)
            {
                return null;
            }
            return geoLocationResponse;
        }

        public dynamic GetBankList(int cmpID)
        {
            var Response = _userRepository.GetBankList(cmpID);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic GetBranch(int cmpId)
        {
            EmployeeDetails empDetails = new EmployeeDetails();
            empDetails.Cmp_ID = cmpId;
            empDetails.Type = "B";
            var employeeResponse = _employeeRepository.EmployeeDetails(empDetails);
            if (employeeResponse == null || (employeeResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return employeeResponse;
        }

        public dynamic GetCity(int cmpId, int stateID)
        {
            var Response = _userRepository.GetCity(cmpId, stateID);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic GetCountry()
        {
            var Response = _userRepository.GetCountry();
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic GetDashboardApplicationsCount(int cmpId, int empId)
        {
            var Response = _userRepository.GetDashboardApplicationsCount(cmpId, empId);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic GetEmployeeOTDetails(int cmpId, int empId)
        {
            var Response = _userRepository.GetEmployeeOTDetails(cmpId, empId);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic GetHolidayList(int cmpId, int empId, int year)
        {
            var Response = _userRepository.GetHolidayList(cmpId, empId, year);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic GetLikeCommentList(GetLikeCommentListRequest getLikeCommentListRequest)
        {
            var getNotificatioon = _mapper.Map<GetLikeCommentListRequest, GetNotification>(getLikeCommentListRequest);
            getNotificatioon.NotificationDate = DateTime.Now;
            getNotificatioon.Fordate = Convert.ToDateTime(getLikeCommentListRequest.Date).ToString();
            getNotificatioon.strType = "N";
            var notificationResponse = _userRepository.GetNotification(getNotificatioon);
            if ((notificationResponse as ICollection)?.Count == 0 || notificationResponse == null)
            {
                return null;
            }
            return notificationResponse;
        }

        public dynamic GetNewJoiningUpdatedRecords(LeaveFilter getLikeCommentListRequest)
        {
            var Response = _userRepository.GetNewJoiningUpdatedRecords(getLikeCommentListRequest);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic GetNewsFeedDetail(int cmpId, int empId)
        {
            var Response = _userRepository.GetNewsFeedDetail(cmpId, empId);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic GetNotification(GetNotificationRequest getNotificationRequest)
        {
            var getNotificatioon = _mapper.Map<GetNotificationRequest, GetNotification>(getNotificationRequest);
            getNotificatioon.NotificationDate = DateTime.Now;
            getNotificatioon.Fordate = DateTime.Now.ToString();

            var notificationResponse = _userRepository.GetNotification(getNotificatioon);

            if ((notificationResponse as ICollection)?.Count == 0 || notificationResponse == null)
            {
                return null;
            }
            foreach (var item in notificationResponse)
            {
                if (getNotificatioon.strType == "D")
                {
                    if (item.DocType.ToString() != "Circular")
                    {
                        item.DocPath = _appSettings.ImagePath.ToString() + "App_File/" + item.Doc_Name.ToString();
                    }
                }
                else if (getNotificatioon.strType == "R")
                {
                    item.PImageName = _appSettings.ImagePath.ToString() + "App_File/EMPIMAGES/" + item.PImageName.ToString();
                    item.PImageName = _appSettings.ImagePath.ToString() + "App_File/EMPIMAGES/" + item.RImageName.ToString();
                }
                else if (getNotificatioon.strType == "G")
                {
                    item.DocPath = _appSettings.ImagePath.ToString() + "App_File/Emp_Gallery/" + item.DocPath.ToString();
                }
                else if (getNotificatioon.strType == "B")
                {
                    item.DocPath = _appSettings.ImagePath.ToString() + "App_File/EMPIMAGES/" + item.Image_Name.ToString();
                }
            }
            return notificationResponse;
        }

        public dynamic GetPostRequestEmployee(int cmpID, int loginId, string request_Type)
        {
            var Response = _userRepository.GetPostRequestEmployee(cmpID, loginId, request_Type);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic GetReasonforResignation()
        {
            var Response = _userRepository.GetReasonforResignation();
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic GetShiftDeatails(int cmpID, int empID, string forDate)
        {
            var Response = _userRepository.GetShiftDeatails(cmpID, empID, forDate);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic GetState(int cmpId, int countryId)
        {
            var Response = _userRepository.GetState(cmpId, countryId);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic GetSurveyList(int cmpId, int empId)
        {
            var Response = _userRepository.GetSurveyList(cmpId, empId);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic GetSurveyQuestionAnswerList(int cmpID, int empId, int surveyID)
        {
            var Response = _userRepository.GetSurveyQuestionAnswerList(cmpID, empId, surveyID);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic GetVertical(int cmpId, int empId, int verticalID)
        {
            EmployeeDetails empDetails = new EmployeeDetails();
            empDetails.Emp_ID = empId;
            empDetails.Cmp_ID = cmpId;
            empDetails.Vertical_ID = verticalID;
            if (empDetails.Vertical_ID == 0)
            {
                empDetails.Type = "V";
            }
            empDetails.Type = "L";
            var employeeResponse = _employeeRepository.EmployeeDetails(empDetails);
            if (employeeResponse == null || (employeeResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return employeeResponse;
        }

        public dynamic get_currency(int cmpId)
        {
            var Response = _userRepository.get_currency(cmpId);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic KilometerRateMaster(KilometerRateMasterRequest kilometerRateMasterRequest)
        {
            var Response = _userRepository.KilometerRateMaster(kilometerRateMasterRequest);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic LiveTrackingTotalDistance(int cmpId, int empId, DateTime createdDate)
        {
            var Response = _userRepository.LiveTrackingTotalDistance(cmpId, empId, createdDate);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic MatchFingerPrint(int cmpId, int empId)
        {
            var Response = _userRepository.MatchFingerPrint(cmpId, empId);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic MobileSalesStockResponse(MobileSalseStockResponseRequest mobileSalseStockResponseRequest)
        {
            var Response = _userRepository.MobileSalesStockResponse(mobileSalseStockResponseRequest);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic MoodTracker(LeaveBalanceRequest moodTracker)
        {
            var Response = _userRepository.MoodTracker(moodTracker);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic MoodTrackerActivityList()
        {
            var Response = _userRepository.MoodTrackerActivityList();
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            foreach (var item in Response)
            {
                item.Selected_ImageName = _appSettings.ImagePath.ToString() + "App_File/MOODTRACKER/" + item.Selected_ImageName.ToString();
                item.Unselected_ImageName = _appSettings.ImagePath.ToString() + "App_File/MOODTRACKER/" + item.Unselected_ImageName.ToString();
            }
            return Response;
        }

        public dynamic MoodTrackerInsert(MoodTrackerInsertRequest moodTrackerInsertRequest)
        {
            var Response = _userRepository.MoodTrackerInsert(moodTrackerInsertRequest);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic PostFingerPrintDetails(int cmpId, int empID, string base64)
        {
            var Response = _userRepository.PostFingerPrintDetails(empID,cmpId,base64);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic PostRequest(PostRequest postRequest)
        {
            postRequest.StrType = "";
            var Response = _userRepository.PostRequest(postRequest);

            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic SalaryDetails(LeaveBalanceRequest salaryDetails)
        {
            var Response = _userRepository.SalaryDetails(salaryDetails);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic SendNotification(SendNotificationRequest sendNotificationRequest)
        {
            //dynamic EmpId;
            //if (sendNotificationRequest.DeptID != 0)
            //{
            //    //strDepID = strDepID.Replace("#", ",");
            //    var EmpIds = _userRepository.GetEmpIDFromDeptID(sendNotificationRequest.DeptID);

            //    if (EmpIds.Rows.Count > 0)
            //    {
            //        EmpIds = EmpIds.Rows[0]["EmpList"].ToString();
            //    }
            //    EmpIds = null;
            //}

            //if (sendNotificationRequest.CmpID != 0 )
            //{
            //    //strCmpID = strCmpID.Replace("#", ",");
            //    EmpId = _userRepository.GetEmpIDFromCmpID(sendNotificationRequest.CmpID);

            //    if (EmpId.Rows.Count > 0)
            //    {
            //        EmpId = EmpId.Rows[0]["EmpList"].ToString();
            //    }
            //    EmpId = null;
            //}

            ////if (!string.IsNullOrEmpty(EmpId))
            ////{
            ////    strEmpID = strEmpID.Replace("#", ",");
            ////}

            //var deviceId = _userRepository.GetDeviceIdFromEmpID(EmpId[0]);
            //deviceId = new string[deviceId.Rows.Count];
            //if (deviceId.Rows.Count > 0)
            //{
            //    int J = 0;
            //    int NotificationGroup = 500;

            //    for (int i = 0; i < deviceId.Rows.Count; i++)
            //    {
            //        if (!string.IsNullOrEmpty(deviceId.Rows[i]["DeviceID"].ToString()))
            //        {
            //            if (J < NotificationGroup)
            //            {
            //                deviceId[J] = deviceId.Rows[i]["DeviceID"].ToString();
            //                Array objArray = _userRepository.SendNotification(sendNotificationRequest.Title, sendNotificationRequest.Body,deviceId[J], "");

            //                J++;

            //                // If ((J == NotificationGroup) Or i == (dtDeviceID.Rows.Count - 1)) Then
            //                // {
            //                //     J = 0;
            //                //     objArray = oClsMobileHRMS.SendNotification(strTitle, strBody, strDeviceID[J], "");
            //                //     strDeviceID = new string[NotificationGroup - 1];
            //                // }
            //            }
            //        }
            //    }
            //}


            ////var Response = _userRepository.GetEmpIDFromDeptID(sendNotificationRequest.DeptID);
            //var Response = _userRepository.GetDeviceIdFromEmpID(sendNotificationRequest.EmpID);

            //if (Response == null || (Response as ICollection)?.Count == 0)
            //{
            //    return null;
            //}
            //return Response;
            return null;
        }
        public static bool IsXmlFormat(string input)
        {
            try
            {
                // Attempt to parse the input string as an XML document
                XDocument.Parse(input);
                return true; // Parsing succeeded, so it's in XML format
            }
            catch (Exception)
            {
                try
                {
                    // Attempt to parse the input string as an XML element
                    XElement.Parse(input);
                    return true; // Parsing succeeded, so it's in XML format
                }
                catch (Exception)
                {
                    return false; // Parsing failed, so it's not in XML format
                }
            }
        }
        public dynamic SurveyApplication(SurveyApplicationRequest surveyApplicationRequest)
        {
            if (IsXmlFormat(surveyApplicationRequest.SurveyDetails))
            {
                var Response = _userRepository.SurveyApplication(surveyApplicationRequest);
                if ((Response as ICollection)?.Count == 0 || Response == null)
                {
                    return null;
                }
                return Response;
            }
            else
            {
                return "Invalid data passed.";
            }
            
        }

        public dynamic TemplateApplicationDetails(TemplateApplicationDetailsRequest templateApplicationDetailsRequest)
        {
            var Response = _userRepository.TemplateApplicationDetails(templateApplicationDetailsRequest);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic TemplateFieldData(TemplateFieldDataRequest templateFieldDataRequest)
        {
            var Response = _userRepository.TemplateFieldData(templateFieldDataRequest);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic TemplateFieldDataView(TemplateFieldDataViewRequest templateFieldDataRequest)
        {
            var Response = _userRepository.TemplateFieldDataView(templateFieldDataRequest);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic Trigger_FCM(Trigger_FCMRequest trigger_FCMRequest)
        {
            string FCM_API_Path = _appSettings.FCM_API_Path;
            string Server_API_Key = _appSettings.Server_API_Key;
            string Sender_API_ID = _appSettings.Sender_API_ID;

            var tRequest = WebRequest.Create(FCM_API_Path);
            tRequest.Method = "POST";
            tRequest.ContentType = "application/json";

            var data = new
            {
                to = trigger_FCMRequest.DeviceID,
                notification = new
                {
                    body = trigger_FCMRequest.Body,
                    title = trigger_FCMRequest.Title,
                    sound = "Enabled"
                }
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var byteArray = Encoding.UTF8.GetBytes(json);

            tRequest.Headers.Add($"Authorization: key={Server_API_Key}");
            tRequest.Headers.Add($"Sender: id={Sender_API_ID}");
            tRequest.ContentLength = byteArray.Length;

            using (var dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);

                using (var tResponse = tRequest.GetResponse())
                using (var dataStreamResponse = tResponse.GetResponseStream())
                using (var tReader = new StreamReader(dataStreamResponse))
                {
                    string sResponseFromServer = tReader.ReadToEnd();
                    return sResponseFromServer;
                }
            }
        }

        public dynamic UnisonMaster(int cmpId, int empId, string master)
        {
            var Response = _userRepository.UnisonMaster(cmpId,empId,master);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic UpdateBankDetails(UpdateBankDetailsRequest updateBankDetailsRequest)
        {
            var Response = _userRepository.UpdateBankDetails(updateBankDetailsRequest);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic UploadDocument(UploadDocumentRequest updateBankDetailsRequest)
        {
            string strDocPath = _appSettings.DocPath.ToString();

            if (!string.IsNullOrEmpty(updateBankDetailsRequest.FilePath))
            {
                updateBankDetailsRequest.DocName = $"{updateBankDetailsRequest.CmpID}_{updateBankDetailsRequest.EmpID}_{DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_")}_Doc_{updateBankDetailsRequest.DocName}";
                strDocPath = Path.Combine(strDocPath, updateBankDetailsRequest.DocName);
                byte[] docBytes = Convert.FromBase64String(updateBankDetailsRequest.FilePath);
                using (MemoryStream ms = new MemoryStream(docBytes))
                {
                    using (FileStream fs = new FileStream(strDocPath, FileMode.Create))
                    {
                        ms.WriteTo(fs);
                    }
                }
            }

            //if (!string.IsNullOrEmpty(updateBankDetailsRequest.DocName))
            //{
            //    string docNameWithTimestamp = $"{updateBankDetailsRequest.CmpID}_{updateBankDetailsRequest.EmpID}_{DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_")}_Doc_{updateBankDetailsRequest.DocName}";
            //    string filePath = Path.Combine(updateBankDetailsRequest.FilePath, docNameWithTimestamp);

            //    byte[] docBytes = Convert.FromBase64String(updateBankDetailsRequest.FilePath);

            //    using (MemoryStream ms = new MemoryStream(docBytes))
            //    {
            //        using (FileStream fs = new FileStream(filePath, FileMode.Create))
            //        {
            //            ms.WriteTo(fs);
            //        }
            //    }
            //}

            var Response = _userRepository.UploadDocument(updateBankDetailsRequest);
            if (Response == null || (Response as ICollection)?.Count == 0)
            {
                return null;
            }
            return Response;
        }

        public dynamic TemplateApplication(TemplateApplicationRequest templateApplicationDetailsRequest)
        {

            foreach (var item in templateApplicationDetailsRequest.TemplateDetails)
            {
                if (item.Field_Type.ToString() == "File Upload")
                {
                    if (!string.IsNullOrEmpty(item.Answer.ToString()))
                    {
                        string strDocName = $"{DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_")}_TemplateDoc.jpg";
                        string strDocPath = $"{_appSettings.DocPath.ToString()}/TemplateDoc/{strDocName}";
                        byte[] docBytes = Convert.FromBase64String(item.Answer.ToString());
                        using (MemoryStream ms = new MemoryStream(docBytes))
                        {
                            using (FileStream fs = new FileStream(strDocPath, FileMode.Create))
                            {
                                ms.WriteTo(fs);
                            }
                        }
                        item.Answer = strDocPath;
                    }
                }
            }

            // Create XML structure
            XElement rootElement = new XElement("TemplateDetails");

            // Loop through each object in the JSON array
            foreach (var detail in templateApplicationDetailsRequest.TemplateDetails)
            {
                XElement detailElement = new XElement("Detail",
                    new XElement("T_ID", detail.T_ID),
                    new XElement("Answer", detail.Answer),
                    new XElement("Field_Name", detail.Field_Name),
                    new XElement("F_ID", detail.F_ID),
                    new XElement("Field_Type", detail.Field_Type)
                );
                rootElement.Add(detailElement);
            }

            // Create XML document with the root element
            XDocument xmlDocument = new XDocument(rootElement);

            if (xmlDocument != null)
            {
                var Response = _userRepository.TemplateApplication(templateApplicationDetailsRequest, xmlDocument);
                if ((Response as ICollection)?.Count == 0 || Response == null)
                {
                    return null;
                }
                return Response;
            }
            else
            {
                return "Invalid data passed.";
            }
        }
    }
}
