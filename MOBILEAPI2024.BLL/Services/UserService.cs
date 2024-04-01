using Microsoft.VisualBasic;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.Ticket;
using MOBILEAPI2024.DTO.RequestDTO.User;
using MOBILEAPI2024.DTO.ResponseDTO.User;
using System;
using System.Collections;
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
            var geoLocationResponse = _userRepository.GeoLocationTrackingList(cmpId,empId,date);
            if ((geoLocationResponse as ICollection)?.Count == 0 || geoLocationResponse == null)
            {
                return null;
            }
            return geoLocationResponse;
        }
    }
}
