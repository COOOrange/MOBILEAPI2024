using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Orange_RestWebAPI.Connections;
using Orange_RestWebAPI.Model;
using System;
using System.Data;
using System.Net;
using System.Net.Security;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Microsoft.Exchange.WebServices.Data;
using System.Net.Mail;
using static Orange_RestWebAPI.Model.PredefinedClasses.Response;
using System.Security.Cryptography.X509Certificates;


namespace Orange_RestWebAPI.BusinessLogic
{
    public class Leave
    {
        ClsDataccess ObjclsDataccess;// = new ClsDataccess(IConfiguration config);
        EncryptDecryptAlgo ObjencryptDecryptAlgo;
        LogHelper logHelper;
        MyClassToken<LoginModel> response = new MyClassToken<LoginModel>();
       
        EmailSetting Emailsetting;
        public Leave(IConfiguration config)
        {
            ObjclsDataccess = new ClsDataccess(config);
            ObjencryptDecryptAlgo = new EncryptDecryptAlgo();
            logHelper = new LogHelper();
           // Emailsetting = new EmailSetting();
        }

        public void GetFilterLeaveBalance(int LeaveAppID, int EmpID, int CmpID, DateTime FromDate, DateTime Todate, string strType, ref DataTable dtLeaveBalance)
        {

            
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("Leave_Application_ID", LeaveAppID),
                    new SqlParameter("@Emp_ID", EmpID),
                    new SqlParameter("@Cmp_ID", CmpID),
                    new SqlParameter("@Leave_ID", Convert.ToInt32("0")),
                    new SqlParameter("@From_Date", FromDate),
                    new SqlParameter("@To_Date", Todate),
                    new SqlParameter("@Period", Convert.ToInt32("0")),
                    new SqlParameter("@Leave_Assign_As", ""),
                    new SqlParameter("@Comment", ""),
                    new SqlParameter("@Half_Leave_Date", DateTime.Now),
                    new SqlParameter("@InTime", DateTime.Now),
                    new SqlParameter("@OutTime", DateTime.Now),
                    new SqlParameter("@Login_ID", Convert.ToInt32("0")),
                    new SqlParameter("@strLeaveCompOff_Dates", ""),
                    new SqlParameter("@Attachment", ""),
                    new SqlParameter("@Type", strType),
                    new SqlParameter("@Result", "")
                };
                dtLeaveBalance = ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_WebAPI_Leave", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("GetLeaveRecords : " + ex.Message);
                throw;
            }
        }
        public void GetLeaveBalance(int LeaveAppID, int EmpID, int CmpID , DateTime FromDate, DateTime Todate, string strType, ref DataTable dtLeaveBalance)
        {


            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("Leave_Application_ID", LeaveAppID),
                    new SqlParameter("@Emp_ID", EmpID),
                    new SqlParameter("@Cmp_ID", CmpID),
                    new SqlParameter("@Leave_ID", Convert.ToInt32("0")),
                   new SqlParameter("@From_Date", FromDate),
                    new SqlParameter("@To_Date", Todate),
                    new SqlParameter("@Period", Convert.ToInt32("0")),
                    new SqlParameter("@Leave_Assign_As", ""),
                    new SqlParameter("@Comment", ""),
                    new SqlParameter("@Half_Leave_Date", DateTime.Now),
                    new SqlParameter("@InTime", DateTime.Now),
                    new SqlParameter("@OutTime", DateTime.Now),
                    new SqlParameter("@Login_ID", Convert.ToInt32("0")),
                    new SqlParameter("@strLeaveCompOff_Dates", ""),
                    new SqlParameter("@Attachment", ""),
                    new SqlParameter("@Type", strType),
                    new SqlParameter("@Result", "")
                };
                dtLeaveBalance = ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_WebAPI_Leave", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("GetLeaveRecords : " + ex.Message);
                throw;
            }
        }


        
public void ValidateCheckPeriod(int EmpID, int CmpID, int LeaveID, DateTime FromDate,int Period, string strType, ref DataSet dsLeaveApp)
        {


            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("Leave_Application_ID",Convert.ToInt32("0")),
                    new SqlParameter("@Emp_ID", EmpID),
                    new SqlParameter("@Cmp_ID", CmpID),
                    new SqlParameter("@Leave_ID", LeaveID),
                   new SqlParameter("@From_Date", FromDate),
                    new SqlParameter("@To_Date", ""),
                    new SqlParameter("@Period", Period),
                    new SqlParameter("@Leave_Assign_As", ""),
                    new SqlParameter("@Comment", ""),
                    new SqlParameter("@Half_Leave_Date", DateTime.Now),
                    new SqlParameter("@InTime", DateTime.Now),
                    new SqlParameter("@OutTime", DateTime.Now),
                    new SqlParameter("@Login_ID", Convert.ToInt32("0")),
                    new SqlParameter("@strLeaveCompOff_Dates", ""),
                    new SqlParameter("@Attachment", ""),
                    new SqlParameter("@Type", strType),
                    new SqlParameter("@Result", "")
                };
                dsLeaveApp = ObjclsDataccess.ExecuteDataSet("SP_Mobile_HRMS_WebAPI_Leave", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("GetLeaveRecords : " + ex.Message);
                throw;
            }
        }
        public void GetLeaveTypeBind(int LeaveAppID, int EmpID, int CmpID,  string strType, ref DataTable dtLeaveBalance)
        {


            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("Leave_Application_ID", LeaveAppID),
                    new SqlParameter("@Emp_ID", EmpID),
                    new SqlParameter("@Cmp_ID", CmpID),
                    new SqlParameter("@Leave_ID", Convert.ToInt32("0")),
                   new SqlParameter("@From_Date", ""),
                    new SqlParameter("@To_Date", ""),
                    new SqlParameter("@Period", Convert.ToInt32("0")),
                    new SqlParameter("@Leave_Assign_As", ""),
                    new SqlParameter("@Comment", ""),
                    new SqlParameter("@Half_Leave_Date", DateTime.Now),
                    new SqlParameter("@InTime", DateTime.Now),
                    new SqlParameter("@OutTime", DateTime.Now),
                    new SqlParameter("@Login_ID", Convert.ToInt32("0")),
                    new SqlParameter("@strLeaveCompOff_Dates", ""),
                    new SqlParameter("@Attachment", ""),
                    new SqlParameter("@Type", strType),
                    new SqlParameter("@Result", "")
                };
                dtLeaveBalance = ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_WebAPI_Leave", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("GetLeaveRecords : " + ex.Message);
                throw;
            }
        }
        public void LeaveApplication(int LeaveAppID, int EmpID, int CmpID, int LeaveID, string FromDate, decimal Period, string ToDate, string AssignAs, string Comment, string HLeaveDate, string InTime, string OutTime, int Login_ID, string Attachement, string LeaveCompOff_Dates, string strType, ref DataSet dsLeaveApplicationData)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("Leave_Application_ID", LeaveAppID),
                    new SqlParameter("@Emp_ID", EmpID),
                    new SqlParameter("@Cmp_ID", CmpID),
                    new SqlParameter("@Leave_ID", LeaveID),
                    new SqlParameter("@From_Date", Convert.ToDateTime(FromDate).ToString("dd/MM/yyyy")),
                    new SqlParameter("@To_Date", Convert.ToDateTime(ToDate).ToString("dd/MM/yyyy")),
                    new SqlParameter("@Period", Period),
                    new SqlParameter("@Leave_Assign_As", AssignAs),
                    new SqlParameter("@Comment", Comment),
                    new SqlParameter("@Half_Leave_Date", Convert.ToDateTime(HLeaveDate).ToString("dd/MM/yyyy")),
                    new SqlParameter("@InTime", Convert.ToDateTime(InTime).ToString("dd/MM/yyyy")),
                    new SqlParameter("@OutTime", Convert.ToDateTime(OutTime).ToString("dd/MM/yyyy")),
                    new SqlParameter("@Login_ID", Login_ID),
                    new SqlParameter("@strLeaveCompOff_Dates", LeaveCompOff_Dates),
                    new SqlParameter("@Attachment", Attachement),
                    new SqlParameter("@Type", strType),
                    new SqlParameter("@Result", "")
                };
                dsLeaveApplicationData = ObjclsDataccess.ExecuteDataSet("SP_Mobile_HRMS_WebService_Leave", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("LeaveApplication : " + ex.Message);
                throw;
            }
        }
        public void LeaveApproval(int EmpID, int CmpID,  int LeaveAppID, ref DataSet dsLeaveApproval)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@Leave_Approval_ID",Convert.ToInt32("0")),
                    new SqlParameter("@Leave_Application_ID", LeaveAppID),
                    new SqlParameter("@Leave_ID", Convert.ToInt32("0")),
                    new SqlParameter("@Emp_ID", EmpID),
                    new SqlParameter("@Cmp_ID", CmpID),
                    new SqlParameter("@Approval_Date", DateTime.Now.ToString("dd/MMM/yyyy")),
                    new SqlParameter("@From_Date", ""),
                    new SqlParameter("@To_Date", ""),
                    new SqlParameter("@Leave_Period", Convert.ToDecimal("0.0")),
                    new SqlParameter("@Leave_AssignAs", ""),
                    new SqlParameter("@Leave_Reason", ""),
                    new SqlParameter("@Half_Leave_Date", ""),
                    new SqlParameter("@Approval_Status", ""),
                    new SqlParameter("@Approval_Comments",""),
                    new SqlParameter("@Final_Approve", Convert.ToInt32("0")),
                    new SqlParameter("@Is_Fwd_Leave_Rej", Convert.ToInt32("0")),
                    new SqlParameter("@Rpt_Level", Convert.ToInt32("0")),
                    new SqlParameter("@SEmp_ID", Convert.ToInt32("0")),
                    new SqlParameter("@InTime", ""),
                    new SqlParameter("@OutTime", ""),
                    new SqlParameter("@Login_ID",Convert.ToInt32("0")),
                    new SqlParameter("@strLeaveCompOff_Dates", ""),
                    new SqlParameter("@Status", ""),
                    new SqlParameter("@Type", "E"),
                    new SqlParameter("@Result", "")
                };
                dsLeaveApproval = ObjclsDataccess.ExecuteDataSet("SP_Mobile_HRMS_WebService_Leave_Approve", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("LeaveApproval : " + ex.Message);
                throw;
            }
        }
    }
}

