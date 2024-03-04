using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Orange_RestWebAPI.BusinessLogic;
using Orange_RestWebAPI.Connections;
using Orange_RestWebAPI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CoreApiResponse;
using static Orange_RestWebAPI.Model.PredefinedClasses.Response;

namespace Orange_RestWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("Api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    
    public class LeaveController : Controller
    {
        // GET: LeaveController
        private IConfiguration _config;
        MyClassToken<LoginModel> response = new MyClassToken<LoginModel>();
        MyClassMutipleData<LoginModel> singleResponse = new MyClassMutipleData<LoginModel>();
        LogHelper logHelper = new LogHelper();
        ClsDataccess objclsdataaccess;
        Leave ObjLeave;
        public LeaveController(IConfiguration config)
        {
            _config = config;
        }
        
        [HttpGet(nameof(FilterLeaveBalance))]
        public IActionResult FilterLeaveBalance(int EmpID, int CmpID, int Month, int Year)
        {
            try
            {
                DataTable dtLeaveBalance = null;
                IActionResult Response = Unauthorized();
                ClsDataccess objclsdataaccess = new ClsDataccess(_config);
                Leave ObjLeave = new Leave(_config);
                var FromDate = new DateTime(Year, Month, 1);
                var ToDate = new DateTime();
                ToDate = FromDate.AddMonths(1).AddDays(-1);

                ObjLeave.GetFilterLeaveBalance(0, EmpID, CmpID, FromDate, ToDate, "R", ref dtLeaveBalance);

                if (dtLeaveBalance.Rows.Count > 0 && dtLeaveBalance != null)
                {
                    var data = CommonClass.ToJson(dtLeaveBalance);
                    singleResponse.StatusCode = 200;
                    singleResponse.msg = "Successfully";
                    singleResponse.data = data;

                    Response = Ok(singleResponse);
                }
                else
                {
                    singleResponse.StatusCode = 404;
                    singleResponse.msg = "No Data Available";
                    singleResponse.data = "";

                    Response = Unauthorized(singleResponse);
                }
                return Response;
            }
            catch (Exception ex)
            {
                singleResponse.StatusCode = 500;
                singleResponse.msg = "Some thing went wrong.";
                singleResponse.data = "";

                logHelper.Error("ClockInOutStatus : " + ex.Message);
                throw;
            }
            finally
            {
                singleResponse = null;
            }





           
        }
        [HttpGet(nameof(FilterLeaveStatus))]
        public IActionResult FilterLeaveStatus(int EmpID, int CmpID, int Month, int Year)
        {
            try
            {
                DataTable dtLeaveBalance = null;
                IActionResult Response = Unauthorized();
                ClsDataccess objclsdataaccess = new ClsDataccess(_config);
                Leave ObjLeave = new Leave(_config);
                var FromDate = new DateTime(Year, Month, 1);
                var ToDate = new DateTime();
                ToDate = FromDate.AddMonths(1).AddDays(-1);

                ObjLeave.GetFilterLeaveBalance(0, EmpID, CmpID, FromDate, ToDate, "S", ref dtLeaveBalance);

                if (dtLeaveBalance.Rows.Count > 0 && dtLeaveBalance != null)
                {
                    var data = CommonClass.ToJson(dtLeaveBalance);
                    //singleResponse.StatusCode = 200;
                    //singleResponse.msg = "Successfully";
                    //singleResponse.data = data;

                    // Response = Ok(singleResponse);
                    Response = Ok(new { message = " Successfully", StatusCode = "200", Data = data });
                }
                else
                {
                    Response = Unauthorized(new { message = " Failed", StatusCode = "404" });
                    //singleResponse.StatusCode = 404;
                    //singleResponse.msg = "No Data Available";
                    //singleResponse.data = "";

                    //Response = Unauthorized(singleResponse);
                }
                return Response;
            }
            catch (Exception ex)
            {
                singleResponse.StatusCode = 500;
                singleResponse.msg = "Some thing went wrong.";
                singleResponse.data = "";

                logHelper.Error("LeaveStatus : " + ex.Message);
                throw;
            }
            finally
            {
                singleResponse = null;
            }





            ////try
            ////{
            ////    DataSet dsdhDetails = null;
            ////    //DataTable dtdhDetails = null;
            ////    IActionResult Response = Unauthorized();
            ////    DataTable dtt = null;
            ////    ClsDataccess objclsdataaccess = new ClsDataccess(_config);
            ////    Attendance ObjAtttendance = new Attendance(_config);
            ////    Dashboard ObjDashB = new Dashboard(_config);
            ////    if ((EmpID != 0) && (CmpID != 0))

            ////    {
            ////        ObjDashB.GetDeshBoardDetails(EmpID, CmpID, ref dsdhDetails);
            ////        ObjDashB.GetLastClockLocation(EmpID, CmpID, "L", ref dtt);
            ////        //ObjDashB.GetLastClockLocation(EmpID, CmpID, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("yyyy/MM/dd"), "C" ,ref dtt);
            ////        // objclsdataaccess.Directexecute(data);
            ////        //ObjAtttendance.AttendanceDetails(EmpID, CmpID, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("yyyy/MM/dd"), "C", ref dtAttendanceDetails);

            ////        if (dsdhDetails.Tables.Count > 0 && dsdhDetails != null && dtt != null && dtt.Rows.Count > 0)
            ////        {
            ////            s_Response.StatusCode = 200;
            ////            s_Response.msg = "Successfully";
            ////            s_Response.data = CommonClass.ToJson(dsdhDetails.Tables[1]);
            ////            s_Response.details = CommonClass.ToJson(dtt);
            ////            // response.additionaldetails= CommonClass.ToJson(dsdhDetails.Tables[5]);
            ////            Response = Ok(s_Response);
            ////        }
            ////        else
            ////        {
            ////            s_Response.StatusCode = 404;
            ////            s_Response.msg = "Not Found";
            ////            //response.data = "";
            ////            Response = Unauthorized(s_Response);
            ////        }
            ////    }
            ////    else
            ////    {
            ////        s_Response.StatusCode = 404;
            ////        s_Response.msg = "Entered Company ID or Employee ID is wrong";
            ////        s_Response.data = "";
            ////        Response = BadRequest(s_Response);
            ////    }
            ////    return Response;
            ////}
            ////catch (Exception ex)
            ////{
            ////    s_Response.StatusCode = 500;
            ////    s_Response.msg = "Some thing went wrong.";
            ////    s_Response.data = "";

            ////    logHelper.Error("DashboardDetails : " + ex.Message);
            ////    throw;
            ////}
            ////finally
            ////{
            ////    s_Response = null;
            ////}
        }


        [HttpGet(nameof(LeaveBalance))]
        public IActionResult LeaveBalance(int EmpID, int CmpID)
        {
            try
            {
                DataTable dtLeaveBalance = null;
                IActionResult Response = Unauthorized();
                ClsDataccess objclsdataaccess = new ClsDataccess(_config);
                Leave ObjLeave = new Leave(_config);
                DateTime currentDate = DateTime.Now;
                // Get the current year
                int currentYear = currentDate.Year;

                // Get the current month
                int currentMonth = currentDate.Month;
                var FromDate = new DateTime(currentYear, currentMonth, 1);
                var ToDate = new DateTime();
                ToDate = FromDate.AddMonths(1).AddDays(-1);

                ObjLeave.GetLeaveBalance(0, EmpID, CmpID, FromDate, ToDate, "R", ref dtLeaveBalance);

                if (dtLeaveBalance.Rows.Count > 0 && dtLeaveBalance != null)
                {
                    var data = CommonClass.ToJson(dtLeaveBalance);
                    singleResponse.StatusCode = 200;
                    singleResponse.msg = "Successfully";
                    singleResponse.data = data;

                    Response = Ok(singleResponse);
                }
                else
                {
                    singleResponse.StatusCode = 404;
                    singleResponse.msg = "No Data Available";
                    singleResponse.data = "";

                    Response = Unauthorized(singleResponse);
                }
                return Response;
            }
            catch (Exception ex)
            {
                singleResponse.StatusCode = 500;
                singleResponse.msg = "Some thing went wrong.";
                singleResponse.data = "";

                logHelper.Error("ClockInOutStatus : " + ex.Message);
                throw;
            }
            finally
            {
                singleResponse = null;
            }

        }



           
            ////}
        
        [HttpGet(nameof(LeaveStatus))]
        public IActionResult LeaveStatus(int EmpID, int CmpID)
        {
            try
            {
                DataTable dtLeaveBalance = null;
                IActionResult Response = Unauthorized();
                ClsDataccess objclsdataaccess = new ClsDataccess(_config);
                Leave ObjLeave = new Leave(_config);
                DateTime currentDate = DateTime.Now;
                // Get the current year
                int currentYear = currentDate.Year;

                // Get the current month
                int currentMonth = currentDate.Month;
                var FromDate = new DateTime(currentYear, currentMonth, 1);
                var ToDate = new DateTime();
                ToDate = FromDate.AddMonths(1).AddDays(-1);

                //var FromDate = new DateTime(Year, Month, 1);
                //var ToDate = new DateTime();
                //ToDate = FromDate.AddMonths(1).AddDays(-1);

                ObjLeave.GetLeaveBalance(0, EmpID, CmpID, FromDate, ToDate, "S", ref dtLeaveBalance);

                if (dtLeaveBalance.Rows.Count > 0 && dtLeaveBalance != null)
                {
                    var data = CommonClass.ToJson(dtLeaveBalance);
                    //singleResponse.StatusCode = 200;
                    //singleResponse.msg = "Successfully";
                    //singleResponse.data = data;

                    // Response = Ok(singleResponse);
                    Response= Ok(new { message = " Successfully",StatusCode="200",Data= data });
                }
                else
                {
                    Response = Unauthorized(new { message = " No Data Available", StatusCode = "401"});
                    //singleResponse.StatusCode = 404;
                    //singleResponse.msg = "No Data Available";
                    //singleResponse.data = "";

                    //Response = Unauthorized(singleResponse);
                }
                return Response;
            }
            catch (Exception ex)
            {
                singleResponse.StatusCode = 500;
                singleResponse.msg = "Some thing went wrong.";
                singleResponse.data = "";

                logHelper.Error("LeaveStatus : " + ex.Message);
                throw;
            }
            finally
            {
                singleResponse = null;
            }





            ////try
            ////{
            ////    DataSet dsdhDetails = null;
            ////    //DataTable dtdhDetails = null;
            ////    IActionResult Response = Unauthorized();
            ////    DataTable dtt = null;
            ////    ClsDataccess objclsdataaccess = new ClsDataccess(_config);
            ////    Attendance ObjAtttendance = new Attendance(_config);
            ////    Dashboard ObjDashB = new Dashboard(_config);
            ////    if ((EmpID != 0) && (CmpID != 0))

            ////    {
            ////        ObjDashB.GetDeshBoardDetails(EmpID, CmpID, ref dsdhDetails);
            ////        ObjDashB.GetLastClockLocation(EmpID, CmpID, "L", ref dtt);
            ////        //ObjDashB.GetLastClockLocation(EmpID, CmpID, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("yyyy/MM/dd"), "C" ,ref dtt);
            ////        // objclsdataaccess.Directexecute(data);
            ////        //ObjAtttendance.AttendanceDetails(EmpID, CmpID, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("yyyy/MM/dd"), "C", ref dtAttendanceDetails);

            ////        if (dsdhDetails.Tables.Count > 0 && dsdhDetails != null && dtt != null && dtt.Rows.Count > 0)
            ////        {
            ////            s_Response.StatusCode = 200;
            ////            s_Response.msg = "Successfully";
            ////            s_Response.data = CommonClass.ToJson(dsdhDetails.Tables[1]);
            ////            s_Response.details = CommonClass.ToJson(dtt);
            ////            // response.additionaldetails= CommonClass.ToJson(dsdhDetails.Tables[5]);
            ////            Response = Ok(s_Response);
            ////        }
            ////        else
            ////        {
            ////            s_Response.StatusCode = 404;
            ////            s_Response.msg = "Not Found";
            ////            //response.data = "";
            ////            Response = Unauthorized(s_Response);
            ////        }
            ////    }
            ////    else
            ////    {
            ////        s_Response.StatusCode = 404;
            ////        s_Response.msg = "Entered Company ID or Employee ID is wrong";
            ////        s_Response.data = "";
            ////        Response = BadRequest(s_Response);
            ////    }
            ////    return Response;
            ////}
            ////catch (Exception ex)
            ////{
            ////    s_Response.StatusCode = 500;
            ////    s_Response.msg = "Some thing went wrong.";
            ////    s_Response.data = "";

            ////    logHelper.Error("DashboardDetails : " + ex.Message);
            ////    throw;
            ////}
            ////finally
            ////{
            ////    s_Response = null;
            ////}
        }


        [HttpGet(nameof(LeaveTypeBind))]
        public IActionResult LeaveTypeBind(int EmpID, int CmpID)
        {
            try
            {
                DataTable dtLeaveBalance = null;
                IActionResult Response = Unauthorized();
                ClsDataccess objclsdataaccess = new ClsDataccess(_config);
                Leave ObjLeave = new Leave(_config);
                //DateTime currentDate = DateTime.Now;
                //// Get the current year
                //int currentYear = currentDate.Year;

                //// Get the current month
                //int currentMonth = currentDate.Month;
                //var FromDate = new DateTime(currentYear, currentMonth, 1);
                //var ToDate = new DateTime();
                //ToDate = FromDate.AddMonths(1).AddDays(-1);

                //var FromDate = new DateTime(Year, Month, 1);
                //var ToDate = new DateTime();
                //ToDate = FromDate.AddMonths(1).AddDays(-1);

                ObjLeave.GetLeaveTypeBind(0, EmpID, CmpID, "B", ref dtLeaveBalance);

                if (dtLeaveBalance.Rows.Count > 0 && dtLeaveBalance != null)
                {
                    var data = CommonClass.ToJson(dtLeaveBalance);
                    //singleResponse.StatusCode = 200;
                    //singleResponse.msg = "Successfully";
                    //singleResponse.data = data;

                    // Response = Ok(singleResponse);
                    Response = Ok(new { message = " Successfully", StatusCode = "200", Data = data });
                }
                else
                {
                    Response = Unauthorized(new { message = " No Data Available", StatusCode = "401" });
                    //singleResponse.StatusCode = 404;
                    //singleResponse.msg = "No Data Available";
                    //singleResponse.data = "";

                    //Response = Unauthorized(singleResponse);
                }
                return Response;
            }
            catch (Exception ex)
            {
                singleResponse.StatusCode = 500;
                singleResponse.msg = "Some thing went wrong.";
                singleResponse.data = "";

                logHelper.Error("LeaveStatus : " + ex.Message);
                throw;
            }
            finally
            {
                singleResponse = null;
            }





            ////try
            ////{
            ////    DataSet dsdhDetails = null;
            ////    //DataTable dtdhDetails = null;
            ////    IActionResult Response = Unauthorized();
            ////    DataTable dtt = null;
            ////    ClsDataccess objclsdataaccess = new ClsDataccess(_config);
            ////    Attendance ObjAtttendance = new Attendance(_config);
            ////    Dashboard ObjDashB = new Dashboard(_config);
            ////    if ((EmpID != 0) && (CmpID != 0))

            ////    {
            ////        ObjDashB.GetDeshBoardDetails(EmpID, CmpID, ref dsdhDetails);
            ////        ObjDashB.GetLastClockLocation(EmpID, CmpID, "L", ref dtt);
            ////        //ObjDashB.GetLastClockLocation(EmpID, CmpID, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("yyyy/MM/dd"), "C" ,ref dtt);
            ////        // objclsdataaccess.Directexecute(data);
            ////        //ObjAtttendance.AttendanceDetails(EmpID, CmpID, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("yyyy/MM/dd"), "C", ref dtAttendanceDetails);

            ////        if (dsdhDetails.Tables.Count > 0 && dsdhDetails != null && dtt != null && dtt.Rows.Count > 0)
            ////        {
            ////            s_Response.StatusCode = 200;
            ////            s_Response.msg = "Successfully";
            ////            s_Response.data = CommonClass.ToJson(dsdhDetails.Tables[1]);
            ////            s_Response.details = CommonClass.ToJson(dtt);
            ////            // response.additionaldetails= CommonClass.ToJson(dsdhDetails.Tables[5]);
            ////            Response = Ok(s_Response);
            ////        }
            ////        else
            ////        {
            ////            s_Response.StatusCode = 404;
            ////            s_Response.msg = "Not Found";
            ////            //response.data = "";
            ////            Response = Unauthorized(s_Response);
            ////        }
            ////    }
            ////    else
            ////    {
            ////        s_Response.StatusCode = 404;
            ////        s_Response.msg = "Entered Company ID or Employee ID is wrong";
            ////        s_Response.data = "";
            ////        Response = BadRequest(s_Response);
            ////    }
            ////    return Response;
            ////}
            ////catch (Exception ex)
            ////{
            ////    s_Response.StatusCode = 500;
            ////    s_Response.msg = "Some thing went wrong.";
            ////    s_Response.data = "";

            ////    logHelper.Error("DashboardDetails : " + ex.Message);
            ////    throw;
            ////}
            ////finally
            ////{
            ////    s_Response = null;
            ////}
        }
        [HttpGet(nameof(CheckPeriod))]
        public IActionResult CheckPeriod(int EmpID, int CmpID,int Leave_ID,DateTime FromDate, int Period)
        {
            DateTime toDate;
            try
            {
                DataSet dsLeaveApp = null;
                IActionResult Response = Unauthorized();
                ClsDataccess objclsdataaccess = new ClsDataccess(_config);
                Leave ObjLeave = new Leave(_config);
                
                
                int year = FromDate.Year;
                int month = FromDate.Month;
                //int periodday = Period;
               // var From_Date = FromDate;
           toDate = FromDate.AddDays(Period);
                // var From_Date = new DateTime(year, month, 1);

                //var ToDate = new DateTime();
                //ToDate = FromDate.AddMonths(1).AddDays(-1);

                 ObjLeave.ValidateCheckPeriod(EmpID, CmpID, Leave_ID,FromDate,Period, "V", ref dsLeaveApp);

                if (dsLeaveApp.Tables.Count > 0 && dsLeaveApp != null)
                //if (EmpID!=0 && CmpID!=0)
                {
                    var Todate= toDate.ToShortDateString();
                  var msg = dsLeaveApp.Tables[0].Rows[0][0].ToString().Split("#")[0].Replace("@", "");
                    var data = dsLeaveApp.Tables[0].Rows[0][0].ToString().Split("#")[2].Replace("@", "");
                    // Response = Ok(singleResponse);
                    Response = Ok(new { message = msg, StatusCode = "200", Data=data});
                }
                else
                {
                    Response = Unauthorized(new { message = " No Data Available", StatusCode = "401" });
                    
                }
                return Response;
            }
            catch (Exception ex)
            {
                singleResponse.StatusCode = 500;
                singleResponse.msg = "Some thing went wrong.";
                singleResponse.data = "";

                logHelper.Error("LeaveStatus : " + ex.Message);
                throw;
            }
            finally
            {
                singleResponse = null;
            }





            
        }

        [HttpGet(nameof(LeaveApplication))]
        public IActionResult LeaveApplication(int LeavAppID, int EmpID, int CmpID, int LeaveID, string FromDate, string Period, string ToDate, string AssignAs, string Comment, string HLeaveDate, string InTime, string OutTime, int LoginID, string Attachement, string DocName, string CompoffLeaveDates, string strType)
        {
            try
            {
                DataSet dsLeaveApplicationData = null;
                IActionResult Response = Unauthorized();
                Leave ObjLeave = new Leave(_config);
                string strDocName = String.Empty;
                string strDocPath = String.Empty;
                byte[] docBytes;

                if (string.IsNullOrEmpty(FromDate))
                {
                    FromDate = "1900-01-01 00:00:00";
                }
                if (string.IsNullOrEmpty(ToDate))
                {
                    ToDate = "1900-01-01 00:00:00";
                }
                if (string.IsNullOrEmpty(HLeaveDate))
                {

                    HLeaveDate = "1900-01-01 00:00:00";
                }
                if (string.IsNullOrEmpty(InTime))
                {
                    InTime = "1900-01-01 00:00:00";
                }
                if (string.IsNullOrEmpty(OutTime))
                {
                    OutTime = "1900-01-01 00:00:00";
                }
                if (string.IsNullOrEmpty(Period))
                {
                    Period = "0.0";
                }
                if (!string.IsNullOrEmpty(Attachement))

                {
                    strDocName = Convert.ToString(EmpID) + "" + Convert.ToString(DateTime.Now).Replace("-", "").Replace(" ", "").Replace("/", "").Replace("+", "").Replace(":", "");
                    strDocPath = _config["DBConfig:ExitPath"].ToString() + strDocName;
                    docBytes = Convert.FromBase64String(Attachement);
                    MemoryStream ms = new MemoryStream(docBytes);
                    FileStream fs = new FileStream(strDocPath, FileMode.Create);
                    ms.WriteTo(fs);
                    fs.Close();
                    fs.Dispose();
                }
                ObjLeave.LeaveApplication(LeavAppID, EmpID, CmpID, LeaveID, FromDate, Convert.ToDecimal(Period), ToDate, AssignAs, Comment, HLeaveDate, InTime, OutTime, LoginID, strDocName, CompoffLeaveDates, strType, ref dsLeaveApplicationData);

                if (dsLeaveApplicationData.Tables[0].Rows.Count > 0 && dsLeaveApplicationData != null)
                {
                    var data = CommonClass.ToJson(dsLeaveApplicationData.Tables[0]);
                   // singleResponse.StatusCode = 200;
                        //Convert.ToBoolean(dsLeaveApplicationData.Tables[0].Rows[0][0].ToString().Split("#")[1]);
                   var msg = dsLeaveApplicationData.Tables[0].Rows[0][0].ToString().Split("#")[0].Replace("@", "");
                   var data1 = dsLeaveApplicationData.Tables[0].Rows[0][0].ToString().Split("#")[2].Replace("@", "");
                    Response = Ok(new { message = " Successfully", StatusCode = "200", Data = data,Data1=msg, Details=data1 });
                    
                }
                else
                {
                    //singleResponse.StatusCode = 401;
                    //singleResponse.msg = "No Data Available";
                    //singleResponse.data = "";

                    Response = Unauthorized(new { Message = "No Data Available", StatusCode = "401"});
                }
                return Response;
            }
            catch (Exception ex)
            {
                singleResponse.StatusCode = 500;
                singleResponse.msg = "Some thing went wrong.";
                singleResponse.data = "";

                logHelper.Error("LeaveApplication : " + ex.Message);
                throw;
            }
            finally
            {
                singleResponse = null;
            }
        }
        //[AllowAnonymous]
        [HttpGet(nameof(LeaveApplicationStatus))]
        public IActionResult LeaveApplicationStatus(int EmpID, int CmpID,  int LeaveAppID)
        {
            try
            {
                DataSet dsLeaveApprovalstatus = null;
                IActionResult Response = Unauthorized();
                Leave ObjLeave = new Leave(_config);

                
                ObjLeave.LeaveApproval(EmpID, CmpID, LeaveAppID, ref dsLeaveApprovalstatus);

                if (dsLeaveApprovalstatus.Tables.Count > 0 && dsLeaveApprovalstatus != null)
                {
                    var data = CommonClass.ToJson(dsLeaveApprovalstatus.Tables[0]);
                    singleResponse.StatusCode = 200;
                    //Convert.ToBoolean(dsLeaveApproval.Tables[0].Rows[0][0].ToString().Split("#")[1]);
                   //var data1 = CommonClass.ToJson(dsLeaveApprovalstatus.Tables[0]);
                    var data1 = CommonClass.ToJson(dsLeaveApprovalstatus.Tables[1]);
                 var  details = CommonClass.ToJson(dsLeaveApprovalstatus.Tables[2]);
                   // Response = Ok(singleResponse);
                    Response = Ok(new { message = "Successfully", StatusCode = "200", Data = data, Data1 = data1, Details = details });
                }
                else
                {
                    //singleResponse.StatusCode = 401;
                    //singleResponse.msg = "No Data Available";
                    //singleResponse.data = "";
                    Response = Unauthorized(new { Message = "No Data Available", StatusCode = "401" });
                   // Response = Ok(singleResponse);
                }
                return Response;
            }
            catch (Exception ex)
            {
                singleResponse.StatusCode = 500;
                singleResponse.msg = "Some thing went wrong.";
                singleResponse.data = "";

                logHelper.Error("LeaveApproval : " + ex.Message);
                throw;
            }
            finally
            {
                singleResponse = null;
            }
        }

    }
}
