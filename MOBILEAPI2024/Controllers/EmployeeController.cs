using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MOBILEAPI2024.BLL.Services;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DTO.Common;
using MOBILEAPI2024.DTO.RequestDTO.Employee;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Reflection.Emit;

namespace MOBILEAPI2024.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route(APIUrls.EmployeeDetails)]
        public IActionResult EmployeeDetails()
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
                        var empCode = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Alpha_Emp_Code")?.Value;
                        if (!string.IsNullOrEmpty(empId) && !string.IsNullOrEmpty(cmpId))
                        {

                            var employeeResponse = _employeeService.EmployeeDetails(Convert.ToInt32(empId), Convert.ToInt32(cmpId), empCode);
                            if (employeeResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = employeeResponse;
                                return Ok(response);
                            }
                            response.code = StatusCodes.Status404NotFound;
                            response.status = false;
                            response.message = CommonMessage.NoDataFound;
                            return StatusCode(StatusCodes.Status404NotFound, response);
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
        [Route(APIUrls.EmployeeDetailsForTally)]
        public IActionResult EmployeeDetailsForTally(string BranchName)
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
                            var employeeResponse = _employeeService.EmployeeDetailsForTally(Convert.ToInt32(cmpId), BranchName);
                            if (employeeResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = employeeResponse;
                                return Ok(response);
                            }
                            response.code = StatusCodes.Status404NotFound;
                            response.status = false;
                            response.message = CommonMessage.NoDataFound;
                            return StatusCode(StatusCodes.Status404NotFound, response);
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
        [Route(APIUrls.EmployeeDirectoryData)]
        public IActionResult EmployeeDirectoryData()
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
                            var employeeResponse = _employeeService.EmployeeDirectoryData(Convert.ToInt32(cmpId));
                            if (employeeResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = employeeResponse;
                                return Ok(response);
                            }
                            response.code = StatusCodes.Status404NotFound;
                            response.status = false;
                            response.message = CommonMessage.NoDataFound;
                            return StatusCode(StatusCodes.Status404NotFound, response);
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
        [Route(APIUrls.EmployeeList)]
        public IActionResult EmployeeList()
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
                            EmployeeListRequest employeeListRequest = new();
                            employeeListRequest.EmpId = Convert.ToInt32(empId);
                            employeeListRequest.CmpId = Convert.ToInt32(cmpId);

                            var employeeResponse = _employeeService.EmployeeList(employeeListRequest);
                            if (employeeResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = employeeResponse;
                                return Ok(response);
                            }
                            response.code = StatusCodes.Status404NotFound;
                            response.status = false;
                            response.message = CommonMessage.NoDataFound;
                            return StatusCode(StatusCodes.Status404NotFound, response);
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
        [Route(APIUrls.ManagerApprovalDetails)]
        public IActionResult ManagerApprovalDetails(ManagerApprovalDetailsRequest managerApprovalDetailsRequest)
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
                            managerApprovalDetailsRequest.EmpId = Convert.ToInt32(empId);
                            managerApprovalDetailsRequest.CmpId = Convert.ToInt32(cmpId);

                            var employeeResponse = _employeeService.ManagerApprovalDetails(managerApprovalDetailsRequest);
                            if (employeeResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = employeeResponse;
                                return Ok(response);
                            }
                            response.code = StatusCodes.Status404NotFound;
                            response.status = false;
                            response.message = CommonMessage.NoDataFound;
                            return StatusCode(StatusCodes.Status404NotFound, response);
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
        [Route(APIUrls.MyTeamAttendance)]
        public IActionResult MyTeamAttendance()
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
                            var employeeResponse = _employeeService.MyTeamAttendance(Convert.ToInt32(empId), Convert.ToInt32(cmpId));
                            if (employeeResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = employeeResponse;
                                return Ok(response);
                            }
                            response.code = StatusCodes.Status404NotFound;
                            response.status = false;
                            response.message = CommonMessage.NoDataFound;
                            return StatusCode(StatusCodes.Status404NotFound, response);
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
        [Route(APIUrls.MyTeamAttendanceInsert)]
        public IActionResult MyTeamAttendanceInsert(MyTeamAttendanceInsertRequest myTeamAttendanceInsertRequest)
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
                            myTeamAttendanceInsertRequest.EmpId = Convert.ToInt32(empId);
                            myTeamAttendanceInsertRequest.CmpId = Convert.ToInt32(cmpId);
                            var employeeResponse = _employeeService.MyTeamAttendanceInsert(myTeamAttendanceInsertRequest);
                            if (employeeResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = employeeResponse;
                                return Ok(response);
                            }
                            response.code = StatusCodes.Status404NotFound;
                            response.status = false;
                            response.message = CommonMessage.NoDataFound;
                            return StatusCode(StatusCodes.Status404NotFound, response);
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
        [Route(APIUrls.MyTeamDetails)]
        public IActionResult MyTeamDetails(string Status)
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

                            var employeeResponse = _employeeService.MyTeamDetails(Convert.ToInt32(empId), Convert.ToInt32(cmpId), Status);
                            if (employeeResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = employeeResponse;
                                return Ok(response);
                            }
                            response.code = StatusCodes.Status404NotFound;
                            response.status = false;
                            response.message = CommonMessage.NoDataFound;
                            return StatusCode(StatusCodes.Status404NotFound, response);
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
        [Route(APIUrls.NewJoiningEmployeeDetails)]
        public IActionResult NewJoiningEmployeeDetails()
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
                            var employeeResponse = _employeeService.NewJoiningEmployeeDetails(Convert.ToInt32(cmpId));
                            if (employeeResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = employeeResponse;
                                return Ok(response);
                            }
                            response.code = StatusCodes.Status404NotFound;
                            response.status = false;
                            response.message = CommonMessage.NoDataFound;
                            return StatusCode(StatusCodes.Status404NotFound, response);
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
        [Route(APIUrls.UpdateEmpFavDetails)]
        public IActionResult UpdateEmpFavDetails(UpdateEmpFavDetailsRequest updateEmpFavDetailsRequest)
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
                            updateEmpFavDetailsRequest.EmpID = Convert.ToInt32(empId);
                            updateEmpFavDetailsRequest.CmpID = Convert.ToInt32(cmpId);

                            var employeeResponse = _employeeService.UpdateEmpFavDetails(updateEmpFavDetailsRequest);
                            if (employeeResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = employeeResponse;
                                return Ok(response);
                            }
                            response.code = StatusCodes.Status404NotFound;
                            response.status = false;
                            response.message = CommonMessage.NoDataFound;
                            return StatusCode(StatusCodes.Status404NotFound, response);
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
        [Route(APIUrls.UpdateEmployeeDetails)]
        public IActionResult UpdateEmployeeDetails(UpdateEmployeeDetailsRequest updateEmployeeDetailsRequest)
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
                            updateEmployeeDetailsRequest.EmpID = Convert.ToInt32(empId);
                            updateEmployeeDetailsRequest.CmpID = Convert.ToInt32(cmpId);

                            var employeeResponse = _employeeService.UpdateEmployeeDetails(updateEmployeeDetailsRequest);
                            if (employeeResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = employeeResponse;
                                return Ok(response);
                            }
                            response.code = StatusCodes.Status404NotFound;
                            response.status = false;
                            response.message = CommonMessage.NoDataFound;
                            return StatusCode(StatusCodes.Status404NotFound, response);
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
