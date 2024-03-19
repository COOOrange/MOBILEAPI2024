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
    public class LeaveService : ILeaveService
    {
        private readonly ILeaveRepository _leaveRepository;
        public LeaveService(ILeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }

        public string AddLeaveAplication(LeaveFilter leaveFilter, ApplyLeaveRequest applyLeaveRequest)
        {
            string applyLeave = _leaveRepository.AddLeaveAplication(leaveFilter, applyLeaveRequest);
            return applyLeave;
        }

        public LeaveApplicationResponse CheckLeaveStatus(LeaveFilter leaveFilter, int leaveAppID)
        {
            var checkLeaveStatus = _leaveRepository.CheckLeaveStatus(leaveFilter, leaveAppID);
            return checkLeaveStatus;
        }

        public string CheckPeriod(LeaveFilter leaveFilter, CheckPeriod checkPeriod)
        {
            string period = _leaveRepository.CheckPeriod(leaveFilter,checkPeriod);
            return period;
        }

        public List<FilterLeaveResponse> GetLeaveBalance(LeaveFilter leaveFilter)
        {
            var filterLeaveBalance = _leaveRepository.GetLeaveBalance(leaveFilter);
            if(filterLeaveBalance == null)
            {
                return null;
            }
            return filterLeaveBalance;
        }

        public List<LeaveStatusResponse> GetLeaveStatus(LeaveFilter leaveFilter)
        {
            var filterLeaveBalance = _leaveRepository.GetLeaveStatus(leaveFilter);
            if (filterLeaveBalance == null)
            {
                return null;
            }
            return filterLeaveBalance;
        }

        public List<LeaveTypeBind> GetLeaveTypeBind(LeaveFilter leaveFilter)
        {
            var LeaveBindType = _leaveRepository.GetLeaveTypeBind(leaveFilter);
            if (LeaveBindType == null)
            {
                return null;
            }
            return LeaveBindType;
        }
    }
}
