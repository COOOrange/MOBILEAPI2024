﻿using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.ResponseDTO.Leave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.BLL.Services.IServices
{
    public interface ILeaveService
    {
        string AddLeaveAplication(LeaveFilter leaveFilter, ApplyLeaveRequest applyLeaveRequest);
        LeaveApplicationResponse CheckLeaveStatus(LeaveFilter leaveFilter, int leaveAppID);
        string CheckPeriod(LeaveFilter leaveFilter,CheckPeriod checkPeriod);
        List<FilterLeaveResponse> GetLeaveBalance(LeaveFilter leaveFilter);
        List<LeaveStatusResponse> GetLeaveStatus(LeaveFilter leaveFilter);
        List<LeaveTypeBind> GetLeaveTypeBind(LeaveFilter leaveFilter);
    }
}
