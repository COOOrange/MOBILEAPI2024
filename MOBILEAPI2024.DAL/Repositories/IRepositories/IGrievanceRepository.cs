using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DTO.RequestDTO.Grievance;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DAL.Repositories.IRepositories
{
    public interface IGrievanceRepository : IGenericRepository<ActiveInActiveUser>
    {
        dynamic GetGrievanceRecords(GrievHearingRequest grievHearingRequest);
        dynamic GrievanceApplication(GrievanceApplication grievanceApplication);
        dynamic GrievHearing(GrievHearingRequest grievHearingRequest);
        dynamic GrievMaster(int cmpId, int empId, string type);
    }
}
