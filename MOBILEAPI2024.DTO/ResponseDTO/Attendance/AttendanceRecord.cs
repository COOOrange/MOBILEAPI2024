using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.ResponseDTO.Attendance
{
    public class AttendanceRecord
    {
        public int Emp_Id { get; set; }
        public int Cmp_ID { get; set; }
        public int Branch_ID { get; set; }
        public string For_Date { get; set; }
        public string Status { get; set; }
        public string Leave_code { get; set; }
        public string Leave_Count { get; set; }
        public string OD { get; set; }
        public string OD_Count { get; set; }
        public string WO_HO { get; set; }
        public string Status_2 { get; set; }
        public int Row_ID { get; set; }
        public string In_Date { get; set; }
        public string? Out_Date { get; set; }
        public int Shift_ID { get; set; }
        public string Shift_Name { get; set; }
        public string Sh_In_Time { get; set; }
        public string Sh_Out_Time { get; set; }
        public string Holiday { get; set; }
        public string Late_Limit { get; set; }
        public string Reason { get; set; }
        public string Half_Full_Day { get; set; }
        public string? Chk_By_Superior { get; set; }
        public string Sup_Comment { get; set; }
        public int Is_Cancel_Late_In { get; set; }
        public int Is_Cancel_Early_Out { get; set; }
        public string Early_Limit { get; set; }
        public string Main_Status { get; set; }
        public string Detail_Status { get; set; }
        public int Is_Late_Calc_On_HO_WO { get; set; }
        public int Is_Early_Calc_On_HO_WO { get; set; }
        public int Late_Minute { get; set; }
        public int Early_Minute { get; set; }
        public int Is_Leave_App { get; set; }
        public string Other_Reason { get; set; }
        public int R_Emp_ID { get; set; }
        public int Att_Approval_Days { get; set; }
        public int Att_App_ID { get; set; }
        public string Att_Apr_Status { get; set; }
        public string Shift_Duration { get; set; }
        public int OT_Apr_ID { get; set; }
        public int Comp_Off_App { get; set; }
        public int Comp_Off_Apr { get; set; }
        public int OT_Applicable { get; set; }
        public string Display_Birth { get; set; }
        public string Display_Marriage_Date { get; set; }
        public string Date_of_Join { get; set; }
        public string? Left_Date { get; set; }
        public string ExFlag { get; set; }
        public int Late_Time { get; set; }
        public int Early_Time { get; set; }
        public int Late_Minutes { get; set; }
        public int Early_Out { get; set; }
        public string Alpha_Emp_Code { get; set; }
        public string Emp_Full_Name { get; set; }
        public string Branch_Name { get; set; }
        public string Dept_Name { get; set; }
        public string Grd_Name { get; set; }
        public string Desig_Name { get; set; }
        public string Branch_Address { get; set; }
        public string Comp_Name { get; set; }
        public string DBRD_Code { get; set; }
        public int P_Days { get; set; }
        public int Emp_Late_Mark { get; set; }
        public int Emp_Early_Mark { get; set; }
        public string Disable_Comment { get; set; }
        public int Grd_ID { get; set; }
        public int R_Emp_ID1 { get; set; }
        public string Working_Hour { get; set; }
        public int Working_Sec { get; set; }
        public int Req_For_App { get; set; }
        public string Slab_Shift_Hours { get; set; }
        public int Week_Off_OT { get; set; }
        public string ExFlag1 { get; set; }
        public bool RowStatus { get; set; }
        public string RowColor { get; set; }
    }
}
