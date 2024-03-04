using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Orange_RestWebAPI.BusinessLogic;
using Orange_RestWebAPI.Connections;
using Orange_RestWebAPI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static Orange_RestWebAPI.Model.PredefinedClasses.Response;
using System.Configuration;

namespace OrangePayrollAPI.Controllers
{
    [Produces("application/json")]
    [Route("Api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AttendanceController : ControllerBase
    {
        private IConfiguration _config;
        MyClassMutipleData<LoginModel> response = new MyClassMutipleData<LoginModel>();
        MyClass<LoginModel> singleResponse = new MyClass<LoginModel>();
        LogHelper logHelper = new LogHelper();

        public AttendanceController(IConfiguration config)
        {
            _config = config;
        }

        //[AllowAnonymous]
        [HttpGet(nameof(AttendanceDetails))]
        public IActionResult AttendanceDetails(int EmpID, int CmpID, int Month, int Year)
        {
            try
            {
                DataTable dtAttendanceDetails = null;
                IActionResult Response = Unauthorized();
                Attendance ObjAtttendance = new Attendance(_config);
                DateTime FromDate = new DateTime(Year, Month, 1);
                DateTime ToDate = new DateTime();
                ToDate = FromDate.AddMonths(1).AddDays(-1);

                ObjAtttendance.AttendanceDetails(EmpID, CmpID, Month, Year, FromDate.ToString("yyyy/MM/dd"), ToDate.ToString("yyyy/MM/dd"), "S", ref dtAttendanceDetails);

                if (dtAttendanceDetails.Rows.Count > 0 && dtAttendanceDetails != null)
                {
                    response.StatusCode = 200;
                    response.msg = "Successfully";
                    response.data = CommonClass.ToJson(dtAttendanceDetails);
                    Response = Ok(response);
                }
                else
                {
                    response.StatusCode = 404;
                    response.msg = "No Data Available";
                    response.data = "";
                    Response = Ok(response);
                }
                return Response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.msg = "Some thing went wrong.";
                response.data = "";

                logHelper.Error("AttendanceDetails : " + ex.Message);
                throw;
            }
            finally
            {
                response = null;
            }
        }

        //[AllowAnonymous]
        [HttpGet(nameof(AttendanceHistory))]
        public IActionResult AttendanceHistory(int EmpID, int CmpID, string FromDate, string Todate)
        {
            try
            {
                DataTable dtAttendanceDetails = null;
                IActionResult Response = Unauthorized();
                Attendance ObjAtttendance = new Attendance(_config);

                ObjAtttendance.AttendanceDetails(EmpID, CmpID, 0, 0, FromDate, Todate, "H", ref dtAttendanceDetails);

                if (dtAttendanceDetails.Rows.Count > 0 && dtAttendanceDetails != null)
                {
                    response.StatusCode = 200;
                    response.msg = "Successfully";
                    response.data = CommonClass.ToJson(dtAttendanceDetails);
                    Response = Ok(response);
                }
                else
                {
                    response.StatusCode = 404;
                    response.msg = "No Data Available";
                    response.data = "";
                    Response = Ok(response);
                }
                return Response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.msg = "Some thing went wrong.";
                response.data = "";

                logHelper.Error("AttendanceHistory : " + ex.Message);
                throw;
            }
            finally
            {
                response = null;
            }
        }

        //[AllowAnonymous]
        [HttpGet(nameof(GetReason))]
        public IActionResult GetReason(int CmpID, string ReasonType)
        {
            try
            {
                DataTable dtReason = null;
                IActionResult Response = Unauthorized();
                Attendance ObjAtttendance = new Attendance(_config);
                ObjAtttendance.GetPostRequestEmployee(CmpID, 0, 0, ReasonType, DateTime.Now.ToString("yyyy/MM/dd"), "", "", 0, 0, "R", ref dtReason);

                if (dtReason.Rows.Count > 0 && dtReason != null)
                {
                    response.StatusCode = 200;
                    response.msg = "Successfully";
                    response.data = CommonClass.ToJson(dtReason);
                    Response = Ok(response);
                }
                else
                {
                    response.StatusCode = 404;
                    response.msg = "No Data Available";
                    response.data = "";
                    Response = Ok(response);
                }
                return Response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.msg = "Some thing went wrong.";
                response.data = "";

                logHelper.Error("GetReason : " + ex.Message);
                throw;
            }
            finally
            {
                response = null;
            }
        }

        //[AllowAnonymous]
        [HttpGet(nameof(CheckINOUT))]
        public IActionResult CheckINOUT(int EmpID, int CmpID)
        {
            try
            {
                DataTable dtAttendanceDetails = null;
                IActionResult Response = Unauthorized();
                Attendance ObjAtttendance = new Attendance(_config);

                ObjAtttendance.AttendanceDetails(EmpID, CmpID, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("yyyy/MM/dd"), "C", ref dtAttendanceDetails);

                if (dtAttendanceDetails.Rows.Count > 0 && dtAttendanceDetails != null)
                {
                    response.StatusCode = 200;
                    response.msg = "Successfully";
                    response.data = CommonClass.ToJson(dtAttendanceDetails);
                    Response = Ok(response);
                }
                else
                {
                    response.StatusCode = 404;
                    response.msg = "No Data Available";
                    response.data = "";
                    Response = Ok(response);
                }
                return Response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.msg = "Some thing went wrong.";
                response.data = "";

                logHelper.Error("CheckINOUT : " + ex.Message);
                throw;
            }
            finally
            {
                response = null;
            }
        }

        //[AllowAnonymous]
        [HttpGet(nameof(AttendanceRegularizeApplicationRecord))]
        public IActionResult AttendanceRegularizeApplicationRecord(int EmpID, int CmpID)
        {
            try
            {
                DataSet dsAttendanceRegularizePeningApplication = null;
                IActionResult Response = Unauthorized();
                Attendance ObjAtttendance = new Attendance(_config);
                string strImageName = string.Empty;
                DataTable dtEmpDetails = null;

                ObjAtttendance.AttendanceRegularizeDetails(0, EmpID, CmpID, 0, 0, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("yyyy/MM/dd"), "P", ref dsAttendanceRegularizePeningApplication);

                if (dsAttendanceRegularizePeningApplication.Tables.Count > 0 && dsAttendanceRegularizePeningApplication != null)
                {
                    dsAttendanceRegularizePeningApplication.Tables[0].Columns.Add("Image_Path");
                    for (int i = 0; i <= dsAttendanceRegularizePeningApplication.Tables[0].Rows.Count - 1; i++)
                    {
                        ObjAtttendance.UpdateEmployeeDetails(Convert.ToInt32(dsAttendanceRegularizePeningApplication.Tables[0].Rows[i]["Emp_ID"]), Convert.ToInt32(dsAttendanceRegularizePeningApplication.Tables[0].Rows[i]["Cmp_ID"]), "", 0, 0, 0, "I", ref dtEmpDetails);
                        strImageName = dtEmpDetails.Rows[0][0].ToString();
                        dsAttendanceRegularizePeningApplication.Tables[0].Rows[i]["Image_Path"] = _config["DBConfig:ImagePath"].ToString() + "App_File/EMPIMAGES/" + strImageName;
                    }
                    response.StatusCode = 200;
                    response.msg = "Successfully";
                    response.data = CommonClass.ToJson(dsAttendanceRegularizePeningApplication.Tables[0]);
                    Response = Ok(response);
                }
                else
                {
                    response.StatusCode = 404;
                    response.msg = "No Data Available";
                    response.data = "";
                    Response = Ok(response);
                }
                return Response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.msg = "Some thing went wrong.";
                response.data = "";

                logHelper.Error("AttendanceRegularizeApplicationRecord : " + ex.Message);
                throw;
            }
            finally
            {
                response = null;
            }
        }

        //[AllowAnonymous]
        [HttpGet(nameof(GetAttendanceRegularizeApplicationDetails))]
        public IActionResult GetAttendanceRegularizeApplicationDetails(int ApplicationID)
        {
            try
            {
                DataSet dsAttendanceRegularizeApplicationDetails = null;
                IActionResult Response = Unauthorized();
                Attendance ObjAtttendance = new Attendance(_config);

                ObjAtttendance.AttendanceRegularizeDetails(ApplicationID, 0, 0, 0, 0, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("yyyy/MM/dd"), "E", ref dsAttendanceRegularizeApplicationDetails);

                if (dsAttendanceRegularizeApplicationDetails.Tables[0].Rows.Count > 0 && dsAttendanceRegularizeApplicationDetails != null)
                {
                    response.StatusCode = 200;
                    response.msg = "Successfully";
                    response.data = CommonClass.ToJson(dsAttendanceRegularizeApplicationDetails.Tables[0]);
                    response.data1 = CommonClass.ToJson(dsAttendanceRegularizeApplicationDetails.Tables[1]);
                    Response = Ok(response);
                }
                else
                {
                    response.StatusCode = 404;
                    response.msg = "No Data Available";
                    response.data = "";
                    Response = Ok(response);
                }
                return Response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.msg = "Some thing went wrong.";
                response.data = "";

                logHelper.Error("GetAttendanceRegularizeApplicationDetails : " + ex.Message);
                throw;
            }
            finally
            {
                response = null;
            }
        }

        //[AllowAnonymous]
        [HttpGet(nameof(AttendanceRegularizeInsert))]
        public IActionResult AttendanceRegularizeInsert(int EmpID, int CmpID, int Month, int Year, string Fordate, string Reason, string HalfFullDay, int CancelLateIn, int CancelEarlyOut, string Intime, string OutTime, int IsApprove, string OtherReason, string IMEINo)
        {
            try
            {
                IActionResult Response = Unauthorized();
                Attendance ObjAtttendance = new Attendance(_config);
                string strImageName = string.Empty;
                DataTable dtAttendanceRegularizeInsert = null;

                ObjAtttendance.AttendanceRegularizeInsert(0, EmpID, CmpID, Month, Year, Fordate, Reason, HalfFullDay, CancelLateIn, CancelEarlyOut, Intime, OutTime, IsApprove, OtherReason, IMEINo, 0, 0, 0, 0, "", "I", ref dtAttendanceRegularizeInsert);

                if (dtAttendanceRegularizeInsert.Rows.Count > 0 && dtAttendanceRegularizeInsert != null)
                {
                    response.StatusCode = 200;
                    response.msg = "Successfully";
                    response.data = CommonClass.ToJson(dtAttendanceRegularizeInsert);
                    Response = Ok(response);
                }
                else
                {
                    response.StatusCode = 404;
                    response.msg = "No Data Available";
                    response.data = "";
                    Response = Ok(response);
                }
                return Response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.msg = "Some thing went wrong.";
                response.data = "";

                logHelper.Error("AttendanceRegularizeApplicationRecord : " + ex.Message);
                throw;
            }
            finally
            {
                response = null;
            }
        }

        //[AllowAnonymous]
        [HttpGet(nameof(AttendanceRegularizeApproval))]
        public IActionResult AttendanceRegularizeApproval(int ApplicationID, int EmpID, int CmpID, string Fordate, string Reason, string HalfFullDay, int CancelLateIn, int CancelEarlyOut, string Intime, string OutTime, string Comment, int SEmpID, int RptLevel, int FinalApproval, int IsFWDRej, string AppStatus)
        {
            try
            {
                IActionResult Response = Unauthorized();
                Attendance ObjAtttendance = new Attendance(_config);
                string strImageName = string.Empty;
                DataTable dtAttendanceRegularizeInsert = null;
                int IsApprove = 0;

                if (AppStatus == "A")
                {
                    IsApprove = 1;
                }
                else if (AppStatus == "R")
                {
                    IsApprove = 2;
                }
                else
                {
                    IsApprove = 0;
                }
                ObjAtttendance.AttendanceRegularizeInsert(0, EmpID, CmpID, 0, 0, Fordate, Reason, HalfFullDay, CancelLateIn, CancelEarlyOut, Intime, OutTime, IsApprove, Comment, "", SEmpID, RptLevel, FinalApproval, IsFWDRej, AppStatus, "A", ref dtAttendanceRegularizeInsert);

                if (dtAttendanceRegularizeInsert.Rows.Count > 0 && dtAttendanceRegularizeInsert != null)
                {
                    response.Status = true;
                    response.msg = "Successfully";
                    response.data = CommonClass.ToJson(dtAttendanceRegularizeInsert);
                    Response = Ok(response);
                }
                else
                {
                    response.Status = false;
                    response.msg = "No Data Available";
                    response.data = "";
                    Response = Ok(response);
                }
                return Response;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.msg = "Some thing went wrong.";
                response.data = "";

                logHelper.Error("AttendanceRegularizeApproval : " + ex.Message);
                throw;
            }
            finally
            {
                response = null;
            }
        }

        [HttpPost(nameof(AddWorkPlanOnClockIn))]
        public IActionResult AddWorkPlanOnClockIn(int EmpID, int CmpID, string WorkPlan, string VisitPlan)
        {
            try
            {
                string Result = string.Empty;
                IActionResult Response = Unauthorized();
                Attendance ObjAtttendance = new Attendance(_config);

                Result = ObjAtttendance.WorkPlanOnClockInOut(EmpID, CmpID, WorkPlan, VisitPlan, "", "", 'I');

                if (!(String.IsNullOrEmpty(Result)))
                {
                    singleResponse.Status = true;
                    singleResponse.msg = Result.Split("#")[0].Replace("@", "");
                    singleResponse.data = "";
                    Response = Ok(singleResponse);
                }
                else
                {
                    singleResponse.Status = false;
                    singleResponse.msg = "No Data Available";
                    singleResponse.data = "";
                    Response = Ok(singleResponse);
                }
                return Response;
            }
            catch (Exception ex)
            {
                singleResponse.Status = false;
                singleResponse.msg = "Some thing went wrong.";
                singleResponse.data = "";

                logHelper.Error("AddWorkPlanOnClockIn : " + ex.Message);
                throw;
            }
            finally
            {
                singleResponse = null;
            }
        }

        [HttpPut(nameof(AddWorkPlanOnClockOut))]
        public IActionResult AddWorkPlanOnClockOut(int EmpID, int CmpID, string WorkSummary, string VisitSummary)
        {
            try
            {
                string Result = string.Empty;
                IActionResult Response = Unauthorized();
                Attendance ObjAtttendance = new Attendance(_config);

                Result = ObjAtttendance.WorkPlanOnClockInOut(EmpID, CmpID, "", "", WorkSummary, VisitSummary, 'U');

                if (!(String.IsNullOrEmpty(Result)))
                {
                    singleResponse.Status = true;
                    singleResponse.msg = Result.Split("#")[0].Replace("@", "");
                    singleResponse.data = "";
                    Response = Ok(singleResponse);
                }
                else
                {
                    singleResponse.Status = false;
                    singleResponse.msg = "No Data Available";
                    singleResponse.data = "";
                    Response = Ok(singleResponse);
                }
                return Response;
            }
            catch (Exception ex)
            {
                singleResponse.Status = false;
                singleResponse.msg = "Some thing went wrong.";
                singleResponse.data = "";

                logHelper.Error("AddWorkPlanOnClockOut : " + ex.Message);
                throw;
            }
            finally
            {
                singleResponse = null;
            }
        }
        [HttpGet(nameof(AllEmployeeAttendance))]
        public IActionResult AllEmployeeAttendance(int CmpID, string FromDate, string ToDate)
        {
            try
            {
                DataSet dtAllEmployeesAttendanceDetail = null;
                IActionResult Response = Unauthorized();
                Attendance ObjAtttendance = new Attendance(_config);

                ObjAtttendance.AttendanceRegularizeDetails(0, 0, CmpID, 0, 0, FromDate, ToDate, "O", ref dtAllEmployeesAttendanceDetail);

                if (dtAllEmployeesAttendanceDetail.Tables[0].Rows.Count > 0 && dtAllEmployeesAttendanceDetail != null)
                {
                    singleResponse.Status = true;
                    singleResponse.msg = "Successfully";
                    singleResponse.data = CommonClass.ToJson(dtAllEmployeesAttendanceDetail.Tables[0]);
                    Response = Ok(singleResponse);
                }
                else
                {
                    singleResponse.Status = false;
                    singleResponse.msg = "No Data Available";
                    singleResponse.data = "";
                    Response = Ok(singleResponse);
                }
                return Response;
            }
            catch (Exception ex)
            {
                singleResponse.Status = false;
                singleResponse.msg = "Some thing went wrong.";
                singleResponse.data = "";

                logHelper.Error("GetAllEmployeesAttendanceDetail : " + ex.Message);
                throw;
            }
            finally
            {
                singleResponse = null;
            }
        }

        //[HttpGet(nameof(AttendanceInsert))]
        //public IActionResult AttendanceInsert(int EmpID, int CmpID, string ForDate, string IOFlage, string Reason, string IMEINO, string Latitude, string Longitude, string Address, string ImageName, int VerticalID, int SubVerticalID, string SubVerticalName)
        //{
        //    try
        //    {
        //        DataTable dtAttendanceDetails = null;
        //        IActionResult Response = Unauthorized();
        //        Attendance ObjAtttendance = new Attendance(_config);

        //        if(ForDate == "")
        //         ForDate = DateTime.Now.ToString("yyyy/MM/dd");

        //        ObjAtttendance.AttendanceRegularizeDetails(0, 0, CmpID, 0, 0, FromDate, ToDate, "O", ref dtAllEmployeesAttendanceDetail);

        //        if (dtAllEmployeesAttendanceDetail.Tables[0].Rows.Count > 0 && dtAllEmployeesAttendanceDetail != null)
        //        {
        //            singleResponse.Status = true;
        //            singleResponse.msg = "Successfully";
        //            singleResponse.data = CommonClass.ToJson(dtAllEmployeesAttendanceDetail.Tables[0]);
        //            Response = Ok(singleResponse);
        //        }
        //        else
        //        {
        //            singleResponse.Status = false;
        //            singleResponse.msg = "No Data Available";
        //            singleResponse.data = "";
        //            Response = Ok(singleResponse);
        //        }
        //        return Response;
        //    }
        //    catch (Exception ex)
        //    {
        //        singleResponse.Status = false;
        //        singleResponse.msg = "Some thing went wrong.";
        //        singleResponse.data = "";

        //        logHelper.Error("GetAllEmployeesAttendanceDetail : " + ex.Message);
        //        throw;
        //    }
        //    finally
        //    {
        //        singleResponse = null;
        //    }
        //}
    }
}

