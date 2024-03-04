using System;

namespace Orange_RestWebAPI.Connections
{
    // private IConfiguration _config;

    public class LogHelper //: AppHelper(_config)
    {
        //set response.

        public string logfile = @"\\logs\\DBLibrary.log";
        public string debugfile = @"\\logs\\DBLibrary.dbg";
        public string errlogfile = @"\\logs\\DBLibrary.err";

        public void Write(string message)
        {
            string datewisefile = "";
            DateTime dt = DateTime.Now;
            byte[] info = null;
            System.IO.FileStream fs = null;
            try
            {
                if (!System.IO.Directory.Exists(AppHelper.AppPath + @"\logs"))
                    System.IO.Directory.CreateDirectory(AppHelper.AppPath + @"\logs");
                // logfile = logfile.Split('.')[0] + System.DateTime.Now.ToString("yyyyMMddHHmm") + '.' + logfile.Split('.')[1]; 
                datewisefile = ((dt.Year + "") + "-" + (Iif(dt.Month <= 9, "0" + dt.Month + "", dt.Month + "")) + "") + "-" + (Iif(dt.Day <= 9, "0" + dt.Day + "", dt.Day + ""));
                logfile = @"\logs\DBLibrary_" + datewisefile + ".log";
                fs = System.IO.File.Open(AppHelper.AppPath + logfile, System.IO.FileMode.Append);
                info = new System.Text.UTF8Encoding(true).GetBytes((System.DateTime.Now.ToString() + ":") + message);
                // fs.Close(); 
                fs.Write(info, 0, info.Length);
                fs.Flush();
                fs.Close();
                fs = null;
                info = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // Error("WriteError:"+ex.Message); 
            finally
            {
            }
        }

        public static object Iif(bool cond, object left, object right)
        {
            return cond ? left : right;
        }

        public void Error(string message)
        {
            string datewisefile = "";
            DateTime dt = DateTime.Now;
            System.IO.FileStream fs = null;
            byte[] info = null;
            try
            {
                if (!System.IO.Directory.Exists(AppHelper.AppPath + "logs"))
                    System.IO.Directory.CreateDirectory(AppHelper.AppPath + "logs");
                // logfile = logfile.Split('.')[0] + System.DateTime.Now.ToString("yyyyMMddHHmm") + '.' + logfile.Split('.')[1]; 
                datewisefile = ((dt.Year + "") + "-" + (Iif(dt.Month <= 9, "0" + dt.Month + "", dt.Month + "")) + "") + "-" + (Iif(dt.Day <= 9, "0" + dt.Day + "", dt.Day + ""));
                errlogfile = @"\DBLibrary_" + datewisefile + ".err";
                fs = System.IO.File.Open(AppHelper.AppPath + "logs" + errlogfile, System.IO.FileMode.Append);
                info = new System.Text.UTF8Encoding(true).GetBytes((System.DateTime.Now.ToString() + ":") + message + "\n");
                // fs.Close();                 
                fs.Write(info, 0, info.Length);
                fs.Flush();
                fs.Close();
                fs = null;
                info = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // Write("Error:"+ex.Message); 
            finally
            {
            }
        }
    }
}
