using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DTO.RequestDTO.User
{
    public class UploadDocumentRequest
    {
        public int EmpID { get; set; }
        public int CmpID { get; set; }
        public int DocID { get; set; }
        public int DocType { get; set; }
        public string DocName { get; set; }
        public string FilePath { get; set; }
        public string DocComment { get; set; }
        public int LoginID { get; set; }
        public string Type { get; set; }
    }
}
