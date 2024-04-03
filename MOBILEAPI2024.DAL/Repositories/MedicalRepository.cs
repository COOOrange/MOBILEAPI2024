using Dapper;
using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.Grievance;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.RequestDTO.Medical;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MOBILEAPI2024.DAL.Repositories
{
    public class MedicalRepository : SqlDbRepository<ActiveInActiveUser>, IMedicalRepository
    {
        public MedicalRepository(string connectionString) : base(connectionString)
        {
        }

        public dynamic BindMedicalDepDetails(int cmpID, int empId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpID);
            vParams.Add("@Emp_Id", empId);
            vParams.Add("@Result", "");
            var response = vconn.Query("SP_Mobile_HRMS_WebService_Get_Medical_DepDetails", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic GetMedicalAppDetails(LeaveBalanceRequest leaveBalanceRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", leaveBalanceRequest.EmpId);
            vParams.Add("@Cmp_ID", leaveBalanceRequest.CmpId);
            vParams.Add("@Month", leaveBalanceRequest.Month);
            vParams.Add("@Year", leaveBalanceRequest.Year);
            var response = vconn.Query("SP_Mobile_HRMS_WebService_Get_Medical_Details", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic GetMedicalAppIdDet(int cmpId, int empID, int aPPId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", empID);
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@AppId", aPPId);
            var response = vconn.Query("SP_Mobile_HRMS_WebService_Get_Medical_AppId_Details", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic MedicalInsert(MedicalInsert medicalInsert)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@App_Id", medicalInsert.AppId);
            vParams.Add("@App_For", medicalInsert.AppFor);
            vParams.Add("@Cmp_ID", medicalInsert.CmpID);
            vParams.Add("@App_Date", medicalInsert.AppDate);
            vParams.Add("@Emp_Name", medicalInsert.EmpName);
            vParams.Add("@Hospital_Name", medicalInsert.HospName);
            vParams.Add("@State_Id", medicalInsert.StateId);
            vParams.Add("@City", medicalInsert.CityName);
            vParams.Add("@Incident_Id", medicalInsert.IncidentId);
            vParams.Add("@Incident_Place", medicalInsert.IncidentPlace);
            vParams.Add("@Hospital_Address", medicalInsert.HospAddr);
            vParams.Add("@Date_Of_Incident", medicalInsert.DateOfInc);
            vParams.Add("@Time_of_Incident", medicalInsert.TimeofInc);
            vParams.Add("@Contact_no", medicalInsert.ContNo1);
            vParams.Add("@Contact_no2", medicalInsert.ContNo2);
            vParams.Add("@EmailId", medicalInsert.EmailId);
            vParams.Add("@Desc_Details", medicalInsert.DescDetails);
            vParams.Add("@Dependent_Details", medicalInsert.DepDetail);
            vParams.Add("@Emp_Id", medicalInsert.EmpID);
            vParams.Add("@Other_Note", medicalInsert.OtherNote);
            vParams.Add("@TransId", medicalInsert.TranType);
            vParams.Add("@CreatedBy", medicalInsert.CreatedBy);
            vParams.Add("@Result", "");
            var response = vconn.Query("SP_Mobile_HRMS_WebService_Medical", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }
    }
}
