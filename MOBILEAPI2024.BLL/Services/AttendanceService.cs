using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.Common;
using MOBILEAPI2024.DTO.RequestDTO.Attendance;
using MOBILEAPI2024.DTO.RequestDTO.Employee;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.BLL.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public AttendanceService(IAttendanceRepository attendanceRepository, IEmployeeRepository employeeRepository)
        {
            _attendanceRepository = attendanceRepository;
            _employeeRepository = employeeRepository;
        }

        public dynamic AllEmployeeAttendance(AllEmployeeAttendanceRequest allEmployeeAttendanceRequest)
        {
            AttendanceRegularizeDetails attendanceRegularizeDetails = new();
            attendanceRegularizeDetails.EmpID = 0;
            attendanceRegularizeDetails.CmpID = allEmployeeAttendanceRequest.CmpId;
            attendanceRegularizeDetails.Type = "O";
            attendanceRegularizeDetails.Fromdate = allEmployeeAttendanceRequest.FromDate;
            attendanceRegularizeDetails.Todate = allEmployeeAttendanceRequest.ToDate;
            var atendanceResponse = _attendanceRepository.AttendanceRegularizeDetails(attendanceRegularizeDetails);

            if (atendanceResponse != null)
            {
                return atendanceResponse;
            }
            return null;
        }

        public dynamic AttendanceDetails(LeaveBalanceRequest attendanceDetails)
        {
            AttendanceDetails attendanceDetails1 = new();
            attendanceDetails1.EmpID = attendanceDetails.EmpId;
            attendanceDetails1.CmpID = attendanceDetails.CmpId;
            attendanceDetails1.Month = attendanceDetails.Month;
            attendanceDetails1.Year = attendanceDetails.Year;
            attendanceDetails1.FromDate = DateTime.Now;
            attendanceDetails1.ToDate = DateTime.Now;
            attendanceDetails1.Year = attendanceDetails.Year;
            attendanceDetails1.Type = "S";

            var atendanceResponse = _attendanceRepository.AttendanceDetails(attendanceDetails1);

            if (atendanceResponse != null)
            {
                return atendanceResponse;
            }
            return null;
        }
    }
}
