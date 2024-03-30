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
    }
}
