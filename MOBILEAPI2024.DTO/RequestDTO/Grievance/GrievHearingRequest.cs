﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Grievance
{
    public class GrievHearingRequest
    {
        public int EmpID { get; set; }
        public int CmpID { get; set; }
        public string From_Date { get; set; }
        public string To_Date { get; set; }
    }
}
