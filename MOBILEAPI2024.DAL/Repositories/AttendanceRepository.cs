using Dapper;
using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.Attendance;
using MOBILEAPI2024.DTO.RequestDTO.Employee;
using MOBILEAPI2024.DTO.ResponseDTO.Employee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DAL.Repositories
{
    public class AttendanceRepository : SqlDbRepository<ActiveInActiveUser>, IAttendanceRepository
    {
        public AttendanceRepository(string connectionString) : base(connectionString)
        {
        }

        public dynamic AttendanceDetails(AttendanceDetails attendanceDetails1)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", attendanceDetails1.EmpID);
            vParams.Add("@Cmp_ID", attendanceDetails1.CmpID);
            vParams.Add("@Vertical_ID", 0);
            vParams.Add("@SubVertical_ID", 0);
            vParams.Add("@ForDate", attendanceDetails1.FromDate);
            vParams.Add("@Time", attendanceDetails1.ToDate);
            vParams.Add("@INOUTFlag", "");
            vParams.Add("@Reason", "");
            vParams.Add("@IMEINo", "");
            vParams.Add("@Latitude", "");
            vParams.Add("@Longitude", "");
            vParams.Add("@Address", "");
            vParams.Add("@Emp_Image", "");
            vParams.Add("@strAttendance", "");
            vParams.Add("@Month", attendanceDetails1.Month);
            vParams.Add("@Year", attendanceDetails1.Year);
            vParams.Add("@Type", attendanceDetails1.Type);
            vParams.Add("@Result", "");
            vParams.Add("@SubVerticalName", "");
            var response = vconn.QueryMultiple("SP_Mobile_HRMS_WebService_Attendance", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic AttendanceRegularizeDetails(AttendanceRegularizeDetails attendanceRegularizeDetails)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@IO_Tran_Id", attendanceRegularizeDetails.IOTranId);
            vParams.Add("@Emp_ID", attendanceRegularizeDetails.EmpID);
            vParams.Add("@Cmp_ID", attendanceRegularizeDetails.CmpID);
            vParams.Add("@Month", attendanceRegularizeDetails.Month);
            vParams.Add("@Year", attendanceRegularizeDetails.Year);
            vParams.Add("@For_Date", DateTime.Now.ToString("dd/MMM/yyyy"));
            vParams.Add("@Reason", "");
            vParams.Add("@Half_Full_Day", "");
            vParams.Add("@Is_Cancel_Late_In", 0);
            vParams.Add("@Is_Cancel_Early_Out", 0);
            vParams.Add("@In_Date_Time", attendanceRegularizeDetails.Fromdate);
            vParams.Add("@Out_Date_Time", attendanceRegularizeDetails.Todate);
            vParams.Add("@Is_Approve", 0);
            vParams.Add("@Other_Reason", "");
            vParams.Add("@IMEINo", "");
            vParams.Add("@S_Emp_ID", 0);
            vParams.Add("@Rpt_Level", 0);
            vParams.Add("@Final_Approve", 0);
            vParams.Add("@Is_Fwd_Leave_Rej", 0);
            vParams.Add("@Approval_Status", "");
            vParams.Add("@Type", attendanceRegularizeDetails.Type);
            vParams.Add("@Result", "");
            var response = vconn.QueryMultiple("SP_Mobile_HRMS_WebService_AttendanceRegularization", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }
    }
}
