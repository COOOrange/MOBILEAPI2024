using Microsoft.Extensions.Configuration;
using Orange_RestWebAPI.Connections;
using Orange_RestWebAPI.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Orange_RestWebAPI.BusinessLogic
{
    public class Attendance
    {
        ClsDataccess ObjclsDataccess;// = new ClsDataccess(IConfiguration config);
        LogHelper logHelper;
        public Attendance(IConfiguration config)
        {
            ObjclsDataccess = new ClsDataccess(config);
            logHelper = new LogHelper();
        }

        //Added by Mukti(15042021)To get Attendance Details
        public void AttendanceDetails(int EmpID, int CmpID, int Month, int Year, string FromDate, string Todate, string strType, ref DataTable dtAttendanceDetails)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                     new SqlParameter("@Emp_ID", EmpID),
                     new SqlParameter("@Cmp_ID", CmpID),
                     new SqlParameter("@Vertical_ID", Convert.ToInt32("0")),
                     new SqlParameter("@SubVertical_ID", Convert.ToInt32("0")),
                     new SqlParameter("@ForDate", Convert.ToDateTime(FromDate).ToString("yyyy/MM/dd")),
                     new SqlParameter("@Time",  Convert.ToDateTime(Todate).ToString("yyyy/MM/dd")),
                     new SqlParameter("@INOUTFlag", ""),
                     new SqlParameter("@Reason", ""),
                     new SqlParameter("@IMEINo", ""),
                     new SqlParameter("@Latitude", ""),
                     new SqlParameter("@Longitude", ""),
                     new SqlParameter("@Address", ""),
                     new SqlParameter("@Emp_Image", ""),
                     new SqlParameter("@strAttendance", ""),
                     new SqlParameter("@Month", Month),
                     new SqlParameter("@Year", Year),
                     new SqlParameter("@Type", strType),
                     new SqlParameter("@Result", ""),
                     new SqlParameter("@SubVerticalName", "")
                };
                dtAttendanceDetails = ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_WebService_Attendance", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("AttendanceDetails : " + ex.Message);
                throw;
            }
        }

        public void GetPostRequestEmployee(int CmpID, int EmpID_LoginID, int RequestID, string Request_Type, string Request_Date, string Request_Details, string Feedback_Details, int Request_Status, int LoginID, string strType, ref DataTable dtPostRequestEmployee)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                     new SqlParameter("@Cmp_ID", CmpID),
                     new SqlParameter("@Emp_Login_ID", EmpID_LoginID),
                     new SqlParameter("@Request_ID", RequestID),
                     new SqlParameter("@Request_Type", Request_Type),
                     new SqlParameter("@Request_Date", Request_Date),
                     new SqlParameter("@Request_Detail", Request_Details),
                     new SqlParameter("@Feedback_Detail",Feedback_Details),
                     new SqlParameter("@Request_Status", Request_Status),
                     new SqlParameter("@Login_ID", LoginID),
                     new SqlParameter("@Type", strType),
                     new SqlParameter("@Result", "")
                };
                dtPostRequestEmployee = ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_WebService_PostRequest", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("GetPostRequestEmployee : " + ex.Message);
                throw;
            }
        }

        public void CheckINOUT(int EmpID, int CmpID, int Month, int Year, string FromDate, string Todate, string strType, ref DataTable dtAttendanceDetails)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                     new SqlParameter("@Emp_ID", EmpID),
                     new SqlParameter("@Cmp_ID", CmpID),
                     new SqlParameter("@Vertical_ID", Convert.ToInt32("0")),
                     new SqlParameter("@SubVertical_ID", Convert.ToInt32("0")),
                     new SqlParameter("@ForDate", FromDate),
                     new SqlParameter("@Time", Todate),
                     new SqlParameter("@INOUTFlag", ""),
                     new SqlParameter("@Reason", ""),
                     new SqlParameter("@IMEINo", ""),
                     new SqlParameter("@Latitude", ""),
                     new SqlParameter("@Longitude", ""),
                     new SqlParameter("@Address", ""),
                     new SqlParameter("@Emp_Image", ""),
                     new SqlParameter("@strAttendance", ""),
                     new SqlParameter("@Month", Month),
                     new SqlParameter("@Year", Year),
                     new SqlParameter("@Type", strType),
                     new SqlParameter("@Result", ""),
                     new SqlParameter("@SubVerticalName", "")
                };
                dtAttendanceDetails = ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_WebService_Attendance", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("CheckINOUT : " + ex.Message);
                throw;
            }
        }

        public void AttendanceRegularizeDetails(int IOTranId, int EmpID, int CmpID, int Month, int Year, string FromDate, string Todate, string strType, ref DataSet dsAttendanceRegularizePeningApplication)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                     new SqlParameter("@IO_Tran_Id",IOTranId),
                     new SqlParameter("@Emp_ID", EmpID),
                     new SqlParameter("@Cmp_ID", CmpID),
                     new SqlParameter("@Month", Month),
                     new SqlParameter("@Year", Year),
                     new SqlParameter("@For_Date", DateTime.Now.ToString("yyyy/MM/dd")),
                     new SqlParameter("@Reason", ""),
                     new SqlParameter("@Half_Full_Day", ""),
                     new SqlParameter("@Is_Cancel_Late_In",Convert.ToInt32("0")),
                     new SqlParameter("@Is_Cancel_Early_Out", Convert.ToInt32("0")),
                     new SqlParameter("@In_Date_Time", Convert.ToDateTime(FromDate).ToString("yyyy/MM/dd")),
                     new SqlParameter("@Out_Date_Time", Convert.ToDateTime(Todate).ToString("yyyy/MM/dd")),
                     new SqlParameter("@Is_Approve", Convert.ToInt32("0")),
                     new SqlParameter("@Other_Reason", ""),
                     new SqlParameter("@IMEINo", ""),
                     new SqlParameter("@S_Emp_ID",  Convert.ToInt32("0")),
                     new SqlParameter("@Rpt_Level", Convert.ToInt32("0")),
                     new SqlParameter("@Final_Approve", Convert.ToInt32("0")),
                     new SqlParameter("@Is_Fwd_Leave_Rej", Convert.ToInt32("0")),
                     new SqlParameter("@Approval_Status", ""),
                     new SqlParameter("@Type", strType),
                     new SqlParameter("@Result", "")
                };
                dsAttendanceRegularizePeningApplication = ObjclsDataccess.ExecuteDataSet("SP_Mobile_HRMS_WebService_AttendanceRegularization", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("AttendanceRegularizeDetails : " + ex.Message);
                throw;
            }
        }

        public void UpdateEmployeeDetails(int EmpID, int CmpID, string EmpCode, int VerticalID, int BranchID, int DepartmentID, string strType, ref DataTable dtEmpDetails)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                   new SqlParameter("@Emp_ID",EmpID),
                   new SqlParameter("@Cmp_ID",CmpID),
                   new SqlParameter("@Vertical_ID",VerticalID),
                   new SqlParameter("@Emp_Code",Convert.ToString(EmpCode)),
                   new SqlParameter("@Address",""),
                   new SqlParameter("@City",""),
                   new SqlParameter("@State",""),
                   new SqlParameter("@Pincode",""),
                   new SqlParameter("@PhoneNo",""),
                   new SqlParameter("@MobileNo",""),
                   new SqlParameter("@Email",""),
                   new SqlParameter("@ImageName",""),
                   new SqlParameter("@Branch_ID",BranchID),
                   new SqlParameter("@Department_ID",DepartmentID),
                   new SqlParameter("@Type",strType),
                   new SqlParameter("@Result","")
            };
                dtEmpDetails = ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_WebService_EmpDetails", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("UpdateEmployeeDetails : " + ex.Message);
                throw;
            }

        }

        public void AttendanceRegularizeInsert(int IOTranId, int EmpID, int CmpID, int Month, int Year, string ForDate, string Reason,string HalfFullDay,int CancelLateIn,int CancelEarlyOut, String Intime, String OutTime , int Is_Approve , string Other_Reason , string IMEINo, int SEmp_ID , int RptLevel , int FinalApprove , int IsFwdLeaveRej , string ApprovalStatus , string strType, ref DataTable dtAttendanceRegularizeInsert)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                     new SqlParameter("@IO_Tran_Id",IOTranId),
                     new SqlParameter("@Emp_ID", EmpID),
                     new SqlParameter("@Cmp_ID", CmpID),
                     new SqlParameter("@Month", Month),
                     new SqlParameter("@Year", Year),
                     new SqlParameter("@For_Date",Convert.ToDateTime(ForDate).ToString("yyyy/MM/dd")),
                     new SqlParameter("@Reason", Reason),
                     new SqlParameter("@Half_Full_Day", HalfFullDay),
                     new SqlParameter("@Is_Cancel_Late_In",CancelLateIn),
                     new SqlParameter("@Is_Cancel_Early_Out", CancelEarlyOut),
                     new SqlParameter("@In_Date_Time", Convert.ToDateTime(Intime).ToString("yyyy/MM/dd")),
                     new SqlParameter("@Out_Date_Time", Convert.ToDateTime(OutTime).ToString("yyyy/MM/dd")),
                     new SqlParameter("@Is_Approve", Is_Approve),
                     new SqlParameter("@Other_Reason", Other_Reason),
                     new SqlParameter("@IMEINo", IMEINo),
                     new SqlParameter("@S_Emp_ID",  SEmp_ID),
                     new SqlParameter("@Rpt_Level",RptLevel),
                     new SqlParameter("@Final_Approve", FinalApprove),
                     new SqlParameter("@Is_Fwd_Leave_Rej", IsFwdLeaveRej),
                     new SqlParameter("@Approval_Status", ApprovalStatus),
                     new SqlParameter("@Type", strType),
                     new SqlParameter("@Result", "")
                };
                dtAttendanceRegularizeInsert = ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_WebService_AttendanceRegularization", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("AttendanceRegularizeInsert : " + ex.Message);
                throw;
            }
        }

        public string WorkPlanOnClockInOut(int EmpID, int CmpID, string WorkPlan, string VisitPlan, string Work_Summary, string Visit_Summary, char Flag)
        {
            string StrResult;
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                     new SqlParameter("@Cmp_ID", CmpID),
                     new SqlParameter("@Emp_ID", EmpID),
                     new SqlParameter("@Work_Plan", WorkPlan),
                     new SqlParameter("@Visit_Plan", VisitPlan),
                     new SqlParameter("@Work_Summary", Work_Summary),
                     new SqlParameter("@Visit_Summary", Visit_Summary),
                     new SqlParameter("@INOUTFlag", Flag),
                     new SqlParameter("@Result", SqlDbType.VarChar, 100, "")
                };
                sqlParams[7].Direction = ParameterDirection.Output;
                ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_WebService_WorkPlan", sqlParams);
                return StrResult = sqlParams[7].Value.ToString();
            }
            catch (Exception ex)
            {
                logHelper.Error("WorkPlanOnClockIn : " + ex.Message);
                throw;
            }
        }

        ////Added by Niraj(26112021)To get ALl Employees Attendance Details
        //public void AllEmployeesAttendanceDetails(int CmpID, string FromDate, string ToDate, ref DataTable dtAllEmployeesAttendanceDetail)
        //{
        //    try
        //    {
        //        SqlParameter[] sqlParams = new SqlParameter[]
        //        {
        //             new SqlParameter("@Cmp_ID", CmpID),
        //             new SqlParameter("@FromDate", FromDate),
        //             new SqlParameter("@ToDate", ToDate)
        //        };
        //        dtAllEmployeesAttendanceDetail = ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_WebService_Get_All_Attendance", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        logHelper.Error("AllEmployeesAttendanceDetails : " + ex.Message);
        //        throw;
        //    }
        //}

    }
}
