using Dapper;
using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.Grievance;
using System.Data;

namespace MOBILEAPI2024.DAL.Repositories
{
    public class GrievanceRepository : SqlDbRepository<ActiveInActiveUser>, IGrievanceRepository
    {
        public GrievanceRepository(string connectionString) : base(connectionString)
        {
        }

        public dynamic GetGrievanceRecords(GrievHearingRequest grievHearingRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@From_Date", grievHearingRequest.From_Date);
            vParams.Add("@To_Date", grievHearingRequest.To_Date);
            vParams.Add("@Cmp_ID", grievHearingRequest.CmpID);
            vParams.Add("@Emp_ID", grievHearingRequest.EmpID);
            var response = vconn.Query("SP_Mobile_WebService_GrievanceApp", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic GrievanceApplication(GrievanceApplication grievanceApplication)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@GrieId", grievanceApplication.GrieId);
            vParams.Add("@AppNo", grievanceApplication.AppNo);
            vParams.Add("@Cmp_ID", grievanceApplication.CmpID);
            vParams.Add("@EmpIDF", grievanceApplication.EmpIDF);
            vParams.Add("@Griev_Against", grievanceApplication.GrievAgainst);
            vParams.Add("@EmpIDT", grievanceApplication.EmpIDT);
            vParams.Add("@NameT", grievanceApplication.NameT);
            vParams.Add("@AddressT", grievanceApplication.AddressT);
            vParams.Add("@EmailT", grievanceApplication.EmailT);
            vParams.Add("@ContactT", grievanceApplication.ContactT);
            vParams.Add("@SubLine", grievanceApplication.SubLine);
            vParams.Add("@Details", grievanceApplication.Details);
            vParams.Add("@tran_type", grievanceApplication.TranType);
            vParams.Add("@FileName", grievanceApplication.FileName);
            vParams.Add("@LoginID", grievanceApplication.LoginID);
            vParams.Add("@Result", "");
            var response = vconn.Query("SP_Mobile_HRMS_WebService_Grievance_Application", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic GrievHearing(GrievHearingRequest grievHearingRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", grievHearingRequest.CmpID);
            vParams.Add("@start_date", grievHearingRequest.From_Date);
            vParams.Add("@End_date", grievHearingRequest.To_Date);
            var response = vconn.Query("SP_Mobile_HRMS_GrievHearing", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic GrievMaster(int cmpId, int empId, string type)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", empId);
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Type", type);
            vParams.Add("@Result", "");
            var response = vconn.Query("SP_Mobile_HRMS_WebService_GrievanceMaster", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }
    }
}
