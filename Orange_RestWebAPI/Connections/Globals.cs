using System.Data;

namespace OrangePayrollAPI.Connections
{
    public class Globals
    {
        /// <summary>
        ///  Create Table Procedure Without Parameter
        /// </summary>
        /// <param name="data"></param>
        /// <returns>DataTable</returns>
        public DataTable CreatedtTblProcedure()
        {
            DataTable dtProcedure = new DataTable();
            dtProcedure.Columns.Add("ProcID", typeof(int));
            dtProcedure.Columns.Add("ProcedureName", typeof(string));
            return dtProcedure;
        }

        /// <summary>
        ///  Create Table Procedure With Parameter
        /// </summary>
        /// <param name="data"></param>
        /// <returns>DataTable</returns>
        public DataTable CreatedtTblParameter()
        {
            DataTable dtParameter = new DataTable();
            dtParameter.Columns.Add("ProcID", typeof(int));
            dtParameter.Columns.Add("ParaName", typeof(string));
            dtParameter.Columns.Add("ParaValue", typeof(string));
            dtParameter.Columns.Add("ParaDirection", typeof(ParameterDirection));
            dtParameter.Columns.Add("SqlDBType", typeof(SqlDbType));
            return dtParameter;
        }
    }
}
