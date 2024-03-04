using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Orange_RestWebAPI.Connections;
using Orange_RestWebAPI.Model;
using System;
using System.Data;
using System.Net;
using System.Net.Security;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Microsoft.Exchange.WebServices.Data;
using System.Net.Mail;
using static Orange_RestWebAPI.Model.PredefinedClasses.Response;
using System.Security.Cryptography.X509Certificates;


namespace Orange_RestWebAPI.BusinessLogic
{
    public class LoginDetails
    {
        ClsDataccess ObjclsDataccess;// = new ClsDataccess(IConfiguration config);
        EncryptDecryptAlgo ObjencryptDecryptAlgo;
        LogHelper logHelper;
        MyClassToken<LoginModel> response = new MyClassToken<LoginModel>();
        private IHttpContextAccessor _accessor;
        EmailSetting Emailsetting;
        public LoginDetails(IConfiguration config)
        {
            ObjclsDataccess = new ClsDataccess(config);
            ObjencryptDecryptAlgo = new EncryptDecryptAlgo();
            logHelper = new LogHelper();
            Emailsetting = new EmailSetting();
        }
       // public LoginDetails(IHttpContextAccessor accessor)
       // {
           // _accessor = accessor;
       // }
        public void ForgetPassword(string Username, ref DataTable dtforgetpassword)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@UserName",Username),
                   new SqlParameter("@Password", ""),

                  new SqlParameter("@NewPassword", Convert.ToString("")),
                   new SqlParameter("@Login_ID",Convert.ToInt32("0")),
                   new SqlParameter("@Emp_ID",Convert.ToInt32("0")),
                   new SqlParameter("@Cmp_ID",Convert.ToInt32("0")),
                   new SqlParameter("@Type", Convert.ToString("F")),
                   new SqlParameter("@Result",Convert.ToString(""))
                };
                //sqlParams[9].Direction = ParameterDirection.Output;

                dtforgetpassword = ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_WebService_Login", sqlParams);
                //return sqlParams[9].SqlValue.ToString();
            }
            catch (Exception ex)
            {
                logHelper.Error("ChangePassword : " + ex.Message);
                throw;
            }
        }



        public string GetOTP()
        {
            string strOTP = string.Empty;
            try
            {
                string numbers = "1234567890";
                string character;
                int index;
                Random random = new Random();
                for (int i = 0; i < 6; i++)
                {
                    do
                    {
                        index = random.Next(0, numbers.Length);
                        character = numbers[index].ToString();
                    } while (strOTP.IndexOf(character) != -1);
                    strOTP += character;
                }
            }
            catch (Exception ex)
            {
                logHelper.Error("GetOTP : " + ex.Message);
            }
            return strOTP;
        }

        public void AddOTP(int Otp_ID, int Otp_TypeID, string EmpID, string CmpID, string Otp_Code, string Email, string MobileNo, int IsVerified, ref DataTable dtotp)
        {


            try
            {

                SqlParameter[] sqlParams = new SqlParameter[]
                {

                   new SqlParameter("@emp_id", EmpID),
                   new SqlParameter("@cmp_id", CmpID),
                    new SqlParameter("@Otp_ID", Otp_ID),

                   new SqlParameter("@Otp_TypeID", Otp_TypeID),
                   new SqlParameter("@Otp_Code", Otp_Code),
                   new SqlParameter("@Email", Email),
                   new SqlParameter("@MobileNo",MobileNo),
                   //new SqlParameter("@CreatedDate", CreatedDate),
                   //new SqlParameter("@ExpiredDate",expirydate ),
                   new SqlParameter("@IsVerified", IsVerified),

                   new SqlParameter("@Tran_Type", "I"),


                   //new SqlParameter("@Result", "")
                };

                dtotp = ObjclsDataccess.ExecuteDataTable("P0011_OTP_TRANSACTIONS", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("AddOTP : " + ex.Message);
                throw;
            }
        }

        public void CheckOtpCode(string emp_id, string cmp_id,string otpcode,string email, ref DataTable otp)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@Emp_ID",emp_id),
                   new SqlParameter("@Cmp_ID", cmp_id),

                  new SqlParameter("@Otp_Code", otpcode),
                   new SqlParameter("@User_Type",""),
                   new SqlParameter("@Email",email),
                   
                };
                //sqlParams[9].Direction = ParameterDirection.Output;

                otp = ObjclsDataccess.ExecuteDataTable("SP_OTP_TRANSACTIONS_VALIDATE_Mobile", sqlParams);
                //return sqlParams[9].SqlValue.ToString();
            }
            catch (Exception ex)
            {
                logHelper.Error("ChangePassword : " + ex.Message);
                throw;
            }
        }
        public void SendOTP(int cmp_id, int emp_id, string email, string Otpcode, string emp_full_name)
        {
            try
            {
                string ForgotPassword_Text = "#VerificationCode# is your verification code to reset your HRMS login password. <br/> Verification code will be expired after 15mins. <br/> Thank You.";




                DataTable dt1;
                dt1 = ObjclsDataccess.Getdatatable("Select Email_Title, Email_Signature from T0010_Email_Format_Setting where (Cmp_Id = '" + cmp_id + "' and Email_Type = 'ForgetPassword')");
                if (dt1.Rows.Count > 0)
                {
                    ForgotPassword_Text = Convert.ToString(dt1.Rows[0]["Email_Signature"]).Replace("#Emp_Full_Name#", emp_full_name).Replace("#VerificationCode#", Otpcode);
                }
                else
                {
                    ForgotPassword_Text = ForgotPassword_Text.Replace("#VerificationCode#", Otpcode);
                }
                if (EMail_OTP_Send(email, "Verification Code", ForgotPassword_Text) == true)
                {
                    //ltrlMsg1.Text = "One Time Password(OTP) has been sent to Your Email " + ViewState["EmailDisplay"] + ", please enter the same here to Reset Password.";
                    //ltrlMsg1.CssClass = "success";
                }
                else
                {
                    DataTable dt = ObjclsDataccess.Getdatatable("update T0090_Common_Request_Detail set status=0 where emp_login_id=" + emp_id + " and request_type='Forget Password' and cast(request_date as varchar(11))='" + DateTime.Now.Date + "'");

                }

            }
            catch (Exception ex)
            {

                logHelper.Error("SendOTP : " + ex.Message);
                throw;
            }
        }
        public bool EMail_OTP_Send(string email, string Subject, string Messages)
        {
            bool int_return = false;
            try
            {
                //string url = "";
                //string PasswordUrl = "";
                //string Qstring = string.Empty;
                string from_mailid = "orangeqa4@gmail.com";
                //if (Request.Url.OriginalString.ToLower().Contains("forget_password.aspx"))
                //{
                //    url = "<a href='" + Request.Url.OriginalString.Replace("forget_password.aspx", "login_comm.aspx") + "' target=_blank>" + Request.Url.OriginalString.Replace("forget_password.aspx", "login_comm.aspx") + "</a>"; //--chg by alpesh 10-5-2011
                //    Qstring = "" + "&IDE=" + oClsCryptoUtil.EncryptTripleDES(ViewState["Emp_ID"].ToString()) + "&IDC=" + oClsCryptoUtil.EncryptTripleDES(ViewState["Cmp_Id"].ToString()) + "&IDK=" + oClsCryptoUtil.EncryptTripleDES(DateTime.Now.Date.ToString()) + "&IDN=" + oClsCryptoUtil.EncryptTripleDES(txtemail.Text); // Rathod 17/04/2012

                //    PasswordUrl = "<a href='" + Request.Url.OriginalString.Replace("forget_password.aspx", "Reset_Password.aspx") + "?IDL=" + Qstring + "' target=_blank>Click Here</a>"; // Rathod 17/04/2012
                //}
                //clsEMail oclsEMail = new clsEMail();
                //oClsTools.SendEmail(email, Subject, Messages, "Verification Code");
               // SendEmail_Sent(from_mailid, email, Subject, Messages.ToString(), "", "", "", "Forgot Password");

                int_return = true;
                return int_return;
            }
            catch (Exception ex)
            {
                logHelper.Error("Email_SendOTP : " + ex.Message);
                return int_return = false;
            }
        }
        public string Get_Email_Setting(string strEmail, string Str_parameter)
        {
            string result = "";
            try
            {
                int i;
                int j;
                i = strEmail.IndexOf(Str_parameter);
                result = strEmail.Substring(i);
                j = result.IndexOf("#");
                result = result.Substring(0, j);
                result = result.Substring(result.IndexOf(":") + 2);
            }
            catch (Exception ex)
            {
                // Handle exception if necessary
            }
            return result;
        }

        //public void SendEmail_Sent(string from_mailid, string To_EmailId,string  subject,string message,string body,string CC,String BCC,string attach,string Module_Name)
        //{
        //    int cmp_Id = (int)HttpContext.Current.Session["Cmp_Id"];

        //    string Email_Settings = "";

        //    if (HttpContext.Current.Session["Base_Class"] != null)
        //    {
        //        Email_Settings = ((ClsUser)HttpContext.Current.Session["Base_Class"]).pEmail_Setting;
        //    }
        //    else
        //    {
        //        ClsDataccess oClsDataccess = new ClsDataccess();
        //        ColSqlparram oColSqlparram = new ColSqlparram();
        //        oColSqlparram.Add("@table", "t0010_email_setting");
        //        oColSqlparram.Add("@key_column", "cmp_id");
        //        oColSqlparram.Add("@key_Values", cmp_Id);
        //        oColSqlparram.Add("@String", Email_Settings, SqlDbType.VarChar, ParameterDirection.InputOutput);
        //        oClsDataccess.ExecStoredProcedure_Login("P9999_Audit_get", oColSqlparram);
        //        Email_Settings = oColSqlparram["@String"].ParaValue;
        //    }

        //    string is_mes = "";

        //    if (!string.IsNullOrEmpty(Email_Settings))
        //    {
        //        is_mes = Get_Email_Setting(Email_Settings, "IsMES");
        //    }
        //    else
        //    {
        //        is_mes = ConfigurationManager.AppSettings["IsMES"];
        //    }

        //    if (is_mes == "1")
        //    {
        //        try
        //        {
        //            if (To_EmailId.LastIndexOf(",") == To_EmailId.Length)
        //            {
        //                To_EmailId = To_EmailId.Substring(0, To_EmailId.Length);
        //            }

        //            if (CC.LastIndexOf(",") == CC.Length)
        //            {
        //                CC = To_EmailId.Substring(0, CC.Length);
        //            }

        //            if (BCC.LastIndexOf(",") == BCC.Length)
        //            {
        //                BCC = To_EmailId.Substring(0, BCC.Length);
        //            }

        //            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);

        //            if (!string.IsNullOrEmpty(Email_Settings))
        //            {
        //                service.Url = new Uri(Get_Email_Setting(Email_Settings, "MESURI"));
        //            }
        //            else
        //            {
        //                service.Url = new Uri(ConfigurationManager.AppSettings["MESURI"]);
        //            }

        //            service.UseDefaultCredentials = true;
        //            service.TraceEnabled = true;

        //            if (!string.IsNullOrEmpty(Email_Settings))
        //            {
        //                service.Credentials = new WebCredentials(Get_Email_Setting(Email_Settings, "MailServer_UserName"), Get_Email_Setting(Email_Settings, "MailServer_Password"));
        //            }
        //            else
        //            {
        //                service.Credentials = new WebCredentials(ConfigurationManager.AppSettings["MailServer_UserName"], ConfigurationManager.AppSettings["MailServer_Password"]);
        //            }

        //            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RemoteCertificateValidationCallback);

        //            EmailMessage message = new EmailMessage(service);
        //            message.Subject = Subject;

        //            if (!string.IsNullOrEmpty(CC))
        //            {
        //                string[] ccList = CC.Split(',');
        //                foreach (string ccEmail in ccList)
        //                {
        //                    message.CcRecipients.Add(ccEmail.Trim());
        //                }
        //            }

        //            if (!string.IsNullOrEmpty(BCC))
        //            {
        //                string[] bccList = BCC.Split(',');
        //                foreach (string bccEmail in bccList)
        //                {
        //                    message.BccRecipients.Add(bccEmail.Trim());
        //                }
        //            }

        //            string[] toList = To_EmailId.Split(',');
        //            foreach (string toEmail in toList)
        //            {
        //                message.ToRecipients.Add(toEmail.Trim());
        //            }

        //            if (!string.IsNullOrEmpty(Email_Settings))
        //            {
        //                message.ReplyTo.Add(Get_Email_Setting(Email_Settings, "MESReplyTo"));
        //            }
        //            else
        //            {
        //                message.ReplyTo.Add(ConfigurationManager.AppSettings["MESReplyTo"]);
        //            }

        //            message.Body = Body;
        //            message.Body.BodyType = BodyType.HTML;

        //            if (!string.IsNullOrEmpty(attach))
        //            {
        //                string[] attachments = attach.Split(',');
        //                foreach (string attachmentPath in attachments)
        //                {
        //                    message.Attachments.AddFileAttachment(attachmentPath);
        //                }
        //            }

        //            ClsTools.EMailParameter param = new ClsTools.EMailParameter();
        //            param.mailType = "EXCHANGE";
        //            param.client = service;
        //            param.message = message;
        //            param.Module_Name = Module_Name;
        //            param.attach = attach;
        //            param.Cmp_Id = cmp_Id;

        //            Thread thread = new Thread(new ParameterizedThreadStart(smtpMailSub));
        //            thread.Start(param);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    else
        //    {
        //        // SMTP mail sending logic goes here
        //        // Please complete this part according to your SMTP mail sending implementation
        //    }

        //}

        public void NewPasswordSet(string cmp_id,string newpassword,string emp_id,string Username,ref  DataTable dt_changepass )
        {
            try
            {
                //Cmp_Id = (int)ViewState["CMP_ID"];
                //Emp_id = (int)ViewState["Emp_ID"];
                //ltrlMsg1.Text = "";
                //lblmsg2.Text = "";
               string lblmsgerror="";
                //reset_form();
                DataTable dt_Pass_Validation;
                string strValidations = Password_Validation(cmp_id);
                dt_Pass_Validation = ObjclsDataccess.Getdatatable("Select * From T0011_Password_Settings Where Cmp_ID = " + cmp_id);
                if (dt_Pass_Validation.Rows.Count > 0)
                {


                    Regex regex = new Regex(dt_Pass_Validation.Rows[0]["Password_Format"].ToString());
                    Match match = regex.Match(newpassword);
                    if (!match.Success)
                    {
                        response.msg = strValidations;

                        return ;
                    }
                    string StrQuery = string.Empty;
                    string Emp_Name = string.Empty;
                    string Emp_Email = string.Empty;
                    DataTable dt;
                    int Emp_id = Convert.ToInt32(emp_id);
                    //Cmp_Id = Convert.ToInt32(ViewState["CMP_ID"]);

                    if (Emp_id == 0)
                    {
                        StrQuery = "select isnull(cmp_email,'') as cmp_email,cast(getdate() as varchar(11))as to_date from t0010_company_master Where Cmp_Id = " + cmp_id;
                        dt = ObjclsDataccess.Getdatatable(StrQuery);
                        Emp_Name = "Admin";
                        Emp_Email = dt.Rows[0]["cmp_email"].ToString();
                    }
                    else
                    {
                        StrQuery = "Select Emp_Full_Name,Work_Email  from T0080_Emp_Master  Where Emp_ID = " + Emp_id + " and Cmp_Id = " + cmp_id;
                        dt = ObjclsDataccess.Getdatatable(StrQuery);
                        Emp_Name = dt.Rows[0]["Emp_Full_Name"].ToString();
                        Emp_Email = dt.Rows[0]["Work_Email"].ToString();
                    }

                    if (string.IsNullOrEmpty(lblmsgerror))
                    {
                        string New_Password = ObjencryptDecryptAlgo.EncryptTripleDES(newpassword);

                        string strquery_login;
                        DataTable dt_login;

                        if (Emp_id == 0)
                        {
                            StrQuery = "Update  dbo.T0011_LOGIN Set Login_Password='" + New_Password + "'Where  Login_Name like 'admin@%' and Cmp_ID = " + cmp_id;
                            strquery_login = "Select * from T0011_LOGIN Where  Login_Name like 'admin@%' and Cmp_ID = " + cmp_id;
                        }
                        else
                        {
                            StrQuery = "Update  dbo.T0011_LOGIN Set Login_Password='" + New_Password + "'Where Emp_Id = " + Emp_id + " and Cmp_ID = " + cmp_id;
                            strquery_login = "Select * from T0011_LOGIN Where Emp_ID = " + Emp_id + " and Cmp_Id = " + cmp_id;
                        }

                        dt_login = ObjclsDataccess.Getdatatable(strquery_login);

                        ObjclsDataccess.Directexecute(StrQuery);
                    
                        int Tran_id = 0;
                        dt = new DataTable();
                        dt = ObjclsDataccess.Getdatatable("select Isnull(Max(Tran_ID),0) + 1 as Tran_ID from dbo.T0250_Change_Password_History");
                        if (dt.Rows.Count > 0)
                        {
                            Tran_id = Convert.ToInt32(dt.Rows[0]["Tran_ID"]);
                            string pwd_Str = "Insert Into T0250_Change_Password_History (Tran_ID, Cmp_ID, Emp_ID, Password, Effective_From_Date) Values(" + Tran_id + " , " + cmp_id + ", " + Emp_id + ", '" + New_Password + "' , '" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                            ObjclsDataccess.Directexecute(pwd_Str);
                            //string pwd = "select * from ";
                            //dt_changepass = ObjclsDataccess.Getdatatable(pwd_Str);

                        }
                        //ip = HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : "";
                        //if (dt_login.Rows.Count > 0)
                        //{
                        //    string pwd_Str = "Insert Into T0100_Login_Detail_History (Cmp_ID, Emp_ID,Login_id,user_name, Password,system_Date,status, Ip_Address) Values(" + dt_login.Rows[0]["cmp_id"] + ", " + dt_login.Rows[0]["Emp_id"] + ", " + dt_login.Rows[0]["Login_id"] + ", '" + Username + "', '" + New_Password + "' , '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' , 1, '" + _accessor.HttpContext.Connection.RemoteIpAddress.ToString() + "')";
                        //    ObjclsDataccess.Directexecute(pwd_Str);
                        //}

                        //if (!string.IsNullOrEmpty(Emp_Email))
                        //{
                        //    string Subject = "Password Reset";
                        //    string Body = "Dear  " + Emp_Name + "<br/><br/>Kindly take a note that your password has been reset, as per your request in the portal." + "<br/><br/>Thanks," + "<br/>Webmaster";
                        //    // clsEMail oclsEMail = new clsEMail();
                        //    SendEmail(Emp_Email, Subject, Body, "Password");
                        //}


                    }
                }
            }
            catch (Exception ex)
            {
                logHelper.Error("GetState : " + ex.Message);
                throw;
            }
           
        }

        //public void SendEmail(string To_EmailId, string Subject, string Body, string Module_Name)
        //{
        //    string is_mes;
        //    string Email_Settings = "";

        //    int cmp_Id = 0;
        //    //if (HttpContext.Current.Session["Base_Class"] != null)
        //    //{
        //    //    cmp_Id = ((ClsUser)HttpContext.Current.Session["Base_Class"]).pCmp_Id;
        //    //    Email_Settings = ((ClsUser)HttpContext.Current.Session["Base_Class"]).pEmail_Setting;
        //    //}
        //    //else
        //    //{
        //    //ClsDataccess oClsDataccess = new ClsDataccess();
        //    //ColSqlparram oColSqlparram = new ColSqlparram();
        //    DataTable dtdata = null;
        //    SqlParameter[] sqlParams = new SqlParameter[]
        //    { 
        //           new SqlParameter("@table","t0010_email_setting"),
        //           new SqlParameter("@key_column", "cmp_id"),
        //           new SqlParameter("@key_Values", cmp_Id),
        //           new SqlParameter("@String",SqlDbType.VarChar ), //SqlDbType.VarChar, ParameterDirection.InputOutput),
        //           //new SqlParameter("@NewPassword", Convert.ToString("")),
        //           //new SqlParameter("@Login_ID",Convert.ToInt32("0")),
        //           //new SqlParameter("@Emp_ID",Convert.ToInt32("0")),
        //           //new SqlParameter("@Cmp_ID",Convert.ToInt32("0")),
        //           //new SqlParameter("@Type", Convert.ToString("L")),
        //           //new SqlParameter("@Result",Convert.ToString(""))
        //    };

        //    dtdata= ObjclsDataccess.ExecuteDataTable("P9999_Audit_get", sqlParams);
            
        //    if (dtData.Rows.Count > 0)
        //    {
        //        int columnIndex = 0; // Adjust this index to match the column index of "@String" in your result set
        //                             // Retrieve the value from the first row and the appropriate column
        //        Email_Settings = dtData.Rows[0][columnIndex].ToString();
        //        //oColSqlparram.Add("@table", "t0010_email_setting");
        //        //oColSqlparram.Add("@key_column", "cmp_id");
        //        //oColSqlparram.Add("@key_Values", cmp_Id);
        //        //oColSqlparram.Add("@String", Email_Settings, SqlDbType.VarChar, ParameterDirection.InputOutput);
        //        //oClsDataccess.ExecStoredProcedure_Login("P9999_Audit_get", oColSqlparram);
        //        //Email_Settings = dtdata.Rows["@String"].ToString();

        //        //Email_Settings = SqlParameter sqlParams[4].Value.ToString();
        //    }
        //    if (Email_Settings != "")
        //        is_mes = Get_Email_Setting(Email_Settings, "IsMES");
        //    else
        //        is_mes = "0";

        //    if (is_mes == "1")
        //    {
        //        try
        //        {
        //            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
        //            if (Email_Settings != "")
        //                service.Url = new Uri(Get_Email_Setting(Email_Settings, "MESURI"));
        //            else
        //                service.Url = new Uri(Emailsetting.MESURI);

        //            service.UseDefaultCredentials = true;
        //            service.TraceEnabled = true;
        //            if (Email_Settings != "")
        //                service.Credentials = new WebCredentials(Get_Email_Setting(Email_Settings, "MailServer_UserName"), Get_Email_Setting(Email_Settings, "MailServer_Password"));
        //            else
        //                service.Credentials = new WebCredentials(Emailsetting.Username,Emailsetting.Password);

        //            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RemoteCertificateValidationCallback);

        //            EmailMessage message = new EmailMessage(service);
        //            message.Subject = Subject;
        //            message.Body = Body;
        //            if (To_EmailId.Contains(","))
        //            {
        //                string[] strSp = To_EmailId.Split(",");
        //                foreach (string address in strSp)
        //                {
        //                    message.ToRecipients.Add(address);
        //                }
        //            }
        //            else
        //            {
        //                message.ToRecipients.Add(To_EmailId);
        //            }
        //            if (Email_Settings != "")
        //                message.ReplyTo.Add(Get_Email_Setting(Email_Settings, "MESReplyTo"));
        //            else
        //                message.ReplyTo.Add(Emailsetting.MESReplyTo);

        //            message.Body.BodyType = BodyType.HTML;
        //            message.Save();

        //            message.SendAndSaveCopy();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.ToString());
        //        }
        //    }
        //    else
        //    {
        //        string MailServer;
        //        string MailServer_UserName;
        //        string MailServer_Password;
        //        int MailServer_Port;
        //        string From_Email;
        //        string MailServer_DisplayName;
        //        bool Ssl;
        //        System.Net.Mail.SmtpClient oSmtpclient = new System.Net.Mail.SmtpClient();
        //        System.Net.Mail.MailMessage oE_Mail = new System.Net.Mail.MailMessage();
        //        int Err_Flag = 1;
        //        try
        //        {
        //            if (Email_Settings != "")
        //            {
        //                MailServer = Get_Email_Setting(Email_Settings, "MailServer");
        //                MailServer_UserName = Get_Email_Setting(Email_Settings, "MailServer_UserName");
        //                MailServer_Password = Get_Email_Setting(Email_Settings, "MailServer_Password");
        //                MailServer_Port = Convert.ToInt32(Get_Email_Setting(Email_Settings, "MailServer_Port"));
        //                MailServer_DisplayName = Get_Email_Setting(Email_Settings, "MailServer_DisplayName");
        //                From_Email = Get_Email_Setting(Email_Settings, "From_Email");
        //                Ssl = Convert.ToBoolean(Get_Email_Setting(Email_Settings, "Ssl"));
        //            }
        //            else
        //            {
        //                MailServer = Emailsetting.MailServer;
        //                MailServer_UserName = Emailsetting.Username;
        //                //ConfigurationManager.AppSettings["MailServer_UserName"];
        //                MailServer_Password = Emailsetting.Password;
        //                MailServer_Port = Convert.ToInt32(Emailsetting.MailServer_Port);
        //                MailServer_DisplayName = Emailsetting.DisplayName;
        //                From_Email = Emailsetting.From_Mail;
        //                Ssl = Emailsetting.Ssl;
        //            }

        //            oE_Mail.To.Clear();
        //            if (!string.IsNullOrEmpty(To_EmailId))
        //                oE_Mail.To.Add(To_EmailId);

        //            if (string.IsNullOrEmpty(MailServer_DisplayName))
        //                oE_Mail.From = new System.Net.Mail.MailAddress(From_Email);
        //            else
        //                oE_Mail.From = new System.Net.Mail.MailAddress(From_Email, MailServer_DisplayName);

        //            oE_Mail.IsBodyHtml = true;
        //            oE_Mail.Subject = Subject;
        //            oE_Mail.Body = Body;
        //            oE_Mail.Priority = System.Net.Mail.MailPriority.High;

        //            oSmtpclient.Host = MailServer;
        //            oSmtpclient.Port = MailServer_Port;
        //            oSmtpclient.EnableSsl = Ssl;
        //            oSmtpclient.UseDefaultCredentials = false;
        //            oSmtpclient.Credentials = new System.Net.NetworkCredential(MailServer_UserName, MailServer_Password);
        //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //            oSmtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
        //            oSmtpclient.Send(oE_Mail);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.ToString());
        //        }
        //    }
        //}


        //private static bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        //{
        //    return true;
        //}


        public string Password_Validation(string Cmp_Id)
        {
            string Error_Message = string.Empty;
            try
            {
                
                DataTable dt_Pass_Validation;
               
                dt_Pass_Validation = ObjclsDataccess.Getdatatable("Select * From T0011_Password_Settings Where Cmp_ID = " + Cmp_Id);
                if (dt_Pass_Validation.Rows.Count > 0)
                {
                    byte Is_Pass_Validation;
                    Is_Pass_Validation = (byte)(Convert.IsDBNull(dt_Pass_Validation.Rows[0]["Enable_Validation"]) ? 0 : dt_Pass_Validation.Rows[0]["Enable_Validation"]);
                    string Password_Format;
                    Password_Format = Convert.IsDBNull(dt_Pass_Validation.Rows[0]["Password_Format"]) ? "" : dt_Pass_Validation.Rows[0]["Password_Format"].ToString();
                   // ViewState["Password_Format"] = Password_Format;

                    if (Is_Pass_Validation == 1 && Password_Format != "")
                    {
                        //REV_Password.Enabled = true;
                        //REV_Password.ValidationExpression = Convert.IsDBNull(dt_Pass_Validation.Rows[0]["Password_Format"]) ? "" : dt_Pass_Validation.Rows[0]["Password_Format"].ToString();

                        short Min_Chars;
                        bool Is_Upper;
                        bool Is_Lower;
                        bool Is_Digit;
                        bool Is_SpecChar;

                        Error_Message = "";
                        Min_Chars = (short)(Convert.IsDBNull(dt_Pass_Validation.Rows[0]["Min_Chars"]) ? 0 : dt_Pass_Validation.Rows[0]["Min_Chars"]);
                        Is_Upper = (Convert.IsDBNull(dt_Pass_Validation.Rows[0]["Upper_Char"]) ? 0 : (short)dt_Pass_Validation.Rows[0]["Upper_Char"]) == 1 ? true : false;
                        Is_Lower = (Convert.IsDBNull(dt_Pass_Validation.Rows[0]["Lower_Char"]) ? 0 : (short)dt_Pass_Validation.Rows[0]["Lower_Char"]) == 1 ? true : false;
                        Is_Digit = (Convert.IsDBNull(dt_Pass_Validation.Rows[0]["Is_Digit"]) ? 0 : (short)dt_Pass_Validation.Rows[0]["Is_Digit"]) == 1 ? true : false;
                        Is_SpecChar = (Convert.IsDBNull(dt_Pass_Validation.Rows[0]["Special_Char"]) ? 0 : (short)dt_Pass_Validation.Rows[0]["Special_Char"]) == 1 ? true : false;

                        if (Is_Upper)
                        {
                            Error_Message = "One Upper Letter";
                        }

                        if (Is_Lower)
                        {
                            if (Error_Message == "")
                            {
                                Error_Message = "One Lower Letter";
                            }
                            else
                            {
                                Error_Message = Error_Message + ", One Lower Letter";
                            }
                        }

                        if (Is_Digit)
                        {
                            if (Error_Message == "")
                            {
                                Error_Message = "One Digit";
                            }
                            else
                            {
                                Error_Message = Error_Message + ", One Digit";
                            }
                        }

                        if (Is_SpecChar)
                        {
                            if (Error_Message == "")
                            {
                                Error_Message = "One Special Character.";
                            }
                            else
                            {
                                Error_Message = Error_Message + ", One Special Character.";
                            }
                        }

                        if (Min_Chars == 0)
                        {
                            Error_Message = "Password Should Have Atleast " + Error_Message;
                        }
                        else
                        {
                            if (Error_Message == "")
                            {
                                Error_Message = "Password Should have Minimum " + Min_Chars.ToString() + " characters.";
                            }
                            else
                            {
                                Error_Message = "Password Should Have Atleast " + Error_Message + " And Minimum " + Min_Chars.ToString() + " characters.";
                            }
                        }
                        // lblmsgerror.Text = Error_Message;
                        // REV_Password.ErrorMessage = Error_Message;
                    }
                    else
                    {
                        Error_Message = "";// REV_Password.Enabled = false;
                        //REV_Password.ErrorMessage = "";
                        // lblmsgerror.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                logHelper.Error("GetState : " + ex.Message);
                throw;
            }
            return Error_Message;
        }

        public void Getuserdetails(string username, ref DataSet dsLoginDetails)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                   new SqlParameter("@UserName",username),
                   new SqlParameter("@Password", ""),
                   new SqlParameter("@IMEINo",""),
                   new SqlParameter("@DeviceID", ""),
                   new SqlParameter("@NewPassword", Convert.ToString("")),
                   new SqlParameter("@Login_ID", Convert.ToInt32("0")),
                   new SqlParameter("@Emp_ID", Convert.ToInt32("0")),
                   new SqlParameter("@Cmp_ID", Convert.ToInt32("0")),
                   new SqlParameter("@Type", Convert.ToString("F")),
                   new SqlParameter("@Result", Convert.ToString(""))
                };

            dsLoginDetails = ObjclsDataccess.ExecuteDataSet("SP_Mobile_HRMS_WebService_Login", sqlParams);
        }
            catch (Exception ex)
            {

                logHelper.Error("Getuserdetails : " + ex.Message);
                throw;
            }
}
        public void LoginCheck(LoginModel login, ref DataSet dsLoginDetails)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                   new SqlParameter("@UserName",Convert.ToString(login.UserName)),
                   new SqlParameter("@Password", ObjencryptDecryptAlgo.EncryptTripleDES(Convert.ToString(login.Password))),
                   new SqlParameter("@IMEINo", Convert.ToString(login.IMEINo)),
                   new SqlParameter("@DeviceID", Convert.ToString(login.DeviceID)),
                   new SqlParameter("@NewPassword", Convert.ToString("")),
                   new SqlParameter("@Login_ID",Convert.ToInt32("0")),
                   new SqlParameter("@Emp_ID",Convert.ToInt32("0")),
                   new SqlParameter("@Cmp_ID",Convert.ToInt32("0")),
                   new SqlParameter("@Type", Convert.ToString("L")),
                   new SqlParameter("@token", ""),
                   new SqlParameter("@Result",Convert.ToString(""))
                };

                dsLoginDetails = ObjclsDataccess.ExecuteDataSet("SP_Mobile_HRMS_WebAPI_Login", sqlParams);
            }
            catch (Exception ex)
            {
               
                logHelper.Error("LoginCheck : " + ex.Message);
                throw;
            }
        }
        public void LoginUpdateToken(LoginModel login, string logintoken,ref DataSet dsLoginDetails)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                   new SqlParameter("@UserName",Convert.ToString(login.UserName)),
                   new SqlParameter("@Password", ObjencryptDecryptAlgo.EncryptTripleDES(Convert.ToString(login.Password))),
                   new SqlParameter("@IMEINo", Convert.ToString(login.IMEINo)),
                   new SqlParameter("@DeviceID", Convert.ToString(login.DeviceID)),
                   new SqlParameter("@NewPassword", Convert.ToString("")),
                   new SqlParameter("@Login_ID",Convert.ToInt32("0")),
                   new SqlParameter("@Emp_ID",Convert.ToInt32("0")),
                   new SqlParameter("@Cmp_ID",Convert.ToInt32("0")),
                   new SqlParameter("@Type", Convert.ToString("U")),
                   new SqlParameter("@token", logintoken),
                   new SqlParameter("@Result",Convert.ToString(""))
                };

                dsLoginDetails = ObjclsDataccess.ExecuteDataSet("SP_Mobile_HRMS_WebAPI_Login", sqlParams);
            }
            catch (Exception ex)
            {

                logHelper.Error("LoginCheck : " + ex.Message);
                throw;
            }
        }
        public void LogOutDeleteToken( string logintoken, ref DataTable dsLoginDetails)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                   new SqlParameter("@UserName",""),
                   new SqlParameter("@Password", ""),
                   new SqlParameter("@IMEINo", ""),
                   new SqlParameter("@DeviceID", ""),
                   new SqlParameter("@NewPassword", ""),
                   new SqlParameter("@Login_ID",Convert.ToInt32("0")),
                   new SqlParameter("@Emp_ID",Convert.ToInt32("0")),
                   new SqlParameter("@Cmp_ID",Convert.ToInt32("0")),
                   new SqlParameter("@Type", Convert.ToString("D")),
                   new SqlParameter("@token", logintoken),
                   new SqlParameter("@Result",Convert.ToString(""))
                };

                dsLoginDetails = ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_WebAPI_Login", sqlParams);
            }
            catch (Exception ex)
            {

                logHelper.Error("LoginCheck : " + ex.Message);
                throw;
            }
        }
        public void GetDeshBoardDetails(int EmpID, int CmpID, ref DataSet dtDashboardDetails)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                   new SqlParameter("@Emp_ID",EmpID),
                   new SqlParameter("@Cmp_ID",CmpID)
                };

                dtDashboardDetails = ObjclsDataccess.ExecuteDataSet("SP_Mobile_HRMS_WebService_DASHBOARD", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("GetDeshBoardDetails : " + ex.Message);
                throw;
            }
        }

        public void AttendanceDetails(int EmpID, int CmpID, int VerticalID, int SubVerticalID, int Month, int Year, string FromDate, string Todate, string strType, ref DataTable dtAttendanceDetails)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                   new SqlParameter("@Emp_ID", EmpID),
                   new SqlParameter("@Cmp_ID", CmpID),
                   new SqlParameter("@Vertical_ID", VerticalID),
                   new SqlParameter("@SubVertical_ID", SubVerticalID),
                   new SqlParameter("@ForDate", FromDate),
                   new SqlParameter("@Time", Todate),
                   new SqlParameter("@INOUTFlag", ""),
                   new SqlParameter("@Reason", ""),
                   new SqlParameter("@IMEINo", ""),
                   new SqlParameter("@Latitude", ""),
                   new SqlParameter("@Longitude", ""),
                   new SqlParameter("@Address", ""),
                   new SqlParameter("@Emp_Image", ""),
                   new SqlParameter("@strAttendance", ""),
                   new SqlParameter("@Month", Month),
                   new SqlParameter("@Year", Year),
                   new SqlParameter("@Type", strType),
                   new SqlParameter("@Result", ""),
                   new SqlParameter("@SubVerticalName", "")
                };

                dtAttendanceDetails = ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_WebService_Attendance", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("GetAttendanceDetails : " + ex.Message);
                throw;
            }
        }

        public void EmployeeDetails(int EmpID, int CmpID, string EmpCode, int VerticalID, int BranchID, int DepartmentID, string strType, ref DataSet dtEmpDetails)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                   new SqlParameter("@Emp_ID",EmpID),
                   new SqlParameter("@Cmp_ID",CmpID),
                   new SqlParameter("@Vertical_ID",VerticalID),
                   new SqlParameter("@Emp_Code",Convert.ToString(EmpCode)),
                   new SqlParameter("@Address",""),
                   new SqlParameter("@City",""),
                   new SqlParameter("@State",""),
                   new SqlParameter("@Pincode",""),
                   new SqlParameter("@PhoneNo",""),
                   new SqlParameter("@MobileNo",""),
                   new SqlParameter("@Email",""),
                   new SqlParameter("@ImageName",""),
                   new SqlParameter("@Branch_ID",BranchID),
                   new SqlParameter("@Department_ID",DepartmentID),
                   new SqlParameter("@Type",strType),
                   new SqlParameter("@Result","")
                };

                dtEmpDetails = ObjclsDataccess.ExecuteDataSet("SP_Mobile_HRMS_WebService_EmpDetails", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("EmployeeDetails : " + ex.Message);
                throw;
            }

        }

        #region 
        public void TeamDetails(int EmpID, int CmpID, char Status, ref DataTable dtTeamDetails)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                   new SqlParameter("@Emp_ID", EmpID),
                   new SqlParameter("@Cmp_ID", CmpID),
                   new SqlParameter("@Status", Status)
                };

                dtTeamDetails = ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_WebService_MyTeam", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("TeamDetails : " + ex.Message);
                throw;
            }
        }

        public void CheckScheme(int EmpID, int CmpID, string SchemType, int ID, ref DataTable dtScheme)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                   new SqlParameter("@Emp_ID", EmpID),
                   new SqlParameter("@Cmp_ID", CmpID),
                   new SqlParameter("@Loan_ID", SchemType),
                   new SqlParameter("@Leave_Type", ID),
                   new SqlParameter("@From_Date", DateTime.Now)
                };

                dtScheme = ObjclsDataccess.ExecuteDataTable("SP_Emp_Scheme_Details", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("CheckScheme : " + ex.Message);
                throw;
            }
        }

        public void TimeIndicator(int EmpID, int CmpID, int Branch_ID, ref DataTable dtTimeIndicator)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                   new SqlParameter("@Emp_ID", EmpID),
                   new SqlParameter("@Cmp_ID", CmpID),
                   new SqlParameter("@From_Date", DateTime.Now),
                   new SqlParameter("@To_Date", DateTime.Now),
                   new SqlParameter("@Branch_ID", Branch_ID)
                };

                dtTimeIndicator = ObjclsDataccess.ExecuteDataTable("P0380_EMP_IN_OUT_SHIFT_TIME_GET", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("TimeIndicator : " + ex.Message);
                throw;
            }
        }

        public void GetState(int CmpID, int CountryID, ref DataTable dtState)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                   new SqlParameter("@Cmp_ID", CmpID),
                   new SqlParameter("@Country_ID", CountryID)
                };

                dtState = ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_WebService_Get_State", sqlParams);
            }
            catch (Exception ex)
            {
                logHelper.Error("GetState : " + ex.Message);
                throw;
            }
        }
        #endregion
    }
}
