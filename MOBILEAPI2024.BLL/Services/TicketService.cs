using AutoMapper;
using Microsoft.Extensions.Options;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.Common;
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
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        public TicketService(ITicketRepository ticketRepository, IOptions<AppSettings> appsetting, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _appSettings = appsetting.Value;
            _mapper = mapper;
        }

        public dynamic AddTicketFeedback(AddTicketFeedbackRequest addTicketFeedbackRequest)
        {
            var ticketResponse = _ticketRepository.AddTicketFeedback(addTicketFeedbackRequest);
            if ((ticketResponse as ICollection)?.Count == 0 || ticketResponse == null)
            {
                return null;
            }
            return ticketResponse;
        }

        public dynamic BindTicketRecords(int cmpId)
        {
            BindTicketRecords bindTicketRecords = new();
            bindTicketRecords.CmpID = cmpId;
            bindTicketRecords.FromDate = DateTime.Now;
            bindTicketRecords.ToDate = DateTime.Now;
            bindTicketRecords.StrType = "B";
            var ticketResponse = _ticketRepository.BindTicketRecords(bindTicketRecords);
            if ((ticketResponse as ICollection)?.Count == 0 || ticketResponse == null)
            {
                return null;
            }
            return ticketResponse;
        }

        private string GetDocumentName(string attachmentName)
        {
            string timestamp = DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_");
            return $"{timestamp}_Doc__{attachmentName.Replace(" ", "_")}";
        }

        public dynamic TicketApplication(TicketApplicationRequest ticketApplicationRequest)
        {
            string? strDocName = "";
            string? strDocPath = "";
            string? imagePath = "";
            if (!string.IsNullOrEmpty(ticketApplicationRequest.Attachment))
            {
                strDocName = GetDocumentName(ticketApplicationRequest.DocName);
                strDocPath = Path.Combine(_appSettings.DocPath, "TicketDocs", strDocName);
                string? directoryPath = Path.GetDirectoryName(strDocPath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                imagePath = Path.Combine(_appSettings.ImagePath, "App_File", "TravelDocs", strDocName);

                string base64String = ticketApplicationRequest.Attachment;
                byte[] docBytes = Convert.FromBase64String(base64String);

                using (MemoryStream ms = new MemoryStream(docBytes))
                {
                    using (FileStream fs = new FileStream(strDocPath, FileMode.Create))
                    {
                        ms.WriteTo(fs);
                    }
                }
            }

            var ticketApplicastion = _mapper.Map<TicketApplicationRequest, BindTicketRecords>(ticketApplicationRequest);
            ticketApplicastion.Attachment = strDocName;
            ticketApplicastion.FromDate = DateTime.Now;
            ticketApplicastion.ToDate = DateTime.Now;
            ticketApplicastion.StrType = "I";
            var travelResponse = _ticketRepository.BindTicketRecords(ticketApplicastion);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;
        }

        public dynamic TicketApplicationDelete(int cmpId, int empId, int loginId, int TicketAppID)
        {
            BindTicketRecords bindTicketRecords = new();
            bindTicketRecords.FromDate = DateTime.Now;
            bindTicketRecords.ToDate = DateTime.Now;
            bindTicketRecords.StrType = "R";
            bindTicketRecords.TicketAppID = TicketAppID;
            bindTicketRecords.CmpID = cmpId;
            bindTicketRecords.EmpID = empId;
            bindTicketRecords.LoginID = loginId;
            var travelResponse = _ticketRepository.BindTicketRecords(bindTicketRecords);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;
        }

        public dynamic TicketApplicationRecords(int cmpID, int empID, int ticketStatus)
        {
            BindTicketRecords bindTicketRecords = new();
            bindTicketRecords.FromDate = DateTime.Now;
            bindTicketRecords.ToDate = DateTime.Now;
            bindTicketRecords.StrType = "S";
            bindTicketRecords.TicketStatus = ticketStatus;
            bindTicketRecords.CmpID = cmpID;
            bindTicketRecords.EmpID = empID;
            var travelResponse = _ticketRepository.BindTicketRecords(bindTicketRecords);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;
        }

        public dynamic TicketApplicationStatus(TicketApplicationStatusRequest ticketApplicationStatusRequest)
        {
            var ticketApplicastion = _mapper.Map<TicketApplicationStatusRequest, BindTicketRecords>(ticketApplicationStatusRequest);
            ticketApplicastion.StrType = "L";
            var travelResponse = _ticketRepository.BindTicketRecords(ticketApplicastion);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;
        }

        public dynamic TicketApproval(TicketApprovalRequest ticketApprovalRequest)
        {
            var ticketApplicastion = _mapper.Map<TicketApprovalRequest, BindTicketRecords>(ticketApprovalRequest);
            ticketApplicastion.FromDate = DateTime.Now;
            ticketApplicastion.ToDate = DateTime.Now;
            ticketApplicastion.StrType = "A";
            var travelResponse = _ticketRepository.BindTicketRecords(ticketApplicastion);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;
        }

        public dynamic TicketAprovalDelete(int cmpID, int empId, int loginID, int ticketAppID)
        {
            BindTicketRecords bindTicketRecords = new();
            bindTicketRecords.FromDate = DateTime.Now;
            bindTicketRecords.ToDate = DateTime.Now;
            bindTicketRecords.StrType = "DE";
            bindTicketRecords.TicketAppID = ticketAppID;
            bindTicketRecords.CmpID = cmpID;
            bindTicketRecords.EmpID = empId;
            bindTicketRecords.LoginID = loginID;
            var travelResponse = _ticketRepository.BindTicketRecords(bindTicketRecords);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;
        }

        public dynamic TicketDashboard(int cmpID)
        {
            BindTicketRecords bindTicketRecords = new();
            bindTicketRecords.FromDate = DateTime.Now;
            bindTicketRecords.ToDate = DateTime.Now;
            bindTicketRecords.StrType = "D";
            bindTicketRecords.CmpID = cmpID;
            var travelResponse = _ticketRepository.BindTicketRecords(bindTicketRecords);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;
        }

        public dynamic TicketSendTo(int cmpID, int empId, int deptId)
        {
            var travelResponse = _ticketRepository.TicketSendTo(cmpID,empId,deptId);
            if ((travelResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return travelResponse;
        }
    }
}