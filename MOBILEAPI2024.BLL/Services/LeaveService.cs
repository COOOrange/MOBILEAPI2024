using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.ResponseDTO.Leave;
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

        public dynamic GetCompOffLeave(GetCompOffLeaveRequest getCompOffLeaveRequest)
        {
            var getCompOffData = _leaveRepository.GetCompOffLeave(getCompOffLeaveRequest);
            if (getCompOffData == null)
            {
                return null;
            }
            return getCompOffData;
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

        public dynamic GetLeavetransactionRecords(GetLeaveTransactionRequest getLeaveTransactionRequest)
        {
            var getLeaveTransaction = _leaveRepository.GetLeavetransactionRecords(getLeaveTransactionRequest);
            if (getLeaveTransaction == null)
            {
                return null;
            }
            return getLeaveTransaction;
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

        public LeaveApplicationRecordsResponse LeaveApplicationRecords(LeaveApplicationRecordsRequest leaveApplicationRecordsRequest)
        {
            var leaveApprovalDetails = _leaveRepository.LeaveApplicationRecords(leaveApplicationRecordsRequest);
            if (leaveApprovalDetails == null)
            {
                return null;
            }
            return leaveApprovalDetails;
        }

        public dynamic LeaveApproval(LeaveApprovalRequest leaveApprovalRequest)
        {
            var leaveApprovalDetails = _leaveRepository.LeaveApproval(leaveApprovalRequest);
            if (leaveApprovalDetails == null)
            {
                return null;
            }
            return leaveApprovalDetails;
        }

        public dynamic LeaveApprovalDelete(LeaveApprovalDeleteRequest leaveApprovalDeleteRequest)
        {
            var leaveApprovalDetails = _leaveRepository.LeaveApprovalDelete(leaveApprovalDeleteRequest);
            if (leaveApprovalDetails == null)
            {
                return null;
            }
            return leaveApprovalDetails;
        }

        public LeaveApprovalDetailsResponse LeaveApprovalDetails(LeaveApprovalDetailsRequest leaveApprovalDetailsRequest)
        {
            var leaveApprovalDetails = _leaveRepository.LeaveApprovalDetails(leaveApprovalDetailsRequest);
            if (leaveApprovalDetails == null)
            {
                return null;
            }
            return leaveApprovalDetails;
        }

        public LeaveBalanceSummryResponse LeaveBalanceSummary(LeaveFilter leaveFilter, DateTime forDate)
        {
            var LeaveBalanceSummary = _leaveRepository.LeaveBalanceSummary(leaveFilter, forDate);
            if (LeaveBalanceSummary == null)
            {
                return null;
            }
            return LeaveBalanceSummary;
        }

        public dynamic LeaveCancellationApplication(LeaveCancellationApplicationRequest leaveCancellationApplicationRequest)
        {
            string Type = "I";
            var LeaveCancelApplication = _leaveRepository.LeaveCancellationApplication(leaveCancellationApplicationRequest, Type);
            if (LeaveCancelApplication == null)
            {
                return null;
            }
            return LeaveCancelApplication;
        }

        public LeaveCancellationApplicationDetailsResponse LeaveCancellationApplicationDetails(LeaveCancellationApplicationDetailsRequest leaveCancellationApplicationDetailsRequest)
        {
            var LeaveCancelApplication = _leaveRepository.LeaveCancellationApplicationDetails(leaveCancellationApplicationDetailsRequest);
            if (LeaveCancelApplication == null)
            {
                return null;
            }
            return LeaveCancelApplication;
        }

        public dynamic LeaveCancellationApproval(LeaveCancellationApplicationRequest LeaveCancellationApplicationRequest)
        {
            string Type = "A";
            var leaveCancelApprovalDetails = _leaveRepository.LeaveCancellationApplication(LeaveCancellationApplicationRequest, Type);
            if (leaveCancelApprovalDetails == null)
            {
                return null;
            }
            return leaveCancelApprovalDetails;
        }

    }
}
