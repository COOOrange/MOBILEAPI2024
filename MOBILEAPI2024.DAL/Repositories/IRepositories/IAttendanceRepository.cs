using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DTO.RequestDTO.Attendance;
using MOBILEAPI2024.DTO.RequestDTO.Employee;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.ResponseDTO.Attendance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DAL.Repositories.IRepositories
{
    public interface IAttendanceRepository : IGenericRepository<ActiveInActiveUser>
    {
        dynamic AttendanceDetails(AttendanceDetails attendanceDetails1);
        dynamic AttendanceInsert(AttendanceInsert attendanceInsert);
        AttendanceRecordRespomse AttendanceRegularizeDetails(AttendanceRegularizeDetails attendanceRegularizeDetails);
        dynamic AttendanceRegularizeInsert(AttendanceRegularizeInsert attendanceRegularizeInsert);
        int GetMaxapp(LeaveBalanceRequest attendanceRegularizeDetail);
        int GetMaxappEndDate(DateTime st_date, DateTime end_date, LeaveBalanceRequest attendanceRegularizeDetail);
        dynamic GetsalaryEndDate(DateTime st_date);
        DataTable GetsalaryStartDate(DateTime st_date, LeaveBalanceRequest attendanceRegularizeDetail);
    }
}
