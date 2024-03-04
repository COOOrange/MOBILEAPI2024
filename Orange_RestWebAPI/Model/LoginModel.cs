using System.ComponentModel.DataAnnotations;

namespace Orange_RestWebAPI.Model
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        //[JsonProperty("imeino")]
        public string IMEINo { get; set; }

        //[JsonProperty("deviceid")]
        public string DeviceID { get; set; }
    }
}
