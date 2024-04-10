using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.ResponseDTO.Leave;
using System.Collections;

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
            string leaveResponse = _leaveRepository.AddLeaveAplication(leaveFilter, applyLeaveRequest);
            if(leaveResponse == null)
            {
                return null;
            }
            return leaveResponse;
        }

        public LeaveApplicationResponse CheckLeaveStatus(LeaveFilter leaveFilter, int leaveAppID)
        {
            var leaveResponse = _leaveRepository.CheckLeaveStatus(leaveFilter, leaveAppID);
            if(leaveResponse == null || (leaveResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return leaveResponse;
        }

        public string CheckPeriod(LeaveFilter leaveFilter, CheckPeriod checkPeriod)
        {
            string leaveResponse = _leaveRepository.CheckPeriod(leaveFilter,checkPeriod);
            if(leaveResponse == null)
            {
                return null;
            }
            return leaveResponse;
        }

        public dynamic GetCompOffLeave(GetCompOffLeaveRequest getCompOffLeaveRequest)
        {
            var leaveResponse = _leaveRepository.GetCompOffLeave(getCompOffLeaveRequest);
            if(leaveResponse == null || (leaveResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return leaveResponse;
        }

        public List<FilterLeaveResponse> GetLeaveBalance(LeaveFilter leaveFilter)
        {
            var leaveResponse = _leaveRepository.GetLeaveBalance(leaveFilter);
            if(leaveResponse == null)
            {
                return null;
            }
            return leaveResponse;
        }

        public List<LeaveStatusResponse> GetLeaveStatus(LeaveFilter leaveFilter)
        {
            var leaveResponse = _leaveRepository.GetLeaveStatus(leaveFilter);
            if(leaveResponse == null || (leaveResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return leaveResponse;
        }

        public dynamic GetLeavetransactionRecords(GetLeaveTransactionRequest getLeaveTransactionRequest)
        {
            var leaveResponse = _leaveRepository.GetLeavetransactionRecords(getLeaveTransactionRequest);
            if(leaveResponse == null || (leaveResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return leaveResponse;
        }

        public List<LeaveTypeBind> GetLeaveTypeBind(LeaveFilter leaveFilter)
        {
            var leaveResponse = _leaveRepository.GetLeaveTypeBind(leaveFilter);
            if(leaveResponse == null || (leaveResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return leaveResponse;
        }

        public LeaveApplicationRecordsResponse LeaveApplicationRecords(LeaveApplicationRecordsRequest leaveApplicationRecordsRequest)
        {
            var leaveResponse = _leaveRepository.LeaveApplicationRecords(leaveApplicationRecordsRequest);
            if(leaveResponse == null || (leaveResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return leaveResponse;
        }

        public dynamic LeaveApproval(LeaveApprovalRequest leaveApprovalRequest)
        {
            var leaveResponse = _leaveRepository.LeaveApproval(leaveApprovalRequest);
            if(leaveResponse == null || (leaveResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return leaveResponse;
        }

        public dynamic LeaveApprovalDelete(LeaveApprovalDeleteRequest leaveApprovalDeleteRequest)
        {
            var leaveResponse = _leaveRepository.LeaveApprovalDelete(leaveApprovalDeleteRequest);
            if(leaveResponse == null || (leaveResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return leaveResponse;
        }

        public LeaveApprovalDetailsResponse LeaveApprovalDetails(LeaveApprovalDetailsRequest leaveApprovalDetailsRequest)
        {
            var leaveResponse = _leaveRepository.LeaveApprovalDetails(leaveApprovalDetailsRequest);
            if(leaveResponse == null || (leaveResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return leaveResponse;
        }

        public LeaveBalanceSummryResponse LeaveBalanceSummary(LeaveFilter leaveFilter, DateTime forDate)
        {
            var leaveResponse = _leaveRepository.LeaveBalanceSummary(leaveFilter, forDate);
            if(leaveResponse == null || (leaveResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return leaveResponse;
        }

        public dynamic LeaveCancellationApplication(LeaveCancellationApplicationRequest leaveCancellationApplicationRequest)
        {
            string Type = "I";
            var leaveResponse = _leaveRepository.LeaveCancellationApplication(leaveCancellationApplicationRequest, Type);
            if(leaveResponse == null || (leaveResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return leaveResponse;
        }

        public LeaveCancellationApplicationDetailsResponse LeaveCancellationApplicationDetails(LeaveCancellationApplicationDetailsRequest leaveCancellationApplicationDetailsRequest)
        {
            var leaveResponse = _leaveRepository.LeaveCancellationApplicationDetails(leaveCancellationApplicationDetailsRequest);
            if(leaveResponse == null || (leaveResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return leaveResponse;
        }

        public dynamic LeaveCancellationApproval(LeaveCancellationApplicationRequest LeaveCancellationApplicationRequest)
        {
            string Type = "A";
            var leaveResponse = _leaveRepository.LeaveCancellationApplication(LeaveCancellationApplicationRequest, Type);
            if(leaveResponse == null || (leaveResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return leaveResponse;
        }

        public dynamic LeaveTravelTypeDdl(int grd_ID)
        {
            var leaveResponse = _leaveRepository.LeaveTravelTypeDdl(grd_ID);
            if (leaveResponse == null || (leaveResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return leaveResponse;
        }
    }
}
