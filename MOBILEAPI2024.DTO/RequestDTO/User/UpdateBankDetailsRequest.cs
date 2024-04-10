namespace MOBILEAPI2024.DTO.RequestDTO.User
{
    public class UpdateBankDetailsRequest
    {
        public int EmpID { get; set; }
        public int CmpID { get; set; }
        public string BankName { get; set; }
        public string BankAccountNo { get; set; }
        public string IfscCode { get; set; }
        public string PancardNo { get; set; }
        public string BankBranchName { get; set; }
    }
}
