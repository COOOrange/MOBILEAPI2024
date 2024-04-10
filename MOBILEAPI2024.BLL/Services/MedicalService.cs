using AutoMapper;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.RequestDTO.Medical;
using Org.BouncyCastle.Asn1.Cmp;
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
    public class MedicalService : IMedicalService
    {
        private readonly IMedicalRepository _medicalRepository;
        private readonly IMapper _mapper;
        public MedicalService(IMedicalRepository medicalRepository,IMapper mapper)
        {
            _medicalRepository = medicalRepository;
            _mapper = mapper;
        }

        public dynamic BindMedicalDepDetails(int cmpID, int empId)
        {
            var medicalResponse = _medicalRepository.BindMedicalDepDetails(cmpID, empId);
            if ((medicalResponse as ICollection)?.Count == 0 || medicalResponse == null)
            {
                return null;
            }
            return medicalResponse;
        }

        public dynamic BindMedicalIncident(int cmpID)
        {
            var medicalResponse = _medicalRepository.BindMedicalIncident(cmpID);
            if ((medicalResponse as ICollection)?.Count == 0 || medicalResponse == null)
            {
                return null;
            }
            return medicalResponse;
        }

        public dynamic GetMedicalAppDetails(LeaveBalanceRequest leaveBalanceRequest)
        {
            var medicalResponse = _medicalRepository.GetMedicalAppDetails(leaveBalanceRequest);
            if ((medicalResponse as ICollection)?.Count == 0 || medicalResponse == null)
            {
                return null;
            }
            return medicalResponse;
        }

        public dynamic GetMedicalAppIdDet(int cmpId, int empID, int APPId)
        {
            var medicalResponse = _medicalRepository.GetMedicalAppIdDet(cmpId,empID,APPId);
            if ((medicalResponse as ICollection)?.Count == 0 || medicalResponse == null)
            {
                return null;
            }
            return medicalResponse;
        }

        public dynamic MedicalDelete(int cmpId, int empID, int aPPId)
        {
            MedicalInsert medicalInsert = new MedicalInsert();
            medicalInsert.CmpID = cmpId;
            medicalInsert.EmpID = empID;
            medicalInsert.AppId = aPPId;
            medicalInsert.TranType = "D";

            var medicalResponse = _medicalRepository.MedicalInsert(medicalInsert);
            if ((medicalResponse as ICollection)?.Count == 0 || medicalResponse == null)
            {
                return null;
            }
            return medicalResponse;
        }

        public dynamic MedicalInsert(MedicalInsertRequest medicalInsertRequest)
        {
            var medicalInsert = _mapper.Map<MedicalInsertRequest, MedicalInsert>(medicalInsertRequest);
            medicalInsert.TranType = "I";

            var medicalResponse = _medicalRepository.MedicalInsert(medicalInsert);
            if ((medicalResponse as ICollection)?.Count == 0 || medicalResponse == null)
            {
                return null;
            }
            return medicalResponse;
        }

        public dynamic MedicalUpdate(MedicalUpdateRequest medicalUpdateRequest)
        {
            var medicalInsert = _mapper.Map<MedicalUpdateRequest, MedicalInsert>(medicalUpdateRequest);
            medicalInsert.TranType = "U";

            var medicalResponse = _medicalRepository.MedicalInsert(medicalInsert);
            if ((medicalResponse as ICollection)?.Count == 0 || medicalResponse == null)
            {
                return null;
            }
            return medicalResponse;
        }
    }
}
