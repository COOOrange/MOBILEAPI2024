using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.User
{
    public class ChangeRequest
    {
        public int Row_ID { get; set; }
        public int CmpID { get; set; }
        public int Emp_Id { get; set; }
        public int RequestTypeId { get; set; }
        public string Change_Reason { get; set; }
        public DateTime Request_Date { get; set; }
        public string Shift_From_Date { get; set; }
        public string Shift_To_Date { get; set; }
        public string Dependant_Name { get; set; }
        public string Dependant_Relationship { get; set; }
        public string Dependant_Gender { get; set; }
        public DateTime Dependant_DOB { get; set; }
        public int Dependant_Age { get; set; }
        public int Dependant_Is_Resident { get; set; }
        public int Dependant_Is_Depended { get; set; }
        public string TranType { get; set; }
        public string Child_Birth_Date { get; set; }
        public int DepOccupationID { get; set; }
        public string DepHobbyID { get; set; }
        public string DepHobbyName { get; set; }
        public string DepCompany { get; set; }
        public string DepCmpCity { get; set; }
        public int DepStandardId { get; set; }
        public string DepSchCol { get; set; }
        public string DepSchColCity { get; set; }
        public string DepExtAct { get; set; }
        public string ImageName { get; set; }
        public string PanCard { get; set; }
        public string AdharCard { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string OtherHobby { get; set; }
        public string Specialization { get; set; }
    }
}
