using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DTO.RequestDTO;
using MOBILEAPI2024.DTO.ResponseDTO;

namespace MOBILEAPI2024.DAL.Repositories.IRepositories
{
    public interface IUserRepository : IGenericRepository<ActiveInActiveUser>
    {
        void AddClockIn(ClockIn clockIn);
        void AddClockOut(ClockIn clockIn);
        void AddTransactionData(Transaction transactionRequest);
        bool CheckEnrollNoExixts(Transaction transaction);
        bool CheckTransactionExistence(Transaction transaction);
        DashboardDTO DashboardData(string empId, string cmpId);
    }
}
