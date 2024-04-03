using AutoMapper;
using Microsoft.Extensions.Options;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.Common;
using MOBILEAPI2024.DTO.RequestDTO.Grievance;
using MOBILEAPI2024.DTO.RequestDTO.Ticket;
using MOBILEAPI2024.DTO.RequestDTO.Travel;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.BLL.Services
{
    public class GrievanceService : IGrievanceService
    {
        private readonly IGrievanceRepository _grievanceRepository;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        public GrievanceService(IGrievanceRepository grievanceRepository, IOptions<AppSettings> appSettings,IMapper mapper)
        {
            _grievanceRepository = grievanceRepository;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        private string GetDocumentName(string attachmentName)
        {
            string timestamp = DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_");
            return $"{timestamp}_Doc__{attachmentName.Replace(" ", "_")}";
        }

        public dynamic GrievanceApplication(GrievanceApplicationRequest grievanceApplicationRequest)
        {
            string? strDocName = "";
            string? strDocPath = "";
            string? imagePath = "";
            if (!string.IsNullOrEmpty(grievanceApplicationRequest.Attachement))
            {
                strDocName = GetDocumentName(grievanceApplicationRequest.DocName);
                strDocPath = Path.Combine(_appSettings.DocPath, "TravelDocs", strDocName);
                string? directoryPath = Path.GetDirectoryName(strDocPath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                imagePath = Path.Combine(_appSettings.ImagePath, "App_File", "TravelDocs", strDocName);

                string base64String = grievanceApplicationRequest.Attachement;
                byte[] docBytes = Convert.FromBase64String(base64String);

                using (MemoryStream ms = new MemoryStream(docBytes))
                {
                    using (FileStream fs = new FileStream(strDocPath, FileMode.Create))
                    {
                        ms.WriteTo(fs);
                    }
                }
            }
            var grivenceApplicatioon = _mapper.Map<GrievanceApplicationRequest, GrievanceApplication>(grievanceApplicationRequest);
            grivenceApplicatioon.FileName = strDocName;
            grivenceApplicatioon.TranType = "I";
            var grivenceResponse = _grievanceRepository.GrievanceApplication(grivenceApplicatioon);
            if ((grivenceResponse as ICollection)?.Count == 0 || grivenceResponse == null)
            {
                return null;
            }
            return grivenceResponse;
        }

        public dynamic GrievHearing(GrievHearingRequest grievHearingRequest)
        {
            var grivenceResponse = _grievanceRepository.GrievHearing(grievHearingRequest);
            if ((grivenceResponse as ICollection)?.Count == 0 || grivenceResponse == null)
            {
                return null;
            }
            return grivenceResponse;
        }

        public dynamic GrievMaster(int cmpId, int empId, string type)
        {

            var grivenceResponse = _grievanceRepository.GrievMaster(cmpId,empId,type);
            if ((grivenceResponse as ICollection)?.Count == 0 || grivenceResponse == null)
            {
                return null;
            }
            return grivenceResponse;
        }

        public dynamic GrievanceApplicationDelete(int cmpID, int loginId, int grieId)
        {
            GrievanceApplication grievanceApplication = new();
            grievanceApplication.CmpID = cmpID;
            grievanceApplication.LoginID = loginId;
            grievanceApplication.GrieId = grieId;
            grievanceApplication.TranType = "D";

            var grivenceResponse = _grievanceRepository.GrievanceApplication(grievanceApplication);
            if ((grivenceResponse as ICollection)?.Count == 0 || grivenceResponse == null)
            {
                return null;
            }
            return grivenceResponse;
        }

        public dynamic GetGrievanceRecords(GrievHearingRequest grievHearingRequest)
        {
            var grivenceResponse = _grievanceRepository.GetGrievanceRecords(grievHearingRequest);
            if ((grivenceResponse as ICollection)?.Count == 0 || grivenceResponse == null)
            {
                return null;
            }
            return grivenceResponse;
        }
    }
}
