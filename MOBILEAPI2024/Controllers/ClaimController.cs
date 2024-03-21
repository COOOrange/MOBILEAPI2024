using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DTO.Common;
using MOBILEAPI2024.DTO.RequestDTO.Claim;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace MOBILEAPI2024.API.Controllers
{
    [Authorize]
    [Route("api/v1")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _claimService;
        public ClaimController(IClaimService claimService) 
        { 
            _claimService = claimService;
        }

        [HttpGet]
        [Route(APIUrls.ClaimAdminSetting)]
        public IActionResult ClaimAdminSetting()
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
                        var cmpId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Cmp_ID")?.Value;
                        if (!string.IsNullOrEmpty(cmpId))
                        {
                            
                            var leavebalance = _claimService.ClaimAdminSetting(Convert.ToInt32(cmpId));

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
        [Route(APIUrls.ClaimAppDetails)]
        public IActionResult ClaimAppDetails(ClaimAppDetailsRequest claimAppDetailsRequest)
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
                        var cmpId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Cmp_ID")?.Value;
                        if (!string.IsNullOrEmpty(cmpId))
                        {
                            claimAppDetailsRequest.CmpID = Convert.ToInt32(cmpId);
                            var claimAppDetails = _claimService.ClaimAppDetails(claimAppDetailsRequest);
                            if(claimAppDetails != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = claimAppDetails;
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
        [Route(APIUrls.ClaimApplication)]
        public IActionResult ClaimApplication(ClaimApplicationRequest claimApplicationRequest)
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
                        var cmpId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Cmp_ID")?.Value;
                        var empId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Emp_ID")?.Value;
                        var loginId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Login_ID")?.Value;
                        if (!string.IsNullOrEmpty(cmpId))
                        {
                            claimApplicationRequest.CmpID = Convert.ToInt32(cmpId);
                            claimApplicationRequest.EmpID = Convert.ToInt32(empId);
                            claimApplicationRequest.LoginID = Convert.ToInt32(loginId);
                            var claimAppDetails = _claimService.ClaimApplication(claimApplicationRequest);
                            if (claimAppDetails != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = claimAppDetails;
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
        [Route(APIUrls.ClaimApplicationDelete)]
        public IActionResult ClaimApplicationDelete(ClaimApplicationDeleteRequest claimApplicationDeleteRequest)
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
                        var cmpId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Cmp_ID")?.Value;
                        var empId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Emp_ID")?.Value;
                        var loginId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Login_ID")?.Value;
                        if (!string.IsNullOrEmpty(cmpId))
                        {
                            claimApplicationDeleteRequest.CmpID = Convert.ToInt32(cmpId);
                            var claimResponse = _claimService.ClaimApplicationDelete(claimApplicationDeleteRequest);
                            if (claimResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = claimResponse;
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
        [Route(APIUrls.ClaimApplicationDetails)]
        public IActionResult ClaimApplicationDetails(ClaimApplicationDetailsRequest claimApplicationDetailsRequest)
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
                        var cmpId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Cmp_ID")?.Value;
                        var empId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Emp_ID")?.Value;
                        var loginId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Login_ID")?.Value;
                        if (!string.IsNullOrEmpty(cmpId))
                        {
                            claimApplicationDetailsRequest.CmpID = Convert.ToInt32(cmpId);
                            claimApplicationDetailsRequest.EmpID = Convert.ToInt32(empId);
                            var claimResponse = _claimService.ClaimApplicationDetails(claimApplicationDetailsRequest);
                            if (claimResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = claimResponse;
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
        [Route(APIUrls.ClaimApplicationRecords)]
        public IActionResult ClaimApplicationRecords(ClaimApplicationRecordsRequest claimApplicationRecordsRequest)
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
                        var cmpId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Cmp_ID")?.Value;
                        var empId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Emp_ID")?.Value;
                        var loginId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Login_ID")?.Value;
                        if (!string.IsNullOrEmpty(cmpId))
                        {
                            claimApplicationRecordsRequest.CmpID = Convert.ToInt32(cmpId);
                            claimApplicationRecordsRequest.EmpID = Convert.ToInt32(empId);
                            var claimResponse = _claimService.ClaimApplicationRecords(claimApplicationRecordsRequest);
                            if (claimResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = claimResponse;
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
        [Route(APIUrls.ClaimApplicationStatus)]
        public IActionResult ClaimApplicationStatus(ClaimApplicationStatusRequest claimApplicationStatusRequest)
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
                        var cmpId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Cmp_ID")?.Value;
                        var empId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Emp_ID")?.Value;
                        var loginId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Login_ID")?.Value;
                        if (!string.IsNullOrEmpty(cmpId))
                        {
                            claimApplicationStatusRequest.CmpID = Convert.ToInt32(cmpId);
                            claimApplicationStatusRequest.EmpID = Convert.ToInt32(empId);
                            var claimResponse = _claimService.ClaimApplicationStatus(claimApplicationStatusRequest);
                            if (claimResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = claimResponse;
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
        [Route(APIUrls.ClaimApprovalDetailRecords)]
        public IActionResult ClaimApprovalDetailRecords(int Claim_App_ID)
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
                        var cmpId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Cmp_ID")?.Value;
                        var empId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Emp_ID")?.Value;
                        var loginId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "Login_ID")?.Value;
                        if (!string.IsNullOrEmpty(cmpId))
                        {
                            var claimResponse = _claimService.ClaimApprovalDetailRecords(Claim_App_ID);
                            if (claimResponse != null)
                            {
                                response.code = StatusCodes.Status200OK;
                                response.status = true;
                                response.message = CommonMessage.Success;
                                response.data = claimResponse;
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
