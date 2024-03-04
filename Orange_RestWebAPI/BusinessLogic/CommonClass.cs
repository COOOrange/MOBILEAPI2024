using Newtonsoft.Json;
using Orange_RestWebAPI.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace Orange_RestWebAPI.BusinessLogic
{
    public static class CommonClass
    {
        public static List<Dictionary<string, object>> ToJson(this DataTable dt)
        {
            LogHelper logHelper = new LogHelper();

            try
            {
                var list = new List<Dictionary<string, object>>();

                foreach (DataRow row in dt.Rows)
                {
                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt.Columns)
                    {
                        if (row[col].ToString().StartsWith('{') || row[col].ToString().StartsWith('['))
                        {
                            dict[col.ColumnName] = JsonConvert.DeserializeObject(row[col].ToString());
                        }
                        else
                        {
                            dict[col.ColumnName] = row[col];
                        }
                        if (String.IsNullOrEmpty(row[col].ToString()))
                            dict[col.ColumnName] = "";
                    }
                    list.Add(dict);
                }

                return list;
            }
            catch (Exception ex)
            {
                logHelper.Error("ToJson : " + ex.Message);
                throw;
            }
        }

        public static DataTable JsonStringToDataTable(string jsonString)
        {
            DataTable jsondt = new DataTable();
            string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
            List<string> ColumnsName = new List<string>();

            foreach (string jSA in jsonStringArray)
            {
                string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                foreach (string ColumnsNameData in jsonStringData)
                {
                    try
                    {
                        int idx = ColumnsNameData.IndexOf(":");
                        string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                        if (!ColumnsName.Contains(ColumnsNameString))
                        {
                            ColumnsName.Add(ColumnsNameString);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                    }
                }
                break;
            }
            foreach (string AddColumnName in ColumnsName)
            {
                jsondt.Columns.Add(AddColumnName);
            }
            foreach (string jSA in jsonStringArray)
            {
                string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = jsondt.NewRow();
                foreach (string rowData in RowData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                        string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                        nr[RowColumns] = RowDataString;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                jsondt.Rows.Add(nr);
            }
            return jsondt;
        }

        public static string DataTableToJsonWithJsonNet(DataTable table)
        {
            string jsonString = string.Empty;
            jsonString = JsonConvert.SerializeObject(table);
            return jsonString.Replace("\"", "");
        }
    }
}