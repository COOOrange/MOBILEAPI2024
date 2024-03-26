using MOBILEAPI2024.DTO.RequestDTO.Claim;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.BLL.Services.IServices
{
    public interface IClaimService
    {
        dynamic ClaimAdminSetting(int cmpId);
        dynamic ClaimAppDetails(ClaimAppDetailsRequest claimAppDetailsRequest);
        dynamic ClaimApplication(ClaimApplicationRequest  claimApplicationRequest);
        dynamic ClaimApplicationDelete(ClaimApplicationDeleteRequest claimApplicationDeleteRequest);
        dynamic ClaimApplicationDetails(ClaimApplicationDetailsRequest claimApplicationDetailsRequest);
        dynamic ClaimApplicationRecords(ClaimApplicationRecordsRequest claimApplicationRecordsRequest);
        dynamic ClaimApplicationStatus(ClaimApplicationStatusRequest claimApplicationStatusRequest);
        dynamic ClaimApprovalDetailRecords(int claim_App_ID);
        dynamic ClaimApprovalRecords(ClaimApplicationRecordsRequest claimApplicationRecordsRequest);
        dynamic ClaimApprovalUpdate(ClaimApprovalUpdateRequest claimApprovalUpdateRequest);
        dynamic ClaimLimit(ClaimLimitRequest claimLimitRequest);
        dynamic ClaimType(int v1, int v2);
    }
}
