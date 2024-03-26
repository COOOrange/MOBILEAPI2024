using AutoMapper;
using Google.Apis.Util;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.Common;
using MOBILEAPI2024.DTO.RequestDTO.Attendance;
using MOBILEAPI2024.DTO.RequestDTO.Employee;
using MOBILEAPI2024.DTO.ResponseDTO.Employee;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepository,IOptions<AppSettings> appSetting,IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _appSettings = appSetting.Value;
            _mapper = mapper;
        }

        public dynamic EmployeeDetails(int empId, int cmpId, string empCode)
        {
            EmployeeDetails empDetails = new EmployeeDetails();
            empDetails.Emp_ID = empId;
            empDetails.Cmp_ID = cmpId;
            empDetails.Emp_Code=empCode;
            empDetails.Type = "E";
            var employeeResponse = _employeeRepository.EmployeeDetails(empDetails);
            if(employeeResponse != null)
            {
                return employeeResponse;
            }
            return null;
        }

        public dynamic EmployeeDetailsForTally(int cmpId, string branchName)
        {
            var employeeResponse = _employeeRepository.EmployeeDetailsForTally(cmpId,branchName);
            if (employeeResponse != null)
            {
                return employeeResponse;
            }
            return null;
        }

        public dynamic EmployeeDirectoryData(int cmpId)
        {
            List<EmployeeDirectoryDataResponse> employeeResponse = _employeeRepository.EmployeeDirectoryData(cmpId);
            if (employeeResponse != null)
            {
                for (int i = 0; i < employeeResponse.Count(); i++)
                {
                    employeeResponse[0].Image_Path = _appSettings.ImagePath +"App_File/EMPIMAGES/" + employeeResponse[i].Image_Name;
                }

                return employeeResponse;
            }
            return null;
        }

        public dynamic EmployeeList(EmployeeListRequest employeeListRequest)
        {
            EmployeeDetails empDetails = new EmployeeDetails();
            empDetails.Emp_ID = employeeListRequest.EmpId;
            empDetails.Cmp_ID = employeeListRequest.CmpId;
            empDetails.Type = "A";
            var employeeResponse = _employeeRepository.EmployeeDetails(empDetails);
            if (employeeResponse != null)
            {
                return employeeResponse;
            }
            return null;
        }

        public dynamic ManagerApprovalDetails(ManagerApprovalDetailsRequest managerApprovalDetailsRequest)
        {
            var employeeResponse = _employeeRepository.ManagerApprovalDetails(managerApprovalDetailsRequest);
            if (employeeResponse != null)
            {
                return employeeResponse;
            }
            return null;
        }

        public dynamic MyTeamAttendance(int empId, int cmpId)
        {
            AttendanceRegularizeDetails attendanceRegularizeDetails = new();
            attendanceRegularizeDetails.EmpID = empId;
            attendanceRegularizeDetails.CmpID = cmpId;
            attendanceRegularizeDetails.Type = "T";
            attendanceRegularizeDetails.Fromdate = DateTime.Now;
            attendanceRegularizeDetails.Todate = DateTime.Now;
            var employeeResponse = _employeeRepository.AttendanceRegularizeDetails(attendanceRegularizeDetails);

            if (employeeResponse != null)
            {
                UpdateEmployeeDetails updateEmployeeDetails = new();
                updateEmployeeDetails.StrType = "I";
                for (int i = 0; i < employeeResponse.Count(); i++)
                {
                    
                    updateEmployeeDetails.EmpID = employeeResponse[i].Emp_Id;
                    updateEmployeeDetails.CmpID = employeeResponse[i].Cmp_ID;

                    string ImageName = _employeeRepository.UpdateEmployeeDetails(updateEmployeeDetails);
                    employeeResponse[i].Image_Path = _appSettings.ImagePath + "App_File/EMPIMAGES/" + ImageName;
                }
                return employeeResponse;
            }
            return null;
        }

        public dynamic MyTeamAttendanceInsert(MyTeamAttendanceInsertRequest myTeamAttendanceInsertRequest)
        {
            throw new NotImplementedException();
        }

        public dynamic MyTeamDetails(int empId, int cmpId, string status)
        {
            var employeeResponse = _employeeRepository.MyTeamDetails(empId,cmpId,status);
            if (employeeResponse != null)
            {
                return employeeResponse;
            }
            return null;
        }

        public dynamic NewJoiningEmployeeDetails(int cmpId)
        {
            var employeeResponse = _employeeRepository.NewJoiningEmployeeDetails(cmpId);
            if (employeeResponse != null)
            {
                return employeeResponse;
            }
            return null;
        }

        public dynamic UpdateEmpFavDetails(UpdateEmpFavDetailsRequest updateEmpFavDetailsRequest)
        {
            updateEmpFavDetailsRequest.Type = "U";
            var employeeResponse = _employeeRepository.UpdateEmpFavDetails(updateEmpFavDetailsRequest);
            if (employeeResponse != null)
            {
                return employeeResponse;
            }
            return null;
        }

        public dynamic UpdateEmployeeDetails(UpdateEmployeeDetailsRequest updateEmployeeDetailsRequest)
        {
            if (updateEmployeeDetailsRequest.Action == "")
            {
                updateEmployeeDetailsRequest.Type = "U";
                var employeeResponse = _employeeRepository.UpdateEmployeeDetailsMain(updateEmployeeDetailsRequest);
                if (employeeResponse != null)
                {
                    return employeeResponse;
                }
                return null;
            }
            else if(updateEmployeeDetailsRequest.Action == "Delete" || updateEmployeeDetailsRequest.Action == "delete")
            {
                updateEmployeeDetailsRequest.Type = "P";
                var employeeResponse = _employeeRepository.UpdateEmployeeDetailsMain(updateEmployeeDetailsRequest);
                if (employeeResponse != null)
                {
                    return employeeResponse;
                }
                return null;
            }
            return null;

            
        }
    }
}
