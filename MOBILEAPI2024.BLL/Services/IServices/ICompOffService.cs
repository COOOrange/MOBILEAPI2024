using MOBILEAPI2024.DTO.RequestDTO.CompOff;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.BLL.Services.IServices
{
    public interface ICompOffService
    {
        dynamic CompOffApplication(CompOffApplicationRequest compOffApplicationRequest);
        dynamic CompOffApplicationDelete(int v1, int v2, int compoffAppID);
        dynamic CompOffApproval(CompOffApprovalRequest compOffApprovalRequest);
        dynamic CompOffApprovalDelete(int v, int compoffAppID);
        dynamic GetCompOffApplicationDetails(int compoffAppID);
        dynamic GetCompoffApplicationRecord(int v1, int v2,string StrType);
        dynamic GetCompOffApplicationStatus(LeaveFilter getCompOffApplicationStatus);
    }
}
