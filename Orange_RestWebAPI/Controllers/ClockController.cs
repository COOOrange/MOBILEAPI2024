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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using static Orange_RestWebAPI.Model.PredefinedClasses.Response;
using static Orange_RestWebAPI.Model.PredefinedClasses.SingleResponse;

namespace Orange_RestWebAPI.Controllers
{
   
    [Route("Api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class ClockINController : Controller
    {
        private IConfiguration _config;
        //MyClassToken<LoginModel> response = new MyClassToken<LoginModel>();
        MyClass_Single<LoginModel> singleResponse = new MyClass_Single<LoginModel>();
        LogHelper logHelper = new LogHelper();
        ClsDataccess objclsdataaccess;
       
        public static IWebHostEnvironment _webhostenv;

        public ClockINController(IConfiguration config, IWebHostEnvironment webhostenv)
        {
            _config = config;
            _webhostenv = webhostenv;
        }

        [HttpPost(nameof(AddClockIN))]
        public IActionResult AddClockIN(int EmpID, int CmpID,string Date,string IOFlag, string IMEIno,string Address,string Latitude,string Longitude,string Reason,[FromForm] Fileupload fileupload)
            
        {

            try
            {
                DataSet dsdhDetails = null;
                //DataTable dtdhDetails = null;
                string strDocPath = string.Empty;
                IActionResult Response = Unauthorized();
                DataTable dtt = null;
                DataTable dtgeoloc = null;
                string strImage = string.Empty;
                string folderName = string.Empty;
                ClsDataccess objclsdataaccess = new ClsDataccess(_config);
                //Attendance ObjAtttendance = new Attendance(_config);
                Dashboard ObjDashB = new Dashboard(_config);
                if (string.IsNullOrEmpty(Date))
                {
                    Date = DateTime.Now.ToString(); 
                }
                if ((EmpID != 0) && (CmpID != 0) && fileupload.file.Length>0)
                {
                    strImage = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + EmpID + fileupload.file.FileName;
                    folderName = "/EmpImage/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.ToString("ddMMyyyy") + "/";

                   
                    string path = _webhostenv.WebRootPath + "\\uploads\\"+ folderName;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream filestream = System.IO.File.Create(path + strImage))
                        {
                      string   dsFileName = "";
                        
                        //dsFileName = Convert.ToString(EmpID) + "" + Convert.ToString(DateTime.Now).Replace("-", "").Replace(" ", "").Replace("/", "").Replace("+", "").Replace(":", "");
                        //dsFileName = dsFileName.Replace("|", "#");
                        //dsFileName =  fileupload.file.FileName;
                        strDocPath = path + strImage;
                        fileupload.file.CopyTo(filestream);
                        filestream.Flush();
                       // return "Upload Done";
                    }
                    int v_id = 0;
                    int sub_id = 0;
                   // string imeino = string.Empty;
                    
                    //ObjDashB.GetGeoLocation(EmpID, CmpID, "G", ref dtgeoloc);
                    //if (dtgeoloc.Rows.Count > 0)
                    //{
                    //    latitude = dtgeoloc.Rows[0]["Latitude"].ToString();
                    //    longitude = dtgeoloc.Rows[0]["Longitude"].ToString();
                    //}
                   string str =     ObjDashB.PostClockIN(EmpID, CmpID,Date,Date,IOFlag,IMEIno,Address, Latitude, Longitude, Reason, strDocPath, "I");




                        //ObjDashB.GetLastClockLocation(EmpID, CmpID, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("yyyy/MM/dd"), "C" ,ref dtt);
                        // objclsdataaccess.Directexecute(data);
                        //ObjAtttendance.AttendanceDetails(EmpID, CmpID, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("yyyy/MM/dd"), "C", ref dtAttendanceDetails);

                        if (str != null)
                        {


                        singleResponse.StatusCode = 200;
                        // response.msg = "Successfully";
                        singleResponse.msg = str.ToString().Split("#")[0].Replace("@", "");
                        //singleResponse.Status = Convert.ToBoolean(dsEmployeeUpdate.Tables[1].Rows[0][0].ToString().Split("#")[1]);
                        singleResponse.data = str.ToString().Split("#")[2];
                        //response.data = CommonClass.ToJson();
                        // response.additionaldetails= CommonClass.ToJson(dsdhDetails.Tables[5]);
                        Response = Ok(singleResponse);
                        }

                        else
                        {
                        singleResponse.StatusCode = 404;
                        singleResponse.msg = "Not Found";
                            //response.data = "";
                            Response = Ok(singleResponse);
                        }
                    }
                
                else
                {
                    singleResponse.StatusCode = 404;
                    singleResponse.msg = "Entered Company ID or Employee ID is wrong";
                    singleResponse.data = "";
                    Response = Ok(singleResponse);
                }
                return Response;
            }
            catch (Exception ex)
            {
                singleResponse.StatusCode = 500;
                singleResponse.msg = "Some thing went wrong.";
                singleResponse.data = "";

                logHelper.Error("DashboardDetails : " + ex.Message);
                throw;
            }
            finally
            {
                singleResponse = null;
            }
        }



        [HttpPost(nameof(AddClockOUT))]
        public IActionResult AddClockOUT(int EmpID, int CmpID, string Date, string IOFlag, string IMEIno, string Address, string Latitude, string Longitude, string Reason, [FromForm] Fileupload fileupload)

        {

            try
            {
                DataSet dsdhDetails = null;
                //DataTable dtdhDetails = null;
                string strDocPath = string.Empty;
                IActionResult Response = Unauthorized();
                DataTable dtt = null;
                DataTable dtgeoloc = null;
                string strImage = string.Empty;
                string folderName = string.Empty;
                ClsDataccess objclsdataaccess = new ClsDataccess(_config);
                //Attendance ObjAtttendance = new Attendance(_config);
                Dashboard ObjDashB = new Dashboard(_config);
                if (string.IsNullOrEmpty(Date))
                {
                    Date = DateTime.Now.ToString();
                }
                if ((EmpID != 0) && (CmpID != 0) && fileupload.file.Length > 0)
                {
                    strImage = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + EmpID + fileupload.file.FileName;
                    folderName = "/EmpImage/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.ToString("ddMMyyyy") + "/";


                    string path = _webhostenv.WebRootPath + "\\uploads\\" + folderName;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream filestream = System.IO.File.Create(path + strImage))
                    {
                        string dsFileName = "";

                        //dsFileName = Convert.ToString(EmpID) + "" + Convert.ToString(DateTime.Now).Replace("-", "").Replace(" ", "").Replace("/", "").Replace("+", "").Replace(":", "");
                        //dsFileName = dsFileName.Replace("|", "#");
                        //dsFileName =  fileupload.file.FileName;
                        strDocPath = path + strImage;
                        fileupload.file.CopyTo(filestream);
                        filestream.Flush();
                        // return "Upload Done";
                    }
                    int v_id = 0;
                    int sub_id = 0;
                    // string imeino = string.Empty;

                    //ObjDashB.GetGeoLocation(EmpID, CmpID, "G", ref dtgeoloc);
                    //if (dtgeoloc.Rows.Count > 0)
                    //{
                    //    latitude = dtgeoloc.Rows[0]["Latitude"].ToString();
                    //    longitude = dtgeoloc.Rows[0]["Longitude"].ToString();
                    //}
                    string str = ObjDashB.PostClockOUT(EmpID, CmpID, Date, Date, IOFlag, IMEIno, Address, Latitude, Longitude, Reason, strDocPath, "I");




                    //ObjDashB.GetLastClockLocation(EmpID, CmpID, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("yyyy/MM/dd"), "C" ,ref dtt);
                    // objclsdataaccess.Directexecute(data);
                    //ObjAtttendance.AttendanceDetails(EmpID, CmpID, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("yyyy/MM/dd"), "C", ref dtAttendanceDetails);

                    if (str != null)
                    {


                        singleResponse.StatusCode = 200;
                        // response.msg = "Successfully";
                        singleResponse.msg = str.ToString().Split("#")[0].Replace("@", "");
                        //singleResponse.Status = Convert.ToBoolean(dsEmployeeUpdate.Tables[1].Rows[0][0].ToString().Split("#")[1]);
                        singleResponse.data = str.ToString().Split("#")[2];
                        //response.data = CommonClass.ToJson();
                        // response.additionaldetails= CommonClass.ToJson(dsdhDetails.Tables[5]);
                        Response = Ok(singleResponse);
                    }

                    else
                    {
                        singleResponse.StatusCode = 404;
                        singleResponse.msg = "Not Found";
                        //response.data = "";
                        Response = Ok(singleResponse);
                    }
                }

                else
                {
                    singleResponse.StatusCode = 404;
                    singleResponse.msg = "Entered Company ID or Employee ID is wrong";
                    singleResponse.data = "";
                    Response = Ok(singleResponse);
                }
                return Response;
            }
            catch (Exception ex)
            {
                singleResponse.StatusCode = 500;
                singleResponse.msg = "Some thing went wrong.";
                singleResponse.data = "";

                logHelper.Error("DashboardDetails : " + ex.Message);
                throw;
            }
            finally
            {
                singleResponse = null;
            }
        }
        //public IActionResult AddClockOUT(int EmpID, int CmpID, string Date, string IOFlag, string IMEIno, string Address, string Latitude, string Longitude, string Reason, int VerticalID, int SubVerticalID, string SubVerticalName, [FromForm] Fileupload fileupload)

        //{

        //    try
        //    {
        //        DataSet dsdhDetails = null;
        //        DataTable dtdhDetails = null;
        //        string strDocPath = string.Empty;
        //        IActionResult Response = Unauthorized();
        //        DataTable dtt = null;
        //        DataTable dtgeoloc = null;
        //        string strImage = string.Empty;
        //        string folderName = string.Empty;
        //        ClsDataccess objclsdataaccess = new ClsDataccess(_config);
        //        Attendance ObjAtttendance = new Attendance(_config);
        //        Dashboard ObjDashB = new Dashboard(_config);
        //        if (string.IsNullOrEmpty(Date))
        //        {
        //            Date = DateTime.Now.ToString();
        //        }
        //        if ((EmpID != 0) && (CmpID != 0) && fileupload.file.Length > 0)
        //        {

        //            strImage = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + EmpID + "_" + fileupload.file.FileName;
        //            folderName = "~/EmpImage/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.ToString("ddMMyyyy") + "/";



        //            string path = _webhostenv.WebRootPath + "\\uploads\\" + folderName;
        //            if (!Directory.Exists(path))
        //            {
        //                Directory.CreateDirectory(path);
        //            }
        //            using (FileStream filestresam = System.IO.File.Create(path + fileupload.file.FileName))
        //            {
        //                string dsFileName = "";

        //                dsFileName = Convert.ToString(EmpID) + "" + Convert.ToString(DateTime.Now).Replace("-", "").Replace(" ", "").Replace("/", "").Replace("+", "").Replace(":", "");
        //                dsFileName = dsFileName.Replace("|", "#");
        //                dsFileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + EmpID + "_" + fileupload.file.FileName;
        //                strDocPath = path + dsFileName;
        //                fileupload.file.CopyTo(filestresam);
        //                filestresam.Flush();
        //                return "Upload Done";
        //            }
        //            int v_id = 0;
        //            int sub_id = 0;
        //            string imeino = string.Empty;

        //            ObjDashB.GetGeoLocation(EmpID, CmpID, "G", ref dtgeoloc);
        //            if (dtgeoloc.Rows.Count > 0)
        //            {
        //                latitude = dtgeoloc.Rows[0]["Latitude"].ToString();
        //                longitude = dtgeoloc.Rows[0]["Longitude"].ToString();
        //            }
        //            string str = ObjDashB.PostClockOUT(EmpID, CmpID, Date, Date, IOFlag, IMEIno, Address, Latitude, Longitude, Reason, VerticalID, SubVerticalID, SubVerticalName, strDocPath, "");




        //            ObjDashB.GetLastClockLocation(EmpID, CmpID, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("yyyy/MM/dd"), "C", ref dtt);
        //            objclsdataaccess.Directexecute(data);
        //            ObjAtttendance.AttendanceDetails(EmpID, CmpID, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("yyyy/MM/dd"), "C", ref dtAttendanceDetails);

        //            if (str != null)
        //            {


        //                response.StatusCode = 200;
        //                response.msg = "Successfully";
        //                response.msg = str.ToString().Split("#")[0].Replace("@", "");
        //                singleResponse.Status = Convert.ToBoolean(dsEmployeeUpdate.Tables[1].Rows[0][0].ToString().Split("#")[1]);
        //                response.data = str.ToString().Split("#")[2];
        //                response.data = CommonClass.ToJson();
        //                response.additionaldetails = CommonClass.ToJson(dsdhDetails.Tables[5]);
        //                Response = Ok(response);
        //            }

        //            else
        //            {
        //                response.StatusCode = 404;
        //                response.msg = "Not Found";
        //                response.data = "";
        //                Response = Ok(response);
        //            }
        //        }

        //        else
        //        {
        //            response.StatusCode = 404;
        //            response.msg = "Entered Company ID or Employee ID is wrong";
        //            response.data = "";
        //            Response = Ok(response);
        //        }
        //        return Response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.StatusCode = 500;
        //        response.msg = "Some thing went wrong.";
        //        response.data = "";

        //        logHelper.Error("DashboardDetails : " + ex.Message);
        //        throw;
        //    }
        //    finally
        //    {
        //        response = null;
        //    }
        //}














        //// POST: ClockINController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ClockINController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: ClockINController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ClockINController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: ClockINController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
