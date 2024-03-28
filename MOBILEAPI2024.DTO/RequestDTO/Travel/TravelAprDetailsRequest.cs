using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.Travel
{
    public class TravelAprDetailsRequest
    {
        public int TransactionId { get; set; }
        public int Total { get; set; }
        public int TravelApplicationId { get; set; }
        public char Type { get; set; }
        public int CmpId { get; set; }
        public int EmpId { get; set; }
        public int SEmpId { get; set; }
        public int CheckInternational { get; set; }
        public string TravelDetails { get; set; }
        public string OtherTravelDetails { get; set; }
        public string TravelAdvanceDetails { get; set; }
        public string TourAgendaPlanned { get; set; }
        public int LoginId { get; set; }
        public string AttachedDocuments { get; set; }
        public string ApprovalDate { get; set; }
        public string ApprovalComments { get; set; }
        public int ReportLevel { get; set; }
        public char ApprovalStatus { get; set; }
        public int TravelTypeId { get; set; }
    }
}
