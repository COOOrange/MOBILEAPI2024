using MOBILEAPI2024.DTO.RequestDTO.Attendance;
using MOBILEAPI2024.DTO.RequestDTO.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.BLL.Services.IServices
{
    public interface IEmployeeService
    {
        dynamic EmployeeDetails(int empId, int cmpId, string empCode);
        dynamic EmployeeDetailsForTally(int cmpId, string branchName);
        dynamic EmployeeDirectoryData(int cmpId);
        dynamic EmployeeList(EmployeeListRequest employeeListRequest);
        dynamic ManagerApprovalDetails(ManagerApprovalDetailsRequest managerApprovalDetailsRequest);
        dynamic MyTeamAttendance(int empId, int cmpId);
        dynamic MyTeamAttendanceInsert(MyTeamAttendanceInsertRequest myTeamAttendanceInsertRequest);
        dynamic MyTeamDetails(int empId, int cmpId,string status);
        dynamic NewJoiningEmployeeDetails(int cmpId);
        dynamic UpdateEmpFavDetails(UpdateEmpFavDetailsRequest updateEmpFavDetailsRequest);
        dynamic UpdateEmployeeDetails(UpdateEmployeeDetailsRequest updateEmployeeDetailsRequest);
    }
}
