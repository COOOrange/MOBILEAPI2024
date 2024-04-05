﻿using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.RequestDTO.User;
using MOBILEAPI2024.DTO.ResponseDTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.BLL.Services.IServices
{
    public interface IUserService
    {
        void AddClockIn(ClockIn clockIn);
        void AddClockOut(ClockIn clockIn);
        dynamic AddComment(AddCommentRequest addCommentRequest);
        dynamic AddLike(strDetails item);
        void AddTransactionData(Transaction transactionRequest);
        dynamic AddWorkPlanOnClockIn(AddWorkPlanOnClockInRequest addWorkPlanOnClockInRequest);
        dynamic AddWorkPlanOnClockOut(AddWorkPlanOnClockInRequest addWorkPlanOnClockInRequest);
        dynamic ChangeRequestBind(int v1, int v2, string tranType);
        dynamic ChangeRequestDepDelete(int v1, int v2, int row_ID);
        dynamic ChangeRequestDepInsert(ChangeRequestDepInsertRequest changeRequestDepInsert);
        dynamic ChangeRequestDepUpdate(ChangeRequestDepInsertRequest changeRequestDepInsert);
        dynamic ChangeRequestFavDelete(int v1, int v2, int row_ID);
        dynamic ChangeRequestFavInsert(ChangeRequestFavInsertRequest changeRequestDepInsert);
        dynamic ChangeRequestFavUpdate(ChangeRequestFavInsertRequest changeRequestDepInsert);
        bool CheckEnrollNoExixts(Transaction transaction);
        bool CheckTransactionExistence(Transaction transaction);
        DashboardDTO DashboardData(string empId, string cmpId);
        dynamic Dashboard_backup(int v1, int v2);
        dynamic EventDetails(int v1, int v2, string forDate);
        dynamic GeoLocationRecords(int v1, int v2);
        dynamic GeoLocationTracking(GeoLocationRequest geoLocationRequest);
        dynamic GeoLocationTrackingList(int v1, int v2, DateTime date);
        dynamic GetBranch(int v);
        dynamic GetDashboardApplicationsCount(int v1, int v2);
        dynamic GetEmployeeOTDetails(int v1, int v2);
        dynamic GetHolidayList(int v1, int v2, int year);
        dynamic GetLikeCommentList(GetLikeCommentListRequest getLikeCommentListRequest);
        dynamic GetNewJoiningUpdatedRecords(LeaveFilter getLikeCommentListRequest);
        dynamic GetNewsFeedDetail(int v1, int v2);
        dynamic GetNotification(GetNotificationRequest getNotificationRequest);
        dynamic GetPostRequestEmployee(int v1, int v2, string request_Type);
        dynamic GetShiftDeatails(int v1, int v2, string forDate);
        dynamic GetSurveyList(int v1, int v2);
        dynamic GetSurveyQuestionAnswerList(int v1, int v2, int surveyID);
        dynamic GetVertical(int v1, int v2, int verticalID);
        dynamic KilometerRateMaster(KilometerRateMasterRequest kilometerRateMasterRequest);
        dynamic LiveTrackingTotalDistance(int v1, int v2, DateTime createdDate);
        dynamic MatchFingerPrint(int v1, int v2);
        dynamic MobileSalesStockResponse(MobileSalseStockResponseRequest mobileSalseStockResponseRequest);
        dynamic MoodTracker(LeaveBalanceRequest moodTracker);
        dynamic MoodTrackerActivityList();
        dynamic MoodTrackerInsert(MoodTrackerInsertRequest moodTrackerInsertRequest);
        dynamic PostFingerPrintDetails(int v1, int v2, string base64);
        dynamic PostRequest(PostRequest postRequest);
        dynamic SalaryDetails(LeaveBalanceRequest salaryDetails);
        dynamic TemplateApplicationDetails(TemplateApplicationDetailsRequest templateApplicationDetailsRequest);
        dynamic TemplateFieldData(TemplateFieldDataRequest templateFieldDataRequest);
        dynamic TemplateFieldDataView(TemplateFieldDataViewRequest templateFieldDataRequest);
        dynamic UnisonMaster(int v1, int v2, string master);
    }
}
