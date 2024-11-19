using Microsoft.Extensions.Options;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.Common;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.ResponseDTO.Leave;
using System.Collections;

namespace MOBILEAPI2024.BLL.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly ILeaveRepository _leaveRepository;
        private readonly AppSettings _appSettings;
        private readonly AccountService _accountService;
        public LeaveService(ILeaveRepository leaveRepository,IOptions<AppSettings> appSetting, AccountService accountService)
        {
            _leaveRepository = leaveRepository;
            _appSettings = appSetting.Value;
            _accountService = accountService;
        }

        public LeaveResponse AddLeaveAplication(LeaveFilter leaveFilter, ApplyLeaveRequest applyLeaveRequest, string deviceId)
        {
            if (!string.IsNullOrEmpty(applyLeaveRequest.Attachement))
            {
                string strDocName = $"{DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + "_Doc_" + applyLeaveRequest.DocName.Replace(" ", "_")}";
                string strDocPath = $"{_appSettings.DocPath}/LeaveDocs/{strDocName}";

                byte[] docBytes = Convert.FromBase64String(applyLeaveRequest.Attachement);
                using (MemoryStream ms = new MemoryStream(docBytes))
                {
                    using (FileStream fs = new FileStream(strDocPath, FileMode.Create))
                    {
                        ms.WriteTo(fs);
                    }
                }
            }

            MasterLeaveResponse leaveResponse = _leaveRepository.AddLeaveAplicationMain(leaveFilter, applyLeaveRequest);
            if(leaveResponse.LeaveResponse == null || (leaveResponse.LeaveResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            string message = "Leave for " + leaveResponse.LeaveAPIResponse.EmpFullName + " [ " +
                             leaveResponse.LeaveAPIResponse.LeaveName + " ] of " +
                             leaveResponse.LeaveAPIResponse.LeavePeriod + " days from " +
                             DateTime.Parse(leaveResponse.LeaveAPIResponse.FromDate).ToString("dd-MMM-yyyy") + " To " +
                             DateTime.Parse(leaveResponse.LeaveAPIResponse.ToDate).ToString("dd-MMM-yyyy") + " is received";

            _accountService.SendPushNotificationAsync(deviceId, "Leave Application",message);
            return leaveResponse.LeaveResponse;
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
