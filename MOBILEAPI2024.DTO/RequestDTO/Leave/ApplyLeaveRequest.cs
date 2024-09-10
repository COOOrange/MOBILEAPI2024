using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Leave
{
    public class ApplyLeaveRequest
    {
        public int? LeavAppID { get; set; }
        public int? LeaveID { get; set; }
        private DateTime? fromDate;
        public DateTime? FromDate
        {
            get => fromDate ?? new DateTime(2000, 1, 1); // Static default date
            set => fromDate = value;
        }
        public decimal? Period { get; set; }
        private DateTime? toDate;
        public DateTime? Todate
        {
            get => toDate ?? new DateTime(2000, 1, 1); // Static default date
            set => toDate = value;
        }
        public string? AssignAs { get; set; }
        public string? Comment { get; set; }
        private DateTime? hLeaveDate;
        public DateTime? HLeaveDate
        {
            get => hLeaveDate ?? new DateTime(2000, 1, 1); // Static default date
            set => hLeaveDate = value;
        }

        private DateTime? intime;
        public DateTime? Intime
        {
            get => intime ?? new DateTime(2000, 1, 1); // Static default date
            set => intime = value;
        }

        private DateTime? outTime;
        public DateTime? OutTime
        {
            get => outTime ?? new DateTime(2000, 1, 1); // Static default date
            set => outTime = value;
        }

        public int? LoginID { get; set; }
        public string? Attachement { get; set; }
        public string? DocName { get; set; }
        public string? CompoffLeaveDates { get; set; }
        public string? StrType { get; set; }

    }
}
