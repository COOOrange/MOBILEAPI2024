using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DTO.Common;
using MOBILEAPI2024.DTO.RequestDTO.CompOff;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace MOBILEAPI2024.API.Controllers
{
    [Authorize]
    [Route("api/v1")]
    [ApiController]
    public class CompOffController : ControllerBase
    {
        private readonly ICompOffService _compOffService;
        public CompOffController(ICompOffService compOffService)
        {
            _compOffService = compOffService;
        }

        [HttpPost]
        [Route(APIUrls.CompOffApplication)]
        public IActionResult CompOffApplication(CompOffApplicationRequest compOffApplicationRequest)
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
                            compOffApplicationRequest.EmpID = Convert.ToInt32(empId);
                            compOffApplicationRequest.CmpID = Convert.ToInt32(cmpId);
                            compOffApplicationRequest.LoginID = Convert.ToInt32(loginId);

                            var compOffResponse = _compOffService.CompOffApplication(compOffApplicationRequest);
                            if (compOffResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = compOffResponse;
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
        [Route(APIUrls.CompOffApplicationDelete)]
        public IActionResult CompOffApplicationDelete(int CompoffAppID)
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
                            var compOffResponse = _compOffService.CompOffApplicationDelete(Convert.ToInt32(cmpId),Convert.ToInt32(loginId),CompoffAppID);
                            if (compOffResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = compOffResponse;
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
        [Route(APIUrls.CompOffApproval)]
        public IActionResult CompOffApproval(CompOffApprovalRequest compOffApprovalRequest)
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
                            compOffApprovalRequest.CmpID = Convert.ToInt32(cmpId);
                            compOffApprovalRequest.EmpID = Convert.ToInt32(empId);
                            compOffApprovalRequest.LoginID = Convert.ToInt32(loginId);

                            var compOffResponse = _compOffService.CompOffApproval(compOffApprovalRequest);
                            if (compOffResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = compOffResponse;
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
        [Route(APIUrls.CompOffApprovalDelete)]
        public IActionResult CompOffApprovalDelete(int CompoffAppID)
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
                            var compOffResponse = _compOffService.CompOffApprovalDelete(Convert.ToInt32(cmpId), CompoffAppID);
                            if (compOffResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = compOffResponse;
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
        [Route(APIUrls.GetCompOffApplicationDetails)]
        public IActionResult GetCompOffApplicationDetails(int CompoffAppID)
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
                            var compOffResponse = _compOffService.GetCompOffApplicationDetails(CompoffAppID);
                            if (compOffResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = compOffResponse;
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
        [Route(APIUrls.GetCompOffApplicationStatus)]
        public IActionResult GetCompOffApplicationStatus(LeaveFilter getCompOffApplicationStatus)
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
                            getCompOffApplicationStatus.Cmp_Id = Convert.ToInt32(cmpId);
                            getCompOffApplicationStatus.Emp_Id = Convert.ToInt32(empId);

                            var compOffResponse = _compOffService.GetCompOffApplicationStatus(getCompOffApplicationStatus);
                            if (compOffResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = compOffResponse;
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
        [Route(APIUrls.GetCompoffApplicationRecord)]
        public IActionResult GetCompoffApplicationRecord(string StrType)
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

                            var compOffResponse = _compOffService.GetCompoffApplicationRecord(Convert.ToInt32(cmpId), Convert.ToInt32(empId), StrType);
                            if (compOffResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = compOffResponse;
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
