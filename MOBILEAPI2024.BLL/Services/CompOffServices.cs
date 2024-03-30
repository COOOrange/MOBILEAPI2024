using AutoMapper;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.CompOff;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.BLL.Services
{
    public class CompOffServices : ICompOffService
    {
        private readonly ICompOffRepository _compOffRepository;
        private readonly IMapper _mapper;
        public CompOffServices(ICompOffRepository compOffRepository,IMapper mapper)
        {
            _compOffRepository = compOffRepository;
            _mapper = mapper;
        }

        public dynamic CompOffApplication(CompOffApplicationRequest compOffApplicationRequest)
        {
            var compOffApplication = _mapper.Map<CompOffApplicationRequest, CompOffApplication>(compOffApplicationRequest);
            compOffApplication.ForDate = DateTime.Now;
            compOffApplication.strType = "I";
            var compOffResponse = _compOffRepository.CompOffApplication(compOffApplication);
            if(compOffResponse == null || (compOffResponse as ICollection)?.Count == 0) 
            {
                return null;
            }
            return compOffResponse;
        }

        public dynamic CompOffApplicationDelete(int cmpId, int loginId, int compoffAppID)
        {
            CompOffApplication compOffApplication = new();
            compOffApplication.CmpID = cmpId;
            compOffApplication.LoginID = loginId;
            compOffApplication.ForDate = DateTime.Now;
            compOffApplication.AppID = compoffAppID;
            compOffApplication.EWorkDate= DateTime.Now.ToString();
            compOffApplication.strType = "D";

            var compOffResponse = _compOffRepository.CompOffApplication(compOffApplication);
            if (compOffResponse == null || (compOffResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return compOffResponse;
        }

        public dynamic CompOffApproval(CompOffApprovalRequest compOffApprovalRequest)
        {
            var compOffApplication = _mapper.Map<CompOffApprovalRequest, CompOffApplication>(compOffApprovalRequest);
            compOffApplication.strType = "A";
            compOffApplication.ForDate = DateTime.Now;

            var compOffResponse = _compOffRepository.CompOffApplication(compOffApplication);
            if (compOffResponse == null || (compOffResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return compOffResponse;
        }

        public dynamic CompOffApprovalDelete(int cmpId, int compoffAppID)
        {
            CompOffApplication compOffApplication = new();
            compOffApplication.CmpID = cmpId;
            compOffApplication.AppID = compoffAppID;
            compOffApplication.ForDate = DateTime.Now;
            compOffApplication.EWorkDate = DateTime.Now.ToString();
            compOffApplication.strType = "DE";

            var compOffResponse = _compOffRepository.CompOffApplication(compOffApplication);
            if (compOffResponse == null || (compOffResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return compOffResponse;
        }

        public dynamic GetCompOffApplicationDetails(int compoffAppID)
        {
            CompOffApplication compOffApplication = new();
            compOffApplication.AppID = compoffAppID;
            compOffApplication.ForDate = DateTime.Now;
            compOffApplication.EWorkDate = DateTime.Now.ToString();
            compOffApplication.strType = "E";

            var compOffResponse = _compOffRepository.CompOffApplication(compOffApplication);
            if (compOffResponse == null || (compOffResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return compOffResponse;
        }

        public dynamic GetCompoffApplicationRecord(int cmpId, int empId, string StrType)
        {
            CompOffApplication compOffApplication = new();
            compOffApplication.ForDate = DateTime.Now;
            compOffApplication.EWorkDate = DateTime.Now.ToString();
            compOffApplication.CmpID = cmpId;
            compOffApplication.EmpID = empId;
            compOffApplication.strType = StrType;

            var compOffResponse = _compOffRepository.CompOffApplication(compOffApplication);
            if (compOffResponse == null || (compOffResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return compOffResponse;
        }

        public dynamic GetCompOffApplicationStatus(LeaveFilter getCompOffApplicationStatus)
        {
            CompOffApplication compOffApplication = new();
            compOffApplication.ForDate = getCompOffApplicationStatus.FromDate;
            compOffApplication.EWorkDate = getCompOffApplicationStatus.ToDate.ToString();
            compOffApplication.CmpID = getCompOffApplicationStatus.Cmp_Id;
            compOffApplication.EmpID = getCompOffApplicationStatus.Emp_Id;
            compOffApplication.strType = "S";

            var compOffResponse = _compOffRepository.CompOffApplication(compOffApplication);
            if (compOffResponse == null || (compOffResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return compOffResponse;
        }
    }
}
