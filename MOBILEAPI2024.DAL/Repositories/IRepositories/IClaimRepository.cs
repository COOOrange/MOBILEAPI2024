using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DTO.RequestDTO.Claim;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DAL.Repositories.IRepositories
{
    public interface IClaimRepository : IGenericRepository<ActiveInActiveUser>
    {
        dynamic ClaimAdminSetting(int cmpId);
        dynamic ClaimApprovalRecords(ClaimRecords claimRecords);
        object ClaimApprovalRecordsFinalUpdateInsert(ClaimApprovalUpdateRequest claimApprovalUpdateRequest, List<ClaimDetails> claimDetails);
        dynamic ClaimApprovalRecordsUpdateInsert(ClaimApprovalUpdateRequest claimApprovalUpdateRequest, List<ClaimDetails> claimDetails);
        dynamic ClaimRecords(ClaimRecords claimRecords);
    }
}
