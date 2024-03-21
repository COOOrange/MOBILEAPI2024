using AutoMapper;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.Claim;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.BLL.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository _claimRepository;
        private readonly IMapper _mapper;
        public ClaimService(IClaimRepository claimRepository,IMapper mapper)
        {
            _claimRepository = claimRepository;
            _mapper = mapper;
        }

        public dynamic ClaimAdminSetting(int cmpId)
        {
            var claimAdminSetting = _claimRepository.ClaimAdminSetting(cmpId);
            if (claimAdminSetting == null)
            {
                return null;
            }
            return claimAdminSetting;
        }

        public dynamic ClaimAppDetails(ClaimAppDetailsRequest claimAppDetailsRequest)
        {
            var claimRecords = _mapper.Map<ClaimAppDetailsRequest, ClaimRecords>(claimAppDetailsRequest);
            claimRecords.Type = "E";
            claimRecords.ForDate = DateTime.Now;
            claimRecords.ToDate = DateTime.Now;
            var claimAdminSetting = _claimRepository.ClaimRecords(claimRecords);
            if (claimAdminSetting == null)
            {
                return null;
            }
            return claimAdminSetting;
        }

        public dynamic ClaimApplication(ClaimApplicationRequest claimApplicationRequest)
        {
            var claimRecords = _mapper.Map<ClaimApplicationRequest, ClaimRecords>(claimApplicationRequest);
            claimRecords.Type = "I";
            claimRecords.ToDate = DateTime.Now;
            var claimApplication = _claimRepository.ClaimRecords(claimRecords);
            if (claimApplication == null)
            {
                return null;
            }
            return claimApplication;
        }

        public dynamic ClaimApplicationDelete(ClaimApplicationDeleteRequest claimApplicationDeleteRequest)
        {
            var claimRecords = _mapper.Map<ClaimApplicationDeleteRequest, ClaimRecords>(claimApplicationDeleteRequest);
            claimRecords.Type = "D";
            claimRecords.ForDate = DateTime.Now;
            claimRecords.ToDate = DateTime.Now;
            var claimApplication = _claimRepository.ClaimRecords(claimRecords);
            if (claimApplication == null)
            {
                return null;
            }
            return claimApplication;
        }

        public dynamic ClaimApplicationDetails(ClaimApplicationDetailsRequest claimApplicationDetailsRequest)
        {
            var claimRecords = _mapper.Map<ClaimApplicationDetailsRequest, ClaimRecords>(claimApplicationDetailsRequest);
            claimRecords.Type = "E";
            claimRecords.ForDate = DateTime.Now;
            claimRecords.ToDate = DateTime.Now;
            claimRecords.AppStatus = claimApplicationDetailsRequest.ClaimStatus;
            var claimApplication = _claimRepository.ClaimRecords(claimRecords);
            if (claimApplication == null)
            {
                return null;
            }
            return claimApplication;
        }

        public dynamic ClaimApplicationRecords(ClaimApplicationRecordsRequest claimApplicationRecordsRequest)
        {
            var claimRecords = _mapper.Map<ClaimApplicationRecordsRequest, ClaimRecords>(claimApplicationRecordsRequest);
            claimRecords.Type = "R";
            claimRecords.AppStatus = claimApplicationRecordsRequest.ClaimStatus;
            claimRecords.ForDate = claimApplicationRecordsRequest.FromDate;
            var claimApplication = _claimRepository.ClaimRecords(claimRecords);
            if (claimApplication == null)
            {
                return null;
            }
            return claimApplication;
        }

        public dynamic ClaimApplicationStatus(ClaimApplicationStatusRequest claimApplicationStatusRequest)
        {
            var claimRecords = _mapper.Map<ClaimApplicationStatusRequest, ClaimRecords>(claimApplicationStatusRequest);
            claimRecords.Type = "S";
            claimRecords.ForDate = claimApplicationStatusRequest.FromDate;
            var claimApplication = _claimRepository.ClaimRecords(claimRecords);
            if (claimApplication == null)
            {
                return null;
            }
            return claimApplication;
        }

        public dynamic ClaimApprovalDetailRecords(int ClaimAppId)
        {
            ClaimRecords claimRecords = new();
            claimRecords.Type = "AD";
            claimRecords.ClaimAppID = ClaimAppId;
            claimRecords.ForDate = DateTime.Now;
            claimRecords.ToDate = DateTime.Now;
            var claimApplication = _claimRepository.ClaimApprovalRecords(claimRecords);
            if (claimApplication == null)
            {
                return null;
            }
            return claimApplication;
        }
    }
}
