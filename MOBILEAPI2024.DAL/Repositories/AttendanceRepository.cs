using Dapper;
using Microsoft.SqlServer.Server;
using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.Attendance;
using MOBILEAPI2024.DTO.RequestDTO.Employee;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.ResponseDTO.Attendance;
using MOBILEAPI2024.DTO.ResponseDTO.Employee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
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
            var response = vconn.Query("SP_Mobile_HRMS_WebService_Attendance", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic AttendanceInsert(AttendanceInsert attendanceInsert)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", attendanceInsert.EmpID);
            vParams.Add("@Cmp_ID", attendanceInsert.CmpID);
            vParams.Add("@Vertical_ID", attendanceInsert.VerticalID);
            vParams.Add("@SubVertical_ID", attendanceInsert.SubVerticalID);
            vParams.Add("@ForDate", Convert.ToDateTime(attendanceInsert.FromDate));
            vParams.Add("@Time", Convert.ToDateTime(attendanceInsert.Todate));
            vParams.Add("@INOUTFlag", attendanceInsert.IOFlage);
            vParams.Add("@Reason", attendanceInsert.Reason);
            vParams.Add("@IMEINo", attendanceInsert.IMEINO);
            vParams.Add("@Latitude", attendanceInsert.Latitude);
            vParams.Add("@Longitude", attendanceInsert.Longitude);
            vParams.Add("@Address", attendanceInsert.Address);
            vParams.Add("@Emp_Image", attendanceInsert.ImageName);
            vParams.Add("@strAttendance", attendanceInsert.strAttendance);
            vParams.Add("@Month", 0);
            vParams.Add("@Year", 0);
            vParams.Add("@Type", attendanceInsert.Type);
            vParams.Add("@Result", "");
            vParams.Add("@SubVerticalName", attendanceInsert.subVerticalName);
            var response = vconn.Query("SP_Mobile_HRMS_WebService_Attendance", vParams, commandType: CommandType.StoredProcedure).ToList();
            return response;
        }

        public AttendanceRecordRespomse AttendanceRegularizeDetails(AttendanceRegularizeDetails attendanceRegularizeDetails)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            AttendanceRecordRespomse attendanceRecordRespomse = new();
            vParams.Add("@IO_Tran_Id", attendanceRegularizeDetails.IOTranId, DbType.Int32);
            vParams.Add("@Emp_ID", attendanceRegularizeDetails.EmpID, DbType.Int32);
            vParams.Add("@Cmp_ID", attendanceRegularizeDetails.CmpID, DbType.Int32);
            vParams.Add("@Month", attendanceRegularizeDetails.Month, DbType.Int32);
            vParams.Add("@Year", attendanceRegularizeDetails.Year, DbType.Int32);
            vParams.Add("@For_Date", DateTime.Now.ToString("dd/MMM/yyyy"), DbType.String);
            vParams.Add("@Reason", "", DbType.String);
            vParams.Add("@Half_Full_Day", "", DbType.String);
            vParams.Add("@Is_Cancel_Late_In", 0, DbType.Int32);
            vParams.Add("@Is_Cancel_Early_Out", 0, DbType.Int32);
            vParams.Add("@In_Date_Time", attendanceRegularizeDetails.Fromdate, DbType.DateTime);
            vParams.Add("@Out_Date_Time", attendanceRegularizeDetails.Todate, DbType.DateTime);
            vParams.Add("@Is_Approve", 0, DbType.Int32);
            vParams.Add("@Other_Reason", "", DbType.String);
            vParams.Add("@IMEINo", "", DbType.String);
            vParams.Add("@S_Emp_ID", 0, DbType.Int32);
            vParams.Add("@Rpt_Level", 0, DbType.Int32);
            vParams.Add("@Final_Approve", 0, DbType.Int32);
            vParams.Add("@Is_Fwd_Leave_Rej", 0, DbType.Int32);
            vParams.Add("@Approval_Status", "", DbType.String);
            vParams.Add("@Type", attendanceRegularizeDetails.Type, DbType.String);

            // Add output parameter
            vParams.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);

            // Execute the stored procedure
            var response = vconn.QueryMultiple("SP_Mobile_HRMS_WebService_AttendanceRegularization", vParams, commandType: CommandType.StoredProcedure);

            // Retrieve the output parameter value

            // Retrieve and map each result set
            var AttendanceRecords = response.Read<AttendanceRecord>().ToList();
            var AttendanceDatas = response.Read<AttendanceData>().ToList();
            var months = response.Read<MonthDateRange>().ToList();

            // If the third result set is not used, you can skip reading it or handle it as needed
            // For the fourth result set
            var AttendanceSettings = response.Read<AttendanceSettings>().ToList();

            // For the fifth result set
            var SettingDatas = response.Read<SettingData>().ToList();
            var result = vParams.Get<string>("@Result");

            attendanceRecordRespomse.attendanceRecords = AttendanceRecords;
            attendanceRecordRespomse.attendanceDatas = AttendanceDatas;
            attendanceRecordRespomse.monthDateRanges = months;
            attendanceRecordRespomse.attendanceSettings = AttendanceSettings;
            attendanceRecordRespomse.settingDatas = SettingDatas;
            attendanceRecordRespomse.Result = result;
            return attendanceRecordRespomse;
        }

        public dynamic AttendanceRegularizeInsert(AttendanceRegularizeInsert attendanceRegularizeInsert)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@IO_Tran_Id", attendanceRegularizeInsert.IOTranId);
            vParams.Add("@Emp_ID", attendanceRegularizeInsert.EmpID);
            vParams.Add("@Cmp_ID", attendanceRegularizeInsert.CmpID);
            vParams.Add("@Month", attendanceRegularizeInsert.Month);
            vParams.Add("@Year", attendanceRegularizeInsert.Year);
            vParams.Add("@For_Date", attendanceRegularizeInsert.Fordate);
            vParams.Add("@Reason", attendanceRegularizeInsert.Reason);
            vParams.Add("@Half_Full_Day", attendanceRegularizeInsert.HalfFullDay);
            vParams.Add("@Is_Cancel_Late_In", attendanceRegularizeInsert.CancelLateIn);
            vParams.Add("@Is_Cancel_Early_Out", attendanceRegularizeInsert.CancelEarlyOut);
            if (Convert.ToString(attendanceRegularizeInsert.Intime) == "0")
            {
                vParams.Add("@In_Date_Time", DBNull.Value, DbType.DateTime);
            }
            else
            {
                vParams.Add("@In_Date_Time", Convert.ToDateTime(attendanceRegularizeInsert.Intime), DbType.DateTime);
            }

            if (Convert.ToString(attendanceRegularizeInsert.OutTime) == "0")
            {
                vParams.Add("@Out_Date_Time", DBNull.Value, DbType.DateTime);
            }
            else
            {
                vParams.Add("@Out_Date_Time", Convert.ToDateTime(attendanceRegularizeInsert.OutTime), DbType.DateTime);
            }

            vParams.Add("@Is_Approve", attendanceRegularizeInsert.Is_Approve);
            vParams.Add("@Other_Reason", attendanceRegularizeInsert.Other_Reason);
            vParams.Add("@IMEINo", attendanceRegularizeInsert.IMEINo);
            vParams.Add("@S_Emp_ID", attendanceRegularizeInsert.SEmp_ID);
            vParams.Add("@Rpt_Level", attendanceRegularizeInsert.RptLevel);
            vParams.Add("@Final_Approve", attendanceRegularizeInsert.FinalApprove);
            vParams.Add("@Is_Fwd_Leave_Rej", attendanceRegularizeInsert.IsFwdLeaveRej);
            vParams.Add("@Approval_Status", attendanceRegularizeInsert.ApprovalStatus);
            vParams.Add("@Type", attendanceRegularizeInsert.strType);
            vParams.Add("@Result", "");
            var response = vconn.Query("SP_Mobile_HRMS_WebService_AttendanceRegularization", vParams, commandType: CommandType.StoredProcedure).ToList();
            return response;
        }

        public int GetMaxapp(LeaveBalanceRequest attendanceRegularizeDetail)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", attendanceRegularizeDetail.EmpId);
            vParams.Add("@Cmp_ID", attendanceRegularizeDetail.CmpId);
            vParams.Add("@Month", attendanceRegularizeDetail.Month);
            vParams.Add("@Year", attendanceRegularizeDetail.Year);
            string query = @"select COUNT(MONTH(for_date)) FROM T0150_EMP_INOUT_RECORD WHERE Cmp_ID=@Cmp_ID AND Emp_ID=@Emp_ID and Month(For_date)=@Month and Year(for_date) =@Year and App_Date is not null and Reason<>''";

            int data = vconn.QueryFirstOrDefault<int>(query, vParams);
            return data;
        }

        public int GetMaxappEndDate(DateTime st_date, DateTime end_date, LeaveBalanceRequest attendanceRegularizeDetail)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", attendanceRegularizeDetail.EmpId);
            vParams.Add("@Cmp_ID", attendanceRegularizeDetail.CmpId);
            vParams.Add("@StartDate", st_date);
            vParams.Add("@EndDate", end_date);

            string query = @"
        SELECT COUNT(MONTH(for_date)) AS CNTMX 
        FROM T0150_EMP_INOUT_RECORD 
        WHERE Cmp_ID = @Cmp_ID 
          AND Emp_ID = @Emp_ID 
          AND For_date >= @StartDate 
          AND For_date <= @EndDate 
          AND App_Date IS NOT NULL 
          AND Reason <> ''";

            int data = vconn.QueryFirstOrDefault<int>(query, vParams);
            return data;
        }


        public object GetsalaryEndDate(DateTime st_date)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@StartDate", st_date);
            string query = @"select dateadd(d,-1,dateadd(m,1,'@StartDate')) as end_dt";

            var data = vconn.Query(query, vParams);
            return data;
        }

        public DataTable GetsalaryStartDate(DateTime st_date, LeaveBalanceRequest attendanceRegularizeDetail)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@StartDate", st_date);
            vParams.Add("@Month", Convert.ToString(attendanceRegularizeDetail.Month));
            vParams.Add("@Year", Convert.ToString(attendanceRegularizeDetail.Year));
            string query = @"select cast(cast(day('@StartDate')as varchar(5)) + '-' +'@Month' + '-' +  '@Year' as smalldatetime) as mnth_st_dt";

            var data = vconn.Query(query, vParams);
            return (DataTable)data;
        }
    }
}
