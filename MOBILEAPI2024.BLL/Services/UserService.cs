using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO;
using MOBILEAPI2024.DTO.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddClockIn(ClockIn clockIn)
        {
            _userRepository.AddClockIn(clockIn);
        }

        public void AddClockOut(ClockIn clockIn)
        {
            _userRepository.AddClockOut(clockIn);
        }

        public void AddTransactionData(Transaction transactionRequest)
        {
            _userRepository.AddTransactionData(transactionRequest);
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
            if(transactiondata)
            {
                return true;
            }
            return false;
        }

        public DashboardDTO DashboardData(string empId, string cmpId)
        {
            var dashboardData = _userRepository.DashboardData(empId,cmpId);
            if (dashboardData != null)
            {
                return dashboardData;
            }
            return null;
        }
    }
}
