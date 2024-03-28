namespace MOBILEAPI2024.DTO.RequestDTO.Travel
{
    public class TravelApprovalDeleteRequest
    {
        public int TravelApprovalId { get; set; }
        public int TravelApplicationId { get; set; }
        public int CmpId { get; set; }
        public int EmpId { get; set; }
        public int SEmpId { get; set; }
        public string ApprovalDate { get; set; }
        public int LoginId { get; set; }
        public int ReportLevel { get; set; }
        public string Type { get; set; }
    }
}
