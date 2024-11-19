namespace MOBILEAPI2024.DTO.ResponseDTO.Leave
{
    public class LeaveApplicationResponse
    {
        public LeaveApplication leaveApplication { get; set; }
        public LeaveApplicationData leaveApplicationData { get; set; }
    }
    public class LeaveApplication
    {
        public string From_Date { get; set; }
        public string To_Date { get; set; }
        public decimal Leave_Period { get; set; }
        public string Comment { get; set; }
        public string Application_Status { get; set; }
        public string System_Date { get; set; }
        public string Rpt_Level { get; set; }
    }
    public class LeaveApplicationData
    {
        public string EmployeeFullName { get; set; }
        public string EmployeeName { get; set; }
        public decimal Emp_ID { get; set; }
        public decimal Cmp_ID { get; set; }
        public decimal S_Emp_ID { get; set; }
        public decimal Leave_Application_ID { get; set; }
        public string Application_Date { get; set; }
        public string Application_Code { get; set; }
        public string Application_Status { get; set; }
        public string Application_Comments { get; set; }
        public decimal Leave_ID { get; set; }
        public string Leave_Name { get; set; }
        public string Leave_Code { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public double Leave_Period { get; set; }
        public string Leave_Assign_As { get; set; }
        public string Half_Leave_Date { get; set; }
        public string Leave_Reason { get; set; }
        public decimal NightHalt { get; set; }
        public string leave_Out_time { get; set; }
        public string leave_In_time { get; set; }
        public string Image_Name { get; set; }
        public string Image_Path { get; set; }
        public string Leave_CompOff_Dates { get; set; }
        public string Leave_Type { get; set; }
    }

    public class LeaveResponse
    {
        public  string Result { get; set; }
    }

    public class LeaveAPIResponse
    {
    
            public string ApplicationDate { get; set; }
            public int ApplicationCode { get; set; }
            public string EmpFullName { get; set; }
            public string DesigName { get; set; }
            public string DeptName { get; set; }
            public string LeaveName { get; set; }
            public string FromDate { get; set; } // Includes both date and day
            public string ToDate { get; set; }   // Includes both date and day
            public double LeavePeriod { get; set; }
            public string LeaveAssignAs { get; set; }
            public string LeaveReason { get; set; }
            public int LeaveStatus { get; set; }
            public long MobileNo { get; set; }
            public int LeaveApplicationID { get; set; }
    }


    public class MasterLeaveResponse
    { 
        public LeaveResponse LeaveResponse { get; set; }
        public LeaveAPIResponse LeaveAPIResponse { get; set; }
    }

}
