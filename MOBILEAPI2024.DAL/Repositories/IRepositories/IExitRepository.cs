using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DTO.RequestDTO.Exit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DAL.Repositories.IRepositories
{
    public interface IExitRepository : IGenericRepository<ActiveInActiveUser>
    {
        dynamic AddExitAppilcation(AddExitAppilcationRequest addExitAppilcationRequest);
        dynamic ExitAppInsert(ExitAppInsertRequest exitAppInsertRequest, int qUEST_ID, int answer_rate, string comments);
        dynamic ExitApplicationDelete(ExitApplicationDeleteRequest exitApplicationDeleteRequest);
        dynamic ExitApplicationNoticePeriod(ExitApplicationNoticePeriodRequest exitApplicationNoticePeriodRequest);
        dynamic ExitApplicationPreQuestion(int cmpId, int branchID);
        dynamic GetExitApporvalRecords(int cmpID, int empID, string status);
        dynamic GetExitApprovalEMPData(GetExitApprovalEMPDataRequest getExitApprovalEMPDataRequest);
        dynamic GetExitInterviewQAInterview(int cmpId, int empId, int exitId);
    }
}
