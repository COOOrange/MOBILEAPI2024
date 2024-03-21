using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Leave
{
    public class GetLeaveTransactionRequest
    {
        [Required]
        public int Month { get; set; }
        [Required]
        public int Year { get; set; }
        public int CmpId { get; set; }
        public string EmpCode { get; set; }
    }
}
