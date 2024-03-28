using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DTO.RequestDTO.Attendance;
using MOBILEAPI2024.DTO.RequestDTO.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DAL.Repositories.IRepositories
{
    public interface IAttendanceRepository : IGenericRepository<ActiveInActiveUser>
    {
        dynamic AttendanceDetails(AttendanceDetails attendanceDetails1);
        dynamic AttendanceInsert(AttendanceInsert attendanceInsert);
        dynamic AttendanceRegularizeDetails(AttendanceRegularizeDetails attendanceRegularizeDetails);
        dynamic AttendanceRegularizeInsert(AttendanceRegularizeInsert attendanceRegularizeInsert);
    }
}
