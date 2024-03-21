using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DTO.RequestDTO.Claim;
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
        dynamic ClaimRecords(ClaimRecords claimRecords);
    }
}
