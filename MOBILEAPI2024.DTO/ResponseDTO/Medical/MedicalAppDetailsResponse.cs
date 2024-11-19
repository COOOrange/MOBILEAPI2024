using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.ResponseDTO.Medical
{

    public class MedicalAppDetailsResponse
    {
        public IEnumerable<MedicalDetail> MedicalDetails { get; set; }
        public MedicalContactInfo ContactInfo { get; set; }
    }

    public class MedicalDetail
    {
        public int APP_ID { get; set; }
        public int App_For { get; set; }
        public DateTime APP_DATE { get; set; }
        public int CMP_ID { get; set; }
        public string EMP_FULL_NAME { get; set; }
        public int EMP_ID { get; set; }
        public string INCIDENT_NAME { get; set; }
        public string ALPHA_EMP_CODE { get; set; }
        public DateTime DATE_OF_INCIDENT { get; set; }
    }

    public class MedicalContactInfo
    {
        public string MEDICALNAME { get; set; }
        public long CONTACTNO { get; set; }
        public string EMAIL { get; set; }
        public string MEDICALADDR { get; set; }
    }
}
