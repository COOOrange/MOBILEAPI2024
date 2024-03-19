using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Server;
using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.Common;
using MOBILEAPI2024.DTO.RequestDTO;
using MOBILEAPI2024.DTO.ResponseDTO;
using System;
using System.Data;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MOBILEAPI2024.DAL.Repositories
{
    public class UserRepository : SqlDbRepository<ActiveInActiveUser>, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        public void AddClockIn(ClockIn clockIn)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", clockIn.Emp_Id);
            vParams.Add("@Cmp_ID", clockIn.Cmp_Id);
            vParams.Add("@Vertical_ID", Convert.ToInt32("0"));
            vParams.Add("@SubVertical_ID", Convert.ToInt32("0"));
            vParams.Add("@ForDate ", Convert.ToDateTime(DateTime.UtcNow).ToString("yyyy/MM/dd"));
            vParams.Add("@Time", Convert.ToDateTime(DateTime.UtcNow).ToString("yyyy/MM/dd"));
            vParams.Add("@INOUTFlag", "I");
            vParams.Add("@Reason", "");
            vParams.Add("@IMEINo", "");
            vParams.Add("@Latitude", clockIn.Latitude);
            vParams.Add("@Longitude", clockIn.Longitude);
            vParams.Add("@Address", clockIn.Address);
            vParams.Add("@Emp_Image", clockIn.file.FileName);
            vParams.Add("@strAttendance", "");
            vParams.Add("@Month", "");
            vParams.Add("@Year", "");
            vParams.Add("@Type", "I");
            vParams.Add("@Result", "");
            vParams.Add("@SubVerticalName", "");

            vconn.QueryFirstOrDefault<string>("SP_Mobile_HRMS_WebAPI_Attendance", vParams, commandType: CommandType.StoredProcedure);
        }

        public void AddClockOut(ClockIn clockIn)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", clockIn.Emp_Id);
            vParams.Add("@Cmp_ID", clockIn.Cmp_Id);
            vParams.Add("@Vertical_ID", Convert.ToInt32("0"));
            vParams.Add("@SubVertical_ID", Convert.ToInt32("0"));
            vParams.Add("@ForDate ", Convert.ToDateTime(DateTime.UtcNow).ToString("yyyy/MM/dd"));
            vParams.Add("@Time", Convert.ToDateTime(DateTime.UtcNow).ToString("yyyy/MM/dd"));
            vParams.Add("@INOUTFlag", "O");
            vParams.Add("@Reason", "");
            vParams.Add("@IMEINo", "");
            vParams.Add("@Latitude", clockIn.Latitude);
            vParams.Add("@Longitude", clockIn.Longitude);
            vParams.Add("@Address", clockIn.Address);
            vParams.Add("@Emp_Image", clockIn.file.FileName);
            vParams.Add("@strAttendance", "");
            vParams.Add("@Month", "");
            vParams.Add("@Year", "");
            vParams.Add("@Type", "I");
            vParams.Add("@Result", "");
            vParams.Add("@SubVerticalName", "");

            vconn.QueryFirstOrDefault<string>("SP_Mobile_HRMS_WebAPI_Attendance", vParams, commandType: CommandType.StoredProcedure);

        }

        public void AddTransactionData(Transaction transaction)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", Convert.ToInt32("4"));
            vParams.Add("@Enroll_No", transaction.punchId);
            vParams.Add("@IO_Datetime", transaction.txnDateTime);
            vParams.Add("@Ip_Address", transaction.dvcIP);
            if (transaction.mode == "IN" || transaction.mode == "in" || transaction.mode == "In" )
            {
                vParams.Add("@In_Out_flag", "0");
            }
            else if(transaction.mode == "OUT" || transaction.mode == "out" || transaction.mode == "Out")
            {
                vParams.Add("@In_Out_flag", "1");
            }
            else
            {
                vParams.Add("@In_Out_flag", "-1");
            }


            vconn.Execute("P9999_DEVICE_INOUT_DETAIL_DATAINSERT", vParams, commandType: CommandType.StoredProcedure);

        }

        public bool CheckEnrollNoExixts(Transaction transaction)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", Convert.ToInt32("4"));
            vParams.Add("@Enroll_No", transaction.punchId);

            var checkEnroll = vconn.QueryFirstOrDefault("P0080_EMP_MASTER_CHECK_ENROLL", vParams, commandType: CommandType.StoredProcedure);
            if (checkEnroll != null)
            {
                return true;
            }
            return false;
        }

        public bool CheckTransactionExistence(Transaction transaction)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", Convert.ToInt32("4"));
            vParams.Add("@Enroll_No", transaction.punchId);
            vParams.Add("@IO_Datetime", transaction.txnDateTime);
            vParams.Add("@Ip_Address", transaction.dvcIP);
            if (transaction.mode == "IN" || transaction.mode == "in" || transaction.mode == "In")
            {
                vParams.Add("@In_Out_flag", "0");
            }
            else if (transaction.mode == "OUT" || transaction.mode == "out" || transaction.mode == "Out")
            {
                vParams.Add("@In_Out_flag", "1");
            }
            else
            {
                vParams.Add("@In_Out_flag", "-1");
            }

            var transactionData = vconn.QueryFirstOrDefault("P9999_DEVICE_INOUT_DETAIL_CHECK_EXIXTS", vParams, commandType: CommandType.StoredProcedure);
            if(transactionData != null)
            {
                return true;
            }
            return false;

        }

        public DashboardDTO DashboardData(string empId, string cmpId)
        {
            DashboardDTO dashboardDTO = new DashboardDTO();
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", Convert.ToInt32(empId));
            vParams.Add("@Cmp_ID", Convert.ToInt32(cmpId));

            var employeeCount = vconn.QueryMultiple("SP_Mobile_HRMS_WebService_DASHBOARD", vParams, commandType: CommandType.StoredProcedure);
            var emp = employeeCount.Read().FirstOrDefault();
            var finalEmployee = employeeCount.Read().FirstOrDefault();
            if (finalEmployee != null)
            {
                var employeeCou = new EmployeeCount();

                employeeCou.emp_id = finalEmployee.emp_id;
                employeeCou.Present = finalEmployee.Present;
                employeeCou.WO = finalEmployee.WO;
                employeeCou.HO = finalEmployee.HO;
                employeeCou.OD = finalEmployee.OD;
                employeeCou.Absent = finalEmployee.Absent;
                employeeCou.Leave = finalEmployee.Leave;
                employeeCou.Total = finalEmployee.Total;
                employeeCou.D_Present = finalEmployee.D_Present;
                
                dashboardDTO.employeeCount = employeeCou;
            }

            vParams.Add("@Vertical_ID", Convert.ToInt32("0"));
            vParams.Add("@SubVertical_ID", Convert.ToInt32("0"));
            vParams.Add("@ForDate", "");
            vParams.Add("@Time", "");
            vParams.Add("@INOUTFlag", "");
            vParams.Add("@Reason", "");
            vParams.Add("@IMEINo", "");
            vParams.Add("@Latitude", "");
            vParams.Add("@Longitude", "");
            vParams.Add("@Address", "");
            vParams.Add("@Emp_Image", "");
            vParams.Add("@strAttendance", "");
            vParams.Add("@Month", "");
            vParams.Add("@Year", "");
            vParams.Add("@Type", "L");
            vParams.Add("@Result", "");
            vParams.Add("@SubVerticalName", "");

            var employeeData = vconn.QueryFirstOrDefault<EmployeeData>("SP_Mobile_HRMS_Attendance", vParams, commandType: CommandType.StoredProcedure);
            dashboardDTO.employeeData = employeeData;
            return dashboardDTO;
        }

        public void UpdateTransactionData(Transaction transaction)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", Convert.ToInt32("110"));
            vParams.Add("@IO_tran_ID", transaction.txnId);
            vParams.Add("@Enroll_No", transaction.dvcId);
            vParams.Add("@IO_Datetime", transaction.txnDateTime);
            vParams.Add("@Ip_Address", transaction.dvcIP);
            vParams.Add("@In_Out_flag", transaction.mode);
            vParams.Add("@IU_Flag", "U");

            vconn.Execute("SP_Mobile_HRMS_Attendance", vParams, commandType: CommandType.StoredProcedure);

        }
    }
}
