using Microsoft.Extensions.Options;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.Common;
using MOBILEAPI2024.DTO.RequestDTO.Travel;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Ocsp;
using System.Collections;

namespace MOBILEAPI2024.BLL.Services
{
    public class TravelService : ITravelService
    {
        private readonly ITravelRepository _travelRepository;
        private readonly AppSettings _appSettings;
        public TravelService(ITravelRepository travelRepository,IOptions<AppSettings> appsetting)
        {
            _travelRepository = travelRepository;
            _appSettings = appsetting.Value;
        }
        public dynamic DisplayTavelType(int cmpId, string transType)
        {
            var travelResponse = _travelRepository.DisplayTavelType(cmpId, transType);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;
        }

        public dynamic GetTravelAPIData(GetTravelAPIDataRequest getTravelAPIDataRequest)
        {
            var travelResponse = _travelRepository.GetTravelAPIData(getTravelAPIDataRequest);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;
        }

        public dynamic TravelAllDetails(TravelAllDetailsRequest travelAllDetailsRequest)
        {
            
            var travelResponse = _travelRepository.TravelAllDetails(travelAllDetailsRequest);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;
        }

        public dynamic TravelApp(TravelAppRequest travelAppRequest)
        {
            var travelResponse = _travelRepository.TravelApp(travelAppRequest);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;
        }

        public dynamic TravelApplicationDelete(TravelApplicationDeleteRequest travelApplicationDeleteRequest)
        {
            var travelResponse = _travelRepository.TravelApplicationDelete(travelApplicationDeleteRequest);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;
        }

        public dynamic TravelApprovalDelete(TravelApprovalDeleteRequest travelApprovalDeleteRequest)
        {
            var travelResponse = _travelRepository.TravelApprovalDelete(travelApprovalDeleteRequest);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;
        }

        public dynamic TravelAprDetails(TravelAprDetailsRequest travelAprDetailsRequest)
        {
            throw new NotImplementedException();
        }

        public dynamic TravelProof(TravelProofRequest travelProofRequest)
        {
            var travelResponse = _travelRepository.TravelProof(travelProofRequest);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;
        }
        private string GetDocumentName(string attachmentName)
        {
            string timestamp = DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_");
            return $"{timestamp}_Doc__{attachmentName.Replace(" ", "_")}";
        }

        public dynamic TravelProofInsert(TravelProofInsertRequest travelProofInsertRequest)
        {
            string? strDocName = "";
            string? strDocPath = "";
            string? imagePath = "";
            if (!string.IsNullOrEmpty(travelProofInsertRequest.Attachment))
            {
                strDocName = GetDocumentName(travelProofInsertRequest.AttachmentName);
                strDocPath = Path.Combine(_appSettings.DocPath, "TravelDocs", strDocName);
                string? directoryPath = Path.GetDirectoryName(strDocPath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                imagePath = Path.Combine(_appSettings.ImagePath, "App_File", "TravelDocs", strDocName);

                string base64String = travelProofInsertRequest.Attachment;
                byte[] docBytes = Convert.FromBase64String(base64String);

                using (MemoryStream ms = new MemoryStream(docBytes))
                {
                    using (FileStream fs = new FileStream(strDocPath, FileMode.Create))
                    {
                        ms.WriteTo(fs);
                    }
                }
            }

            var travelResponse = _travelRepository.TravelProofInsert(travelProofInsertRequest, imagePath, strDocName);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;


        }

        public dynamic TravelProofValidation(int cmpId, int empId, int travelAppCode)
        {
            var travelResponse = _travelRepository.TravelProofValidation(cmpId,empId, travelAppCode);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;
        }

        public dynamic Travel_Approval(int cmpId, int empId, string strType)
        {
            var travelResponse = _travelRepository.Travel_Approval(cmpId, empId, strType);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;
        }

        public dynamic Travel_Approval_AdminSetting(int cmpId)
        {
            var travelResponse = _travelRepository.Travel_Approval_AdminSetting(cmpId);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;
        }

        public dynamic Travel_Mode_Ddl(int cmpId, int empId, char tranType)
        {
            var travelResponse = _travelRepository.Travel_Mode_Ddl(cmpId,empId,tranType);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;
        }

        public dynamic Travel_Settlement(int cmpId)
        {
            var travelResponse = _travelRepository.Travel_Settlement(cmpId);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;
        }
    }
}
