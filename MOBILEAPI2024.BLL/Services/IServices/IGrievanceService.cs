using MOBILEAPI2024.DTO.RequestDTO.Grievance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.BLL.Services.IServices
{
    public interface IGrievanceService
    {
        dynamic GetGrievanceRecords(GrievHearingRequest grievHearingRequest);
        dynamic GrievanceApplication(GrievanceApplicationRequest grievanceApplicationRequest);
        dynamic GrievanceApplicationDelete(int v1, int v2, int grieId);
        dynamic GrievHearing(GrievHearingRequest grievHearingRequest);
        dynamic GrievMaster(int v1, int v2, string type);
    }
}
    