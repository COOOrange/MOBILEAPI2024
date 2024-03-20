using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.ResponseDTO.Leave
{
    public class LeaveApplicationRecordsResponse
    {
        public List<ApplicationResponse> ApplicationResponses { get; set; }
        public List<CancellationResponse> CancellationResponses { get; set; }
    }
    public class ApplicationResponse
    {
        public int Row_ID { get; set; }
        public int Cmp_ID { get; set; }
        public int Leave_ID { get; set; }
        public int Emp_ID { get; set; }
        public string Emp_Full_Name { get; set; }
        public string Leave_Name { get; set; }
        public string Application_Code { get; set; }
        public string Application_Status { get; set; }
        public string Senior_Employee { get; set; }
        public int Leave_Application_ID { get; set; }
        public string Emp_first_name { get; set; }
        public string Emp_Code { get; set; }
        public string Branch_Name { get; set; }
        public string Desig_Name { get; set; }
        public string Alpha_Emp_code { get; set; }
        public string Leave_Reason { get; set; }
        public DateTime Application_Date { get; set; }
        public int Rpt_Level { get; set; }
        public int Scheme_ID { get; set; }
        public string Leave { get; set; }
        public int Final_Approver { get; set; }
        public int Is_Fwd_Leave_Rej { get; set; }
        public DateTime From_Date { get; set; }
        public DateTime To_Date { get; set; }
        public int Leave_Period { get; set; }
        public int is_pass_over { get; set; }
        public int Actual_leave_id { get; set; }
        public string Actual_cancel_wo_ho { get; set; }
        public int Branch_id { get; set; }
        public string Is_Backdated_Application { get; set; }
        public string Leave_Type { get; set; }
        public int Vertical_ID { get; set; }
        public int SubVertical_Id { get; set; }
        public int Dept_ID { get; set; }
        public string Dept_Name { get; set; }
        public string Leave_Application_Status { get; set; }
        public string In_Time { get; set; }
        public string Out_Time { get; set; }
        public string Image_Path { get; set; }
    }
    public class CancellationResponse
    {
        public int Tran_Id { get; set; }
        public int Cmp_ID { get; set; }
        public int Emp_ID { get; set; }
        public int Leave_Approval_ID { get; set; }
        public int Leave_ID { get; set; }
        public string For_Date { get; set; }
        public int IS_APPROVE { get; set; }
        public string MCOMMENT { get; set; }
        public int A_EMP_ID { get; set; }
        public string Leave_Name { get; set; }
        public int Leave_Application_ID { get; set; }
        public string Alpha_Emp_code { get; set; }
        public string Emp_Full_Name { get; set; }
        public string S_EMP_FULL_NAME { get; set; }
        public string From_Date { get; set; }
        public string To_Date { get; set; }
        public int Leave_Period { get; set; }
        public string Leave_Type { get; set; }
        public string Default_Short_Name { get; set; }
        public string Image_Path { get; set; }
    }
}
