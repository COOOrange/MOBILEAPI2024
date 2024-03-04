using System.Net;
using System.Net.Http;
//using System.Web.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
namespace Orange_RestWebAPI.Controllers
{
    [Authorize] // This attribute checks if the user is authenticated
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // Check if the user is authenticated
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                // Your API logic goes here
                var data = "Authenticated user data";
                return Ok(data);
            }
            else
            {
                // If not authenticated, return 401 Unauthorized status code
                return Unauthorized("Unauthorized request");
            }
        }
    }
}
