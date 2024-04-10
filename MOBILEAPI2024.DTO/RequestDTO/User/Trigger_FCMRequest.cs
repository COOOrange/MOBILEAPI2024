using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.User
{
    public class Trigger_FCMRequest
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string DeviceID { get; set; }
    }
}
