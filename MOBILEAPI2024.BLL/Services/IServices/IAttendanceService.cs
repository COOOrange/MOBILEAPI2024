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
        dynamic AttendanceHistory(AllEmployeeAttendanceRequest allEmployeeAttendanceRequest);
        dynamic AttendanceInsert(AttendanceInsertRequest attendanceInsertRequest);
        dynamic AttendanceInsertOffline(int cmpId, int empId, string strAttendance);
        dynamic AttendanceMissedPunch(LeaveBalanceRequest attendanceMissedPunch);
        dynamic AttendanceRegularizeApplicationRecord(int v1, int v2);
        dynamic AttendanceRegularizeApproval(AttendanceRegularizeApprovalRequest attendanceRegularizeApprovalRequest);
        dynamic AttendanceRegularizeDetails(LeaveBalanceRequest attendanceRegularizeDetails);
        dynamic AttendanceRegularizeInsert(AttendanceRegularizeInsertRequest attendanceRegularizeInsertRequest);
        dynamic AttendanceRoute(int v1, int v2, string strAttendance);
        dynamic CheckINOUT(int v1, int v2);
        dynamic GetAttendanceRegularizeApplicationDetails(int applicationId);
    }
}
