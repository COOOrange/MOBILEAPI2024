using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.ResponseDTO.Attendance
{
    public class AttendanceRecordRespomse
    {
        public List<AttendanceRecord> attendanceRecords {  get; set; }
        public List<AttendanceData> attendanceDatas { get; set; }
        public List<MonthDateRange> monthDateRanges { get; set; }
        public List<AttendanceSettings> attendanceSettings { get; set; }
        public List<SettingData> settingDatas { get; set; }
        public string Result { get; set; }
    }


    public class AttendanceData
    {
        public int Emp_Id { get; set; }
        public decimal Present { get; set; }
        public decimal WO { get; set; }
        public decimal HO { get; set; }
        public decimal OD { get; set; }
        public decimal Absent { get; set; }
        public decimal Leave { get; set; }
        public decimal Total { get; set; }
        public decimal D_Present { get; set; }
    }


    public class AttendanceSettings
    {
        public double Setting_Value { get; set; } // Setting_Value
        public int Attndnc_Reg_Max_Cnt { get; set; } // Attndnc_Reg_Max_Cnt
        public DateTime Sal_St_Date { get; set; } // Sal_St_Date
    }


    public class SettingData
    {
        public int Setting_ID { get; set; }
        public int Cmp_ID { get; set; }
        public string Setting_Name { get; set; }
        public int Setting_Value { get; set; }
        public string Comment { get; set; }
        public string Group_By { get; set; }
        public string Alias { get; set; }
        public string Module_Name { get; set; }
        public int Value_Type { get; set; }
        public string Value_Ref { get; set; }
    }

    public class MonthDateRange
    {
        public DateTime Month_St_Date { get; set; }
        public DateTime Month_End_Date { get; set; }
    }
}
