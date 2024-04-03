using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.User
{
    public class ChangeRequestFavInsertRequest
    {
        public int Row_ID { get; set; }
        public int CmpID { get; set; }
        public int Emp_Id { get; set; }
        public int RequestTypeId { get; set; }
        public string Change_Reason { get; set; }
        public DateTime Request_Date { get; set; }
        public string EmpFavSportID { get; set; }
        public string EmpFavSportName { get; set; }
        public string EmpHobbyID { get; set; }
        public string EmpHobbyName { get; set; }
        public string EmpFavFood { get; set; }
        public string EmpFavRestro { get; set; }
        public string EmpFavTrvDestination { get; set; }
        public string EmpFavFestival { get; set; }
        public string EmpFavSportPerson { get; set; }
        public string EmpFavSinger { get; set; }
        public string CurrEmpFavSportID { get; set; }
        public string CurrEmpHobbyID { get; set; }
        public string CurrEmpHobbyName { get; set; }
        public string CurrEmpFavFood { get; set; }
        public string CurrEmpFavRestro { get; set; }
        public string CurrEmpFavTrvDestination { get; set; }
        public string CurrEmpFavFestival { get; set; }
        public string CurrEmpFavSportPerson { get; set; }
        public string CurrEmpFavSinger { get; set; }
        public string TranType { get; set; }
        public string OtherSport { get; set; }
        public string OtherHobby { get; set; }
        public string ImagePath { get; set; }
    }
}
