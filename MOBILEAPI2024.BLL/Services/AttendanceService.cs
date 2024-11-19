using AutoMapper;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.Common;
using MOBILEAPI2024.DTO.RequestDTO.Attendance;
using MOBILEAPI2024.DTO.RequestDTO.Employee;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.RequestDTO.User;
using MOBILEAPI2024.DTO.ResponseDTO.Attendance;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MOBILEAPI2024.BLL.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public AttendanceService(IMapper mapper, IAttendanceRepository attendanceRepository, IEmployeeRepository employeeRepository)
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
            attendanceInsert.FromDate = Convert.ToDateTime(DateTime.Now).ToString();
            attendanceInsert.Todate = Convert.ToDateTime(DateTime.Now).ToString();
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
            // Define variables needed in the method's context
            int SettingValue = 0;
            double RestrictDate = 0.0;
            int AppCount = 0;
            string SalaryStartDate = string.Empty;
            string SalaryEndDate = string.Empty;
            int Count_Ttl_App = 0;
            DateTime EnableStartDate = DateTime.MinValue;
            DateTime EnableEndDate = DateTime.MinValue;
            string StartDate = string.Empty;
            string EndDate = string.Empty;

            // Map the input request
            var attendanceRegularizeDetails = _mapper.Map<LeaveBalanceRequest, AttendanceRegularizeDetails>(attendanceRegularizeDetail);
            attendanceRegularizeDetails.Type = "S";
            attendanceRegularizeDetails.Fromdate = DateTime.Now;
            attendanceRegularizeDetails.Todate = DateTime.Now;

            // Get attendance response from repository
            var atendanceResponse = _attendanceRepository.AttendanceRegularizeDetails(attendanceRegularizeDetails);

            // Check if atendanceResponse is a DataSet
            if (atendanceResponse != null)
            {
                // Access tables safely knowing atendanceResponse is a DataSet
                if (atendanceResponse.attendanceRecords.Count() > 0)
                {
                    // Process salary dates and other details based on the response tables
                    if (atendanceResponse.attendanceSettings.Count() > 0)
                    {
                        SalaryStartDate = atendanceResponse.monthDateRanges[0].Month_St_Date.ToString();
                        SalaryEndDate = atendanceResponse.monthDateRanges[0].Month_End_Date.ToString();
                    }
                    else if (atendanceResponse.settingDatas.Count() > 0)
                    {
                        DateTime st_date = Convert.ToDateTime(atendanceResponse.attendanceSettings[0].Sal_St_Date);
                        DataTable sal_strt_date = _attendanceRepository.GetsalaryStartDate(st_date, attendanceRegularizeDetail);

                        if (sal_strt_date.Rows.Count > 0)
                        {
                            st_date = Convert.ToDateTime(sal_strt_date.Rows[0]["mnth_st_dt"]);
                            StartDate = st_date.ToString();

                            if (st_date.ToString().StartsWith("01/"))
                            {
                                var get_Max_Apps = _attendanceRepository.GetMaxapp(attendanceRegularizeDetail);
                                if (get_Max_Apps > 0)
                                {
                                    Count_Ttl_App = Convert.ToInt32(get_Max_Apps);
                                }

                                var dt_End_date = _attendanceRepository.GetsalaryEndDate(st_date);
                                if (dt_End_date.Rows.Count > 0)
                                {
                                    DateTime end_date = Convert.ToDateTime(dt_End_date.Rows[0]["end_dt"]);
                                    EndDate = end_date.ToString();
                                    get_Max_Apps = _attendanceRepository.GetMaxappEndDate(st_date, end_date, attendanceRegularizeDetail);
                                    if (get_Max_Apps > 0)
                                    {
                                        Count_Ttl_App = Convert.ToInt32(get_Max_Apps);
                                    }
                                }
                            }
                        }
                    }

                    RestrictDate = Convert.ToDouble(atendanceResponse.settingDatas[0].Setting_Value);
                    AppCount = string.IsNullOrEmpty(atendanceResponse.attendanceSettings[0].Attndnc_Reg_Max_Cnt.ToString())
                        ? 0 : Convert.ToInt32(atendanceResponse.attendanceSettings[0].Attndnc_Reg_Max_Cnt);
                    SettingValue = string.IsNullOrEmpty(atendanceResponse.settingDatas[0].Setting_Value.ToString())
                        ? 0 : Convert.ToInt32(atendanceResponse.settingDatas[0].Setting_Value);

                    EnableStartDate = DateTime.Now.AddDays(-RestrictDate);
                    EnableEndDate = DateTime.Now;

                    var Get_Max_AppliedCount = _attendanceRepository.GetMaxapp(attendanceRegularizeDetail);
                    if (Get_Max_AppliedCount > 0)
                    {
                        Count_Ttl_App = Convert.ToInt32(Get_Max_AppliedCount);
                    }

                    foreach (AttendanceRecord row in atendanceResponse.attendanceRecords)
                    {
                        bool isEnable = false;
                        string strColor;

                        var Fordate = row.For_Date.ToString() ?? "";
                        var Status = row.Main_Status?.ToString() ?? "";
                        var LateIn = row.Late_Minutes.ToString() ?? "";
                        var EarlyOut = row.Early_Limit?.ToString() ?? "";
                        var LateMark = row.Emp_Late_Mark.ToString() ?? "";
                        var EarlyMark = row.Emp_Early_Mark.ToString() ?? "";
                        var LateTime = row.Late_Time.ToString() ?? "";
                        var EarlyTime = row.Early_Time.ToString() ?? "";
                        var ChkSuperior = "";
                        if (row.Chk_By_Superior != null)
                        {
                           ChkSuperior = row.Chk_By_Superior.ToString();
                        }
                        var LeaveCount = row.Leave_Count?.ToString() ?? "";
                        var P_Day = row.P_Days.ToString() ?? "";

                        if (SettingValue == 0 && string.IsNullOrEmpty(row.Status.ToString()) && string.IsNullOrEmpty(row.Status_2.ToString()))
                        {
                            isEnable = false;
                        }
                        else if (Convert.ToDateTime(Fordate) < DateTime.Now.Date)
                        {
                            isEnable = DetermineEnableStatus(row, Fordate, SalaryStartDate, SalaryEndDate, ChkSuperior, LateMark, EarlyMark, LateIn, LateTime, EarlyOut, EarlyTime, RestrictDate, P_Day, LeaveCount, Status, AppCount, EnableStartDate, Count_Ttl_App);
                        }

                        strColor = GetRowColor(Status, P_Day);
                        row.RowStatus = isEnable;
                        row.RowColor = strColor;
                    }

                    return atendanceResponse.attendanceRecords;
                }
            }
            return "No Data Available";
        }

        // Helper methods for enabling status and color
        private bool DetermineEnableStatus(AttendanceRecord row,string  Fordate, string salaryStartDate, string salaryEndDate, string chkSuperior, string LateMark,string EarlyMark, string lateIn, string lateTime, string earlyOut, string earlyTime, double restrictDate, string pDay, string leaveCount,string Status,int AppCount, DateTime EnableStartDate, int Count_Ttl_App)
        {
            bool isEnable = true;

            if (Convert.ToDateTime(Fordate) < DateTime.Now.Date)
            {
                if (!string.IsNullOrEmpty(salaryStartDate) && !string.IsNullOrEmpty(salaryEndDate))
                {
                    if (Convert.ToDateTime(Fordate) >= Convert.ToDateTime(salaryStartDate) && Convert.ToDateTime(Fordate) <= Convert.ToDateTime(salaryEndDate))
                    {
                        isEnable = false;
                    }
                    else
                    {
                        isEnable = true;
                    }
                }
                else if ((chkSuperior == "Pending" || chkSuperior == "" || chkSuperior == "Approved") && leaveCount != "1.0" && Convert.ToDateTime(Fordate) != DateTime.Now.Date && restrictDate <= 0.0)
                {
                    if ((LateMark == "1" && pDay == "1.0") || pDay == "0.5")
                    {
                        if (Convert.ToInt32(lateIn) > Convert.ToInt32(lateTime))
                        {
                            isEnable = true;
                        }
                    }

                    if ((EarlyMark == "1" && pDay == "1.0") || pDay == "0.5")
                    {
                        if (Convert.ToInt32(earlyOut) > Convert.ToInt32(earlyTime))
                        {
                            isEnable = true;
                        }
                    }

                    if (pDay == "0.0" && leaveCount == "0")
                    {
                        isEnable = true;
                    }

                    if (chkSuperior == "Approved" || chkSuperior == "Rejected")
                    {
                        isEnable = true;
                    }

                    if (Status == "A" || Status == "W")
                    {
                        isEnable = true;
                    }
                }
                else
                {
                    if (chkSuperior == "Rejected")
                    {
                        isEnable = true;
                    }
                    else
                    {
                        isEnable = false;
                    }
                }

                if (restrictDate > 0.0)
                {
                    if (Convert.ToDateTime(Fordate) >= EnableStartDate)
                    {
                        isEnable = true;
                    }
                    else
                    {
                        isEnable = false;
                    }
                }
            }
            else
            {
                isEnable = false;
            }

            if (AppCount != 0)
            {
                if (chkSuperior == "" && Count_Ttl_App >= AppCount)
                {
                    isEnable = false;
                }
            }




            // Implement conditions for enabling row (similar to above logic)
            return isEnable;
        }

        private string GetRowColor(string status, string pDay)
        {
            switch (status)
            {
                case "":
                    return pDay == "0.5" ? "HalfDay:#e4f5da" : "Next:#8a6d3b";
                case "P":
                    return "Present:#d0e9c6";
                case "A":
                    return "Absent:#ebcccc";
                case "L":
                    return "Leave:#faf2cc";
                case "W":
                    return "WeekOff:#d9edf7";
                case "HO":
                    return "Holiday:#e0e0e0";
                case "OD":
                    return "OnDuty:#04C9D3";
                default:
                    return "Next:#8a6d3b";
            }
        }



        //public dynamic AttendanceRegularizeDetails(LeaveBalanceRequest attendanceRegularizeDetail)
        //{
        //    var attendanceRegularizeDetails = _mapper.Map<LeaveBalanceRequest, AttendanceRegularizeDetails>(attendanceRegularizeDetail);
        //    attendanceRegularizeDetails.Type = "S";
        //    attendanceRegularizeDetails.Fromdate = DateTime.Now;
        //    attendanceRegularizeDetails.Todate = DateTime.Now;

        //    var atendanceResponse = _attendanceRepository.AttendanceRegularizeDetails(attendanceRegularizeDetails);



        //    if (atendanceResponse != null)
        //    {
        //        return atendanceResponse;
        //    }
        //    return null;
        //}

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
