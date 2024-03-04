using Microsoft.Extensions.Configuration;
using Orange_RestWebAPI.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Orange_RestWebAPI.BusinessLogic
{
    public class Profile
    {
        ClsDataccess ObjclsDataccess;// = new ClsDataccess(IConfiguration config);
        EncryptDecryptAlgo ObjencryptDecryptAlgo;
        LogHelper logHelper;
        public Profile(IConfiguration config)
        {
            ObjclsDataccess = new ClsDataccess(config);
            ObjencryptDecryptAlgo = new EncryptDecryptAlgo();
            logHelper = new LogHelper();
        }

        public void UpdateProfile(int EmpID, int CmpID, string Address, string City, string State, string Pincode, string PhoneNo, string MobileNo, string Email, string ImageName, string strType, ref DataTable dtUpdateProfile, string Result = "", string Emp_Code = "", int Vertical_ID = 0, int Branch_ID = 0, int Department_ID = 0)
        {
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                   new SqlParameter("@Emp_ID", EmpID),
                   new SqlParameter("@Cmp_ID", CmpID),
                   new SqlParameter("@Vertical_ID", Vertical_ID),
                   new SqlParameter("@Emp_Code", Emp_Code),
                   new SqlParameter("@Address", Address),
                   new SqlParameter("@City", City),
                   new SqlParameter("@State", State),
                   new SqlParameter("@Pincode", Pincode),
                   new SqlParameter("@PhoneNo", PhoneNo),
                   new SqlParameter("@MobileNo", MobileNo),
                   new SqlParameter("@Email", Email),
                   new SqlParameter("@ImageName", ImageName),
                   new SqlParameter("@Branch_ID", Branch_ID),
                   new SqlParameter("@Department_ID", Department_ID),
                   new SqlParameter("@Type", strType),
                   new SqlParameter("@Result", Result)
                };
                //sqlParams[15].Direction = ParameterDirection.Output;

                dtUpdateProfile = ObjclsDataccess.ExecuteDataTable("SP_Mobile_HRMS_WebService_EmpDetails", sqlParams);
                //return sqlParams[15].SqlValue.ToString();
            }
            catch (Exception ex)
            {
                logHelper.Error("GetAttendanceDetails : " + ex.Message);
                throw;
            }
        }
    }
}
