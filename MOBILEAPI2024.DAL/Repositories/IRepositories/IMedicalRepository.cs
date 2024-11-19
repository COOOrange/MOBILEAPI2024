using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.RequestDTO.Medical;
using MOBILEAPI2024.DTO.ResponseDTO.Medical;
using System.Collections;

namespace MOBILEAPI2024.DAL.Repositories.IRepositories
{
    public interface IMedicalRepository : IGenericRepository<ActiveInActiveUser>
    {
        dynamic BindMedicalDepDetails(int cmpID, int empId);
        dynamic BindMedicalIncident(int cmpID);
        MedicalAppDetailsResponse GetMedicalAppDetails(LeaveBalanceRequest leaveBalanceRequest);
        dynamic GetMedicalAppIdDet(int cmpId, int empID, int aPPId);
        dynamic MedicalInsert(MedicalInsert medicalInsert);
    }
}
