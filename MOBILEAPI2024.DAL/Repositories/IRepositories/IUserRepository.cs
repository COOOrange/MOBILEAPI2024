using MOBILEAPI2024.DAL.Entities;
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
        dynamic GeoLocationTracking(GeoLocationRequest geoLocationRequest);
        dynamic GeoLocationTrackingList(int cmpId, int empId, DateTime date);
        dynamic GetNotification(GetNotification getNotificatioon);
    }
}
