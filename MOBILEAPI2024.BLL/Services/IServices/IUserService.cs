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
        dynamic GeoLocationTracking(GeoLocationRequest geoLocationRequest);
        dynamic GeoLocationTrackingList(int v1, int v2, DateTime date);
    }
}
