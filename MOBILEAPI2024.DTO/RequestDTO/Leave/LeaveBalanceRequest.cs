using System.ComponentModel.DataAnnotations;

namespace MOBILEAPI2024.DTO.RequestDTO.Leave
{
    public class LeaveBalanceRequest
    {
        [Required]
        public int Month { get; set; }
        [Required]
        public int Year { get; set; }
        public int? EmpId { get; set; }
        public int? CmpId { get; set; }
    }
}
