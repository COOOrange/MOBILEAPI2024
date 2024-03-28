using MOBILEAPI2024.DTO.RequestDTO.Travel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.BLL.Services.IServices
{
    public interface ITravelService
    {
        dynamic DisplayTavelType(int v, string transType);
        dynamic GetTravelAPIData(GetTravelAPIDataRequest getTravelAPIDataRequest);
        dynamic TravelAllDetails(TravelAllDetailsRequest travelAllDetailsRequest);
        dynamic TravelApp(TravelAppRequest travelAppRequest);
        dynamic TravelApplicationDelete(TravelApplicationDeleteRequest travelApplicationDeleteRequest);
        dynamic TravelApprovalDelete(TravelApprovalDeleteRequest travelApprovalDeleteRequest);
        dynamic TravelAprDetails(TravelAprDetailsRequest travelAprDetailsRequest);
        dynamic TravelProof(TravelProofRequest travelProofRequest);
        dynamic TravelProofInsert(TravelProofInsertRequest travelProofInsertRequest);
        dynamic TravelProofValidation(int v1, int v2, int travelAppCode);
        dynamic Travel_Approval(int v1, int v2, string strType);
        dynamic Travel_Approval_AdminSetting(int v);
        dynamic Travel_Mode_Ddl(int v1, int v2, char tranType);
        dynamic Travel_Settlement(int v);
    }
}
