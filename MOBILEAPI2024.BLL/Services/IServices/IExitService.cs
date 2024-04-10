using MOBILEAPI2024.DTO.RequestDTO.Exit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.BLL.Services.IServices
{
    public interface IExitService
    {
        dynamic AddExitAppilcation(AddExitAppilcationRequest addExitAppilcationRequest);
        dynamic AddExitApprovalData(AddExitApprovaldataRequest addExitApprovaldataRequest);
        dynamic ExitAppInsert(ExitAppInsertRequest exitAppInsertRequest);
        dynamic ExitApplicationDelete(ExitApplicationDeleteRequest exitApplicationDeleteRequest);
        dynamic ExitApplicationNoticePeriod(ExitApplicationNoticePeriodRequest exitApplicationNoticePeriodRequest);
        dynamic ExitApplicationPreQuestion(int v, int branchID);
        dynamic ExitApplicationValidate(int v1, int v2);
        dynamic GetExitApplicationRecords(int v1, int v2);
        dynamic GetExitApporvalRecords(int v1, int v2, string status);
        dynamic GetExitApprovalEMPData(GetExitApprovalEMPDataRequest getExitApprovalEMPDataRequest);
        dynamic GetExitInterviewQAInterview(int v1, int v2, int exitId);
        dynamic GetExitTermsandConditions(int v);
    }
}
