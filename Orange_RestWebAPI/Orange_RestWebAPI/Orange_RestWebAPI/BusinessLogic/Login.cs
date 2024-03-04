using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Orange_RestWebAPI.Connections;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Orange_RestWebAPI.BusinessLogic
{
    public class Login
    {
        private IConfiguration _config;
        ClsDataccess ObjclsDataccess;
        EncryptDecryptAlgo ObjencryptDecryptAlgo;
        LogHelper logHelper;

        public Login(IConfiguration config)
        {
            ObjclsDataccess = new ClsDataccess(config);
            ObjencryptDecryptAlgo = new EncryptDecryptAlgo();
            logHelper = new LogHelper();
            _config = config;
        }

        #region 
        
        public string ServerConnection(string strCode)
        {
            string strServer = String.Empty;
            try
            {
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send(_config["LicenseDB:Source"]);
                if (reply.Status == IPStatus.Success)
                {
                    using (var con = new SqlConnection("Initial Catalog=License;Data Source=" + _config["LicenseDB:Source"] + ";uid=" + _config["LicenseDB:UserName"] + ";pwd=" + _config["LicenseDB:Password"] + ""))
                    {
                        using (var cmd = new SqlCommand("SELECT WebService_Link FROM License_Mobile WHERE Client_Name = '" + strCode + "'", con))
                        {
                            con.Open();
                            var dt = new DataTable();
                            var adp = new SqlDataAdapter(cmd);
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                strServer = dt.Rows[0][0].ToString();
                            }
                        }
                    }
                }
                return strServer;
                //SqlParameter[] sqlParams = new SqlParameter[]
                //{
                //   new SqlParameter("@UserName", ""),
                //   new SqlParameter("@Password", ""),
                //   new SqlParameter("@IMEINo", IMEINo),
                //   new SqlParameter("@DeviceID", ""),
                //   new SqlParameter("@NewPassword", ""),
                //   new SqlParameter("@Login_ID", LoginID),
                //   new SqlParameter("@Emp_ID", EmpID),
                //   new SqlParameter("@Cmp_ID", CmpID),
                //   new SqlParameter("@Type", strType),
                //   new SqlParameter("@Result", "")
                //};
                //sqlParams[9].Direction = ParameterDirection.Output;

                //dtLogOut = ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_WebService_Login", sqlParams);
                //return sqlParams[9].SqlValue.ToString();
            }
            catch (Exception ex)
            {
                strServer = "";
                logHelper.Error("Validate: ServerConnection : " + ex.Message);
                throw;
            }
        }

        #endregion
    }
}
