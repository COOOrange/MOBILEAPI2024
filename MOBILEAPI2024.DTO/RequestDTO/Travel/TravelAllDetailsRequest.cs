using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Travel
{
    public class TravelAllDetailsRequest
    {
        public int TravelApplicationID { get; set; }
        public char StringType { get; set; }
        public int CmpID { get; set; }
        public int EmpID { get; set; }
        public int SEmpID { get; set; }
        public string TravelDetails { get; set; }
        public string TravelOtherDetails { get; set; }
        public string TravelAdvanceDetails { get; set; }
        public string TourAgendaPlanned { get; set; }
        public int TravelTypeID { get; set; }
        public int LoginID { get; set; }
        public int CheckInternational { get; set; }
        public string AttachedDocuments { get; set; }
        public string ApplicationDate { get; set; }
        public char ApplicationStatus { get; set; }
    }
}
