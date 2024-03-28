using Dapper;
using Microsoft.SqlServer.Server;
using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.Attendance;
using MOBILEAPI2024.DTO.RequestDTO.Employee;
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
            var response = vconn.Query("SP_Mobile_HRMS_WebService_AttendanceRegularization", vParams, commandType: CommandType.StoredProcedure);
            return response;
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
    }
}
