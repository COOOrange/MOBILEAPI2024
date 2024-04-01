using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.User
{
    public class GeoLocationRequest
    {
        [Required]
        public int EmpID { get; set; }
        [Required]
        public int CmpID { get; set; }

        [Required]
        public float Latitude { get; set; }
        [Required]
        public float Longitude { get; set; }
        [Required]
        public string AddressLocation { get; set; }
    }
}
