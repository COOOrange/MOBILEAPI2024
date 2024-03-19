using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DTO.Common;
using MOBILEAPI2024.DTO.RequestDTO.User;
using MOBILEAPI2024.DTO.ResponseDTO.User;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace MOBILEAPI2024.API.Controllers
{
    [Route("api/v1")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public static IWebHostEnvironment _webhostenv;
        public UserController(IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService;
            _webhostenv = webHostEnvironment;
        }

        [HttpGet]
        [Route(APIUrls.Dashboard)]
        public IActionResult DashboardDetails()
        {
            Response response = new Response();
            try
            {
                var authorization = HttpContext.Request.Headers[HeaderNames.Authorization];
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    var jToken = headerValue.Parameter;
                    var handler = new JwtSecurityTokenHandler();

                    var jsonToken = handler.ReadToken(jToken) as JwtSecurityToken;
                    if (jsonToken != null)
                    {
                        var empId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Emp_ID")?.Value;
                        var cmpId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Cmp_ID")?.Value;
                        if (!string.IsNullOrEmpty(empId) && !string.IsNullOrEmpty(cmpId))
                        {
                            var dashbpardData = _userService.DashboardData(empId, cmpId);
                            if (dashbpardData.employeeCount != null && dashbpardData.employeeData != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = dashbpardData;
                                return Ok(response);
                            }
                        }
                    }
                    else
                    {
                        // Handle the case where the token cannot be read as a JWT token
                        response.code = StatusCodes.Status401Unauthorized;
                        response.status = false;
                        response.message = CommonMessage.TokenExpired;
                        return StatusCode(StatusCodes.Status401Unauthorized, response);
                    }
                }
                // Handle the case where the token cannot be read as a JWT token
                response.code = StatusCodes.Status401Unauthorized;
                response.status = false;
                response.message = CommonMessage.TokenExpired;
                return StatusCode(StatusCodes.Status401Unauthorized, response);
            }
            catch (Exception e)
            {
                response.code = StatusCodes.Status500InternalServerError;
                response.status = false;
                response.message = CommonMessage.SomethingWrong + " " + e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        [Route(APIUrls.ClockIn)]
        public IActionResult ClockIn([FromForm] ClockIn clockIn)
        {
            Response response = new Response();
            try
            {
                var authorization = HttpContext.Request.Headers[HeaderNames.Authorization];
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    var jToken = headerValue.Parameter;
                    var handler = new JwtSecurityTokenHandler();

                    var jsonToken = handler.ReadToken(jToken) as JwtSecurityToken;
                    if (jsonToken != null)
                    {
                        var empId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Emp_ID")?.Value;
                        var cmpId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Cmp_ID")?.Value;
                        if (!string.IsNullOrEmpty(empId) && !string.IsNullOrEmpty(cmpId))
                        {
                            clockIn.Emp_Id = Convert.ToInt32(empId);
                            clockIn.Cmp_Id = Convert.ToInt32(cmpId);
                            if ((clockIn.Emp_Id != 0) && (clockIn.Cmp_Id != 0) && clockIn.file.FileName != null)
                            {
                                string strImage = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + clockIn.Emp_Id + clockIn.file.FileName;
                                string folderName = "/EmpImage/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.ToString("ddMMyyyy") + "/";

                                string path = _webhostenv.WebRootPath + "\\uploads\\" + folderName;
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }
                                using (FileStream filestream = System.IO.File.Create(path + strImage))
                                {
                                    string dsFileName = "";

                                    string strDocPath = path + strImage;
                                    clockIn.file.CopyTo(filestream);
                                    filestream.Flush();
                                }

                                _userService.AddClockIn(clockIn);

                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.ClockInAdded;
                                return Ok(response);
                            }

                            response.code = StatusCodes.Status401Unauthorized;
                            response.status = false;
                            response.message = CommonMessage.TokenExpired;
                            return StatusCode(StatusCodes.Status401Unauthorized, response);
                        }
                    }
                    else
                    {
                        // Handle the case where the token cannot be read as a JWT token
                        response.code = StatusCodes.Status401Unauthorized;
                        response.status = false;
                        response.message = CommonMessage.TokenExpired;
                        return StatusCode(StatusCodes.Status401Unauthorized, response);
                    }
                }
                // Handle the case where the token cannot be read as a JWT token
                response.code = StatusCodes.Status401Unauthorized;
                response.status = false;
                response.message = CommonMessage.TokenExpired;
                return StatusCode(StatusCodes.Status401Unauthorized, response);
            }
            catch (Exception e)
            {
                response.code = StatusCodes.Status500InternalServerError;
                response.status = false;
                response.message = CommonMessage.SomethingWrong + " " + e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        [Route(APIUrls.ClockOut)]
        public IActionResult ClockOut([FromForm] ClockIn clockIn)
        {
            Response response = new Response();
            try
            {
                var authorization = HttpContext.Request.Headers[HeaderNames.Authorization];
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    var jToken = headerValue.Parameter;
                    var handler = new JwtSecurityTokenHandler();

                    var jsonToken = handler.ReadToken(jToken) as JwtSecurityToken;
                    if (jsonToken != null)
                    {
                        var empId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Emp_ID")?.Value;
                        var cmpId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Cmp_ID")?.Value;
                        if (!string.IsNullOrEmpty(empId) && !string.IsNullOrEmpty(cmpId))
                        {
                            clockIn.Emp_Id = Convert.ToInt32(empId);
                            clockIn.Cmp_Id = Convert.ToInt32(cmpId);
                            if ((clockIn.Emp_Id != 0) && (clockIn.Cmp_Id != 0) && clockIn.file.FileName != null)
                            {
                                string strImage = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + clockIn.Emp_Id + clockIn.file.FileName;
                                string folderName = "/EmpImage/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.ToString("ddMMyyyy") + "/";

                                string path = _webhostenv.WebRootPath + "\\uploads\\" + folderName;
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }
                                using (FileStream filestream = System.IO.File.Create(path + strImage))
                                {
                                    string dsFileName = "";

                                    string strDocPath = path + strImage;
                                    clockIn.file.CopyTo(filestream);
                                    filestream.Flush();
                                }

                                _userService.AddClockOut(clockIn);

                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.ClockOutAdded;
                                return Ok(response);
                            }

                            response.code = StatusCodes.Status401Unauthorized;
                            response.status = false;
                            response.message = CommonMessage.TokenExpired;
                            return StatusCode(StatusCodes.Status401Unauthorized, response);
                        }
                    }
                    else
                    {
                        // Handle the case where the token cannot be read as a JWT token
                        response.code = StatusCodes.Status401Unauthorized;
                        response.status = false;
                        response.message = CommonMessage.TokenExpired;
                        return StatusCode(StatusCodes.Status401Unauthorized, response);
                    }
                }
                // Handle the case where the token cannot be read as a JWT token
                response.code = StatusCodes.Status401Unauthorized;
                response.status = false;
                response.message = CommonMessage.TokenExpired;
                return StatusCode(StatusCodes.Status401Unauthorized, response);
            }
            catch (Exception e)
            {
                response.code = StatusCodes.Status500InternalServerError;
                response.status = false;
                response.message = CommonMessage.SomethingWrong + " " + e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        //Customer Requirment API Intigration

        [AllowAnonymous]
        [HttpPost]
        [Route(APIUrls.AddAttendanceData)]
        public IActionResult AddAttendanceData(TransactionRequest transactionRequest)
        {
            Response response = new Response();
            List<TransactionResponse> responseData = new List<TransactionResponse>();
            try
            {
                if (transactionRequest.trans.Count != 0)
                {
                    foreach (var transaction in transactionRequest.trans)
                    {
                        bool checkEnroll = _userService.CheckEnrollNoExixts(transaction);
                        if(checkEnroll)
                        {
                            bool transactionExists = _userService.CheckTransactionExistence(transaction);

                            if (transactionExists)
                            {
                                responseData.Add(new TransactionResponse { txnId = transaction.txnId, status = 1 });
                            }
                            else
                            {
                                _userService.AddTransactionData(transaction);
                                responseData.Add(new TransactionResponse { txnId = transaction.txnId, status = 1 });
                            }
                        }
                        else
                        {
                            responseData.Add(new TransactionResponse { txnId = transaction.txnId, status = 0 });
                        }
                        
                    }
                    response.code = StatusCodes.Status200OK;
                    response.status = true;
                    response.message = CommonMessage.Success;
                    response.data = responseData;
                    return Ok(response);
                }
                response.code = StatusCodes.Status400BadRequest;
                response.status = false;
                response.message = CommonMessage.NoDataFound;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.code = StatusCodes.Status500InternalServerError;
                response.status = false;
                response.message = CommonMessage.SomethingWrong + " " + ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}