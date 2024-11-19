using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.Common;
using MOBILEAPI2024.DTO.RequestDTO.Account;
using MOBILEAPI2024.DTO.ResponseDTO.Account;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Builder.Extensions;
using Google.Apis.Auth.OAuth2;

namespace MOBILEAPI2024.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailHelper;
        private IHostingEnvironment _environment;
        public AccountService(IEmailService emailHelper, IHostingEnvironment environment, IAccountRepository accountRepository, IOptions<AppSettings> appSettings)
        {
            _accountRepository = accountRepository;
            _appSettings = appSettings.Value;
            _emailHelper = emailHelper;
            _environment = environment;
            if (FirebaseApp.DefaultInstance == null)
            {
                // Initialize Firebase only if it hasn't been initialized
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(_appSettings.Firebase_Path)
                });
            }
        }

        public void AddOtp(ForgotPasswordInfo user, string otp)
        {
            _accountRepository.AddOtp(user, otp);

            // Send email 
            var vSubject = "Forgot Password";
            string path = Path.Combine(_environment.WebRootPath, "EmailTemplates/Otp.html");
            using var vStreamReader = System.IO.File.OpenText(path);
            var vTemplate = vStreamReader.ReadToEnd();
            vTemplate = vTemplate.Replace("{PageTitle}", vSubject);
            vTemplate = vTemplate.Replace("{OTP}", otp);
            vTemplate = vTemplate.Replace("{Name}", user.Emp_Full_Name);
            _emailHelper.SendEmail(user.Emp_Full_Name, user.Work_Email, vSubject, vTemplate);
        }

        public LoginResponseDTO AuthenticateUser(LoginDTO loginDTO)
        {
            LoginResponseDTO loginResponseDTO = _accountRepository.LoginCheck(loginDTO);
            if (loginResponseDTO.LoginData != null)
            {
                loginResponseDTO.LoginData.Image_Name = _appSettings.ImagePath + "App_File/EMPIMAGES/" + loginResponseDTO.LoginData.Image_Name; ;
                loginResponseDTO.LoginData.Cmp_Logo = _appSettings.ImagePath + "App_File/CMPIMAGES/" + loginResponseDTO.LoginData.Cmp_Logo;

                return loginResponseDTO;
            }
            return null;
        }

        public string GenerateOtp()
        {
            // Create a random number generator
            Random rand = new Random();

            // Generate a random 4-digit number
            int otp = rand.Next(1000, 10000);

            // Convert the number to a string and return
            return otp.ToString();
        }

        public string GenerateToken(LoginData validatedUser, string deviceToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.JWTTokenGenKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Login_ID", Convert.ToString(validatedUser.Login_ID.ToString())),
                    new Claim("Cmp_ID", Convert.ToString(validatedUser.Cmp_ID.ToString())),
                    new Claim("Emp_ID", Convert.ToString(validatedUser.Emp_ID.ToString())),
                    new Claim("Dept_ID", Convert.ToString(validatedUser.DEPT_Id.ToString())),
                    new Claim("Alpha_Emp_Code", Convert.ToString(validatedUser.Alpha_Emp_Code.ToString())),
                    new Claim("Emp_Full_Name", validatedUser.Emp_Full_Name.ToString()),
                    new Claim("Dept_Name", validatedUser.Dept_Name.ToString()),
                    new Claim("DesigName", validatedUser.Desig_Name.ToString()),
                    new Claim("DeviceToken", deviceToken.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ForgotPasswordInfo GetUserByUserName(string userName)
        {
            var user = _accountRepository.GetUserByUserName(userName);
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public string OtpVerification(string userName, int otp)
        {
            var user = _accountRepository.GetUserByUserName(userName);
            if (user != null)
            {
                string verifyOtp = _accountRepository.OtpVerification(user, otp);
                return verifyOtp;

            }
            return null;
        }

        public string RemoveLoginToken(string loginToken)
        {
            string removeToken = _accountRepository.RemoveLoginToken(loginToken);
            if (removeToken == "Logout Successfully")
            {
                return removeToken;
            }
            return null;
        }

        public string ResetPassword(ForgotPasswordInfo user, ResetPasswordDTO resetPasswordDTO)
        {
            _accountRepository.ResetPassword(user, resetPasswordDTO);

            return "Success.";
        }

        public async Task SendPushNotificationAsync(string deviceID, string title, string message)
        {
            try
            {
                // Create the message to be sent
                var messageToSend = new Message
                {
                    Token = deviceID, // Device token (registration token)
                    Notification = new Notification
                    {
                        Title = title,   // Notification title
                        Body = message   // Notification message
                    }
                };

                // Send the message using Firebase Cloud Messaging
                string response = await FirebaseMessaging.DefaultInstance.SendAsync(messageToSend);
                Console.WriteLine("Successfully sent message: " + response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending notification: " + ex.Message);
            }
        }

        public string UpdateToken(LoginResponseDTO authenticateUser, string Password)
        {
            string updateToken = _accountRepository.UpdateToken(authenticateUser, Password);
            if (updateToken != null)
            {
                return updateToken;
            }
            return null;
        }
    }
}
