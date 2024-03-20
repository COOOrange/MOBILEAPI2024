using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MOBILEAPI2024.BLL.Services;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DTO.Common;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace MOBILEAPI2024.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveService _leaveService;

        public LeaveController(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        [HttpPost]
        [Route(APIUrls.LeaveBalance)]
        public IActionResult FilterLeaveBalance(LeaveBalanceRequest leaveBalanceRequest)
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
                            LeaveFilter leaveFilter = new LeaveFilter();
                            leaveFilter.FromDate = new DateTime(leaveBalanceRequest.Year, leaveBalanceRequest.Month, 1);
                            leaveFilter.ToDate = leaveFilter.FromDate.AddMonths(1).AddDays(-1);
                            leaveFilter.Emp_Id = Convert.ToInt32(empId);
                            leaveFilter.Cmp_Id = Convert.ToInt32(cmpId);

                            var leavebalance = _leaveService.GetLeaveBalance(leaveFilter);

                            response.code = StatusCodes.Status200OK;
                            response.status = true;
                            response.message = CommonMessage.Success;
                            response.data = leavebalance;
                            return Ok(response);
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
        [Route(APIUrls.FilterLeaveStatus)]
        public IActionResult FilterLeaveStatus(LeaveBalanceRequest leaveBalanceRequest)
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
                            LeaveFilter leaveFilter = new LeaveFilter();
                            leaveFilter.FromDate = new DateTime(leaveBalanceRequest.Year, leaveBalanceRequest.Month, 1);
                            leaveFilter.ToDate = leaveFilter.FromDate.AddMonths(1).AddDays(-1);
                            leaveFilter.Emp_Id = Convert.ToInt32(empId);
                            leaveFilter.Cmp_Id = Convert.ToInt32(cmpId);

                            var leavebalance = _leaveService.GetLeaveStatus(leaveFilter);

                            response.code = StatusCodes.Status200OK;
                            response.status = true;
                            response.message = CommonMessage.Success;
                            response.data = leavebalance;
                            return Ok(response);
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

        [HttpGet]
        [Route(APIUrls.AllLeaveBalance)]
        public IActionResult LeaveBalance()
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
                            DateTime currentDate = DateTime.Now;
                            int currentYear = currentDate.Year;
                            // Get the current month
                            int currentMonth = currentDate.Month;
                            LeaveFilter leaveFilter = new LeaveFilter();
                            leaveFilter.FromDate = new DateTime(currentYear, currentMonth, 1);
                            leaveFilter.ToDate = leaveFilter.FromDate.AddMonths(1).AddDays(-1);
                            leaveFilter.Emp_Id = Convert.ToInt32(empId);
                            leaveFilter.Cmp_Id = Convert.ToInt32(cmpId);

                            var leavebalance = _leaveService.GetLeaveBalance(leaveFilter);

                            response.code = StatusCodes.Status200OK;
                            response.status = true;
                            response.message = CommonMessage.Success;
                            response.data = leavebalance;
                            return Ok(response);
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

        [HttpGet]
        [Route(APIUrls.GetLeaveApplicationRecords)]
        public IActionResult GetLeaveApplicationRecords()
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
                            DateTime currentDate = DateTime.Now;
                            int currentYear = currentDate.Year;
                            // Get the current month
                            int currentMonth = currentDate.Month;
                            LeaveFilter leaveFilter = new LeaveFilter();
                            leaveFilter.FromDate = new DateTime(currentYear, currentMonth, 1);
                            leaveFilter.ToDate = leaveFilter.FromDate.AddMonths(1).AddDays(-1);
                            leaveFilter.Emp_Id = Convert.ToInt32(empId);
                            leaveFilter.Cmp_Id = Convert.ToInt32(cmpId);

                            var leavebalance = _leaveService.GetLeaveStatus(leaveFilter);

                            response.code = StatusCodes.Status200OK;
                            response.status = true;
                            response.message = CommonMessage.Success;
                            response.data = leavebalance;
                            return Ok(response);
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

        [HttpGet]
        [Route(APIUrls.GetLeaveRecords)]
        public IActionResult LeaveTypeBind()
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
                            LeaveFilter leaveFilter = new LeaveFilter();
                            leaveFilter.Emp_Id = Convert.ToInt32(empId);
                            leaveFilter.Cmp_Id = Convert.ToInt32(cmpId);

                            var leavetypebind = _leaveService.GetLeaveTypeBind(leaveFilter);

                            response.code = StatusCodes.Status200OK;
                            response.status = true;
                            response.message = CommonMessage.Success;
                            response.data = leavetypebind;
                            return Ok(response);
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
        [Route(APIUrls.CheckPeriod)]
        public IActionResult CheckPeriod(CheckPeriod checkPeriod)
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
                            LeaveFilter leaveFilter = new LeaveFilter();
                            leaveFilter.FromDate = checkPeriod.Fromdate;
                            leaveFilter.ToDate = leaveFilter.FromDate.AddMonths(1).AddDays(-1);
                            leaveFilter.Emp_Id = Convert.ToInt32(empId);
                            leaveFilter.Cmp_Id = Convert.ToInt32(cmpId);

                            var checkPerioddata = _leaveService.CheckPeriod(leaveFilter, checkPeriod);

                            response.code = StatusCodes.Status200OK;
                            response.status = true;
                            response.message = CommonMessage.Success;
                            response.data = checkPerioddata;
                            return Ok(response);
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
        [Route(APIUrls.LeaveApplication)]
        public IActionResult LeaveApplication(ApplyLeaveRequest applyLeaveRequest)
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
                        var loginId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Login_ID")?.Value;
                        if (!string.IsNullOrEmpty(empId) && !string.IsNullOrEmpty(cmpId) && !string.IsNullOrEmpty(loginId))
                        {
                            LeaveFilter leaveFilter = new LeaveFilter();
                            leaveFilter.Emp_Id = Convert.ToInt32(empId);
                            leaveFilter.Cmp_Id = Convert.ToInt32(cmpId);
                            leaveFilter.Login_ID = Convert.ToInt32(loginId);

                            string addleaveAplication = _leaveService.AddLeaveAplication(leaveFilter, applyLeaveRequest);

                            response.code = StatusCodes.Status200OK;
                            response.status = true;
                            response.message = CommonMessage.Success;
                            response.data = addleaveAplication;
                            return Ok(response);
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
        [Route(APIUrls.LeaveApplicationDetails)]
        public IActionResult LeaveApplicationDetails(int leaveAppID)
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
                            LeaveFilter leaveFilter = new LeaveFilter();
                            leaveFilter.Emp_Id = Convert.ToInt32(empId);
                            leaveFilter.Cmp_Id = Convert.ToInt32(cmpId);

                            var checkLeaveStatus = _leaveService.CheckLeaveStatus(leaveFilter, leaveAppID);

                            response.code = StatusCodes.Status200OK;
                            response.status = true;
                            response.message = CommonMessage.Success;
                            response.data = checkLeaveStatus;
                            return Ok(response);
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
        [Route(APIUrls.LeaveApprovalDetails)]
        public IActionResult LeaveApprovalDetails(LeaveApprovalDetailsRequest leaveApprovalDetailsRequest)
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
                        var loginId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Login_ID")?.Value;
                        if (!string.IsNullOrEmpty(empId) && !string.IsNullOrEmpty(cmpId))
                        {

                            leaveApprovalDetailsRequest.EmpId = Convert.ToInt32(empId);
                            leaveApprovalDetailsRequest.CmpId = Convert.ToInt32(cmpId);
                            leaveApprovalDetailsRequest.LoginId = Convert.ToInt32(loginId);

                            var leaveApprovalDetails = _leaveService.LeaveApprovalDetails(leaveApprovalDetailsRequest);
                            if(leaveApprovalDetails != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = leaveApprovalDetails;
                                return Ok(response);
                            }

                            response.code = StatusCodes.Status401Unauthorized;
                            response.status = false;
                            response.message = CommonMessage.NoDataFound;
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
        [Route(APIUrls.LeaveApplicationRecords)]
        public IActionResult LeaveApplicationRecords(LeaveApplicationRecordsRequest leaveApplicationRecordsRequest)
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

                            leaveApplicationRecordsRequest.EmpId = Convert.ToInt32(empId);
                            leaveApplicationRecordsRequest.CmpId = Convert.ToInt32(cmpId);

                            var leaveApplicationRecords = _leaveService.LeaveApplicationRecords(leaveApplicationRecordsRequest);
                            if (leaveApplicationRecords.CancellationResponses != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = leaveApplicationRecords.CancellationResponses;
                                return Ok(response);
                            }
                            else if (leaveApplicationRecords.ApplicationResponses != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = leaveApplicationRecords.ApplicationResponses;
                                return Ok(response);
                            }

                            response.code = StatusCodes.Status401Unauthorized;
                            response.status = false;
                            response.message = CommonMessage.NoDataFound;
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
        [Route(APIUrls.LeaveApproval)]
        public IActionResult LeaveApproval(LeaveApprovalRequest leaveApprovalRequest)
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
                        var loginId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Login_ID")?.Value;
                        if (!string.IsNullOrEmpty(empId) && !string.IsNullOrEmpty(cmpId))
                        {

                            leaveApprovalRequest.EmpID = Convert.ToInt32(empId);
                            leaveApprovalRequest.CmpID = Convert.ToInt32(cmpId);
                            leaveApprovalRequest.LoginID = Convert.ToInt32(loginId);

                            var leaveApproval = _leaveService.LeaveApproval(leaveApprovalRequest);
                            if (leaveApproval != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = leaveApproval;
                                return Ok(response);
                            }

                            response.code = StatusCodes.Status401Unauthorized;
                            response.status = false;
                            response.message = CommonMessage.NoDataFound;
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
        [Route(APIUrls.LeaveApprovalDelete)]
        public IActionResult LeaveApprovalDelete(LeaveApprovalDeleteRequest leaveApprovalDeleteRequest)
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
                        var loginId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Login_ID")?.Value;
                        if (!string.IsNullOrEmpty(empId) && !string.IsNullOrEmpty(cmpId))
                        {

                            leaveApprovalDeleteRequest.EmpID = Convert.ToInt32(empId);
                            leaveApprovalDeleteRequest.CmpID = Convert.ToInt32(cmpId);
                            leaveApprovalDeleteRequest.LoginID = Convert.ToInt32(loginId);

                            var leaveApprovalDelete = _leaveService.LeaveApprovalDelete(leaveApprovalDeleteRequest);
                            if (leaveApprovalDelete != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = leaveApprovalDelete;
                                return Ok(response);
                            }

                            response.code = StatusCodes.Status401Unauthorized;
                            response.status = false;
                            response.message = CommonMessage.NoDataFound;
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
        [Route(APIUrls.LeaveBalanceSummary)]
        public IActionResult LeaveBalanceSummary(DateTime ForDate)
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
                            LeaveFilter leaveFilter = new();
                            leaveFilter.Emp_Id = Convert.ToInt32(empId);
                            leaveFilter.Cmp_Id = Convert.ToInt32(cmpId);

                            var leaveBalanceSummry = _leaveService.LeaveBalanceSummary(leaveFilter,ForDate);
                            if (leaveBalanceSummry.leaveBalanceSummries != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = leaveBalanceSummry.leaveBalanceSummries;
                                return Ok(response);
                            }

                            response.code = StatusCodes.Status401Unauthorized;
                            response.status = false;
                            response.message = CommonMessage.NoDataFound;
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
        [Route(APIUrls.LeaveCancellationApplication)]
        public IActionResult LeaveCancellationApplication(LeaveCancellationApplicationRequest leaveCancellationApplicationRequest)
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
                        var loginId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Login_ID")?.Value;
                        if (!string.IsNullOrEmpty(empId) && !string.IsNullOrEmpty(cmpId))
                        {
                            leaveCancellationApplicationRequest.EmpID = Convert.ToInt32(empId);
                            leaveCancellationApplicationRequest.CmpID = Convert.ToInt32(cmpId);
                            leaveCancellationApplicationRequest.LoginID = Convert.ToInt32(loginId);

                            var leaveCancellationRequest = _leaveService.LeaveCancellationApplication(leaveCancellationApplicationRequest);
                            if (leaveCancellationRequest != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = leaveCancellationRequest;
                                return Ok(response);
                            }

                            response.code = StatusCodes.Status401Unauthorized;
                            response.status = false;
                            response.message = CommonMessage.NoDataFound;
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
        [Route(APIUrls.LeaveCancellationApplicationDetails)]
        public IActionResult LeaveCancellationApplicationDetails(LeaveCancellationApplicationDetailsRequest leaveCancellationApplicationDetailsRequest)
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
                            leaveCancellationApplicationDetailsRequest.EmpID = Convert.ToInt32(empId);
                            leaveCancellationApplicationDetailsRequest.CmpID = Convert.ToInt32(cmpId);

                            var leaveCancellationApplicationDetails = _leaveService.LeaveCancellationApplicationDetails(leaveCancellationApplicationDetailsRequest);
                            if (leaveCancellationApplicationDetails.LeaveCancellationApplicationDetails != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = leaveCancellationApplicationDetails.LeaveCancellationApplicationDetails;
                                return Ok(response);
                            }

                            response.code = StatusCodes.Status401Unauthorized;
                            response.status = false;
                            response.message = CommonMessage.NoDataFound;
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

    }
}
