using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DTO.RequestDTO.Claim;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MOBILEAPI2024.DAL.Repositories.IRepositories
{
    public interface IClaimRepository : IGenericRepository<ActiveInActiveUser>
    {
        dynamic ClaimAdminSetting(int cmpId);
        dynamic ClaimApprovalRecords(ClaimRecords claimRecords);
        object ClaimApprovalRecordsFinalUpdateInsert(ClaimApprovalUpdateRequest claimApprovalUpdateRequest, XDocument claimDetails);
        dynamic ClaimApprovalRecordsUpdateInsert(ClaimApprovalUpdateRequest claimApprovalUpdateRequest, XDocument claimDetails);
        dynamic ClaimRecords(ClaimRecords claimRecords);
    }
}
