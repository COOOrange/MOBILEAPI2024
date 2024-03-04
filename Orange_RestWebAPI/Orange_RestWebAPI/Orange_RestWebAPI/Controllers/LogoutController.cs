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
namespace Orange_RestWebAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class LogoutController : Controller
    {
        private IConfiguration _config;
        MyClassToken<LoginModel> response = new MyClassToken<LoginModel>();
        MyClass<LoginModel> singleResponse = new MyClass<LoginModel>();
        LogHelper logHelper = new LogHelper();
        //private IHttpContextAccessor _accessor;
        public LogoutController(IConfiguration config)
        {
            _config = config;
        }

        [Authorize]
        [HttpPost("Logout")]

        public ActionResult Logout(string logintoken)
        {
            DataTable dsLogin = null;

            LoginDetails ObjDashboard = new LoginDetails(_config);

            //Validate the User Credentials  
            ObjDashboard.LogOutDeleteToken(logintoken, ref dsLogin);

            if (dsLogin.Rows.Count > 0 && dsLogin != null && dsLogin.Rows[0][0].ToString().Contains("Logout Successfully"))
            {


                return Ok(new { message = "Logout Successfully" });
            }
            else
            {
                return BadRequest(new { message = "Logout Failed" });
            }
           
        }
           
        }
    }
        // GET: LogoutController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: LogoutController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: LogoutController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: LogoutController/Create
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

        //// GET: LogoutController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: LogoutController/Edit/5
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

        //// GET: LogoutController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: LogoutController/Delete/5
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
    

