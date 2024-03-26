using MOBILEAPI2024.DTO.RequestDTO.Attendance;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.BLL.Services.IServices
{
    public interface IAttendanceService
    {
        dynamic AllEmployeeAttendance(AllEmployeeAttendanceRequest allEmployeeAttendanceRequest);
        dynamic AttendanceDetails(LeaveBalanceRequest attendanceDetails);
    }
}
