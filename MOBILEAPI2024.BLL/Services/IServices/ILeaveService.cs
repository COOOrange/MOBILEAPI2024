using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.ResponseDTO.Leave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.BLL.Services.IServices
{
    public interface ILeaveService
    {
        dynamic AddLeaveAplication(LeaveFilter leaveFilter, ApplyLeaveRequest applyLeaveRequest,string deviceId);
        LeaveApplicationResponse CheckLeaveStatus(LeaveFilter leaveFilter, int leaveAppID);
        string CheckPeriod(LeaveFilter leaveFilter,CheckPeriod checkPeriod);
        dynamic GetCompOffLeave(GetCompOffLeaveRequest getCompOffLeaveRequest);
        List<FilterLeaveResponse> GetLeaveBalance(LeaveFilter leaveFilter);
        List<LeaveStatusResponse> GetLeaveStatus(LeaveFilter leaveFilter);
        dynamic GetLeavetransactionRecords(GetLeaveTransactionRequest getLeaveTransactionRequest);
        List<LeaveTypeBind> GetLeaveTypeBind(LeaveFilter leaveFilter);
        LeaveApplicationRecordsResponse LeaveApplicationRecords(LeaveApplicationRecordsRequest leaveApplicationRecordsRequest);
        dynamic LeaveApproval(LeaveApprovalRequest leaveApprovalRequest);
        dynamic LeaveApprovalDelete(LeaveApprovalDeleteRequest leaveApprovalDeleteRequest);
        LeaveApprovalDetailsResponse LeaveApprovalDetails(LeaveApprovalDetailsRequest leaveApprovalDetailsRequest);
        LeaveBalanceSummryResponse LeaveBalanceSummary(LeaveFilter leaveFilter, DateTime forDate);
        dynamic LeaveCancellationApplication(LeaveCancellationApplicationRequest leaveCancellationApplicationRequest);
        LeaveCancellationApplicationDetailsResponse LeaveCancellationApplicationDetails(LeaveCancellationApplicationDetailsRequest leaveCancellationApplicationDetailsRequest);
        dynamic LeaveCancellationApproval(LeaveCancellationApplicationRequest leaveCancellationApplicationRequest);
        dynamic LeaveTravelTypeDdl(int grd_ID);
    }
}
