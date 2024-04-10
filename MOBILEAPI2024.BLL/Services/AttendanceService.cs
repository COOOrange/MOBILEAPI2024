using AutoMapper;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.Common;
using MOBILEAPI2024.DTO.RequestDTO.Attendance;
using MOBILEAPI2024.DTO.RequestDTO.Employee;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.RequestDTO.User;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MOBILEAPI2024.BLL.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public AttendanceService(IMapper mapper,IAttendanceRepository attendanceRepository, IEmployeeRepository employeeRepository)
        {
            _attendanceRepository = attendanceRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public dynamic AllEmployeeAttendance(AllEmployeeAttendanceRequest allEmployeeAttendanceRequest)
        {
            AttendanceRegularizeDetails attendanceRegularizeDetails = new();
            attendanceRegularizeDetails.EmpID = 0;
            attendanceRegularizeDetails.CmpID = allEmployeeAttendanceRequest.CmpId;
            attendanceRegularizeDetails.Type = "O";
            attendanceRegularizeDetails.Fromdate = Convert.ToDateTime(allEmployeeAttendanceRequest.FromDate);
            attendanceRegularizeDetails.Todate = Convert.ToDateTime(allEmployeeAttendanceRequest.ToDate);
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
            attendanceDetails1.Type = "S";

            var atendanceResponse = _attendanceRepository.AttendanceDetails(attendanceDetails1);

            if (atendanceResponse != null)
            {
                return atendanceResponse;
            }
            return null;
        }

        public dynamic AttendanceHistory(AllEmployeeAttendanceRequest allEmployeeAttendanceRequest)
        {
            AttendanceDetails attendanceDetails1 = new();
            attendanceDetails1.EmpID = allEmployeeAttendanceRequest.EmpId;
            attendanceDetails1.CmpID = allEmployeeAttendanceRequest.CmpId;
            attendanceDetails1.Month = 0;
            attendanceDetails1.Year = 0;
            attendanceDetails1.FromDate = Convert.ToDateTime(allEmployeeAttendanceRequest.FromDate);
            attendanceDetails1.ToDate = Convert.ToDateTime(allEmployeeAttendanceRequest.ToDate);
            attendanceDetails1.Type = "H";

            var atendanceResponse = _attendanceRepository.AttendanceDetails(attendanceDetails1);

            if (atendanceResponse != null)
            {
                return atendanceResponse;
            }
            return null;
        }

        public dynamic AttendanceInsert(AttendanceInsertRequest attendanceInsertRequest)
        {
            var attendanceInsert = _mapper.Map<AttendanceInsertRequest, AttendanceInsert>(attendanceInsertRequest);
            attendanceInsert.Type = "I";
            var atendanceResponse = _attendanceRepository.AttendanceInsert(attendanceInsert);

            if (atendanceResponse != null)
            {
                return atendanceResponse;
            }
            return null;
        }

        public dynamic AttendanceInsertOffline(int cmpId, int empId, string strAttendance)
        {
            AttendanceInsert attendanceInsert = new AttendanceInsert();
            attendanceInsert.strAttendance = strAttendance;
            attendanceInsert.CmpID = cmpId;
            attendanceInsert.EmpID = empId;
            attendanceInsert.FromDate = Convert.ToDateTime(DateTime.Now).ToString();
            attendanceInsert.Todate = Convert.ToDateTime(DateTime.Now).ToString();
            attendanceInsert.Type = "O";
            var atendanceResponse = _attendanceRepository.AttendanceInsert(attendanceInsert);

            if (atendanceResponse != null)
            {
                return atendanceResponse;
            }
            return null;
        }

        public dynamic AttendanceMissedPunch(LeaveBalanceRequest attendanceMissedPunch)
        {
            var attendanceRegularizeDetails = _mapper.Map<LeaveBalanceRequest, AttendanceRegularizeDetails>(attendanceMissedPunch);
            attendanceRegularizeDetails.Type = "S";
            attendanceRegularizeDetails.Fromdate = DateTime.Now;
            attendanceRegularizeDetails.Todate = DateTime.Now;

            var atendanceResponse = _attendanceRepository.AttendanceRegularizeDetails(attendanceRegularizeDetails);

            if (atendanceResponse != null)
            {
                return atendanceResponse;
            }
            return null;
        }

        public dynamic AttendanceRegularizeApplicationRecord(int cmpId, int empId)
        {
            AttendanceRegularizeDetails attendanceRegularizeDetails = new();
            attendanceRegularizeDetails.EmpID = empId;
            attendanceRegularizeDetails.CmpID = cmpId;
            attendanceRegularizeDetails.Type = "P";
            attendanceRegularizeDetails.Fromdate = DateTime.Now;
            attendanceRegularizeDetails.Todate = DateTime.Now;
            var atendanceResponse = _attendanceRepository.AttendanceRegularizeDetails(attendanceRegularizeDetails);

            if (atendanceResponse != null)
            {
                return atendanceResponse;
            }
            return null;
        }

        public dynamic AttendanceRegularizeApproval(AttendanceRegularizeApprovalRequest attendanceRegularizeApprovalRequest)
        {
            if (string.IsNullOrEmpty(attendanceRegularizeApprovalRequest.Fordate))
            {
                attendanceRegularizeApprovalRequest.Fordate = DateTime.Now.ToString();
            }

            int IsApprove;
            if (attendanceRegularizeApprovalRequest.AppStatus == "A")
            {
                IsApprove = 1;
            }
            else if (attendanceRegularizeApprovalRequest.AppStatus == "R")
            {
                IsApprove = 2;
            }
            else
            {
                IsApprove = 0;
            }

            var attendanceRegularizeInsert = _mapper.Map<AttendanceRegularizeApprovalRequest, AttendanceRegularizeInsert>(attendanceRegularizeApprovalRequest);
            attendanceRegularizeInsert.Is_Approve = IsApprove;
            attendanceRegularizeInsert.IOTranId = attendanceRegularizeApprovalRequest.ApplicationID;
            attendanceRegularizeInsert.strType = "A";
            var atendanceResponse = _attendanceRepository.AttendanceRegularizeInsert(attendanceRegularizeInsert);

            if (atendanceResponse != null)
            {
                return atendanceResponse;
            }
            return null;
        }

        public dynamic AttendanceRegularizeDetails(LeaveBalanceRequest attendanceRegularizeDetail)
        {
            var attendanceRegularizeDetails = _mapper.Map<LeaveBalanceRequest, AttendanceRegularizeDetails>(attendanceRegularizeDetail);
            attendanceRegularizeDetails.Type = "S";
            attendanceRegularizeDetails.Fromdate = DateTime.Now;
            attendanceRegularizeDetails.Todate = DateTime.Now;

            var atendanceResponse = _attendanceRepository.AttendanceRegularizeDetails(attendanceRegularizeDetails);

            if (atendanceResponse != null)
            {
                return atendanceResponse;
            }
            return null;
        }

        public dynamic AttendanceRegularizeInsert(AttendanceRegularizeInsertRequest attendanceRegularizeInsertRequest)
        {
            var attendanceRegularizeInsert = _mapper.Map<AttendanceRegularizeInsertRequest, AttendanceRegularizeInsert>(attendanceRegularizeInsertRequest);
            attendanceRegularizeInsert.strType = "I";
            var atendanceResponse = _attendanceRepository.AttendanceRegularizeInsert(attendanceRegularizeInsert);

            if (atendanceResponse != null)
            {
                return atendanceResponse;
            }
            return null;
        }

        public dynamic AttendanceRoute(int cmpId, int empId, string strAttendance)
        {
            AttendanceInsert attendanceInsert = new AttendanceInsert();
            attendanceInsert.strAttendance = strAttendance;
            attendanceInsert.CmpID = cmpId;
            attendanceInsert.EmpID = empId;
            attendanceInsert.FromDate = Convert.ToDateTime(DateTime.Now).ToString();
            attendanceInsert.Todate = Convert.ToDateTime(DateTime.Now).ToString();
            attendanceInsert.Type = "R";
            var atendanceResponse = _attendanceRepository.AttendanceInsert(attendanceInsert);

            if (atendanceResponse != null)
            {
                return atendanceResponse;
            }
            return null;
        }

        public dynamic CheckINOUT(int cmpId, int empId)
        {
            AttendanceDetails attendanceDetails1 = new();
            attendanceDetails1.EmpID = empId;
            attendanceDetails1.CmpID = cmpId;
            attendanceDetails1.Month = DateTime.Now.Month;
            attendanceDetails1.Year = DateTime.Now.Year;
            attendanceDetails1.FromDate = DateTime.Now;
            attendanceDetails1.ToDate = DateTime.Now;
            attendanceDetails1.Type = "C";

            var atendanceResponse = _attendanceRepository.AttendanceDetails(attendanceDetails1);

            if (atendanceResponse != null)
            {
                return atendanceResponse;
            }
            return null;
        }

        public dynamic GetAttendanceRegularizeApplicationDetails(int applicationId)
        {
             AttendanceRegularizeDetails attendanceRegularizeDetails = new();
            attendanceRegularizeDetails.Type = "E";
            attendanceRegularizeDetails.Fromdate = DateTime.Now;
            attendanceRegularizeDetails.Todate = DateTime.Now;
            attendanceRegularizeDetails.IOTranId = applicationId;

            var atendanceResponse = _attendanceRepository.AttendanceRegularizeDetails(attendanceRegularizeDetails);

            if (atendanceResponse != null)
            {
                return atendanceResponse;
            }
            return null;
        }
        
    }
}
