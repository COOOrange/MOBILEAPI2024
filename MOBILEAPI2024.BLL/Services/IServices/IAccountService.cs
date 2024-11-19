using MOBILEAPI2024.DTO.RequestDTO.Account;
using MOBILEAPI2024.DTO.ResponseDTO.Account;

namespace MOBILEAPI2024.BLL.Services.IServices
{
    public interface IAccountService
    {
        void AddOtp(ForgotPasswordInfo user, string otp);
        string GenerateOtp();
        LoginResponseDTO AuthenticateUser(LoginDTO loginDTO);
        string GenerateToken(LoginData? loginData, string deviceToken);
        string UpdateToken(LoginResponseDTO authenticateUser, string password);
        ForgotPasswordInfo GetUserByUserName(string userName);
        string OtpVerification(string userName, int otp);
        string RemoveLoginToken(string loginToken);
        string ResetPassword(ForgotPasswordInfo user, ResetPasswordDTO resetPasswordDTO);
        //void SendPushNotificationAsync(string deviceID, string v1, string v2);
        Task SendPushNotificationAsync(string deviceID, string title, string message);
    }
}
