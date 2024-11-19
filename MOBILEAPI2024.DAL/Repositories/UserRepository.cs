using Azure.Core;
using Dapper;
using Microsoft.SqlServer.Server;
using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.RequestDTO.User;
using MOBILEAPI2024.DTO.ResponseDTO.User;
using System.Collections;
using System.Data;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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


            vconn.Execute("P9999_DEVICE_INOUT_DETAIL_DATAINSERT", vParams, commandType: CommandType.StoredProcedure);

        }

        public dynamic AddWorkPlanOnClockIn(AddWorkPlanOnClockInRequest addWorkPlanOnClockInRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", addWorkPlanOnClockInRequest.CmpID);
            vParams.Add("@Emp_ID", addWorkPlanOnClockInRequest.EmpID);
            vParams.Add("@Work_Plan", addWorkPlanOnClockInRequest.WorkPlan);
            vParams.Add("@Visit_Plan", addWorkPlanOnClockInRequest.VisitPlan);
            vParams.Add("@Work_Summary", "");
            vParams.Add("@Visit_Summary", "");
            vParams.Add("@INOUTFlag", "I");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_WorkPlan", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic AddWorkPlanOnClockOut(AddWorkPlanOnClockInRequest addWorkPlanOnClockInRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", addWorkPlanOnClockInRequest.CmpID);
            vParams.Add("@Emp_ID", addWorkPlanOnClockInRequest.EmpID);
            vParams.Add("@Work_Plan", addWorkPlanOnClockInRequest.WorkPlan);
            vParams.Add("@Visit_Plan", addWorkPlanOnClockInRequest.VisitPlan);
            vParams.Add("@Work_Summary", "");
            vParams.Add("@Visit_Summary", "");
            vParams.Add("@INOUTFlag", "O");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_WorkPlan", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic ChangeRequest(ChangeRequest changeRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Row_ID", changeRequest.Row_ID);
            vParams.Add("@Cmp_ID", changeRequest.CmpID);
            vParams.Add("@Emp_Id", changeRequest.Emp_Id);
            vParams.Add("@Request_Type_id", changeRequest.RequestTypeId);
            vParams.Add("@Change_Reason", changeRequest.Change_Reason);
            vParams.Add("@Request_Date", changeRequest.Request_Date);
            vParams.Add("@Shift_From_Date", changeRequest.Shift_From_Date);
            vParams.Add("@Shift_To_Date", changeRequest.Shift_To_Date);
            vParams.Add("@Dependant_Name", changeRequest.Dependant_Name);
            vParams.Add("@Dependant_Relationship", changeRequest.Dependant_Relationship);
            vParams.Add("@Dependant_Gender", changeRequest.Dependant_Gender);
            vParams.Add("@Dependant_DOB", changeRequest.Dependant_DOB);
            vParams.Add("@Dependant_Age", changeRequest.Dependant_Age);
            vParams.Add("@Dependant_Is_Resident", changeRequest.Dependant_Is_Resident);
            vParams.Add("@Dependant_Is_Depended", changeRequest.Dependant_Is_Depended);
            vParams.Add("@Tran_Type", changeRequest.TranType);
            vParams.Add("@Child_Birth_Date", changeRequest.Child_Birth_Date);
            vParams.Add("@DepOccupationID", changeRequest.DepOccupationID);
            vParams.Add("@DepHobbyID", changeRequest.DepHobbyID);
            vParams.Add("@DepHobbyName", changeRequest.DepHobbyName);
            vParams.Add("@DepCompany", changeRequest.DepCompany);
            vParams.Add("@DepCmpCity", changeRequest.DepCmpCity);
            vParams.Add("@DepStandardId", changeRequest.DepStandardId);
            vParams.Add("@DepSchCol", changeRequest.DepSchCol);
            vParams.Add("@DepSchColCity", changeRequest.DepSchColCity);
            vParams.Add("@DepExtAct", changeRequest.DepExtAct);
            vParams.Add("@Image_path", changeRequest.ImageName);
            vParams.Add("@PanCard", changeRequest.PanCard);
            vParams.Add("@AdharCard", changeRequest.AdharCard);
            vParams.Add("@Height", changeRequest.Height);
            vParams.Add("@Weight", changeRequest.Weight);
            vParams.Add("@OtherHobby", changeRequest.OtherHobby);
            vParams.Add("@DepSpecialization", changeRequest.Specialization);
            vParams.Add("@Request_id", dbType: DbType.Decimal, direction: ParameterDirection.Output);
            vParams.Add("@Result", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            var Response = vconn.Query("SP_Mobile_WebService_ChangeRequestApp", vParams, commandType: CommandType.StoredProcedure);
            int requestId = Convert.ToInt32(vParams.Get<string>("@Request_id"));
            string result = vParams.Get<string>("@Result");
            return result;
        }

        public dynamic ChangeRequestBind(int cmpId, int empID, string tranType)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Emp_Id", empID);
            vParams.Add("@Tran_Type", tranType);
            vParams.Add("@Result", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            var Response = vconn.Query("SP_Mobile_WebService_ChangeRequest", vParams, commandType: CommandType.StoredProcedure);
            return Response;

        }

        public dynamic ChangeRequestFav(ChangeRequestFav changeRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Row_ID", changeRequest.Row_ID);
            vParams.Add("@Cmp_ID", changeRequest.CmpID);
            vParams.Add("@Emp_Id", changeRequest.Emp_Id);
            vParams.Add("@Request_Type_id", changeRequest.RequestTypeId);
            vParams.Add("@Change_Reason", changeRequest.Change_Reason);
            vParams.Add("@Request_Date", changeRequest.Request_Date);
            vParams.Add("@EmpFavSportID", changeRequest.EmpFavSportID);
            vParams.Add("@EmpFavSportName", changeRequest.EmpFavSportName);
            vParams.Add("@EmpHobbyID", changeRequest.EmpHobbyID);
            vParams.Add("@EmpHobbyName", changeRequest.EmpHobbyName);
            vParams.Add("@EmpFavFood", changeRequest.EmpFavFood);
            vParams.Add("@EmpFavRestro", changeRequest.EmpFavRestro);
            vParams.Add("@EmpFavTrvDestination", changeRequest.EmpFavTrvDestination);
            vParams.Add("@EmpFavFestival", changeRequest.EmpFavFestival);
            vParams.Add("@EmpFavSportPerson", changeRequest.EmpFavSportPerson);
            vParams.Add("@EmpFavSinger", changeRequest.EmpFavSinger);
            vParams.Add("@CurrEmpFavSportID", changeRequest.CurrEmpFavSportID);
            vParams.Add("@CurrEmpFavSportName", changeRequest.CurrEmpFavSportID);
            vParams.Add("@CurrEmpHobbyID", changeRequest.CurrEmpHobbyID);
            vParams.Add("@CurrEmpHobbyName", changeRequest.CurrEmpHobbyName);
            vParams.Add("@CurrEmpFavFood", changeRequest.CurrEmpFavFood);
            vParams.Add("@CurrEmpFavRestro", changeRequest.CurrEmpFavRestro);
            vParams.Add("@CurrEmpFavTrvDestination", changeRequest.CurrEmpFavTrvDestination);
            vParams.Add("@CurrEmpFavFestival", changeRequest.CurrEmpFavFestival);
            vParams.Add("@CurrEmpFavSportPerson", changeRequest.CurrEmpFavSportPerson);
            vParams.Add("@CurrEmpFavSinger", changeRequest.CurrEmpFavSinger);
            vParams.Add("@Image_path", changeRequest.ImagePath);
            vParams.Add("@tran_type", changeRequest.TranType);
            vParams.Add("@OtherSport", changeRequest.OtherSport);
            vParams.Add("@OtherHobby", changeRequest.OtherHobby);
            vParams.Add("@Request_id", dbType: DbType.Decimal, direction: ParameterDirection.Output);
            vParams.Add("@Result", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            var Response = vconn.Query("SP_Mobile_WebService_ChgReqFavApp", vParams, commandType: CommandType.StoredProcedure);
            //int requestId = Convert.ToInt32(vParams.Get<string>("@Request_id"));
            string result = vParams.Get<string>("@Result");
            return result;
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
            if (transactionData != null)
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

        public dynamic Dashboard_backup(int cmpId, int empID)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", empID);
            vParams.Add("@Cmp_ID", cmpId);
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_DASHBOARD", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic EventDetails(int cmpId, int empId, string forDate)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@For_Date", Convert.ToDateTime(forDate));
            vParams.Add("@Emp_ID", empId);
            vParams.Add("@Cmp_ID", cmpId);
            var Response = vconn.Query("P0100_EVENT_DETAIL_GET", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic GeoLocationTracking(GeoLocationRequest geoLocationRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();

            // Adding all parameters to the dynamic parameters object
            vParams.Add("@Emp_ID", geoLocationRequest.EmpID);
            vParams.Add("@Cmp_ID", geoLocationRequest.CmpID);
            vParams.Add("@TrackingDate", DateTime.Now);  // Changed from "@Date"
            vParams.Add("@Latitude", geoLocationRequest.Latitude);
            vParams.Add("@Longitude", geoLocationRequest.Longitude);
            vParams.Add("@Address_location", geoLocationRequest.AddressLocation ?? ""); // Nullable check
            vParams.Add("@City", "");  // New parameter with null check
            vParams.Add("@Area", "");  // New parameter with null check
            vParams.Add("@Battery_Level", "");  // New parameter with null check
            vParams.Add("@IMEI_No", "");  // New parameter with null check
            vParams.Add("@GPS_Accuracy", "");  // New parameter
            vParams.Add("@Model_Name", "");  // New parameter with null check
            vParams.Add("@Type", "I");
            vParams.Add("@Result", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);  // Updated Result handling

            // Execute stored procedure
            var geoLocationResponse = vconn.Query("SP_GeoLocationTracing_API", vParams, commandType: CommandType.StoredProcedure);

            // Get the output parameter value
            string result = vParams.Get<string>("@Result");

            // Return the response
            return new { Response = geoLocationResponse, Result = result };
        }


        public dynamic GeoLocationTrackingList(int cmpId, int empId, DateTime date)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", empId);
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@TrackingDate", date);
            vParams.Add("@Latitude", "");
            vParams.Add("@Longitude", "");
            vParams.Add("@Address_location", "");
            vParams.Add("@Type", "L");
            vParams.Add("@Result", "");
            var geoLocationResponse = vconn.Query("SP_GeoLocationTracing_API", vParams, commandType: CommandType.StoredProcedure);
            return geoLocationResponse;
        }

        public dynamic GetBankList(int cmpID)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            string query;

            query = "SELECT * FROM T0040_BANK_MASTER WHERE Cmp_ID = @CmpID ORDER BY Bank_Branch_Name ASC";

            vParams.Add("@CmpID", cmpID);
            var response = vconn.Query<dynamic>(query, vParams);
            return response;
        }

        public dynamic GetCity(int cmpId, int stateID)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            string query;

            query = "SELECT City_ID,City_Name FROM T0030_CITY_MASTER WHERE State_ID =@StateID AND Cmp_ID = @CmpID ORDER BY City_Name ASC";
            vParams.Add("@CmpID", cmpId);
            vParams.Add("@StateID", stateID);
            var response = vconn.Query<dynamic>(query, vParams); // Pass the parameters object
            return response;
        }

        public dynamic GetCountry()
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            string query;

            query = "SELECT Loc_ID,Loc_name FROM T0001_LOCATION_MASTER ORDER BY Loc_ID ASC";

            var response = vconn.Query<dynamic>(query, vParams); // Pass the parameters object
            return response;
        }

        public dynamic GetDashboardApplicationsCount(int cmpId, int empId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Emp_Id", empId);
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_GETCOUNT", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic GetDeviceIdFromEmpID(int empID)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            var query = "SELECT DISTINCT A.DeviceID FROM T0095_Emp_IMEI_Details AS A Inner Join (SELECT Emp_ID, MAX(Tran_ID) AS maxId FROM T0095_Emp_IMEI_Details WHERE Is_Active=1 GROUP BY Emp_ID) AS B ON A.Emp_ID = B.Emp_ID And A.Tran_ID = B.maxId where A.Emp_ID in(@EmpID) and A.DeviceID is Not NUll and A.DeviceID <> ''";
            vParams.Add("@EmpID", empID);
            var response = vconn.Query<dynamic>(query, vParams); // Pass the parameters object
            return response;
        }

        public dynamic GetEmpIDFromCmpID(int cmpID)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            var query = "SELECT STUFF((SELECT DISTINCT ',' + CAST(I_1.Emp_ID AS varchar) FROM  T0080_EMP_MASTER AS E LEFT OUTER JOIN (SELECT i.Emp_ID, i.Dept_ID, i.Cmp_ID FROM T0095_INCREMENT AS i INNER JOIN (SELECT MAX(I2.Increment_ID) AS Increment_ID, I2.Emp_ID FROM T0095_INCREMENT AS I2 INNER JOIN (SELECT MAX(Increment_Effective_Date) AS INCREMENT_EFFECTIVE_DATE, Emp_ID FROM T0095_INCREMENT AS I3 WHERE (Increment_Effective_Date <= GETDATE()) GROUP BY Emp_ID) AS I3_1 ON I2.Increment_Effective_Date = I3_1.INCREMENT_EFFECTIVE_DATE AND I2.Emp_ID = I3_1.Emp_ID GROUP BY I2.Emp_ID) AS I2_1 ON i.Emp_ID = I2_1.Emp_ID AND i.Increment_ID = I2_1.Increment_ID) AS I_1 ON E.Emp_ID = I_1.Emp_ID LEFT OUTER JOIN T0040_DEPARTMENT_MASTER AS DM ON DM.Dept_Id = ISNULL(I_1.Dept_ID, E.Dept_ID) where E.Emp_Left = 'N'  and ISNULL(I_1.Cmp_ID, ISNULL(E.Cmp_ID,0)) in (@CmpID) FOR XML PATH('')), 1 ,1, '') AS EmpList";
            vParams.Add("@CmpID", cmpID);
            var response = vconn.Query<dynamic>(query, vParams); // Pass the parameters object
            return response;
        }

        public dynamic GetEmpIDFromDeptID(int deptID)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            var query = "SELECT STUFF((SELECT DISTINCT ',' + CAST(I_1.Emp_ID AS varchar) FROM  T0080_EMP_MASTER AS E LEFT OUTER JOIN (SELECT i.Emp_ID, i.Dept_ID FROM T0095_INCREMENT AS i INNER JOIN (SELECT MAX(I2.Increment_ID) AS Increment_ID, I2.Emp_ID FROM T0095_INCREMENT AS I2 INNER JOIN (SELECT MAX(Increment_Effective_Date) AS INCREMENT_EFFECTIVE_DATE, Emp_ID FROM T0095_INCREMENT AS I3 WHERE (Increment_Effective_Date <= GETDATE()) GROUP BY Emp_ID) AS I3_1 ON I2.Increment_Effective_Date = I3_1.INCREMENT_EFFECTIVE_DATE AND I2.Emp_ID = I3_1.Emp_ID GROUP BY I2.Emp_ID) AS I2_1 ON i.Emp_ID = I2_1.Emp_ID AND i.Increment_ID = I2_1.Increment_ID) AS I_1 ON E.Emp_ID = I_1.Emp_ID LEFT OUTER JOIN T0040_DEPARTMENT_MASTER AS DM ON DM.Dept_Id = ISNULL(I_1.Dept_ID, E.Dept_ID) where E.Emp_Left = 'N'  and ISNULL(I_1.Dept_ID, ISNULL(DM.Dept_Id,0)) in (@deptID) FOR XML PATH('')), 1 ,1, '') AS EmpList";
            vParams.Add("@deptID", deptID);
            var response = vconn.Query<dynamic>(query, vParams); // Pass the parameters object
            return response;
        }

        public dynamic GetEmployeeOTDetails(int cmpId, int empId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Compoff_App_ID", 0);
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Emp_ID", empId);
            vParams.Add("@SEmp_ID", 0);
            vParams.Add("@ForDate", DateTime.Now);
            vParams.Add("@Extra_Work_Date", DateTime.Now);
            vParams.Add("@Extra_Work_Hours", "");
            vParams.Add("@Extra_Work_Reason", "");
            vParams.Add("@Sanctioned_Hours", "");
            vParams.Add("@CompOff_Type", "");
            vParams.Add("@IMEINo", "");
            vParams.Add("@OT_Type", 0);
            vParams.Add("@Login_ID", 0);
            vParams.Add("@Email", "");
            vParams.Add("@ContactNo", "");
            vParams.Add("@Approval_Status", "");
            vParams.Add("@Approval_Comments", "");
            vParams.Add("@Type", "O");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_CompOff", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic GetHolidayList(int cmpId, int empId, int year)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", empId);
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Dept_ID", 0);
            vParams.Add("@GalleryType", 0);
            vParams.Add("@Type", "H");
            vParams.Add("@Year", year);
            vParams.Add("@Notification_ID", 0);
            vParams.Add("@NEmp_ID", 0);
            vParams.Add("@For_date", DateTime.Now);
            vParams.Add("@U_Comment_Id", 0);
            vParams.Add("@Notification_date", DateTime.Now);
            vParams.Add("@Flag", 0);
            vParams.Add("@Comment", "");
            vParams.Add("@Comment_Status", "");
            vParams.Add("@Reply_Comment_Id", 0);
            vParams.Add("@Reminder_Type", "");
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_Notification", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic GetNewJoiningUpdatedRecords(LeaveFilter getLikeCommentListRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@FROMDATE", getLikeCommentListRequest.FromDate);
            vParams.Add("@TODATE", getLikeCommentListRequest.ToDate);
            vParams.Add("@TYPE", "R");

            var Response = vconn.Query("SP_Common_Webservice_UpdatedRecords", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic GetNewsFeedDetail(int cmpId, int empId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Emp_ID", empId);
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_NewsFeed", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic GetNotification(GetNotification getNotificatioon)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", getNotificatioon.EmpID);
            vParams.Add("@Cmp_ID", getNotificatioon.CmpID);
            vParams.Add("@Dept_ID", getNotificatioon.DeptID);
            vParams.Add("@GalleryType", getNotificatioon.GalleryType);
            vParams.Add("@Type", getNotificatioon.strType);
            vParams.Add("@Year", getNotificatioon.Year);
            vParams.Add("@Notification_ID", getNotificatioon.NotificationID);
            vParams.Add("@NEmp_ID", getNotificatioon.NEmpID);
            vParams.Add("@For_date", Convert.ToDateTime(getNotificatioon.Fordate));
            vParams.Add("@U_Comment_Id", getNotificatioon.UCommentID);
            vParams.Add("@Notification_date", Convert.ToDateTime(getNotificatioon.NotificationDate));
            vParams.Add("@Flag", getNotificatioon.Flag);
            vParams.Add("@Comment", getNotificatioon.Comment);
            vParams.Add("@Comment_Status", getNotificatioon.CommentStatus);
            vParams.Add("@Reply_Comment_Id", getNotificatioon.RCommentID);
            vParams.Add("@Reminder_Type", getNotificatioon.ReminderType);
            var geoLocationResponse1 = vconn.Query("SP_Mobile_HRMS_WebService_Notification", vParams, commandType: CommandType.StoredProcedure);
            return geoLocationResponse1;
        }

        public dynamic GetPostRequestEmployee(int cmpID, int loginId, string request_Type)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpID);
            vParams.Add("@Emp_Login_ID", loginId);
            vParams.Add("@Request_ID", 0);
            vParams.Add("@Request_Type", request_Type);
            vParams.Add("@Request_Date", DateTime.Now);
            vParams.Add("@Request_Detail", "");
            vParams.Add("@Feedback_Detail", "");
            vParams.Add("@Request_Status", 0);
            vParams.Add("@Login_ID", 0);
            vParams.Add("@Type", "R");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_PostRequest", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic GetPresentDayDuration(int empid, int cmpid)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            string query = @"SELECT 
                    FORMAT(
                            DATEADD(MINUTE, DATEDIFF(MINUTE, MIN(In_Time), MAX(Out_Time)), 0), 
                            'HH:mm'
                        ) AS Duration,
                        For_Date,
                        MIN(In_Time) AS First_In_Time,
                        MAX(Out_Time) AS Last_Out_Time
                        
                    FROM T0150_EMP_INOUT_RECORD
                    WHERE Emp_ID = @EmpId 
                        AND Cmp_ID = @CmpId 
                        AND For_Date = @Date
                    GROUP BY Emp_ID, Cmp_ID, For_Date;";
            vParams.Add("@CmpId", cmpid);
            vParams.Add("@EmpId", empid);
            vParams.Add("@Date", DateTime.Now.Date);
            var Response = vconn.Query(query, vParams);
            return Response;
        }

        public dynamic GetReason(string cmpId, string reasonType, string type)
        {
            string date = Convert.ToString(DateTime.Now);
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpId, DbType.Int32);
            vParams.Add("@Emp_Login_ID", 0);
            vParams.Add("@Request_ID", 0);
            vParams.Add("@Request_Type", reasonType);
            vParams.Add("@Request_Date", DateTime.Now);
            vParams.Add("@Request_Detail", "", DbType.String);
            vParams.Add("@Feedback_Detail", "", DbType.String);
            vParams.Add("@Request_Status", 0);
            vParams.Add("@Login_ID", 0, DbType.Int32);
            vParams.Add("@Type", type, DbType.String);
            vParams.Add("@Result", "", DbType.String, direction: ParameterDirection.Output);

            var response = vconn.Query("SP_Mobile_HRMS_WebService_PostRequest", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic GetReasonforResignation()
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            string query;

            query = "select Res_Id,Reason_Name from T0040_Reason_Master with(nolock) where Type='Exit' and Isactive=1";

            var response = vconn.Query<dynamic>(query, vParams); // Pass the parameters object
            return response;
        }

        public dynamic GetShiftDeatails(int cmpID, int empID, string forDate)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Leave_Application_ID", 0);
            vParams.Add("@Emp_ID", empID);
            vParams.Add("@Cmp_ID", cmpID);
            vParams.Add("@Leave_ID", 0);
            vParams.Add("@From_Date", DateTime.ParseExact(forDate, "dd-MM-yyyy", null));
            vParams.Add("@To_Date", DateTime.Now);
            vParams.Add("@Period", 0.0);
            vParams.Add("@Leave_Assign_As", "");
            vParams.Add("@Comment", "");
            vParams.Add("@Half_Leave_Date", DateTime.Now);
            vParams.Add("@InTime", DateTime.Now);
            vParams.Add("@OutTime", DateTime.Now);
            vParams.Add("@Login_ID", 0);
            vParams.Add("@Attachment", "");
            vParams.Add("@Type", "T");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_Leave", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic GetState(int cmpId, int countryId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            string query;

            query = "SELECT State_ID,State_Name FROM T0020_STATE_MASTER WHERE Loc_ID = @CountryID AND Cmp_ID = @CmpID ORDER BY State_Name ASC";
            vParams.Add("@CmpID", cmpId);
            vParams.Add("@CountryID", countryId);
            var response = vconn.Query<dynamic>(query, vParams); // Pass the parameters object
            return response;
        }

        public dynamic GetSurveyList(int cmpId, int empId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", empId);
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Survey_ID", 0);
            vParams.Add("@Survey_Details", "");
            vParams.Add("@Login_ID", 0);
            vParams.Add("@IMEINo", "");
            vParams.Add("@Type", "B");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_Survey", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic GetSurveyQuestionAnswerList(int cmpID, int empId, int surveyID)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", empId);
            vParams.Add("@Cmp_ID", cmpID);
            vParams.Add("@Survey_ID", surveyID);
            vParams.Add("@Survey_Details", "");
            vParams.Add("@Login_ID", 0);
            vParams.Add("@IMEINo", "");
            vParams.Add("@Type", "Q");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_Survey", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic get_currency(int cmpId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@CmpId", cmpId); // Make sure parameter name matches the one used in the query
            var query = "SELECT Curr_Symbol, Curr_ID FROM T0040_CURRENCY_MASTER WHERE Cmp_ID = @CmpId";
            var response = vconn.Query<T0040_CURRENCY_MASTER>(query, vParams); // Pass the parameters object
            return response;

        }

        public dynamic KilometerRateMaster(KilometerRateMasterRequest kilometerRateMasterRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", kilometerRateMasterRequest.CmpID);
            vParams.Add("@Emp_Category", kilometerRateMasterRequest.EmployeeCategory);
            vParams.Add("@Vehicle_Type", kilometerRateMasterRequest.VehicleType);
            var Response = vconn.Query("SP_Mobile_KilometerRate_Master", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic LiveTrackingTotalDistance(int cmpId, int empId, DateTime createdDate)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Emp_Id", empId);
            vParams.Add("@Created_Date", createdDate);
            var Response = vconn.Query("SP_Mobile_LiveTracking_TotalDistance", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic MatchFingerPrint(int cmpId, int empId)
        {
            byte[] bt = { 0, 0 };

            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Emp_ID", empId);
            //vParams.Add("@Base64", bt);
            vParams.Add("@Type", "S");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_WebService_FingerPrint", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic MobileSalesStockResponse(MobileSalseStockResponseRequest mobileSalseStockResponseRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            //vParams.Add("@Cmp_ID", CmpID)
            //vParams.Add("@Emp_ID", EmpID)
            //vParams.Add("@Store_ID", StoreID)
            //vParams.Add("@Login_ID", LoginID)
            //vParams.Add("@Mobile_Remark_ID", RemarkID)
            //vParams.Add("@SaleStockDetails", SaleStockDetails, SqlDbType.NVarChar)
            //vParams.Add("@Type", strType, SqlDbType.VarChar)
            //vParams.Add("@For_Date", Convert.ToDateTime(ForDate).ToString("dd/MMM/yyyy"), SqlDbType.DateTime)
            //vParams.Add("@Result", "", SqlDbType.VarChar, ParameterDirection.Output)
            var Response = vconn.Query("SP_Mobile_LiveTracking_TotalDistance", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic MoodTracker(LeaveBalanceRequest moodTracker)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", moodTracker.CmpId);
            vParams.Add("@Emp_ID", moodTracker.EmpId);
            vParams.Add("@Month", moodTracker.Month);
            vParams.Add("@Year", moodTracker.Year);
            vParams.Add("@Activity", 0);
            vParams.Add("@MoodDetails", "");
            vParams.Add("@Type", "S");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_WebService_MoodTracker", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic MoodTrackerActivityList()
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            var Response = vconn.Query("SP_Mobile_WebService_MoodTracker_ActLists", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic MoodTrackerInsert(MoodTrackerInsertRequest moodTrackerInsertRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", moodTrackerInsertRequest.CmpID);
            vParams.Add("@Emp_ID", moodTrackerInsertRequest.EmpID);
            vParams.Add("@Month", 0);
            vParams.Add("@Year", 0);
            vParams.Add("@Activity", moodTrackerInsertRequest.ActivityId);
            vParams.Add("@MoodDetails", moodTrackerInsertRequest.MoodDetail);
            vParams.Add("@Type", "I");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_WebService_MoodTracker", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic PostFingerPrintDetails(int empID, int cmpId, string base64)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Emp_ID", empID);
            //vParams.Add("@Base64", base64);
            vParams.Add("@Type", "I");
            vParams.Add("@Result", "");
            var Response = vconn.Query("SP_Mobile_WebService_FingerPrint", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic PostRequest(PostRequest postRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", postRequest.CmpID);
            vParams.Add("@Emp_Login_ID", postRequest.EmpID_LoginID);
            vParams.Add("@Request_ID", postRequest.RequestID);
            vParams.Add("@Request_Type", postRequest.Request_Type);
            vParams.Add("@Request_Date", DateTime.Now);
            vParams.Add("@Request_Detail", postRequest.Request_Details);
            vParams.Add("@Feedback_Detail", postRequest.Feedback_Details);
            vParams.Add("@Request_Status", postRequest.Request_Status);
            vParams.Add("@Login_ID", postRequest.LoginID);
            vParams.Add("@Type", postRequest.StrType);
            vParams.Add("@Result", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_PostRequest", vParams, commandType: CommandType.StoredProcedure);
            string result = vParams.Get<string>("@Result");
            return result;
        }

        public dynamic SalaryDetails(LeaveBalanceRequest salaryDetails)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", salaryDetails.EmpId);
            vParams.Add("@Cmp_ID", salaryDetails.CmpId);
            vParams.Add("@Month", salaryDetails.Month);
            vParams.Add("@Year", salaryDetails.Year);
            vParams.Add("@Type", "V");
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_Salary", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public Array SendNotification(string title, string body, dynamic dynamic, string v)
        {
            throw new NotImplementedException();
        }

        public dynamic SurveyApplication(SurveyApplicationRequest surveyApplicationRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", surveyApplicationRequest.EmpID);
            vParams.Add("@Cmp_ID", surveyApplicationRequest.CmpID);
            vParams.Add("@Survey_ID", surveyApplicationRequest.SurveyID);
            vParams.Add("@Survey_Details", surveyApplicationRequest.SurveyDetails);
            vParams.Add("@Login_ID", surveyApplicationRequest.LoginID);
            vParams.Add("@IMEINo", surveyApplicationRequest.IMEINo);
            vParams.Add("@Type", "I");
            vParams.Add("@Result", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_Survey", vParams, commandType: CommandType.StoredProcedure);
            string result = vParams.Get<string>("@Result");
            return result;
        }

        public dynamic TemplateApplication(TemplateApplicationRequest templateApplicationDetailsRequest, XDocument xmlDocument)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", templateApplicationDetailsRequest.EmpID);
            vParams.Add("@Cmp_ID", templateApplicationDetailsRequest.CmpID);
            vParams.Add("@T_ID", templateApplicationDetailsRequest.TID);
            vParams.Add("@Template_Details", xmlDocument);
            vParams.Add("@Login_ID", templateApplicationDetailsRequest.LoginID);
            vParams.Add("@Type", "I");
            vParams.Add("@Result", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            var Response = vconn.Query("SP_Mobile_TemplateField_Insert", vParams, commandType: CommandType.StoredProcedure);
            string result = vParams.Get<string>("@Result");
            return result;
        }

        public dynamic TemplateApplicationDetails(TemplateApplicationDetailsRequest templateApplicationDetailsRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@From_Date", templateApplicationDetailsRequest.FromDate);
            vParams.Add("@To_Date", templateApplicationDetailsRequest.ToDate);
            vParams.Add("@Cmp_Id", templateApplicationDetailsRequest.CmpID);
            vParams.Add("@Branch_ID", templateApplicationDetailsRequest.BranchId.ToString());
            vParams.Add("@Cat_ID", templateApplicationDetailsRequest.CatId.ToString());
            vParams.Add("@Grd_ID", templateApplicationDetailsRequest.GrdId.ToString());
            vParams.Add("@Dept_ID", templateApplicationDetailsRequest.DeptId.ToString());
            vParams.Add("@Desig_ID", templateApplicationDetailsRequest.DesgId.ToString());
            vParams.Add("@Emp_ID", templateApplicationDetailsRequest.EmpId);
            vParams.Add("@T_Id", templateApplicationDetailsRequest.TId);
            var Response = vconn.Query("SP_Mobile_TemplateApplication_Data", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic TemplateFieldData(TemplateFieldDataRequest templateFieldDataRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", templateFieldDataRequest.CmpID);
            vParams.Add("@Emp_ID", templateFieldDataRequest.EmpID);
            vParams.Add("@T_Id", templateFieldDataRequest.TID);
            vParams.Add("@From_Date", templateFieldDataRequest.FromDate);
            vParams.Add("@To_Date", templateFieldDataRequest.ToDate);
            vParams.Add("@Flag", templateFieldDataRequest.Flag);
            vParams.Add("@Response_Flag", templateFieldDataRequest.RFlag);
            var Response = vconn.Query("SP_Mobile_TemplateField_Data", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic TemplateFieldDataView(TemplateFieldDataViewRequest templateFieldDataRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", templateFieldDataRequest.CmpID);
            vParams.Add("@T_ID", templateFieldDataRequest.TId);
            vParams.Add("@Emp_Id", templateFieldDataRequest.EmpId);
            vParams.Add("@Response_Flag", templateFieldDataRequest.RFlag);
            var Response = vconn.Query("SP_Mobile_TemplateField_View", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }


        public dynamic UnisonMaster(int cmpId, int empId, string master)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@MASTER", master);
            vParams.Add("@EMP_ID", empId);
            var Response = vconn.Query("SP_MOBILE_HRMS_WEBSERVICE_UNISONMASTER", vParams, commandType: CommandType.StoredProcedure);
            return Response;
        }

        public dynamic UpdateBankDetails(UpdateBankDetailsRequest updateBankDetailsRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", updateBankDetailsRequest.EmpID);
            vParams.Add("@Cmp_ID", updateBankDetailsRequest.CmpID);
            vParams.Add("@Vertical_ID", 0);
            vParams.Add("@Emp_Code", "");
            vParams.Add("@Address", "");
            vParams.Add("@City", "");
            vParams.Add("@State", "");
            vParams.Add("@Pincode", "");
            vParams.Add("@PhoneNo", "");
            vParams.Add("@MobileNo", "");
            vParams.Add("@Email", "");
            vParams.Add("@ImageName", "");
            vParams.Add("@Branch_ID", 0);
            vParams.Add("@Department_ID", 0);
            vParams.Add("@Type", "R");
            vParams.Add("@Result", "");
            vParams.Add("@Bank_Name", updateBankDetailsRequest.BankName);
            vParams.Add("@Bank_Ac_No", updateBankDetailsRequest.BankAccountNo);
            vParams.Add("@Bank_IFSC_Code", updateBankDetailsRequest.IfscCode);
            vParams.Add("@Pan_No", updateBankDetailsRequest.PancardNo);
            vParams.Add("@Bank_Branch_Name", updateBankDetailsRequest.BankBranchName);
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_EmpDetails", vParams, commandType: CommandType.StoredProcedure);
            return Response;
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

        public dynamic UploadDocument(UploadDocumentRequest updateBankDetailsRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_ID", updateBankDetailsRequest.EmpID);
            vParams.Add("@Cmp_ID", updateBankDetailsRequest.CmpID);
            vParams.Add("@Doc_ID", updateBankDetailsRequest.DocID);
            vParams.Add("@Doc_Type", updateBankDetailsRequest.DocType);
            vParams.Add("@Doc_Name", updateBankDetailsRequest.DocName);
            vParams.Add("@Doc_Comment", updateBankDetailsRequest.DocComment);
            vParams.Add("@Login_ID", updateBankDetailsRequest.LoginID);
            vParams.Add("@Type", updateBankDetailsRequest.Type);
            vParams.Add("@Result", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            var Response = vconn.Query("SP_Mobile_HRMS_WebService_Emp_Document", vParams, commandType: CommandType.StoredProcedure);
            string result = vParams.Get<string>("@Result");
            return result;
        }
    }
}
