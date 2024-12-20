﻿using Dapper;
using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.Employee;
using MOBILEAPI2024.DTO.ResponseDTO.Account;
using MOBILEAPI2024.DTO.ResponseDTO.Employee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DAL.Repositories
{
    public class EmployeeRepository : SqlDbRepository<ActiveInActiveUser>, IEmployeeRepository
    {
        public EmployeeRepository(string connectionString) : base(connectionString)
        {
        }

        public dynamic EmployeeDetails(EmployeeDetails empDetails)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", empDetails.Emp_ID);
            vParams.Add("@Cmp_ID", empDetails.Cmp_ID);
            vParams.Add("@Vertical_ID", empDetails.Vertical_ID);
            vParams.Add("@Emp_Code", empDetails.Emp_Code);
            vParams.Add("@Address", empDetails.Address);
            vParams.Add("@City", empDetails.City);
            vParams.Add("@State", empDetails.State);
            vParams.Add("@Pincode", empDetails.Pincode);
            vParams.Add("@PhoneNo", empDetails.PhoneNo);
            vParams.Add("@MobileNo", empDetails.MobileNo);
            vParams.Add("@Email", empDetails.Email);
            vParams.Add("@ImageName", empDetails.ImageName);
            vParams.Add("@Branch_ID", empDetails.Branch_ID);
            vParams.Add("@Department_ID", empDetails.Department_ID);
            vParams.Add("@Type", empDetails.Type);
            vParams.Add("@Result", "");
            var response = vconn.Query("SP_Mobile_HRMS_WebService_EmpDetails", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic EmployeeDetailsForTally(int cmpId, string branchName)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Branch_Name", branchName);
            vParams.Add("@Type", "E");
            var response = vconn.Query("SP_Tally_WebService", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic EmployeeDirectoryData(int cmpId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpId);
            var response = vconn.Query<EmployeeDirectoryDataResponse>("SP_Mobile_EmployeeDirectory_Data", vParams, commandType: CommandType.StoredProcedure).ToList();
            return response;
        }

        public dynamic ManagerApprovalDetails(ManagerApprovalDetailsRequest managerApprovalDetailsRequest)
        {
            ManagerApprovalData managerApprovalData = new();
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();

            vParams.Add("@Cmp_ID", managerApprovalDetailsRequest.CmpId);
            vParams.Add("@Emp_ID", managerApprovalDetailsRequest.EmpId);
            vParams.Add("@Travel_Application_ID", managerApprovalDetailsRequest.TravelApplicationId);
            vParams.Add("@Claim_App_ID", managerApprovalDetailsRequest.ClaimAppId);
            vParams.Add("@Leave_Application_ID", managerApprovalDetailsRequest.LeaveApplicationId);
            vParams.Add("@flag", managerApprovalDetailsRequest.Flag);

            var response = vconn.QueryMultiple("MOBILE_HRMS_SCHEME_DETAILS_ESS_GET", vParams, commandType: CommandType.StoredProcedure);

            // Get the first manager approval detail or null if no result is returned
            var managerApproval = response.Read<ManagerApprovalDetail>().FirstOrDefault();

            // Read the list of application details
            var applications = response.Read<ApplicationDetail>().ToList();

            // Assign the results to the respective properties
            managerApprovalData.Data = managerApproval;
            managerApprovalData.Data1 = applications;

            return managerApprovalData;
        }


        public List<AttendanceRegularizeDetailsResponse> AttendanceRegularizeDetails(AttendanceRegularizeDetails attendanceRegularizeDetails)
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
            vParams.Add("@Half_Full_Day", "" );
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
            vParams.Add("@Approval_Status", "" );
            vParams.Add("@Type", attendanceRegularizeDetails.Type);
            vParams.Add("@Result", "");
            var response = vconn.Query<AttendanceRegularizeDetailsResponse>("SP_Mobile_HRMS_WebService_AttendanceRegularization", vParams, commandType: CommandType.StoredProcedure).ToList();
            return response;
        }

        public string UpdateEmployeeDetails(UpdateEmployeeDetails updateEmployeeDetails)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", updateEmployeeDetails.EmpID);
            vParams.Add("@Cmp_ID", updateEmployeeDetails.CmpID);
            vParams.Add("@Vertical_ID", 0);
            vParams.Add("@Emp_Code", "");
            vParams.Add("@Address", updateEmployeeDetails.Address);
            vParams.Add("@City", updateEmployeeDetails.City);
            vParams.Add("@State", updateEmployeeDetails.State);
            vParams.Add("@Pincode", updateEmployeeDetails.Pincode);
            vParams.Add("@PhoneNo", updateEmployeeDetails.PhoneNo);
            vParams.Add("@MobileNo", updateEmployeeDetails.MobileNo);
            vParams.Add("@Email", updateEmployeeDetails.Email);
            vParams.Add("@ImageName", updateEmployeeDetails.Imagename);
            vParams.Add("@Branch_ID", 0);
            vParams.Add("@Department_ID", 0);
            vParams.Add("@Type", updateEmployeeDetails.StrType);
            vParams.Add("@Result", "");
            string response = vconn.QueryFirst<string>("SP_Mobile_HRMS_WebService_EmpDetails", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic MyTeamDetails(int empId, int cmpId, string status)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", empId);
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Status", status);
            var response = vconn.Query("SP_Mobile_HRMS_WebService_MyTeam", vParams, commandType: CommandType.StoredProcedure).ToList();
            return response;
        }

        public dynamic NewJoiningEmployeeDetails(int cmpId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@P_Branch", "");
            vParams.Add("@P_Department", "");
            vParams.Add("@P_Vertical", "");
            vParams.Add("@P_SubVertical", "");
            var response = vconn.Query("P_Common_Home_Pages_New_Joining_Details", vParams, commandType: CommandType.StoredProcedure).ToList();
            return response;
        }

        public dynamic UpdateEmpFavDetails(UpdateEmpFavDetailsRequest updateEmpFavDetailsRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", updateEmpFavDetailsRequest.EmpID);
            vParams.Add("@Cmp_ID", updateEmpFavDetailsRequest.CmpID);
            vParams.Add("@Emp_Code", "");
            vParams.Add("@EmpFavSportId", updateEmpFavDetailsRequest.EmpFavSportID);
            vParams.Add("@EmpFavSportName", updateEmpFavDetailsRequest.EmpFavSportName); ;
            vParams.Add("@EmpHobbyID", updateEmpFavDetailsRequest.EmpHobbyID);
            vParams.Add("@EmpHobbyName", updateEmpFavDetailsRequest.EmpHobbyName);
            vParams.Add("@EmpFavFood", updateEmpFavDetailsRequest.EmpFavFood);
            vParams.Add("@EmpFavRestro", updateEmpFavDetailsRequest.EmpFavRestro);
            vParams.Add("@EmpFavTrvDest", updateEmpFavDetailsRequest.EmpFavTrvDest);
            vParams.Add("@EmpFavFest", updateEmpFavDetailsRequest.EmpFavFest);
            vParams.Add("@EmpFavSportPerson", updateEmpFavDetailsRequest.EmpFavSportPerson);
            vParams.Add("@EmpFavSinger", updateEmpFavDetailsRequest.EmpFavSinger);
            vParams.Add("@Type", updateEmpFavDetailsRequest.Type);
            vParams.Add("@Result", "");
            var response = vconn.Query("SP_Mobile_HRMS_WebService_EmpFavDetails", vParams, commandType: CommandType.StoredProcedure).ToList();
            return response;
        }

        public dynamic UpdateEmployeeDetailsMain(UpdateEmployeeDetailsRequest updateEmployeeDetails)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", updateEmployeeDetails.EmpID);
            vParams.Add("@Cmp_ID", updateEmployeeDetails.CmpID);
            vParams.Add("@Vertical_ID", 0);
            vParams.Add("@Emp_Code", "");
            vParams.Add("@Address", updateEmployeeDetails.Address);
            vParams.Add("@City", updateEmployeeDetails.City);
            vParams.Add("@State", updateEmployeeDetails.State);
            vParams.Add("@Pincode", updateEmployeeDetails.Pincode);
            vParams.Add("@PhoneNo", updateEmployeeDetails.PhoneNo);
            vParams.Add("@MobileNo", updateEmployeeDetails.MobileNo);
            vParams.Add("@Email", updateEmployeeDetails.Email);
            vParams.Add("@ImageName", "");
            vParams.Add("@Branch_ID", 0);
            vParams.Add("@Department_ID", 0);
            vParams.Add("@Type", updateEmployeeDetails.Type);
            vParams.Add("@Result", "");
            string response = vconn.QueryFirst<string>("SP_Mobile_HRMS_WebService_EmpDetails", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic MyTeamAttendanceInsert(MyTeamAttendanceInsertRequest myTeamAttendanceInsertRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", myTeamAttendanceInsertRequest.EmpId);
            vParams.Add("@Details", myTeamAttendanceInsertRequest.Details);
            vParams.Add("@Type", myTeamAttendanceInsertRequest.strType);
            vParams.Add("@Result", "");
            var response = vconn.Query("SP_Tally_WebService", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }
    }
}
