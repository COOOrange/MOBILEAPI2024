using Dapper;
using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.CompOff;
using MOBILEAPI2024.DTO.ResponseDTO.Account;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DAL.Repositories
{
    public class CompOffRepository : SqlDbRepository<ActiveInActiveUser>, ICompOffRepository
    {
        public CompOffRepository(string connectionString) : base(connectionString)
        {
        }

        public dynamic CompOffApplication(CompOffApplication compOffApplication)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Compoff_App_ID", compOffApplication.AppID);
            vParams.Add("@Cmp_ID", compOffApplication.CmpID);
            vParams.Add("@Emp_ID", compOffApplication.EmpID);
            vParams.Add("@SEmp_ID", compOffApplication.SEmpID);
            vParams.Add("@ForDate", compOffApplication.ForDate);
            vParams.Add("@Extra_Work_Date", Convert.ToDateTime(compOffApplication.EWorkDate));
            vParams.Add("@Extra_Work_Hours", compOffApplication.EWorkHours);
            vParams.Add("@Extra_Work_Reason", compOffApplication.EWorkReason);
            vParams.Add("@Sanctioned_Hours", compOffApplication.SHours);
            vParams.Add("@CompOff_Type", compOffApplication.CompoffType);
            vParams.Add("@IMEINo", compOffApplication.IMEINo);
            vParams.Add("@OT_Type", compOffApplication.OTType);
            vParams.Add("@Login_ID", compOffApplication.LoginID);
            vParams.Add("@Email", compOffApplication.Email);
            vParams.Add("@ContactNo", compOffApplication.ContactNo);
            vParams.Add("@Approval_Status", compOffApplication.AppStatus);
            vParams.Add("@Approval_Comments", compOffApplication.AppComment);
            vParams.Add("@Type", compOffApplication.strType);
            vParams.Add("@Result", "");
            var response = vconn.Query("SP_Mobile_HRMS_WebService_CompOff", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }
    }
}
