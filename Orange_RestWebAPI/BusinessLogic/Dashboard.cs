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
    public class Dashboard
    {
        ClsDataccess ObjclsDataccess;// = new ClsDataccess(IConfiguration config);
        EncryptDecryptAlgo ObjencryptDecryptAlgo;
        LogHelper logHelper;
        MyClassToken<LoginModel> response = new MyClassToken<LoginModel>();
        private IHttpContextAccessor _accessor;
        EmailSetting Emailsetting;
        public Dashboard(IConfiguration config)
        {
            ObjclsDataccess = new ClsDataccess(config);
            ObjencryptDecryptAlgo = new EncryptDecryptAlgo();
            logHelper = new LogHelper();
            Emailsetting = new EmailSetting();
        }
        public void GetDeshBoardDetails(int EmpID, int CmpID, ref DataSet dtDashboardDetails)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                   new SqlParameter("@Emp_ID",EmpID),
                   new SqlParameter("@Cmp_ID",CmpID)
                };

                dtDashboardDetails = ObjclsDataccess.ExecuteDataSet("SP_Mobile_HRMS_WebService_DASHBOARD", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("GetDeshBoardDetails : " + ex.Message);
                throw;
            }
        }





        //public void GetLastClockLocation(int EmpID, int CmpID, ref DataTable dtt)
        //{
        //    try
        //    {
        //        SqlParameter[] sqlParams = new SqlParameter[]
        //        {
        //           new SqlParameter("@Emp_ID",EmpID),
        //           new SqlParameter("@Cmp_ID",CmpID)
        //        };

        //        //string str = "select top 1 Location from T9999_MOBILE_INOUT_DETAIL WHERE CMP_ID = " + CmpID + " AND EMP_ID=" + EmpID + " order by IO_Datetime desc ";
        //        dtt = ObjclsDataccess.ExecuteDataTable(str);

        //    }
        //    catch (Exception ex)
        //    {
        //        logHelper.Error("GetDeshBoardDetails : " + ex.Message);
        //        throw;
        //    }
        //}

    public void GetGeoLocation(int Empid,int Cmpid,string strtype, ref DataTable dtgeoloc)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                   new SqlParameter("@Emp_ID",Empid),
                   new SqlParameter("@Cmp_ID",Cmpid),
                    new SqlParameter("@Type", strtype),
                    new SqlParameter("@Vertical_ID", Convert.ToInt32("0")),
                     new SqlParameter("@SubVertical_ID", Convert.ToInt32("0")),
                     new SqlParameter("@ForDate", ""),
                     new SqlParameter("@Time",  ""),
                     new SqlParameter("@INOUTFlag", ""),
                     new SqlParameter("@Reason", ""),
                     new SqlParameter("@IMEINo", ""),
                     new SqlParameter("@Latitude", ""),
                     new SqlParameter("@Longitude", ""),
                     new SqlParameter("@Address", ""),
                     new SqlParameter("@Emp_Image", ""),
                     new SqlParameter("@strAttendance", ""),
                     new SqlParameter("@Month", ""),
                     new SqlParameter("@Year", ""),
                     new SqlParameter("@Result", ""),
                };

                //string str = "select top 1 Location from T9999_MOBILE_INOUT_DETAIL WHERE CMP_ID = " + CmpID + " AND EMP_ID=" + EmpID + " order by IO_Datetime desc ";
                dtgeoloc = ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_WebService_Attendance", sqlParams);

            }
            catch (Exception ex)
            {
                logHelper.Error("GetDeshBoardDetails : " + ex.Message);
                throw;
            }
        }
    public void GetLastClockLocation(int EmpID, int CmpID, string strType, ref DataTable dtAttendanceDetails)
    {
        try
        {
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                     new SqlParameter("@Emp_ID", EmpID),
                     new SqlParameter("@Cmp_ID", CmpID),
                     new SqlParameter("@Vertical_ID", Convert.ToInt32("0")),
                     new SqlParameter("@SubVertical_ID", Convert.ToInt32("0")),
                     new SqlParameter("@ForDate", ""),
                     new SqlParameter("@Time",  ""),
                     new SqlParameter("@INOUTFlag", ""),
                     new SqlParameter("@Reason", ""),
                     new SqlParameter("@IMEINo", ""),
                     new SqlParameter("@Latitude", ""),
                     new SqlParameter("@Longitude", ""),
                     new SqlParameter("@Address", ""),
                     new SqlParameter("@Emp_Image", ""),
                     new SqlParameter("@strAttendance", ""),
                     new SqlParameter("@Month", ""),
                     new SqlParameter("@Year", ""),
                     new SqlParameter("@Type", strType),
                     new SqlParameter("@Result", ""),
                     new SqlParameter("@SubVerticalName", "")
            };
            dtAttendanceDetails = ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_Attendance", sqlParams);
        }
        catch (Exception ex)
        {
            logHelper.Error("GetDeshBoardDetails : " + ex.Message);
           
            throw;
        }
    }
        public string PostClockIN(int EmpID, int CmpID,string date,string todate,string Ioflag,string IMEIno, string address, string latitude,string longitude,string Reason,string empimagepath, string strType)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                     new SqlParameter("@Emp_ID", EmpID),
                     new SqlParameter("@Cmp_ID", CmpID),
                     new SqlParameter("@Vertical_ID", "0"),
                     new SqlParameter("@SubVertical_ID", "0"),
                     new SqlParameter("@ForDate ", Convert.ToDateTime(date).ToString("yyyy/MM/dd")),
                     new SqlParameter("@Time",  Convert.ToDateTime(todate).ToString("yyyy/MM/dd")),
                     new SqlParameter("@INOUTFlag", Ioflag),
                     new SqlParameter("@Reason", ""),
                     new SqlParameter("@IMEINo", ""),
                     new SqlParameter("@Latitude", latitude),
                     new SqlParameter("@Longitude", longitude),
                     new SqlParameter("@Address", address),
                     new SqlParameter("@Emp_Image", empimagepath),
                     new SqlParameter("@strAttendance", ""),
                     new SqlParameter("@Month", ""),
                     new SqlParameter("@Year", ""),
                     new SqlParameter("@Type", strType),
                     new SqlParameter("@Result",SqlDbType.VarChar,100){ Direction = ParameterDirection.Output },
                     new SqlParameter("@SubVerticalName", "")
                };
               ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_Attendance", sqlParams);

                string resultValue = sqlParams[sqlParams.Length - 2].Value.ToString(); // Assuming @Result is the second last parameter
                string strResult = null;
                //string strResult = resultValue.ToString();
                int intValue;
                if (int.TryParse(resultValue, out intValue))
                {
                    // Successfully parsed as integer, use intValue
                     strResult = intValue.ToString();
                }
                else
                {
                    // Unable to parse as integer, handle error or use the original string value
                    strResult = resultValue;
                    // Handle error or notify the user about the invalid format
                }
                //string strResult = oColSqlparram("@Result").ParaValue().ToString();
                return strResult;
            }
            catch (Exception ex)
            {
                logHelper.Error("GetDashBoardDetails : " + ex.Message);

                throw;
            }
        }
        public string PostClockOUT(int EmpID, int CmpID, string date, string todate, string Ioflag, string IMEIno, string address, string latitude, string longitude, string Reason, string empimagepath, string strType)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                     new SqlParameter("@Emp_ID", EmpID),
                     new SqlParameter("@Cmp_ID", CmpID),
                     new SqlParameter("@Vertical_ID", 0),
                     new SqlParameter("@SubVertical_ID", 0),
                     new SqlParameter("@ForDate ", Convert.ToDateTime(date).ToString("yyyy/MM/dd")),
                     new SqlParameter("@Time",  Convert.ToDateTime(todate).ToString("yyyy/MM/dd")),
                     new SqlParameter("@INOUTFlag", Ioflag),
                     new SqlParameter("@Reason", ""),
                     new SqlParameter("@IMEINo", ""),
                     new SqlParameter("@Latitude",latitude ),
                     new SqlParameter("@Longitude", longitude),
                     new SqlParameter("@Address", address),
                     new SqlParameter("@Emp_Image", empimagepath),
                     new SqlParameter("@strAttendance", ""),
                     new SqlParameter("@Month", ""),
                     new SqlParameter("@Year", ""),
                     new SqlParameter("@Type", strType),
                     new SqlParameter("@Result",SqlDbType.VarChar,100){ Direction = ParameterDirection.Output },
                     new SqlParameter("@SubVerticalName", "")
                };
                ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_Attendance", sqlParams);

                string resultValue = sqlParams[sqlParams.Length - 2].Value.ToString(); // Assuming @Result is the second last parameter
                string strResult = null;
                //string strResult = resultValue.ToString();
                int intValue;
                if (int.TryParse(resultValue, out intValue))
                {
                    // Successfully parsed as integer, use intValue
                    strResult = intValue.ToString();
                }
                else
                {
                    // Unable to parse as integer, handle error or use the original string value
                    strResult = resultValue;
                    // Handle error or notify the user about the invalid format
                }
                //string strResult = oColSqlparram("@Result").ParaValue().ToString();
                return strResult;
            }
            catch (Exception ex)
            {
                logHelper.Error("GetDeshBoardDetails : " + ex.Message);

                throw;
            }
        }
    }
    }
