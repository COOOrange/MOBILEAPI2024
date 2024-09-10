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

    public class GeoLocation
    {
        public int? EmpID { get; set; }
        public int? CmpID { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string TrackingDate { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Area { get; set; }
        public string? Battery { get; set; }
        public string? GPS { get; set; }
        public string? IMEI { get; set; }
        public string? ModelName { get; set; }
    }
}
