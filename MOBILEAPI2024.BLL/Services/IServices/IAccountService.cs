using MOBILEAPI2024.DTO.RequestDTO.Account;
using MOBILEAPI2024.DTO.ResponseDTO.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.BLL.Services.IServices
{
    public interface IAccountService
    {
        void AddOtp(ForgotPasswordInfo user, string otp);
        LoginResponseDTO AuthenticateUser(LoginDTO loginDTO);
        string GenerateOtp();
        string GenerateToken(LoginData? loginData);
        ForgotPasswordInfo GetUserByUserName(string userName);
        string OtpVerification(string userName,int otp);
        string RemoveLoginToken(string loginToken);
        string ResetPassword(ForgotPasswordInfo user, ResetPasswordDTO resetPasswordDTO);
        string UpdateToken(LoginResponseDTO authenticateUser,string password);
    }
}
