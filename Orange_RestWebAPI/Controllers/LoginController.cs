


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
using Microsoft.AspNetCore.Session;


namespace OrangePayrollAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private IConfiguration _config;
        MyClassToken<LoginModel> response = new MyClassToken<LoginModel>();
        MyClass<LoginModel> singleResponse = new MyClass<LoginModel>();
        LogHelper logHelper = new LogHelper();
        //private IHttpContextAccessor _accessor;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        //public LoginController(IHttpContextAccessor accessor)
        //{
        //    _accessor = accessor;
        //}
        private string GenerateJSONWebToken(LoginModel userInfo)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                  _config["Jwt:Issuer"],
                  null,
                  expires: DateTime.Now.AddMinutes(5),
                  signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                logHelper.Error("GenerateJSONWebToken : " + ex.Message);
                throw;
            }
        }

        #region AuthenticateUser  
        /// <summary>  
        /// Hardcoded the User authentication  
        /// </summary>  
        /// 
        /// 
        /// <param name="login"></param>  
        /// <returns></returns>  
        private MyClassToken<LoginModel> AuthenticateUser(LoginModel login)
        {
            try
            {
                DataSet dsLogin = null;
                
                LoginDetails ObjDashboard = new LoginDetails(_config);

                //Validate the User Credentials  
                ObjDashboard.LoginCheck(login, ref dsLogin);

                if (dsLogin.Tables.Count > 0 && dsLogin != null)
                {
                    //ObjDashboard.LoginUpdateToken(login, ref dsLogin);

                    //If dsLogin.Tables(0).Rows(0)(0).ToString().Contains("OK") Then
                    if (dsLogin.Tables[0].Rows[0][0].ToString().Contains("OK"))
                    {
                        //response.Status = false;
                        response.StatusCode = 401;
                        response.msg = dsLogin.Tables[0].Rows[0][0].ToString().Split(":")[1]; //dsLogin.Tables(0).Rows(0)(0).ToString().Split(":")(1)
                                                                                              //response.data = Array.Empty<string>(); // Changed by Niraj(02022022)
                        response.data = null;
                    }
                    else
                    {
                        //response.Status = true;
                        response.StatusCode = 200;
                        //set the url to images as per live link set in appsettings file
                        dsLogin.Tables[0].Rows[0]["Image_Name"] = _config["DBConfig:ImagePath"].ToString() + "App_File/EMPIMAGES/" + dsLogin.Tables[0].Rows[0]["Image_Name"];
                        dsLogin.Tables[0].Rows[0]["Cmp_Logo"] = _config["DBConfig:ImagePath"].ToString() + "App_File/CMPIMAGES/" + dsLogin.Tables[0].Rows[0]["Cmp_Logo"];
                        var LoginDetails = CommonClass.ToJson(dsLogin.Tables[0]);
                        var PrivilegeDetails = CommonClass.ToJson(dsLogin.Tables[1]);
                        //response.data = CommonClass.ToJson(dsLogin.Tables[0]);  //dtLogin.Tables[0].AsEnumerable();
                        response.data = LoginDetails;
                        response.details = PrivilegeDetails;
                        response.msg = "Login Successfully.";


                    }
                }
                else
                {
                    response.msg = "You Can not Access, Contact to Administrator.";
                }
                return response;
                //return Ok(response.data);
            }
            catch (Exception ex)
            {

                response.StatusCode = 500;
                response.msg = "Some thing went wrong.";
                response.data = "";

                logHelper.Error("AuthenticateUser : " + ex.Message);
                throw;
            }


        }
        #endregion

        #region Login Validation  
        /// <summary>  
        /// Login Authenticaton using JWT Token Authentication  
        /// </summary>  
        /// <param name="data"></param>  
        /// <returns></returns>  
        //[AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginModel data)
        {
            try
            {
                IActionResult response = Unauthorized(new { message = "UnAuthorized", StatusCode = 401 });
                DataSet dsLogin = null;

                LoginDetails ObjLogin = new LoginDetails(_config);
                string connection = _config["DBConfig:ConnectionString"];

                if (connection == "" || connection == null)
                {
                    response = NotFound(new { status = false, Msg = "Database connection string not found.", StatusCode = 404, data = "", token = "" });
                }
                else
                {
                    var user = AuthenticateUser(data);

                    if (user != null)
                    {
                        var tokenString = GenerateJSONWebToken(data);
                        ObjLogin.LoginUpdateToken(data, tokenString, ref dsLogin);
                        if (user.StatusCode == 200)
                        {
                            user.token = tokenString;
                            response = Ok(user);
                        }
                        else
                        {
                            user.token = "";
                            response = Ok(user);
                        }
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                logHelper.Error("Login : " + ex.Message);
                throw;
            }
        }
        #endregion

        //#region Get  
        ///// <summary>  
        ///// Authorize the Method  
        ///// </summary>  
        ///// <returns></returns>  
        //[HttpGet(nameof(Get))]
        //public async Task<IEnumerable<string>> Get()
        //{
        //    try
        //    {
        //        var accessToken = await HttpContext.GetTokenAsync("access_token");

        //        return new string[] { accessToken };
        //    }
        //    catch (Exception ex)
        //    {
        //        logHelper.Error("Get : " + ex.Message);
        //        throw;
        //    }
        //}
        //#endregion
       
        #region 
        [HttpPost(nameof(ForgetPassword))]
        public IActionResult ForgetPassword(string UserName)
        {
            try
            {
                DataTable dtforgetpassword = null;
                string strImage = String.Empty;
                IActionResult Response = Unauthorized(new { message = "UnAuthorized", StatusCode = 401 });
                LoginDetails ObjLogin = new LoginDetails(_config);


                ObjLogin.ForgetPassword(UserName, ref dtforgetpassword);

                if (dtforgetpassword.Rows.Count > 0 && dtforgetpassword != null)
                {
                    string strOTP = string.Empty;
                    DataTable dtottp = null;
                    //DataTable sendotp = null;
                    DataSet dsLogin = null;
                    // strOTP = ObjLogin.GetOTP().ToString();
                    strOTP = "1000";
                    //int emp_id = Convert.ToInt64(dtforgetpassword.Rows[0]["Emp_ID"]);
                    //int cmp_id = Convert.ToInt32(dtforgetpassword.Rows[0]["Cmp_ID"]);
                    string emp_id = dtforgetpassword.Rows[0]["Emp_ID"].ToString();
                    string cmp_id = dtforgetpassword.Rows[0]["Cmp_ID"].ToString();
                    string mobileno = dtforgetpassword.Rows[0]["Mobile_No"].ToString();
                    string email = dtforgetpassword.Rows[0]["Work_Email"].ToString();
                    //DateTime createddate = DateTime.Now.Date;
                    //DateTime expirydate = DateTime.Now.Date;
                    int otp_id = 0;
                    int otp_typeid = 1;
                    int isverified = 0;
                  //  ObjLogin.LoginCheck(login, ref dsLogin);
                    // string Emp_Full_name = dsLogin.Tables[0].Rows[0]["Emp_Full_Name"].ToString();
                    ObjLogin.AddOTP(otp_id, otp_typeid, emp_id, cmp_id, strOTP, email, mobileno, isverified, ref dtottp);
                    //ObjLogin.SendOTP(cmp_id, emp_id, email, strOTP, Emp_Full_name);
                    //var data = CommonClass.ToJson(dtforgetpassword);
                    //singleResponse.Status = Convert.ToBoolean(dtLogOut.Rows[0][0].ToString().Split("#")[1]);
                    //singleResponse.StatusCode = 200;
                    //singleResponse.msg = "OTP Generated";
                    //dtforgetpassword.Rows[0][0].ToString().Split("#")[0].Replace("@", "");
                    //singleResponse.data = strOTP;
                    return Ok(new { message = "OTP Generated", data = strOTP});
                   // Response = Ok(singleResponse);
                }
                else
                {
                   // singleResponse.StatusCode = 404;
                   // singleResponse.msg = "No Data Available";
                   // singleResponse.data = "";

                   return NotFound(new { message = "No Data Available", StatusCode = 404 });
                }
                 return Response;
                //return Unauthorized(new { message = "Logout Successfully", StatusCode=401 });
            }
            catch (Exception ex)
            {
                singleResponse.StatusCode = 500;
                singleResponse.msg = "Some thing went wrong.";
                singleResponse.data = "";

                logHelper.Error("LogOut : " + ex.Message);
                throw;
            }
            finally
            {
                singleResponse = null;
            }
        }
        [HttpPost(nameof(OTP_Verification))]
        public IActionResult OTP_Verification(string UserName, string otpcode)
        {
            try
            {
                DataTable otpcheck = null;
                DataSet dsLogin = null;
                string strImage = String.Empty;
                IActionResult Response = Unauthorized();
                LoginDetails ObjLogin = new LoginDetails(_config);

                ObjLogin.Getuserdetails(UserName, ref dsLogin);
                if (dsLogin.Tables[0].Rows.Count > 0)
                {
                    string emp_id = dsLogin.Tables[0].Rows[0]["Emp_ID"].ToString();
                    string cmp_id = dsLogin.Tables[0].Rows[0]["Cmp_ID"].ToString();
                    string email = dsLogin.Tables[0].Rows[0]["Work_Email"].ToString();
                    ObjLogin.CheckOtpCode(emp_id, cmp_id, otpcode, email, ref otpcheck);

                    if (otpcheck.Rows.Count > 0 && otpcheck.Rows[0][0].ToString().Contains("OTP Verified"))
                    {
                       // singleResponse.StatusCode = 200;
                        // singleResponse.msg = "OTP Verified";
                        //singleResponse.StatusCode = 404;
                        //singleResponse.msg = otpcheck.Rows[0][0].ToString();
                        // dtLogin.Tables[0].Rows[0][0].ToString().Contains("OK")
                        //singleResponse.data = "";
                      Response= Ok(new { StatusCode=200,message = otpcheck.Rows[0][0].ToString()});
                      //  Response = Ok(singleResponse);
                        //    string strOTP = string.Empty;
                        //    DataTable dtottp = null;
                        //    DataTable sendotp = null;
                        //    DataSet dsLogin = null;
                        //    strOTP = ObjLogin.GetOTP().ToString();
                        //    int emp_id = Convert.ToInt32(dtforgetpassword.Rows[0]["Emp_ID"]);
                        //    int cmp_id = Convert.ToInt32(dtforgetpassword.Rows[0]["Cmp_ID"]);
                        //    int mobileno = Convert.ToInt32(dtforgetpassword.Rows[0]["Mobile_No"]);
                        //    string email = dtforgetpassword.Rows[0]["Work_Email"].ToString();
                        //    DateTime createddate = DateTime.Now.Date;
                        //    DateTime expirydate = DateTime.Now.Date;
                        //    int otp_id = 0;
                        //    int otp_typeid = 1;
                        //    int isverified = 0;
                        //    ObjLogin.LoginCheck(login, ref dsLogin);
                        //    string Emp_Full_name = dsLogin.Tables[0].Rows[0]["Emp_Full_Name"].ToString();
                        //    ObjLogin.AddOTP(otp_id, otp_typeid, emp_id, cmp_id, Convert.ToInt32(strOTP), email, mobileno, createddate, expirydate, isverified, ref dtottp);
                        //    //ObjLogin.SendOTP(cmp_id, emp_id, email, strOTP, Emp_Full_name);
                        //    //var data = CommonClass.ToJson(dtforgetpassword);
                        //    //singleResponse.Status = Convert.ToBoolean(dtLogOut.Rows[0][0].ToString().Split("#")[1]);

                        //    //dtforgetpassword.Rows[0][0].ToString().Split("#")[0].Replace("@", "");
                        //    singleResponse.data = strOTP;

                    }
                    else
                    {
                        //singleResponse.StatusCode = 404;
                        //singleResponse.msg = "No Data Available";
                        //singleResponse.msg = otpcheck.Rows[0][0].ToString();
                        //singleResponse.data = "";
                       Response=  NotFound(new { StatusCode = 404, message = otpcheck.Rows[0][0].ToString() });
                        //Response = NotFound(singleResponse);



                    }
                }
                return Response;
            }

            catch (Exception ex)
            {
                singleResponse.StatusCode = 500;
                singleResponse.msg = "Some thing went wrong.";
                singleResponse.data = "";

                logHelper.Error("LogOut : " + ex.Message);
                throw;
            }
            finally
            {
                singleResponse = null;
            }
        }
        //[AllowAnonymous]
        [HttpGet(nameof(ServerConnection))]
        public IActionResult ServerConnection(string strCode)
        {
            try
            {
                string dtServerLink = String.Empty;
                IActionResult Response = Unauthorized();
                Login ObjLogin = new Login(_config);


                dtServerLink = ObjLogin.ServerConnection(strCode);

                if (!string.IsNullOrEmpty(dtServerLink))
                {
                    //singleResponse.Status = true;
                    singleResponse.StatusCode = 200;
                    singleResponse.msg = "Successfully";
                    singleResponse.data = dtServerLink;

                    Response = Ok(singleResponse);
                }
                else
                {
                    // singleResponse.Status = false;
                    singleResponse.StatusCode = 404;
                    singleResponse.msg = "No Data Available";
                    singleResponse.data = "";

                    Response = NotFound(singleResponse);
                }
                return Response;
            }
            catch (Exception ex)
            {
                //singleResponse.Status = false;
                singleResponse.StatusCode = 500;
                singleResponse.msg = "Some thing went wrong.";
                singleResponse.data = "";

                logHelper.Error("LogOut : " + ex.Message);
                throw;
            }
            finally
            {
                singleResponse = null;
            }
        }
        #endregion

        [HttpPost(nameof(ResetPassword))]
        public IActionResult ResetPassword(string UserName, string NewPassword, string ConfirmPassword)
        {
            try
            {
                DataTable dtnewpass = null;
                string newpass = null;
                DataSet dsLogin = null;
                string strImage = String.Empty;
                IActionResult Response = Unauthorized();
                LoginDetails ObjLogin = new LoginDetails(_config);
                ObjLogin.Getuserdetails(UserName, ref dsLogin);
                if (dsLogin.Tables[0].Rows.Count > 0)
                {
                    string emp_id = dsLogin.Tables[0].Rows[0]["Emp_ID"].ToString();
                    string cmp_id = dsLogin.Tables[0].Rows[0]["Cmp_ID"].ToString();
                    string email = dsLogin.Tables[0].Rows[0]["Work_Email"].ToString();



               ObjLogin.NewPasswordSet(cmp_id,NewPassword,emp_id,UserName,ref dtnewpass);
                    //if(dtnewpass.Rows.Count>0)
                    //singleResponse.msg = "Password Reset Successfully";
                    //singleResponse.StatusCode = 200;
                   // singleResponse.data = "";
                    Response =  Ok(new { StatusCode = 200, message = "Password Reset Successfully" });
                }

                //ObjLogin.ForgetPassword(login, ref dtforgetpassword);

                //if (dtforgetpassword.Rows.Count > 0 && dtforgetpassword != null)
                //{
                //    string strOTP = string.Empty;
                //    DataTable dtottp = null;
                //    //DataTable sendotp = null;
                //    DataSet dsLogin = null;
                //    strOTP = ObjLogin.GetOTP().ToString();
                //    //int emp_id = Convert.ToInt64(dtforgetpassword.Rows[0]["Emp_ID"]);
                //    //int cmp_id = Convert.ToInt32(dtforgetpassword.Rows[0]["Cmp_ID"]);
                //    string emp_id = dtforgetpassword.Rows[0]["Emp_ID"].ToString();
                //    string cmp_id = dtforgetpassword.Rows[0]["Cmp_ID"].ToString();
                //    string mobileno = dtforgetpassword.Rows[0]["Mobile_No"].ToString();
                //    string email = dtforgetpassword.Rows[0]["Work_Email"].ToString();
                //    //DateTime createddate = DateTime.Now.Date;
                //    //DateTime expirydate = DateTime.Now.Date;
                //    int otp_id = 0;
                //    int otp_typeid = 1;
                //    int isverified = 0;
                //    //ObjLogin.LoginCheck(login, ref dsLogin);
                //    // string Emp_Full_name = dsLogin.Tables[0].Rows[0]["Emp_Full_Name"].ToString();
                //    ObjLogin.AddOTP(otp_id, otp_typeid, emp_id, cmp_id, strOTP, email, mobileno, isverified, ref dtottp);
                //    //ObjLogin.SendOTP(cmp_id, emp_id, email, strOTP, Emp_Full_name);
                //    //var data = CommonClass.ToJson(dtforgetpassword);
                //    //singleResponse.Status = Convert.ToBoolean(dtLogOut.Rows[0][0].ToString().Split("#")[1]);
                //    singleResponse.StatusCode = 200;
                //    singleResponse.msg = "OTP Generated";
                //    //dtforgetpassword.Rows[0][0].ToString().Split("#")[0].Replace("@", "");
                //    singleResponse.data = strOTP;

                //    Response = Ok(singleResponse);
                //}
                else
                {
                    singleResponse.StatusCode = 404;
                    singleResponse.msg = "No Data Available";
                    singleResponse.data = "";

                    Response = NotFound (new { StatusCode = 404, message = "No Data Available" });
                }
                return Response;
            }
            catch (Exception ex)
            {
                singleResponse.StatusCode = 500;
                singleResponse.msg = "Some thing went wrong.";
               
                //Response = BadRequest(new { StatusCode = 404, message = "No Data Available" });
                logHelper.Error("LogOut : " + ex.Message);
                throw;
            }
            finally
            {
               // return Response;
                singleResponse = null;
            }
        }
    }
}


























    //public class LoginController : Controller
    //{
    //    // GET: HomeController
    //    public ActionResult Index()
    //    {
    //        return View();
    //    }

    //    // GET: HomeController/Details/5
    //    public ActionResult Details(int id)
    //    {
    //        return View();
    //    }

    //    // GET: HomeController/Create
    //    public ActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: HomeController/Create
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Create(IFormCollection collection)
    //    {
    //        try
    //        {
    //            return RedirectToAction(nameof(Index));
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }

    //    // GET: HomeController/Edit/5
    //    public ActionResult Edit(int id)
    //    {
    //        return View();
    //    }

    //    // POST: HomeController/Edit/5
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Edit(int id, IFormCollection collection)
    //    {
    //        try
    //        {
    //            return RedirectToAction(nameof(Index));
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }

    //    // GET: HomeController/Delete/5
    //    public ActionResult Delete(int id)
    //    {
    //        return View();
    //    }

    //    // POST: HomeController/Delete/5
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Delete(int id, IFormCollection collection)
    //    {
    //        try
    //        {
    //            return RedirectToAction(nameof(Index));
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }
    //}

