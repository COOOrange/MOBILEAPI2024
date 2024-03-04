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
    public class DashboardController : Controller
    {
        // GET: DashboardController
        private IConfiguration _config;
        MyClassToken<LoginModel> response = new MyClassToken<LoginModel>();
        MyClassMutipleData<LoginModel> s_Response = new MyClassMutipleData<LoginModel>();
        LogHelper logHelper = new LogHelper();
        ClsDataccess objclsdataaccess ;
        public DashboardController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet(nameof(DashboardDetails))]
        public IActionResult DashboardDetails(int EmpID, int CmpID)
        {

            try
            {
                DataSet dsdhDetails = null;
                //DataTable dtdhDetails = null;
                IActionResult Response = Unauthorized();
                DataTable dtt = null;
                ClsDataccess objclsdataaccess=new ClsDataccess(_config);
                Attendance ObjAtttendance = new Attendance(_config);
                Dashboard ObjDashB = new Dashboard(_config);
                if ((EmpID != 0) && (CmpID !=0))

{
                    ObjDashB.GetDeshBoardDetails(EmpID, CmpID, ref dsdhDetails);
                    ObjDashB.GetLastClockLocation(EmpID, CmpID, "L", ref dtt);
                  //ObjDashB.GetLastClockLocation(EmpID, CmpID, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("yyyy/MM/dd"), "C" ,ref dtt);
                  // objclsdataaccess.Directexecute(data);
                  //ObjAtttendance.AttendanceDetails(EmpID, CmpID, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("yyyy/MM/dd"), "C", ref dtAttendanceDetails);

                    if (dsdhDetails.Tables.Count > 0 && dsdhDetails != null && dtt != null  && dtt.Rows.Count>0)
                    {
                        s_Response.StatusCode = 200;
                        s_Response.msg = "Successfully";
                        s_Response.data = CommonClass.ToJson(dsdhDetails.Tables[1]);
                        s_Response.details = CommonClass.ToJson(dtt);
                       // response.additionaldetails= CommonClass.ToJson(dsdhDetails.Tables[5]);
                        Response = Ok(s_Response);
                    }
                    else
                    {
                        s_Response.StatusCode = 404;
                        s_Response.msg = "Not Found";
                        //response.data = "";
                        Response = Unauthorized(s_Response);
                    }
                }
                else
                {
                    s_Response.StatusCode = 404;
                    s_Response.msg = "Entered Company ID or Employee ID is wrong";
                    s_Response.data = "";
                    Response = BadRequest(s_Response);
                }
                return Response;
            }
            catch (Exception ex)
            {
                s_Response.StatusCode = 500;
                s_Response.msg = "Some thing went wrong.";
                s_Response.data = "";

                logHelper.Error("DashboardDetails : " + ex.Message);
                throw;
            }
            finally
            {
                s_Response = null;
            }
        }
    }
}
