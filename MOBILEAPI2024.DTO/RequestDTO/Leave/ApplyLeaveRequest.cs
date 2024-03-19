using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Leave
{
    public class ApplyLeaveRequest
    {
        [Required]
        public int Leave_ID { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        [Required]
        public int Period { get; set; }
        [Required]
        public string Comment { get; set; }
        public string Leave_Assign_As { get; set; }
    }
}
