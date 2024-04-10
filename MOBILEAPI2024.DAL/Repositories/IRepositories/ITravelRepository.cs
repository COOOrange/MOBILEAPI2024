using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DTO.RequestDTO.Travel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DAL.Repositories.IRepositories
{
    public interface ITravelRepository : IGenericRepository<ActiveInActiveUser>
    {
        dynamic DisplayTavelType(int cmpId, string transType);
        dynamic GetTravelAPIData(GetTravelAPIDataRequest getTravelAPIDataRequest);
        dynamic TravelAllDetails(TravelAllDetailsRequest travelAllDetailsRequest);
        dynamic TravelApp(TravelAppRequest travelAppRequest);
        dynamic TravelApplicationDelete(TravelApplicationDeleteRequest travelApplicationDeleteRequest);
        dynamic TravelApprovalDelete(TravelApprovalDeleteRequest travelApprovalDeleteRequest);
        dynamic TravelAprDetails(TravelAprDetailsRequest travelAprDetailsRequest);
        dynamic TravelProof(TravelProofRequest travelProofRequest);
        dynamic TravelProofInsert(TravelProofInsertRequest travelProofInsertRequest,string? imagePath,string? strDocName);
        dynamic TravelProofValidation(int cmpId, int empId, int travelAppCode);
        dynamic Travel_Approval(int cmpId, int empId, string strType);
        dynamic Travel_Approval_AdminSetting(int cmpId);
        dynamic Travel_Mode_Ddl(int cmpId, int empId, char tranType);
        dynamic Travel_Settlement(int cmpId);
    }
}
