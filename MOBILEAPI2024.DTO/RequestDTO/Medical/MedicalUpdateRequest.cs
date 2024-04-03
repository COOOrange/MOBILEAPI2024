using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Medical
{
    public class MedicalUpdateRequest
    {
        public int AppID { get; set; }
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public int AppFor { get; set; }
        public int CmpID { get; set; }
        public string AppDate { get; set; }
        public string HospName { get; set; }
        public int StateId { get; set; }
        public string CityName { get; set; }
        public int IncidentId { get; set; }
        public string IncidentPlace { get; set; }
        public string HospAddr { get; set; }
        public string DateOfInc { get; set; }
        public string TimeofInc { get; set; }
        public string ContNo1 { get; set; }
        public string ContNo2 { get; set; }
        public string EmailId { get; set; }
        public string DescDetails { get; set; }
        public string DepDetail { get; set; }
        public string OtherNote { get; set; }
        public int CreatedBy { get; set; }
    }
}
