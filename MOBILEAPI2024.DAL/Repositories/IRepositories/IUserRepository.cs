﻿using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.RequestDTO.User;
using MOBILEAPI2024.DTO.ResponseDTO.User;
using System.Collections;

namespace MOBILEAPI2024.DAL.Repositories.IRepositories
{
    public interface IUserRepository : IGenericRepository<ActiveInActiveUser>
    {
        void AddClockIn(ClockIn clockIn);
        void AddClockOut(ClockIn clockIn);
        void AddTransactionData(Transaction transactionRequest);
        dynamic AddWorkPlanOnClockIn(AddWorkPlanOnClockInRequest addWorkPlanOnClockInRequest);
        dynamic AddWorkPlanOnClockOut(AddWorkPlanOnClockInRequest addWorkPlanOnClockInRequest);
        dynamic ChangeRequest(ChangeRequest changeRequest);
        dynamic ChangeRequestBind(int cmpId, int empID, string tranType);
        dynamic ChangeRequestFav(ChangeRequestFav changeRequest);
        bool CheckEnrollNoExixts(Transaction transaction);
        bool CheckTransactionExistence(Transaction transaction);
        DashboardDTO DashboardData(string empId, string cmpId);
        dynamic Dashboard_backup(int cmpId, int empID);
        dynamic EventDetails(int cmpId, int empId, string forDate);
        dynamic GeoLocationTracking(GeoLocationRequest geoLocationRequest);
        dynamic GeoLocationTrackingList(int cmpId, int empId, DateTime date);
        dynamic GetDashboardApplicationsCount(int cmpId, int empId);
        dynamic GetEmployeeOTDetails(int cmpId, int empId);
        dynamic GetHolidayList(int cmpId, int empId, int year);
        dynamic GetNewJoiningUpdatedRecords(LeaveFilter getLikeCommentListRequest);
        dynamic GetNewsFeedDetail(int cmpId, int empId);
        dynamic GetNotification(GetNotification getNotificatioon);
        dynamic GetPostRequestEmployee(int cmpID, int empId, string request_Type);
        dynamic GetShiftDeatails(int cmpID, int empID, string forDate);
        dynamic GetSurveyList(int cmpId, int empId);
        dynamic GetSurveyQuestionAnswerList(int cmpID, int empId, int surveyID);
        dynamic KilometerRateMaster(KilometerRateMasterRequest kilometerRateMasterRequest);
        dynamic LiveTrackingTotalDistance(int cmpId, int empId, DateTime createdDate);
        dynamic MatchFingerPrint(int cmpId, int empId);
        dynamic MobileSalesStockResponse(MobileSalseStockResponseRequest mobileSalseStockResponseRequest);
        dynamic MoodTracker(LeaveBalanceRequest moodTracker);
        dynamic MoodTrackerActivityList();
        dynamic MoodTrackerInsert(MoodTrackerInsertRequest moodTrackerInsertRequest);
        dynamic PostFingerPrintDetails(int empID, int cmpId, string base64);
        dynamic PostRequest(PostRequest postRequest);
        dynamic SalaryDetails(LeaveBalanceRequest salaryDetails);
        dynamic TemplateApplicationDetails(TemplateApplicationDetailsRequest templateApplicationDetailsRequest);
        dynamic TemplateFieldData(TemplateFieldDataRequest templateFieldDataRequest);
        dynamic TemplateFieldDataView(TemplateFieldDataViewRequest templateFieldDataRequest);
        dynamic UnisonMaster(int cmpId, int empId, string master);
    }
}
