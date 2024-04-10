using AutoMapper;
using Microsoft.IdentityModel.Logging;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.Claim;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MOBILEAPI2024.BLL.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository _claimRepository;
        private readonly IMapper _mapper;
        public ClaimService(IClaimRepository claimRepository, IMapper mapper)
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

        public dynamic ClaimApprovalRecords(ClaimApplicationRecordsRequest claimApplicationRecordsRequest)
        {
            var claimRecords = _mapper.Map<ClaimApplicationRecordsRequest, ClaimRecords>(claimApplicationRecordsRequest);

            if (claimApplicationRecordsRequest.ClaimStatus == "P")
            {
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
            else
            {
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

        }

        public dynamic ClaimApprovalUpdate(ClaimApprovalUpdateRequest claimApprovalUpdateRequest)
        {
            // Create XML structure
            XElement rootElement = new XElement("ClaimDetails");

            foreach (var detail in claimApprovalUpdateRequest.ClaimDetails)
            {
                // Create XML elements using field names
                XElement detailElement = new XElement("Detail",
                    new XElement("CURR_RATE", detail.CURR_RATE),
                    new XElement("Approval_Date", detail.Approval_Date),
                    new XElement("APPROVED_PETROL_KM", detail.APPROVED_PETROL_KM),
                    new XElement("Max_Limit", detail.Max_Limit),
                    new XElement("CLAIM_ATTACHMENT", detail.CLAIM_ATTACHMENT),
                    new XElement("APPLICATION_AMOUNT", detail.APPLICATION_AMOUNT),
                    new XElement("CLAIM_ID", detail.CLAIM_ID),
                    new XElement("FOR_DATE", detail.FOR_DATE),
                    new XElement("DESCRIPTION", detail.DESCRIPTION),
                    new XElement("CMP_ID", detail.CMP_ID),
                    new XElement("PETROL_KM", detail.PETROL_KM),
                    new XElement("CLAIM_NAME", detail.CLAIM_NAME),
                    new XElement("Rpt_Level", detail.Rpt_Level),
                    new XElement("Claim_Status", detail.Claim_Status),
                    new XElement("Emp_ID", detail.Emp_ID),
                    new XElement("TOTALAMOUNT", detail.TOTALAMOUNT),
                    new XElement("Claim_Apr_Amnt", detail.Claim_Apr_Amnt),
                    new XElement("CLAIM_APP_DETAIL_ID", detail.CLAIM_APP_DETAIL_ID),
                    new XElement("ClaimIDSum", detail.ClaimIDSum),
                    new XElement("CLAIM_APP_ID", detail.CLAIM_APP_ID),
                    new XElement("CLAIM_ALLOW_BEYOND_LIMIT", detail.CLAIM_ALLOW_BEYOND_LIMIT)
                );

                // Add detail element to root element
                rootElement.Add(detailElement);
            }

            // Create XML document with the root element
            XDocument xmlDocument = new XDocument(rootElement);


            if (xmlDocument != null)
            {

                var claimApprovalUpdate = _claimRepository.ClaimApprovalRecordsUpdateInsert(claimApprovalUpdateRequest, xmlDocument);
                if (claimApprovalUpdateRequest.FinalApproval == 1)
                {
                    var claimFinalApproval = _claimRepository.ClaimApprovalRecordsFinalUpdateInsert(claimApprovalUpdateRequest, xmlDocument);
                    return claimFinalApproval;
                }
                return claimApprovalUpdate;
            }
            return "Invalid data passed.";

        }

        public dynamic ClaimLimit(ClaimLimitRequest claimLimitRequest)
        {
            var claimRecords = _mapper.Map<ClaimLimitRequest, ClaimRecords>(claimLimitRequest);
            claimRecords.Type = "L";
            claimRecords.ForDate = claimLimitRequest.ForDate;
            claimRecords.ToDate = DateTime.Now;
            var claimApplication = _claimRepository.ClaimRecords(claimRecords);
            if (claimApplication == null)
            {
                return null;
            }
            return claimApplication;
        }

        public dynamic ClaimType(int cmpId, int empId)
        {
            ClaimRecords claimRecords = new();
            claimRecords.Type = "B";
            claimRecords.ForDate = DateTime.Now;
            claimRecords.ToDate = DateTime.Now;
            claimRecords.CmpID = cmpId;
            claimRecords.EmpID = empId;
            var claimApplication = _claimRepository.ClaimRecords(claimRecords);
            if (claimApplication == null)
            {
                return null;
            }
            return claimApplication;
        }
    }
}
