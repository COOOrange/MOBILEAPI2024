using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DTO.Common;
using MOBILEAPI2024.DTO.RequestDTO.Account;

namespace MOBILEAPI2024.API.Controllers
{
    [Authorize]
    [Route("api/v1")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(APIUrls.LoginCheck)]
        public IActionResult SignIn(LoginDTO loginDTO)
        {
            Response response = new Response();
            try
            {
                TryValidateModel(loginDTO);
                if(ModelState.IsValid)
                {
                    if (loginDTO != null)
                    {
                        var authenticateUser = _accountService.AuthenticateUser(loginDTO);
                        if (authenticateUser != null && authenticateUser.LoginData != null && authenticateUser.Details.Count() > 0)
                        {
                            string token = _accountService.GenerateToken(authenticateUser.LoginData, loginDTO.DeviceID);
                            authenticateUser.Token = token;
                            string updatetoken = _accountService.UpdateToken(authenticateUser, loginDTO.Password);
                            response.code = StatusCodes.Status200OK;
                            response.status = true;
                            response.message = CommonMessage.LoginUser;
                            response.data = authenticateUser;
                            _accountService.SendPushNotificationAsync(loginDTO.DeviceID, "Login", "Login Successfully Welcome !!");
                            return Ok(response);
                        }
                    }
                    response.code = StatusCodes.Status400BadRequest;
                    response.status = false;
                    response.message = CommonMessage.InValidUser;
                    return BadRequest(response);
                }
                response.code = StatusCodes.Status400BadRequest;
                response.status = false;
                response.message = CommonMessage.UserNameAndPasswordMandatory;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.code = StatusCodes.Status500InternalServerError;
                response.status = false;
                response.message = CommonMessage.SomethingWrong + ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(APIUrls.ForgotPassword)]
        public IActionResult ForgotPassword(string UserName)
        {
            Response response = new Response();
            try
            {
                if (UserName != null)
                {
                    var user = _accountService.GetUserByUserName(UserName);
                    if (user != null)
                    {
                        string otp = _accountService.GenerateOtp();
                        _accountService.AddOtp(user, otp);
                        response.code = StatusCodes.Status200OK;
                        response.status = true;
                        response.message = CommonMessage.EmailSent;
                        response.data = otp;
                        return Ok(response);
                    }
                    response.code = StatusCodes.Status404NotFound;
                    response.status = false;
                    response.message = CommonMessage.InValidUser;
                    response.data = UserName;
                    return NotFound(response);
                }
                response.code = StatusCodes.Status400BadRequest;
                response.status = false;
                response.message = CommonMessage.NoUserNamePassed;
                return BadRequest(response);

            }
            catch (Exception ex)
            {
                response.code = StatusCodes.Status500InternalServerError;
                response.status = false;
                response.message = CommonMessage.SomethingWrong + ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(APIUrls.OTPVarification)]
        public IActionResult OtpVerification(string UserName, int OtpCode)
        {
            Response response = new Response();
            try
            {
                if (UserName != null)
                {
                    var user = _accountService.OtpVerification(UserName, OtpCode);
                    if (user != null)
                    {
                        response.code = StatusCodes.Status200OK;
                        response.status = true;
                        response.message = CommonMessage.OTPVerified;
                        return Ok(response);
                    }
                    response.code = StatusCodes.Status400BadRequest;
                    response.status = false;
                    response.message = CommonMessage.InValidUser;
                    return NotFound(response);
                }
                response.code = StatusCodes.Status400BadRequest;
                response.status = false;
                response.message = CommonMessage.NoUserNamePassed;
                return BadRequest(response);

            }
            catch (Exception ex)
            {
                response.code = StatusCodes.Status500InternalServerError;
                response.status = false;
                response.message = CommonMessage.SomethingWrong + ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(APIUrls.ChangePassword)]
        public IActionResult ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            Response response = new Response();
            try
            {
                TryValidateModel(resetPasswordDTO);
                if(ModelState.IsValid)
                {
                    var user = _accountService.GetUserByUserName(resetPasswordDTO.UserName);
                    if (user != null)
                    {
                        string resetPassword = _accountService.ResetPassword(user,resetPasswordDTO);
                        if(resetPassword != null)
                        {
                            response.code = StatusCodes.Status200OK;
                            response.status = true;
                            response.message = CommonMessage.PasswordReset;
                            return Ok(response);
                        }
                    }
                    response.code = StatusCodes.Status404NotFound;
                    response.status = false;
                    response.message = CommonMessage.InValidUser;
                    response.data = resetPasswordDTO;
                    return NotFound(response);
                }
                response.code = StatusCodes.Status400BadRequest;
                response.status = false;
                response.message = CommonMessage.InValidUser;
                response.data = resetPasswordDTO;
                return BadRequest(response);

            }
            catch (Exception ex)
            {
                response.code = StatusCodes.Status500InternalServerError;
                response.status = false;
                response.message = CommonMessage.SomethingWrong + ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        [Route(APIUrls.Logout)]
        public IActionResult LogoutUser(string loginToken)
        {
            Response response = new Response();
            try
            {
                if (loginToken != null)
                {
                    string logout = _accountService.RemoveLoginToken(loginToken);
                    if (logout != "Logout Failed")
                    {
                        response.code = StatusCodes.Status200OK;
                        response.status = true;
                        response.message = CommonMessage.Logout;
                        return Ok(response);
                    }
                }
                response.code = StatusCodes.Status400BadRequest;
                response.status = false;
                response.message = CommonMessage.InValidToken;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.code = StatusCodes.Status500InternalServerError;
                response.status = false;
                response.message = CommonMessage.SomethingWrong + ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

    }
}
