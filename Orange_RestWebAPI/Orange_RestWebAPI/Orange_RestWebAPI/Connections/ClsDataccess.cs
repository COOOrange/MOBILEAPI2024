using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Orange_RestWebAPI.Connections
{
    public class ClsDataccess 
    {
        private IConfiguration _config = null;

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlParameter parameter = new SqlParameter();
        SqlTransaction sqlTrans;
        LogHelper logHelper = new LogHelper();
         
        private string DBgetConnectionString()
        {
            string ConnectionString = "";
            try
            {
                ConnectionString = _config["DBConfig:ConnectionString"];
            }
            catch (Exception ex)
            {
                logHelper.Error("DBgetConnectionString : " + ex.Message);
                throw;
            }
            return ConnectionString;
        }

        //initialize connection constructor
        public ClsDataccess(IConfiguration config)
        {
            _config = config;
            con.ConnectionString = DBgetConnectionString();
            cmd = con.CreateCommand();
        }

        //Open database connection
        private void DBOpenConnection()
        {
            try
            {
                if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
                {
                    con.Open();
                }
            }
            catch (Exception ex)
            {
                logHelper.Error("DBOpenConnection : " + ex.Message);
                throw;
            }
        }

        //close database conection
        private void DBCloseConnection()
        {
            try
            {
                con.Close();
                con = null;
            }
            catch (Exception ex)
            {
                logHelper.Error("DBCloseConnection : " + ex.Message);
                throw;
            }
        }

        //commit database connection
        private void DBCommitConnection()
        {
            sqlTrans.Commit();
            sqlTrans = null;
        }

        //rollback batabase transaction
        private void DbRollbackConnection()
        {
            try
            {
                sqlTrans.Rollback();
                sqlTrans = null;
            }
            catch (Exception ex)
            {
                logHelper.Error("DbRollbackConnection : " + ex.Message);
                throw;
            }
        }
        
        public DataTable ExecuteDataTable(string storedProcedureName, params SqlParameter[] arrParam)
        {
            DataTable dt = new DataTable();
            string ConnectionString = _config["DBConfig:ConnectionString"]; 

            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cm = new SqlCommand(storedProcedureName, cn))
                {
                    try
                    {
                        cn.Open();
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 600;
                        
                        if (arrParam != null)
                        {
                            foreach (SqlParameter param in arrParam)
                                cm.Parameters.Add(param);
                        }

                        SqlDataAdapter da = new SqlDataAdapter(cm);
                        da.Fill(dt);
                         
                        return dt;
                    }
                    catch (Exception ex)
                    {
                         
                        logHelper.Error("ExecuteDataTable : " + ex.Message);
                        throw new Exception("Error: " + ex.Message);
                    }
                    finally
                    {
                        dt = null;
                        cm.Dispose();
                        cn.Close();
                    }
                }

            }
        }

        public DataSet ExecuteDataSet(string storedProcedureName, params SqlParameter[] arrParam)
        {
            DataSet ds = new DataSet();
            string ConnectionString = _config["DBConfig:ConnectionString"]; 

            using (SqlConnection cn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cm = new SqlCommand(storedProcedureName, cn))
                {
                    try
                    {
                        cn.Open();
                        cm.CommandType = CommandType.StoredProcedure;
                        cm.CommandTimeout = 600;
                        
                        if (arrParam != null)
                        {
                            foreach (SqlParameter param in arrParam)
                                cm.Parameters.Add(param);
                        }

                        SqlDataAdapter da = new SqlDataAdapter(cm);
                        da.Fill(ds);
                         
                        return ds;
                    }
                    catch (Exception ex)
                    {
                        
                        logHelper.Error("ExecuteDataSet : " + ex.Message);
                        throw new Exception("Error: " + ex.Message);
                    }
                    finally
                    {
                        ds = null;
                        cm.Dispose();
                        cn.Close();
                    }
                }

            }
        }


        public DataTable Getdatatable(string Sqlquery)
        {
            DataTable glb_dt = new DataTable();
            System.Data.SqlClient.SqlCommand glb_com = new System.Data.SqlClient.SqlCommand();
            System.Data.SqlClient.SqlDataAdapter glb_adp = new System.Data.SqlClient.SqlDataAdapter();
            try
            {
                using (SqlConnection connection = new SqlConnection(DBgetConnectionString()))
                {
                    connection.Open();
                    glb_com.CommandTimeout = 0;
                    glb_com.CommandText = Sqlquery;
                    glb_com.CommandType = CommandType.Text;
                    glb_com.Connection = connection;
                    glb_adp.SelectCommand = glb_com;
                    glb_dt.Clear();
                    glb_adp.Fill(glb_dt);
                }
            }
            catch (Exception ex)
            {
                logHelper.Error("Getdatatable : " + ex.Message);
                throw;
            }
            finally
            {
                glb_com = null;
                glb_adp = null;
            }
            return glb_dt;
        }

        public void ExecBatchStoredProcedure(DataTable dtProcedureName, DataTable dtSqlparram)
        {
            int intParaCount = 0;
            int intProcedure = 0;
            SqlCommand com = new SqlCommand();
            DataTable dtParam = new DataTable();
            DataRow[] dtRow;
            dtParam = dtSqlparram.Clone();

            using (SqlConnection cn = new SqlConnection(DBgetConnectionString()))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cm = new SqlCommand())
                    {
                        cm.Connection = cn;
                        cm.Transaction = cn.BeginTransaction();
                        for (intProcedure = 0; intProcedure <= dtProcedureName.Rows.Count - 1; intProcedure++)
                        {
                            cm.CommandType = CommandType.StoredProcedure;
                            cm.CommandText = dtProcedureName.Rows[intProcedure]["ProcedureName"].ToString();
                            cm.CommandTimeout = 600;
                            cm.Parameters.Clear();
                            dtParam.Clear();
                            dtRow = dtSqlparram.Select("ProcID ='" + dtProcedureName.Rows[intProcedure]["ProcID"].ToString() + "' ");
                            for (int i = 0; i <= dtRow.GetUpperBound(0); i++)
                            {
                                dtParam.Rows.Add(dtRow[i].ItemArray);
                            }
                            for (intParaCount = 0; intParaCount <= dtParam.Rows.Count - 1; intParaCount++)
                            {
                                SqlParameter parameter = new SqlParameter();
                                parameter.ParameterName = dtParam.Rows[intParaCount]["ParaName"].ToString();

                                parameter.Value = dtParam.Rows[intParaCount]["ParaValue"].ToString();
                                if (dtParam.Rows[intParaCount]["ParaDirection"].ToString() == "1")
                                {
                                    parameter.Direction = ParameterDirection.Input;
                                }
                                else if (dtParam.Rows[intParaCount]["ParaDirection"].ToString() == "2")
                                {
                                    parameter.Direction = ParameterDirection.Output;
                                }
                                else if (dtParam.Rows[intParaCount]["ParaDirection"].ToString() == "3")
                                {
                                    parameter.Direction = ParameterDirection.InputOutput;
                                }
                                else if (dtParam.Rows[intParaCount]["ParaDirection"].ToString() == "6")
                                {
                                    parameter.Direction = ParameterDirection.ReturnValue;
                                }

                                if (parameter.Direction == ParameterDirection.InputOutput)
                                {
                                    if (parameter.Value == "0")
                                        parameter.Size = 16;
                                    else
                                        parameter.Size = 64;

                                }

                                cm.Parameters.Add(parameter);
                            }
                            try
                            {
                                cm.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                cm.Transaction.Rollback();
                                 
                                logHelper.Error("ExecBatchStoredProcedure : " + ex.Message);
                                throw;
                            }
                            
                        }
                        cm.Transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    logHelper.Error("ExecBatchStoredProcedure : " + ex.Message);
                    throw;
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        public void ExecBatchStoredProcedure_with_OutputPara(DataTable dtProcedureName, ref DataTable dtSqlparram)
        {
            int intParaCount = 0;
            int intProcedure = 0;
            System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand();
            DataTable dtParam = new DataTable();
            DataRow[] dtRow;
            DataTable dtParmOut = new DataTable();
            dtParam = dtSqlparram.Clone();
            dtParmOut = dtSqlparram.Clone();

            using (SqlConnection cn = new SqlConnection(DBgetConnectionString()))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cm = new SqlCommand())
                    {
                        cm.Connection = cn;
                        cm.Transaction = cn.BeginTransaction();
                        for (intProcedure = 0; intProcedure <= dtProcedureName.Rows.Count - 1; intProcedure++)
                        {

                            cm.CommandType = CommandType.StoredProcedure;
                            cm.CommandText = dtProcedureName.Rows[intProcedure]["ProcedureName"].ToString();
                            cm.CommandTimeout = 600;
                            cm.Parameters.Clear();
                            dtParam.Clear();
                            dtRow = dtSqlparram.Select("ProcID ='" + dtProcedureName.Rows[intProcedure]["ProcID"].ToString() + "' ");

                            for (int i = 0; i <= dtRow.GetUpperBound(0); i++)
                            {
                                dtParam.Rows.Add(dtRow[i].ItemArray);
                            }

                            for (intParaCount = 0; intParaCount <= dtParam.Rows.Count - 1; intParaCount++)
                            {
                                SqlParameter parameter = new SqlParameter();
                                parameter.ParameterName = dtParam.Rows[intParaCount]["ParaName"].ToString();

                                parameter.Value = dtParam.Rows[intParaCount]["ParaValue"].ToString();
                                if (dtParam.Rows[intParaCount]["ParaDirection"].ToString() == "1")
                                {
                                    parameter.Direction = ParameterDirection.Input;
                                }
                                else if (dtParam.Rows[intParaCount]["ParaDirection"].ToString() == "2")
                                {
                                    parameter.Size = 8000;
                                    parameter.Direction = ParameterDirection.Output;
                                }
                                else if (dtParam.Rows[intParaCount]["ParaDirection"].ToString() == "3")
                                {
                                    parameter.Size = 8000;
                                    parameter.Direction = ParameterDirection.InputOutput;
                                }
                                else if (dtParam.Rows[intParaCount]["ParaDirection"].ToString() == "6")
                                {
                                    parameter.Direction = ParameterDirection.ReturnValue;
                                }
                                cm.Parameters.Add(parameter);
                            }
                            try
                            {
                                cm.ExecuteNonQuery();

                                for (intParaCount = 0; intParaCount <= dtParam.Rows.Count - 1; intParaCount++)
                                {
                                    {
                                        if (dtParam.Rows[intParaCount]["ParaDirection"].ToString() == "2" || dtParam.Rows[intParaCount]["ParaDirection"].ToString() == "3")
                                        {
                                            dtParam.Rows[intParaCount]["ParaValue"] = cm.Parameters[intParaCount].Value.ToString();
                                        }
                                    }
                                }

                                dtParmOut.Merge(dtParam);

                            }
                            catch (Exception ex)
                            {
                                cm.Transaction.Rollback();
                                 
                                logHelper.Error("ExecBatchStoredProcedure_with_OutputPara : " + ex.Message);
                                throw;
                            }
                        }

                        cm.Transaction.Commit();
                       
                        dtSqlparram = dtParmOut;
                    }
                }
                catch (Exception ex)
                {   
                    logHelper.Error("ExecBatchStoredProcedure_with_OutputPara : " + ex.Message);
                    throw;
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        public void Directexecute(string strqrye)
        {
            using (SqlConnection cn = new SqlConnection(DBgetConnectionString()))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand com = new SqlCommand())
                    {
                        com.CommandText = strqrye;
                        com.CommandType = CommandType.Text;
                        com.Connection = cn;
                        com.ExecuteScalar();
                    }

                }


                catch (Exception ex)
                {
                    logHelper.Error("Directexecute : " + ex.Message);
                    throw;
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        //public void ExecStoredProcedure_Login(string strProcedureName, ColSqlparram oColSqlparram)
        //{
        //    int intParaCount = 0;
        //    SqlCommand com = new SqlCommand();
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(DBgetConnectionString()))
        //        {
        //            connection.Open();
        //            com.CommandText = strProcedureName;
        //            com.CommandType = CommandType.StoredProcedure;
        //            com.Connection = connection;
        //            com.CommandTimeout = 0;
        //            com.Parameters.Clear();
        //            foreach (var item in oColSqlparram)
        //            {
        //                SqlParameter parameter = new SqlParameter();
        //                parameter.ParameterName = item.ParaName;
        //                parameter.SqlDbType = item.ParaType;
        //                if (parameter.SqlDbType == SqlDbType.VarChar && item.ParaDirection == ParameterDirection.Input)
        //                {
        //                    parameter.Size = (item.ParaValue.Length == 0) ? 1 : item.ParaValue.Length;
        //                }
        //                else if (parameter.SqlDbType == SqlDbType.VarChar && item.ParaDirection != ParameterDirection.Input)
        //                {
        //                    parameter.Size = (item.ParaValue.Length == 0) ? 10000 : item.ParaValue.Length;
        //                }
        //                parameter.Value = item.ParaValue;
        //                parameter.Direction = item.ParaDirection;
        //                com.Parameters.Add(parameter);
        //            }
        //            com.ExecuteScalar();
        //            foreach (var item in oColSqlparram)
        //            {
        //                if (item.ParaDirection == ParameterDirection.InputOutput || item.ParaDirection == ParameterDirection.Output)
        //                {
        //                    item.ParaValue = CheckNull(com.Parameters[item.ParaName].Value);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.ToUpper().StartsWith("DELETE STATEMENT CONFLICTED") || ex.Message.Contains("DELETE STATEMENT CONFLICTED"))
        //        {
        //            foreach (var item in oColSqlparram)
        //            {
        //                if (item.ParaDirection == ParameterDirection.InputOutput || item.ParaDirection == ParameterDirection.Output)
        //                {
        //                    item.ParaValue = 0;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (ex.Message.Contains("Version Expired"))
        //            {
        //                throw ex;
        //            }
        //            else
        //            {
        //                logHelper.Error("CheckNull : " + ex.Message);
        //                throw;
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        com = null;
        //    }
        //}
        public string CheckNull(object vData)
        {
            try
            {
                return (vData == DBNull.Value || vData == null) ? "" : vData.ToString();
            }
            catch (Exception ex)
            {
                logHelper.Error("CheckNull : " + ex.Message);
                throw;
            }
        }


    }
}
