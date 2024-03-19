using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.User
{
    public class ClockIn
    {
        public int? Emp_Id { get; set; }
        public int? Cmp_Id { get; set; }
        public DateTime? Date { get; set; }
        public string? IOFlag { get; set; }
        public string? IMEIno { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? Latitude { get; set; }
        [Required]
        public string? Longitude { get; set; }
        [Required]
        public string? Reason { get; set; }
        [Required]
        public IFormFile? file { get; set; }
    }
    public class ClockInRequest
    {
        public IFormFile file { get; set; }
        public ClockIn clockIn { get; set; }
    }
}
