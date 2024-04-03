using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.RequestDTO.Medical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.BLL.Services.IServices
{
    public interface IMedicalService
    {
        dynamic BindMedicalDepDetails(int v1, int v2);
        dynamic BindMedicalIncident(int v);
        dynamic GetMedicalAppDetails(LeaveBalanceRequest leaveBalanceRequest);
        dynamic GetMedicalAppIdDet(int v1, int v2,int APPId);
        dynamic MedicalDelete(int v1, int v2, int aPPId);
        dynamic MedicalInsert(MedicalInsertRequest medicalInsertRequest);
        dynamic MedicalUpdate(MedicalUpdateRequest medicalUpdateRequest);
    }
}
